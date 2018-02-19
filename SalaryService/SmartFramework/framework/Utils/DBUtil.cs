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

        public static string[] GetResponse(string request, int responseFields)
        {
            var sqlCommand = new SqlCommand(request, _sqlConnection);
            var reader = sqlCommand.ExecuteReader();
            string[] response = {};
            if (reader.Read())
            {
                for (var i = 0; i < responseFields; i++)
                {
                    response[i] = reader.GetString(i);
                }
            }
            return response;
        }
    }
}
