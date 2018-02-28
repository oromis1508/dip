namespace demo.framework.Utils.Database
{
    public class DatabaseRequests
    {
        public string Body { get; }

        private DatabaseRequests(string body)
        {
            Body = body;
        }

        public static DatabaseRequests Delete(string databaseTable, string[] deleteCriteria)
        {
            var request = $"Delete FROM {databaseTable} WHERE ";
            request = AddArrayToRequest(request, deleteCriteria, " AND ");
            return new DatabaseRequests(request);
        }

        public static DatabaseRequests Select(string[] searchFields, string databaseTable, string[] searchCriteria)
        {

            var request = $"SELECT ";
            request = AddArrayToRequest(request, searchFields, ", ");
            request += $" FROM {databaseTable} WHERE ";            
            request = AddArrayToRequest(request, searchCriteria, " AND ");

            return new DatabaseRequests(request);
        }

        private static string AddArrayToRequest(string request, string[] array, string separator)
        {
            var result = request;
            for (var i = 0; i < array.Length; i++)
            {
                result += array[i];
                if (i + 1 != array.Length)
                {
                    result += separator;
                }
            }
            return result;
        }
    }
}
