using System;
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
        [Given(@"Connect to the database with settings")]
        public void GivenConnectToTheDatabase(Table dbData)
        {
            DbUtil.ConnectDatabase(dbData.Rows[0][0], dbData.Rows[0][1], dbData.Rows[0][2], dbData.Rows[0][3], dbData.Rows[0][4]);
        }

        [When(@"New Employee created on the web service with data")]
        public void WhenNewEmployeeCreatedOnTheWebService(Table table)
        {
            var newEmployee = table.CreateInstance<Employee>();

            if (!newEmployee.Id.Equals(""))
            {
                DbUtil.GetResponse($"Delete FROM Employees WHERE Id={newEmployee.Id}");
            }
            UpdateContextKey(EmployeeInContext, newEmployee);

            if (!ScenarioContext.Current.ContainsKey(FirstEmployeeInContext))
            {
                ScenarioContext.Current.Add(FirstEmployeeInContext, newEmployee);
            }
            var responseXml = new XmlDocument();
            try
            {
                responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.AddEmployee(newEmployee));
            }
            catch(Exception e)
            {
                Log.Fatal($"The web service response not received, error:\n {e.Message}");
            }

            UpdateContextKey(XmlResponseInContext, responseXml);
        }

        [When(@"Request sent to the database '(.*)''(.*)'")]
        public void WhenRequestSentToTheDatabaseEmployee_Id(string databaseRequest, string searchCriteria)
        {
            var databaseResponse = DbUtil.GetResponse(databaseRequest + ScenarioContext.Current.Get<Employee>(EmployeeInContext).Id);
            UpdateContextKey(DatabaseResponseInContext, databaseResponse);
        }

        [When(@"The created Employee was updated on the web service by data")]
        public void WhenNewEmployeeUpdatedOnTheWebService(Table table)
        {
            var newEmployee = table.CreateInstance<Employee>();

            var responseXml = new XmlDocument();
            try
            {
                responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.AddEmployee(newEmployee));
            }
            catch (Exception e)
            {
                Log.Fatal($"The web service response not received, error:\n {e.Message}");
            }
            UpdateContextKey(XmlResponseInContext, responseXml);

            UpdateContextKey(EmployeeInContext, newEmployee);
        }

        [When(@"Employee searched on the web service by '(.*)'")]
        public void WhenEmployeeSearchedOnTheWebServiceBy(string searchCriteria)
        {
            var employee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);

            switch (searchCriteria)
            {
                case "Employee.PrivateId":
                    var responseXml = SoapUtil.SendMessage(WebServiceName,
                        WebServiceMethod.GetEmployeeByPrivateId(employee));
                    UpdateContextKey(XmlResponseInContext, responseXml);
                    break;
                case "Employee.Experiense":
                    break;
                case "Employee.LastName":
                    break;
            }
        }
        
        [When(@"Request to getting employee salary was sended to the web service")]
        public void WhenTheEmloyeeWorked(Table table)
        {
            var salaryData = table.CreateInstance<SalaryData>();
            var employee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);

            var responseXml = SoapUtil.SendMessage(WebServiceName, WebServiceMethod.GetEmployeeSalary(employee, salaryData));
            UpdateContextKey(XmlResponseInContext, responseXml);
        }

        [Then(@"The server sent response '(.*)' in the tag '(.*)'")]
        public void ThenTheServerSentResponseInTheTag(string expectedTagValue, string expectedTagName)
        {
            var responseXml = ScenarioContext.Current.Get<XmlDocument>(XmlResponseInContext);
            var expectedTagsInResponse = responseXml.GetElementsByTagName(expectedTagName);
            Asserts.IsTrue(expectedTagsInResponse.Count == 1, $"Check on tag {expectedTagName} exist", true);
            var responseTagValue = expectedTagsInResponse.Item(0)?.InnerText;
            Asserts.AreEqual(expectedTagValue, responseTagValue, "Check value response tag", true);
        }
        
        [Then(@"The response data match the Employee created in the previous step")]
        public void ThenTheResponseDataMatchTheEmployeeCreatedInThePreviousStep()
        {
            var expectedEmployee = ScenarioContext.Current.Get<Employee>(EmployeeInContext);
            var actualEmployee = new Employee();
            var databaseResponse = ScenarioContext.Current.Get<string[]>(DatabaseResponseInContext);
            actualEmployee.Id = databaseResponse[0];
            actualEmployee.PrivateId = databaseResponse[1];
            actualEmployee.FirstName = databaseResponse[2];
            actualEmployee.LastName = databaseResponse[3];
            actualEmployee.MiddleName = databaseResponse[4];
            actualEmployee.Experiense = databaseResponse[5];
            actualEmployee.ProfessionId = databaseResponse[6];

            EmployeeAssert.AreEqual(expectedEmployee, actualEmployee, "Check on correct creating Employee in database", true);
        }

        [Then(@"Data in web service response match created Employee in the begin scenario")]
        public void ThenTheResponseMatchDocumentation()
        {
            var employee = ScenarioContext.Current.Get<Employee>(FirstEmployeeInContext);
        }

        [Then(@"The web service response tags named and placed as")]
        public void ThenDataMatchCreatedEmployeeInTheBeginScenario(Table table)
        {
            var response = ScenarioContext.Current.Get<XmlDocument>(XmlResponseInContext);
            var responseInfo = response.GetElementsByTagName("NewDataSet")[0].FirstChild;
            var values = new string[responseInfo.ChildNodes.Count]; 

            for (var i = 0; i < responseInfo.ChildNodes.Count; i++)
            {
                Asserts.AreEqual(table.Header.ToArray()[i], responseInfo.ChildNodes[i], "Check sequence of the privateId search method response tags and the value", true);
                values[i] = responseInfo.ChildNodes[i].InnerText;
            }
        }

        [When(@"New Employees without one of the parameters created on the web service with data")]
        public void Thethe(Table table)
        {
            foreach(var row in table.Rows)
            {
                var oneRowTable = new Table(table.Header.ToArray());
                oneRowTable.AddRow(row);

                WhenNewEmployeeCreatedOnTheWebService(oneRowTable);
                ThenTheServerSentResponseInTheTag("Указаны не все параметры", "AddNewEmployeeResult");
                var request =
                    "Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.";

                var employee = ScenarioContext.Current.Get<Employee>(EmployeeInContext);
                request += employee.Id.Equals("") ? $"private_id='{employee.PrivateId}'" : $"Id={employee.Id}";
                WhenRequestSentToTheDatabaseEmployee_Id(request, "");
                var responseDb = ScenarioContext.Current.Get<string[]>(DatabaseResponseInContext);
                Asserts.IsNull(responseDb, "Check on the entry in the database not created");
            }
        }

        public void UpdateContextKey(string key, object newValue)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                ScenarioContext.Current.Remove(key);
            }
            ScenarioContext.Current.Add(key, newValue);
        }
    }
}
