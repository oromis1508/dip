using System.Collections;
using System.Data.SqlClient;

namespace demo.framework.Utils
{
    public static class DbUtil
    {
        private static SqlConnection _sqlConnection;

        public static void ConnectDatabase(string ip, string port, string dbName, string userId, string password)
        {
            _sqlConnection = new SqlConnection($"Data Source={ip},{port};" +
                                              $"Initial Catalog={dbName};" +
                                              $"User ID={userId};" +
                                              $"Password={password}");
            _sqlConnection.Open();
        }

        public static string[] GetResponse(string request)
        {
            var sqlCommand = new SqlCommand(request, _sqlConnection);
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var response = new string[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {

                    response[i] = reader.GetSqlValue(i).ToString();
                }
                reader.Close();
                return response;
            }
            reader.Close();
            return null;
        }
    }
}
