using System.Collections.Generic;

namespace FormsAndLocators
{
    public partial class ApplicationMenu
    {
        public ApplicationMenu() : base("/form[@processname='notepad++']/menubar[@accessiblename='Application']", "Application menu")
        {
        }

        public ApplicationMenu(string menuName) : this()
        {
            SelectedMenus.Clear();
            SelectedMenus.Add(menuName);
        }

        protected const string ContextMenuPattern = "//contextmenu[@accessiblename='{0}']";
        protected static List<string> SelectedMenus = new List<string>();
    }
}
