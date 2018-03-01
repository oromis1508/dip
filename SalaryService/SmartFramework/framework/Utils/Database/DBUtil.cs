using System.Data.SqlClient;

namespace demo.framework.Utils.Database
{
    public static class DbUtil
    {
        private static SqlConnection _sqlConnection;

        public static SqlConnection ConnectDatabase(string ip, string port, string dbName, string userId, string password)
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection($"Data Source={ip},{port};" +
                                                   $"Initial Catalog={dbName};" +
                                                   $"User ID={userId};" +
                                                   $"Password={password}");
                _sqlConnection.Open();
            }

            return _sqlConnection;
        }

        public static string[] GetResponse(DatabaseRequests request)
        {
            var sqlCommand = new SqlCommand(request.Body, _sqlConnection);
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

        public static void CloseConnection() => _sqlConnection?.Close();
    }
}
