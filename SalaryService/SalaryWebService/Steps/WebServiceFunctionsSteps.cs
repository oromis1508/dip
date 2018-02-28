using demo.framework.Utils;
using demo.framework.Utils.Database;
using demo.framework.Utils.SalaryWebService;
using SalaryWebServiceEntities.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalaryWebService.Steps
{
    [Binding]
    public class WebServiceFunctionsSteps : BaseSalarySteps
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

            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.AddEmployee(employee));
            UpdateContextKey("Employee", employee);
            UpdateContextKey("Response", responseXml);
        }

        [When(@"I send request to getting the employee salary to the web service")]
        public void GetEmployeeSalaryFromWebService(Table table)
        {
            var salaryData = table.CreateInstance<SalaryData>();
            var employee = ScenarioContext.Current.Get<Employee>("Employee");

            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.GetEmployeeSalary(employee, salaryData));
            UpdateContextKey("Response", responseXml);
        }

        [When(@"I search the employee on the web service by '(PrivateId|Experiense|LastName)'")]
        public void SearchEmployeeOnWebService(string searchCriteria)
        {
            var employee = ScenarioContext.Current.Get<Employee>("Employee");

            switch (searchCriteria)
            {
                case "PrivateId":
                    var responsePrivateId = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByPrivateId(employee));
                    UpdateContextKey("Response", responsePrivateId);
                    break;
                case "Experiense":
                    var responseExperiense = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByExperiense(employee));
                    UpdateContextKey("Response", responseExperiense);
                    break;
                case "LastName":
                    var responseLastName = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByLastName(employee));
                    UpdateContextKey("Response", responseLastName);
                    break;
            }
        }
    }
}
