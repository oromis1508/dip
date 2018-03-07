namespace RxFramework.Elements
{
    public class MenuBar : BaseElement
    {
        public MenuBar(string locator, string name) : base(locator, name)
        {
        }

        public void ClickMenuItem(string subMenu) => GetElement<Ranorex.MenuBar>()[subMenu].Click();
    }
}
