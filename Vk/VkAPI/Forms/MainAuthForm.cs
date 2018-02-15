using demo.framework.BaseEntities;
using demo.framework.Elements;
using OpenQA.Selenium;

namespace VkAPI.Forms
{
    internal class MainAuthForm : BaseForm {

        private readonly TextBox _txbLogin = new TextBox(By.Id("index_email"), "Login field");
        private readonly TextBox _txbPass = new TextBox(By.Id("index_pass"), "Pass field");
        private readonly Button _btnSubmit = new Button (By.Id("index_login_button"), "Log in submit");

        public MainAuthForm() : base(By.Id("index_email"), "Main login page")
        {
        }

        public void LogIn(string username, string password){
            _txbLogin.SetText(username);
            _txbPass.SetText(password);
            _btnSubmit.Click();
        }
    }
}
