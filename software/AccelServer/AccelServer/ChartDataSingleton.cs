using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Data.SQLite;

namespace ImuServer
{
    class PackageInfo
    {
 
        public byte[] bytes = new byte[18];
        public int cur_len = 0;
        public int package_cnt = 0;
        public DateTime SyncTime = DateTime.Now;
        public int SyncTicks = 0;

        public void Clear()
        {
            bytes = new byte[18];
            cur_len = 0;
            package_cnt = 0;
        }
    }

    public class ChartDataSingleton 
    {
        private int package_size = 18;
        private static ChartDataSingleton _instance;
        private static readonly object padlock = new object();

        private ConcurrentQueue<ReceivedObject> _queues;
        private Dictionary<int, PackageInfo> _packageInfoDict;
        public Dictionary<int, IMUDataList> _dataLists;

        private ChartDataSingleton()
        {
            _queues = new ConcurrentQueue<ReceivedObject>();
            _packageInfoDict = new Dictionary<int, PackageInfo>();
            _packageInfoDict.Add(1, new PackageInfo());
            _packageInfoDict.Add(2, new PackageInfo());
            _packageInfoDict.Add(3, new PackageInfo());
            _packageInfoDict.Add(4, new PackageInfo());
            _packageInfoDict.Add(5, new PackageInfo());
            _packageInfoDict.Add(6, new PackageInfo());

            _dataLists = new Dictionary<int, IMUDataList>();
         
        }

        public static ChartDataSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                        _instance = new ChartDataSingleton();
                }
                return _instance;
            }
        }

        public void PutData(ReceivedObject obj)
        {
            _queues.Enqueue(obj);
        }

        public void SetSyncTime(int id, DateTime syncTime, int syncTicks)
        {
            _packageInfoDict[id].SyncTime = syncTime;
            _packageInfoDict[id].SyncTicks = syncTicks;
        }

        public bool ProcessData()
        {
            var isProcessed = true;
            ReceivedObject obj;
            if(_queues.TryDequeue(out obj))
            { 
                if(!_dataLists.ContainsKey(obj.id))
                    _dataLists[obj.id] = new IMUDataList(obj.id, _packageInfoDict[obj.id].SyncTime, _packageInfoDict[obj.id].SyncTicks);

                int bytes_proceed = 0;
                SQLUtils sqlUtils = DBManager.Instance.GetUtils();
                while (bytes_proceed < obj.length)
                {
                    try
                    {
                        int copy_len = Math.Min(package_size - _packageInfoDict[obj.id].cur_len, obj.length - bytes_proceed);
                        Array.Copy(obj.bytes, bytes_proceed, _packageInfoDict[obj.id].bytes, _packageInfoDict[obj.id].cur_len, copy_len);
                        _packageInfoDict[obj.id].cur_len += copy_len;
                        bytes_proceed += copy_len;
                        if (_packageInfoDict[obj.id].cur_len == package_size)
                        {                         
                            _dataLists[obj.id].PutBytes(_packageInfoDict[obj.id].bytes);
                            sqlUtils.AddValues(obj.id, AccelServer.SessionId, AccelServer.UserName, _dataLists[obj.id].agList.Last());
                            _packageInfoDict[obj.id].package_cnt++;
                            _packageInfoDict[obj.id].cur_len = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                sqlUtils.InsertValues();
            }
            else
            {
                isProcessed = _queues.Count > 0;
            }
            return isProcessed;
        }

        public int Count(int id)
        {
            var cnt = 0;
            if (_dataLists.ContainsKey(id))
                cnt = _dataLists[id].TimeStamps.Count;
            return cnt;
        }

        public void Clear()
        {
            foreach (var o in _packageInfoDict.Values) { o.Clear(); }
            foreach (var o in _dataLists.Values) { o.Clear(); }
        }
    }
}
