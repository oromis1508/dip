using System.Linq;
using System.Xml;
using demo.framework.Utils;
using demo.framework.Utils.SalaryWebService;
using demo.framework.Utils.SalaryWebService.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalaryWebService.Steps
{
    [Binding]
    public class SalaryWebServiceSteps : BaseSalarySteps
    {

        [When(@"I create new employee on the web service with the data")]
        public void CreateEmployeeOnWebService(Table table)
        {
            var newEmployee = table.CreateInstance<Employee>();

            DbUtil.GetResponse(!newEmployee.Id.Equals("")
                ? $"Delete FROM Employees WHERE Id={newEmployee.Id}"
                : $"Delete FROM Employees WHERE private_id={newEmployee.PrivateId}");
            UpdateContextKey(EmployeeInContext, newEmployee);

            if (!ScenarioContext.Current.ContainsKey(FirstEmployeeInContext))
            {
                ScenarioContext.Current.Add(FirstEmployeeInContext, newEmployee);
            }

            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.AddEmployee(newEmployee));
            UpdateContextKey(XmlResponseInContext, responseXml);
        }

        [When(@"Request sent to the database '(.*)''(.*)'")]
        public void SentDbRequest(string databaseRequest, string searchCriteria)
        {
            var databaseResponse = DbUtil.GetResponse(databaseRequest + ScenarioContext.Current.Get<Employee>(EmployeeInContext).Id);
            UpdateContextKey(DatabaseResponseInContext, databaseResponse);
        }

        [When(@"The created Employee was updated on the web service by data")]
        public void UpdateEmployeeOnWebService(Table table)
        {
            var newEmployee = table.CreateInstance<Employee>();
            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.AddEmployee(newEmployee));

            UpdateContextKey(XmlResponseInContext, responseXml);
            UpdateContextKey(EmployeeInContext, newEmployee);
        }

        [When(@"Employee searched on the web service by '(.*)'")]
        public void SearchEmployeeOnWebService(string searchCriteria)
        {
            var employee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);

            switch (searchCriteria)
            {
                case "Employee.PrivateId":
                    var responsePrivateId = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByPrivateId(employee));
                    UpdateContextKey(XmlResponseInContext, responsePrivateId);
                    break;
                case "Employee.Experiense":
                    var responseExperiense = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByExperiense(employee));
                    UpdateContextKey(XmlResponseInContext, responseExperiense);               
                    break;
                case "Employee.LastName":
                    var responseLastName = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByLastName(employee));
                    UpdateContextKey(XmlResponseInContext, responseLastName);
                    break;
            }
        }
        
        [When(@"Request to getting employee salary was sended to the web service")]
        public void SendSoapRequestOnSalary(Table table)
        {
            var salaryData = table.CreateInstance<SalaryData>();
            var employee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);

            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.GetEmployeeSalary(employee, salaryData));
            UpdateContextKey(XmlResponseInContext, responseXml);
        }

        [Then(@"The server sent response '(.*)' in the tag '(.*)'")]
        public void CheckWebServiceResponseTag(string expectedTagValue, string expectedTagName)
        {
            var responseXml = ScenarioContext.Current.Get<XmlDocument>(XmlResponseInContext);
            var expectedTagsInResponse = responseXml.GetElementsByTagName(expectedTagName);
            Asserts.IsTrue(expectedTagsInResponse.Count == 1, $"Check on tag {expectedTagName} exist", true);
            var responseTagValue = expectedTagsInResponse.Item(0)?.InnerText;
            Asserts.AreEqual(expectedTagValue, responseTagValue, "Check value response tag", true);
        }
        
        [Then(@"The response data match the Employee (.*) in the previous step")]
        public void CheckCorrectCreatingEmployee(string action)
        {
            var expectedEmployee = ScenarioContext.Current.Get<Employee>(EmployeeInContext);
            var databaseResponse = ScenarioContext.Current.Get<string[]>(DatabaseResponseInContext);
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
            EmployeeAssert.AreEqual(expectedEmployee, actualEmployee, $"Check on correct {action} Employee in database", true);
        }

        [Then(@"Data in web service response match created Employee in the begin scenario")]
        public void CheckValuesSearchByPrivateId()
        {
            var responseEmployee = ScenarioContext.Current.Get<string[]>(SearchResponseInContext);
            var responseProfId =
                DbUtil.GetResponse(
                    $"SELECT Id from BASE_SALARIES WHERE Salary_amount={responseEmployee[6]} AND Prof_name='{responseEmployee[5]}'");
            var expectedEmployee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);

            var actualEmployee = new Employee
            {
                Id = expectedEmployee.Id,
                PrivateId = responseEmployee[0],
                LastName = responseEmployee[1],
                FirstName = responseEmployee[2],
                MiddleName = responseEmployee[3],
                Experiense = responseEmployee[4],
                ProfessionId = responseProfId[0]
        };
            EmployeeAssert.AreEqual(expectedEmployee, actualEmployee, "Check the search by PrivateID method response", true);
        }

        [Then(@"The web service response tags named and placed as")]
        public void CheckTagsSearchByPrivateId(Table table)
        {
            var response = ScenarioContext.Current.Get<XmlDocument>(XmlResponseInContext);
            var responseInfo = response.GetElementsByTagName("NewDataSet")[0].FirstChild;
            var values = new string[responseInfo.ChildNodes.Count]; 

            for (var i = 0; i < responseInfo.ChildNodes.Count; i++)
            {
                Asserts.AreEqual(table.Header.ToArray()[i], responseInfo.ChildNodes[i].Name, $"Check sequence of the privateId search method response tags ({table.Header.ToArray()[i]})", true);
                values[i] = responseInfo.ChildNodes[i].InnerText;
            }
            ScenarioContext.Current.Add(SearchResponseInContext, values);
        }

        [When(@"New Employees without one of the parameters created on the web service with data")]
        public void CheckCreatingEmployeesByUnvalidData(Table table)
        {
            foreach(var row in table.Rows)
            {
                var oneRowTable = new Table(table.Header.ToArray());
                oneRowTable.AddRow(row);

                CreateEmployeeOnWebService(oneRowTable);
                CheckWebServiceResponseTag("Указаны не все параметры", "AddNewEmployeeResult");

                var request =
                    "Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.";
                var employee = ScenarioContext.Current.Get<Employee>(EmployeeInContext);
                request += employee.Id.Equals("") ? $"private_id='{employee.PrivateId}'" : $"Id={employee.Id}";
                SentDbRequest(request, "");

                var responseDb = ScenarioContext.Current.Get<string[]>(DatabaseResponseInContext);
                Asserts.IsNull(responseDb, "Check on the entry in the database not created");
            }
        }
    }
}
