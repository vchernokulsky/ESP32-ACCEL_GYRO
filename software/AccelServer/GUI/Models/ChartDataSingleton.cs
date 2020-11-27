using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GUI
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

        public int lastProcessedId = 0;

        public string userName = "";
        public int sessionId = 0;

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
                lastProcessedId = obj.id;
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
                            sqlUtils.AddValues(obj.id, userName, sessionId, _dataLists[obj.id].GetImuData(_packageInfoDict[obj.id].bytes));
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

        public int ReceivedMeasurementCount(int id)
        {
            var cnt = 0;
            if (_packageInfoDict!= null && _packageInfoDict.ContainsKey(id))
                cnt = _packageInfoDict[id].package_cnt;
            return cnt;
        }

        public void Clear()
        {
            foreach (var o in _packageInfoDict.Values) { o.Clear(); }
        }

        public void SetSessionId()
        {
            sessionId = DBManager.Instance.GetUtils().GetSessionId() + 1;
        }
    }
}
