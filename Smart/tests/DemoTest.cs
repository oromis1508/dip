using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using demo.framework;
using Assert = NUnit.Framework.Assert;

namespace demo.tests
{
    public class DemoTest : BaseTest
    {
        private readonly String username = RunConfigurator.GetValue("username");
        private readonly String password = RunConfigurator.GetValue("password");
        
        [Test]
        public void RunTest()
        {
            Log.Step(1);
            TutSearchForm tsf = new TutSearchForm();
            tsf.AssertLogo();

            Log.Step(2);
            tsf.SearchFor("A1QA");

            Log.Step(3);
            tsf.AssertA1QaString();
        }
    }
}
