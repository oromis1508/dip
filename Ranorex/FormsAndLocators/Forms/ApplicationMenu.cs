using System;
using RxFramework;

namespace FormsAndLocators
{
    public partial class ApplicationMenu : BaseForm
    {
        public void OpenMenu(string menuPath, string separator)
        {
            var subMenus = menuPath.Split(new[] { separator }, StringSplitOptions.None);
            _applicationMenuBar.ClickMenuItem(subMenus);
        }
    }
}
