using demo.framework.BaseEntities;
using demo.framework.Utils;
using demo.framework.Utils.Database;
using TechTalk.SpecFlow;

namespace SalaryWebService.Steps
{
    [Binding]
    public class BeforeAfterTestRun : BaseEntity
    {
        private static readonly string DbIp = Configuration.GetParameterValue("databaseIp");
        private static readonly string DbPort = Configuration.GetParameterValue("databasePort");
        private static readonly string DbName = Configuration.GetParameterValue("databaseName");
        private static readonly string DbUser = Configuration.GetParameterValue("databaseUser");
        private static readonly string DbPassword = Configuration.GetParameterValue("databasePassword");

        [BeforeTestRun]
        public static void ConnectDb()
        {
            DbUtil.ConnectDatabase(DbIp, DbPort, DbName, DbUser, DbPassword);
        }

        [AfterTestRun]
        public static void CloseConnectDb()
        {
            DbUtil.CloseConnection();
        }
    }
}
