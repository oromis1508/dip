using System;
using demo.framework;
using NUnit.Framework;
using VkAPI.Forms;

namespace VkAPI.tests
{
    public class DemoTest : BaseTest
    {
        private readonly string _username = RunConfigurator.GetValue("username");
        private readonly string _password = RunConfigurator.GetValue("password");
        
        [Test]
        public void RunTest()
        {
            Log.Step(1);
            var authForm = new MainAuthForm();
            authForm.LogIn(_username, _password);

            Log.Step(2);
            var news = new NewsForm();
            news.NavigateMyPage();

            Log.Step(3);
            Random rand = new Random();
        }
    }
}
