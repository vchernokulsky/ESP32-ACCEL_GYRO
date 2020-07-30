using GUI;
using System;
using System.Data.SQLite;
using System.IO;

namespace ImuServer
{
    class DBManager
    {
        private static AppType APP_TYPE;

        private readonly string AccDbName = "AccelerationMeasurement.sqlite";
        private readonly string AngDbbName = "AnglesMeasurement.sqlite";


        private readonly string createTable = "CREATE TABLE IF NOT EXISTS \"ReseivedData\" (" +
                                        "\"Id\"	INTEGER NOT NULL UNIQUE," +
                                        "\"DeviceId\"	INTEGER NOT NULL," +
                                        "\"SessionId\"	INTEGER NOT NULL," +
                                        "\"Time\"	TEXT NOT NULL," +
                                        "\"UserName\"	TEXT," +
                                        "\"AccelerationX\"	REAL NOT NULL," +
                                        "\"AccelerationY\"	REAL NOT NULL," +
                                        "\"AccelerationZ\"	REAL NOT NULL," +
                                        "\"GyroscopX\"	REAL NOT NULL," +
                                        "\"GyroscopY\"	REAL NOT NULL," +
                                        "\"GyroscopZ\"	REAL NOT NULL," +
                                        "PRIMARY KEY(\"Id\" AUTOINCREMENT)" +
                                    ");";

        private static DBManager _instance;

        private SQLiteConnection _connection;

        private DBManager()
        {
        }

        public static void SetAppType(AppType appType)
        {
            APP_TYPE = appType;
        }

        public static DBManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBManager();
                    if (!_instance.Init())
                        _instance = null;
                }
                return _instance;
            }
        }

        private string GetDbName()
        {
            string ret = "";
            switch (APP_TYPE)
            {
                case AppType.AccelerationMeasurement:
                    ret = AccDbName;
                    break;
                case AppType.AnglesMeasurement:
                    ret = AngDbbName;
                    break;
                default:
                    Console.WriteLine("Wrong Application type {0}", APP_TYPE.ToString());
                    break;
            }
            return ret;
        }

        private bool Init()
        {
            string dbName = GetDbName();
            string connString = "Data Source="+dbName+";Version=3;";
            if (!File.Exists(dbName))
                SQLiteConnection.CreateFile(dbName);

            try
            {
                _connection = new SQLiteConnection(connString);
                _connection.Open();


                SQLiteCommand cmd = new SQLiteCommand(createTable, _connection);
                cmd.ExecuteNonQuery();
				//_connection.Close();
              
            }
            catch (SQLiteException sqlite_ex)
            {
                Console.WriteLine(sqlite_ex.Message);
                _connection = null;
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
                _connection = null;
            }

            return _connection != null;
        }

        public SQLUtils GetUtils()
        {
            SQLUtils result = null;
            if (_instance != null)
                result = new SQLUtils(_connection);

            return result;
        }
    }
}
