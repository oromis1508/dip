using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class ApplicationMenu
    {
        public ApplicationMenu() : base("/form[@processname='notepad++']/menubar[@accessiblename='Application']", "Application menu")
        {
            _applicationMenuBar = new MenuBar(FormLocator, FormName);
        }

        private const string ContextMenuXpath = "//contextmenu[@accessiblename='{0}']";
        private readonly MenuBar _applicationMenuBar;
    }
}
