using RxFramework.BaseEntities;
using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class ApplicationMenu : BaseForm
    {
        public void OpenMenu(string[] subMenus)
        {
            _applicationMenuBar.ClickMenuItem(subMenus[0]);

            for (var i = 1; i < subMenus.Length; i++)
            {
                new ContextMenu(string.Format(ContextMenuXpath, subMenus[i-1]), $"Context menu {subMenus[i-1]}").ClickSubItem(subMenus[i]);
            }
        }
    }
}
