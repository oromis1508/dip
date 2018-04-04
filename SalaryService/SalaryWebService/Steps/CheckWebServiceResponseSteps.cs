using System.Collections.Generic;
using System.Xml;
using demo.framework.Utils;
using demo.framework.Utils.Database;
using demo.framework.Utils.SalaryWebService;
using SalaryWebServiceEntities.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalaryWebService.Steps
{
    [Binding]
    public class CheckWebServiceResponseSteps : BeforeAfterTestRun
    {
        [Then(@"The server sent the response '(.*)' in the tag '(.*)'")]
        public void CheckWebServiceResponseTag(string expectedTagValue, string expectedTagName)
        {
            var responseXml = ScenarioContext.Current.Get<XmlDocument>("Response");
            var responseTag = responseXml?.GetElementsByTagName("soap:Body")[0].FirstChild.FirstChild;

            Asserts.AreEqual(expectedTagName, responseTag?.Name, $"Check on tag {expectedTagName} exist");
            Asserts.AreEqual(expectedTagValue, responseTag?.InnerText, "Check value response tag", true);
        }

        [Then(@"The web service response tags named and placed as")]
        public void CheckTagsSearchByPrivateId(Table table)
        {
            var response = ScenarioContext.Current.Get<XmlDocument>("Response");
            var responseInfo = response.GetElementsByTagName("NewDataSet")[0].FirstChild;
            var values = new Dictionary<string, string>();
            for (var i = 0; i < responseInfo.ChildNodes.Count; i++)
            {
                Asserts.AreEqual(table.Rows[i]["Tag"], responseInfo.ChildNodes[i].Name, $"Check sequence of the privateId search method response tags ({table.Rows[i]["Tag"]})", true);
                values[table.Rows[i]["Tag"]] = responseInfo.ChildNodes[i].InnerText;
            }
            ScenarioContextUtil.UpdateContext("Response", values);
        }

        [Then(@"Data in the web service response match the employee with the data")]
        public void CheckWebServiceResponse(Table table)
        {
            var response = ScenarioContext.Current.Get<Dictionary<string, string>>("Response");
            var responseProfId =
                DbUtil.GetResponse(DatabaseRequests.Select(new[] { "Id" }, DBTable.BaseSalaries.Name,
                    new[] { $"Salary_amount={response["salary_amount"]}", $"Prof_name='{response["prof_name"]}'" }));

            var employee = table.CreateInstance<Employee>();
            var responseEmployee = new Employee
            {
                Id = employee.Id,
                PrivateId = response["private_id"],
                LastName = response["last_name"],
                FirstName = response["first_name"],
                MiddleName = response["middle_name"],
                Experiense = response["exp"],
                ProfessionId = responseProfId[0]
            };
            EmployeeAssert.AreEqual(employee, responseEmployee, "Check the search by PrivateID method response", true);
        }
    }
}
