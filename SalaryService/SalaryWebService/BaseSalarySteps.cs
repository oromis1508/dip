using demo.framework.BaseEntities;
using demo.framework.Utils;
using demo.framework.Utils.SalaryWebService.Entities;
using TechTalk.SpecFlow;

namespace SalaryWebService
{
    public class BaseSalarySteps : BaseEntity
    {
        protected static string WebServiceName = Configuration.GetParameterValue("webServiceURI");
        protected string EmployeeInContext = "NewEmployee";
        protected string FirstEmployeeInContext = "FirstEmployee";
        protected string XmlResponseInContext = "ResponseXml";
        protected string DatabaseResponseInContext = "ResponseDatabase";

/*
        [StepArgumentTransformation(@"Request sent to the database '(.*)''(.*)'")]
        public string RequestTransform(string request, string searchCriteria)
        {
            return request + ScenarioContext.Current.Get<Employee>(EmployeeInContext).Id;
        }
*/
    }
}
