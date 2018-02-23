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
        protected string SearchResponseInContext = "SearchResponse";

        private int _stepNumber;

        [BeforeStep]
        public void LogSteps()
        {
            Log.Step(++_stepNumber);
        }

        protected void UpdateContextKey(string key, object newValue)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                ScenarioContext.Current.Remove(key);
            }
            ScenarioContext.Current.Add(key, newValue);
        }

        protected Employee CreateEmployee(string[] values)
        {
            return new Employee
            {
                Id = values[0],
                PrivateId = values[1],
                FirstName = values[2],
                LastName = values[3],
                MiddleName = values[4],
                Experiense = values[5],
                ProfessionId = values[6]
            };
        }

    }
}
