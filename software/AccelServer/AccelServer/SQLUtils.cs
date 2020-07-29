using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImuServer
{
    class SQLUtils
    {
        private SQLiteConnection _connection;

        private string INSERT_TEMPLATE = "INSERT INTO ReseivedData(" +
            "`DeviceId`,`SessionId`,`Time`,`UserName`,"+
            "`AccelerationX`,`AccelerationY`,`AccelerationZ`," +
            "`GyroscopX`,`GyroscopY`,`GyroscopZ`) VALUES {0} ;";

        private string VALUES_TEMPLATE =
            "({0}, {1}, '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9})";

        private string ADD_PARAMS_TEMPLATE = "{0}, {1}";

        private string TIME_FORMAT = "MM/dd/yy H:mm:ss fff";

        private readonly string specifier = "F4";
        private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        private string paramsStr = "";

        public SQLUtils(SQLiteConnection conn)
        {
            _connection = conn;
        }

        public void AddValues(int deviceId, int sessionId, string userName, IMUData imuData)
        {
            string curParams = string.Format(VALUES_TEMPLATE, deviceId, sessionId, imuData.Time.ToString(TIME_FORMAT), userName,
                imuData.AcX.ToString(specifier, culture), imuData.AcY.ToString(specifier, culture), imuData.AcZ.ToString(specifier, culture), 
                imuData.GyX.ToString(specifier, culture), imuData.GyY.ToString(specifier, culture), imuData.GyZ.ToString(specifier, culture));
            if (paramsStr == null || paramsStr.Length == 0)
            {
                paramsStr = curParams;
            } else
            {
                paramsStr = string.Format(ADD_PARAMS_TEMPLATE, paramsStr, curParams);
            }
        }

        public bool InsertValues()
        {
            bool result = false;
            if (_connection != null && paramsStr != null && paramsStr.Length > 0)
            {
                string sqlQuery = string.Format(INSERT_TEMPLATE, paramsStr);
                using (SQLiteCommand cmd = new SQLiteCommand(_connection))
                {
                    try
                    {
                        cmd.CommandText = sqlQuery;
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(sqlQuery);
                    }
                }
            }
               
            paramsStr = "";
            return result;
        }

        public int GetSessionId()
        {
            int ret = 0;
            if(_connection != null)
            {
                string sqlQuery = "SELECT SessionId FROM ReseivedData ORDER BY Id Desc LIMIT 1;";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, _connection))
                    {
                        var reader = cmd.ExecuteReader();
                        if (reader != null)
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ret = reader.GetInt32(0);
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlQuery);
                }
            }
            return ret;
        }
    }
}
