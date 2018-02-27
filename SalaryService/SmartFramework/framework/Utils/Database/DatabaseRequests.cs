using SalaryWebServiceEntities.Entities;

namespace demo.framework.Utils.Database
{
    public class DatabaseRequests
    {
        public string Body { get; }

        private DatabaseRequests(string body)
        {
            Body = body;
        }

        public static DatabaseRequests Delete(DatabaseTable databaseTable, EmployeeSearchCriteria employeeSearchCriteria)
            => new DatabaseRequests($"Delete FROM {databaseTable} WHERE {employeeSearchCriteria}");

        public static DatabaseRequests Select(DatabaseTable databaseTable, EmployeeSearchCriteria employeeSearchCriteria)
            => new DatabaseRequests($"Delete FROM {databaseTable} WHERE {employeeSearchCriteria}");
    }
}
