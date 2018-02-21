using System;
using demo.framework.Utils;
using demo.framework.Utils.SalaryWebService;
using demo.framework.Utils.SalaryWebService.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryWebService
{
    [TestClass]
    public class UnitTest1
    {
        private string dbIp = RunConfigurator.GetValue("databaseIp");
        private string dbPort = RunConfigurator.GetValue("databasePort");
        private string dbName = RunConfigurator.GetValue("databaseName");
        private string dbUser = RunConfigurator.GetValue("databaseUser");
        private string dbPassword = RunConfigurator.GetValue("databasePassword");


        [TestMethod]
        public void TestMethod1()
        {
/*
            Employee emp = new Employee();
            emp.Id = "10";
            emp.PrivateId = "34ftg";
            emp.FirstName = "qwerty";
            emp.LastName = "asdf";
            emp.MiddleName = "zxcv";
            emp.Experiense = "8";
            emp.ProfessionId = "2";
            try
            {
                SoapUtil.GetSoapMessage(WebServiceMethod.AddEmployee(emp));
            } catch(Exception) { }
*/

            DbUtil.ConnectDatabase(dbIp, dbPort, dbName, dbUser, dbPassword);
            var resp = DbUtil.GetResponse("SELECT * FROM base_salaries");
            //1500-1100-900-700-950-750-650-500-400-300-100-1000
            var days = DbUtil.GetResponse("SELECT * FROM work_days");
            // 21-10-05-2019
            Console.Write("sfdgds");

        }
    }
}
