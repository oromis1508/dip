using demo.framework.Utils.SalaryWebService.Entities;
using TechTalk.SpecFlow;

namespace SalaryWebService
{
    internal class BaseSalarySteps
    {
        [StepArgumentTransformation(@"I send request to the database '(.*)''(.*)'")]
        public string RequestTransform(string request, string searchCriteria)
        {
            return request + ScenarioContext.Current.Get<Employee>("NewEmloyee").Id;
        }
    }
}
