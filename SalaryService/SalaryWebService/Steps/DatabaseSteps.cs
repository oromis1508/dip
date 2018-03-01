using demo.framework.Utils;
using demo.framework.Utils.Database;
using demo.framework.Utils.SalaryWebService;
using SalaryWebServiceEntities.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalaryWebService.Steps
{
    [Binding]
    public class DatabaseSteps : BeforeAfterTestRun
    {
        [When(@"I send request to the database to search with the parameters")]
        public void SendDatabaseRequest(Table table)
        {
            var databaseResponse =
                DbUtil.GetResponse(DatabaseRequests.Select(new[] { table.Rows[0]["SeachFields"] },
                    table.Rows[0]["TableName"], new[] { table.Rows[0]["SeachCriteria"] }));
            ScenarioContextUtil.UpdateContext("Response", databaseResponse);
        }

        [Then(@"The data of the database response match the employee with the data")]
        public void CheckDatabaseEntry(Table table)
        {
            var employee = table.CreateInstance<Employee>();
            var databaseResponse = ScenarioContext.Current.Get<string[]>("Response");
            var actualEmployee = new Employee
            {
                Id = databaseResponse[0],
                PrivateId = databaseResponse[1],
                FirstName = databaseResponse[2],
                LastName = databaseResponse[3],
                MiddleName = databaseResponse[4],
                Experiense = databaseResponse[5],
                ProfessionId = databaseResponse[6]
            };
            EmployeeAssert.AreEqual(employee, actualEmployee, $"Check on correct Employee in database", true);
        }

        [Then(@"The database response not contains entries")]
        public void CheckDatabaseNotContainsEntry()
        {
            var databaseResponse = ScenarioContext.Current.Get<string[]>("Response");
            Asserts.IsNull(databaseResponse, "Check on the entry in the database not created");
        }
    }
}
