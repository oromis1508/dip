using demo.framework.BaseEntities;
using demo.framework.Elements;
using OpenQA.Selenium;
using VkApi.Forms;

namespace VkAPI.Forms
{
    internal class NavigateMenu : BaseForm
    {
        public NavigateMenu() : base(By.Id("side_bar_inner"), "Left navigate menu")
        {
        }

        public void NavigatePage(NavigateMenuItem menuItem) => new Link(By.Id(menuItem.ToString()), "My page").Click();
    }
}
