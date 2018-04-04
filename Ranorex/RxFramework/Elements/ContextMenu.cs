using Ranorex;

namespace RxFramework.Elements
{
    public class ContextMenu : BaseElement
    {
        public ContextMenu(string locator, string name) : base(locator, name)
        {
        }

        public void ClickSubItem(string subItem) => GetElement<Ranorex.ContextMenu>().FindSingle<MenuItem>($"./descendant::menuitem[@accessiblename~'{subItem}']").Click();
    }
}
