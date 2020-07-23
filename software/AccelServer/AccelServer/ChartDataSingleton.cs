using System;
using System.Collections.Generic;
using System.Collections.Concurrent;


namespace AccelServer
{
    class PackageInfo
    {
        public byte[] bytes = new byte[18];
        public int cur_len = 0;
        public int package_cnt = 0;
        public DateTime SyncTime = DateTime.Now;
        public int SyncTicks = 0;
    }

    public class ChartDataSingleton 
    {
        private int package_size = 18;
        private static ChartDataSingleton _instance;
        private static readonly object padlock = new object();

        private ConcurrentQueue<ReceivedObject> _queues;
        private Dictionary<int, PackageInfo> _packageInfos;
        public Dictionary<int, IMUDataList> _dataLists;

        private ChartDataSingleton()
        {
            _queues = new ConcurrentQueue<ReceivedObject>();
            _packageInfos = new Dictionary<int, PackageInfo>();
            _packageInfos.Add(1, new PackageInfo());
            _packageInfos.Add(2, new PackageInfo());
            _packageInfos.Add(3, new PackageInfo());
            _packageInfos.Add(4, new PackageInfo());
            _packageInfos.Add(5, new PackageInfo());
            _packageInfos.Add(6, new PackageInfo());

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
            _packageInfos[id].SyncTime = syncTime;
            _packageInfos[id].SyncTicks = syncTicks;
        }

        public void ProcessData()
        {
            ReceivedObject obj;
            if(_queues.TryDequeue(out obj))
            { 
                if(!_dataLists.ContainsKey(obj.id))
                    _dataLists[obj.id] = new IMUDataList(obj.id, _packageInfos[obj.id].SyncTime, _packageInfos[obj.id].SyncTicks);

                int bytes_proceed = 0;
                while (bytes_proceed < obj.length)
                {
                    try
                    {
                        int copy_len = Math.Min(package_size - _packageInfos[obj.id].cur_len, obj.length - bytes_proceed);
                        Array.Copy(obj.bytes, bytes_proceed, _packageInfos[obj.id].bytes, _packageInfos[obj.id].cur_len, copy_len);
                        _packageInfos[obj.id].cur_len += copy_len;
                        bytes_proceed += copy_len;
                        if (_packageInfos[obj.id].cur_len == package_size)
                        {
                            //Console.WriteLine("found {0} packages", ++_packageInfos[obj.id].package_cnt);
                            _dataLists[obj.id].PutBytes(_packageInfos[obj.id].bytes);
                            _packageInfos[obj.id].cur_len = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
