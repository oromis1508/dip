using demo.framework.BaseEntities;
using demo.framework.Elements;
using OpenQA.Selenium;

namespace VkAPI.Forms
{
    internal class NewsForm : BaseForm
    {
        private readonly Link _btnMyPage = new Link(By.Id("l_pr"), "My page");

        public NewsForm() : base(By.Id("ui_rmenu_news"), "News page")
        {
        }

        public void NavigateMyPage()
        {
            _btnMyPage.Click();
        }
    }
}
