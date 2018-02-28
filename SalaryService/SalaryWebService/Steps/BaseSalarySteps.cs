using demo.framework.BaseEntities;
using demo.framework.Utils;
using demo.framework.Utils.Database;
using TechTalk.SpecFlow;

namespace SalaryWebService.Steps
{
    public class BaseSalarySteps : BaseEntity
    {
        protected static string WebServiceName = Configuration.GetParameterValue("webServiceURI");
        protected static string DbIp = Configuration.GetParameterValue("databaseIp");
        protected static string DbPort = Configuration.GetParameterValue("databasePort");
        protected static string DbName = Configuration.GetParameterValue("databaseName");
        protected static string DbUser = Configuration.GetParameterValue("databaseUser");
        protected static string DbPassword = Configuration.GetParameterValue("databasePassword");

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
    }
}
