using demo.framework.BaseEntities;
using demo.framework.Utils;
using demo.framework.Utils.SalaryWebService.Entities;
using TechTalk.SpecFlow;

namespace SalaryWebService
{
    public class BaseSalarySteps : BaseEntity
    {
        protected static string WebServiceName = Configuration.GetParameterValue("webServiceURI");
        protected static string DbIp = Configuration.GetParameterValue("webServiceURI");
        protected static string DbPort = Configuration.GetParameterValue("webServiceURI");
        protected static string DbName = Configuration.GetParameterValue("webServiceURI");
        protected static string DbUser = Configuration.GetParameterValue("webServiceURI");
        protected static string DbPassword = Configuration.GetParameterValue("webServiceURI");

        private int _stepNumber;

        [BeforeStep]
        public void LogSteps()
        {
            Log.Step(++_stepNumber);
        }

        [BeforeScenario]
        public void ConnectDb()
        {
            DbUtil.ConnectDatabase(DbIp, DbPort, DbName, DbUser, DbPassword);
        }

        [AfterScenario]
        public void CloseConnectDb()
        {
            DbUtil.CloseConnection();
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
