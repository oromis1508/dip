using demo.framework.Utils;
using demo.framework.Utils.Database;
using demo.framework.Utils.SalaryWebService;
using SalaryWebServiceEntities.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalaryWebService.Steps
{
    [Binding]
    public class WebServiceFunctionsSteps : BeforeAfterTestRun
    {
        [Given(@"The employee created on the web service with the data")]
        public void CreateEmployeeOnWebService(Table table)
        {
            CreateEmployeeOnWebService("create", table);
        }

        [When(@"I (create|update) employee on the web service with the data")]
        public void CreateEmployeeOnWebService(string action, Table table)
        {
            var employee = table.CreateInstance<Employee>();

            if (action.Equals("create"))
            {
                var deleteCriteria =
                    new[]
                    {
                        employee.Id.Equals("") ? $"private_id='{employee.PrivateId}'" : $"id={employee.Id}"
                    };
                DbUtil.GetResponse(DatabaseRequests.Delete(DBTable.Employees.Name, deleteCriteria));
            }

            var responseXml = SoapUtil.SendMessage(WebServiceMethod.AddEmployee(employee));
            ScenarioContextUtil.UpdateContext("Employee", employee);
            ScenarioContextUtil.UpdateContext("Response", responseXml);
        }

        [When(@"I send request to getting the employee salary to the web service")]
        public void GetEmployeeSalaryFromWebService(Table table)
        {
            var salaryData = table.CreateInstance<SalaryData>();
            var employee = ScenarioContext.Current.Get<Employee>("Employee");

            var responseXml = SoapUtil.SendMessage(WebServiceMethod.GetEmployeeSalary(employee, salaryData));
            ScenarioContextUtil.UpdateContext("Response", responseXml);
        }

        [When(@"I search the employee on the web service by '(PrivateId|Experiense|LastName)'")]
        public void SearchEmployeeOnWebService(string searchCriteria)
        {
            var employee = ScenarioContext.Current.Get<Employee>("Employee");

            switch (searchCriteria)
            {
                case "PrivateId":
                    var responsePrivateId = SoapUtil.SendMessage(WebServiceMethod.GetEmployeeByPrivateId(employee));
                    ScenarioContextUtil.UpdateContext("Response", responsePrivateId);
                    break;
                case "Experiense":
                    var responseExperiense = SoapUtil.SendMessage(WebServiceMethod.GetEmployeeByExperiense(employee));
                    ScenarioContextUtil.UpdateContext("Response", responseExperiense);
                    break;
                case "LastName":
                    var responseLastName = SoapUtil.SendMessage(WebServiceMethod.GetEmployeeByLastName(employee));
                    ScenarioContextUtil.UpdateContext("Response", responseLastName);
                    break;
            }
        }
    }
}
