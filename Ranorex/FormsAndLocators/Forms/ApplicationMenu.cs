using FormsAndLocators.ApplicationMenus;
using RxFramework.BaseEntities;
using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class ApplicationMenu : BaseForm
    {
        public void OpenMenu()
        {
            new MenuBar(FormLocator, FormName).ClickMenuItem(SelectedMenus[0]);

            for (var i = 1; i < SelectedMenus.Count; i++)
            {
                new ContextMenu(string.Format(ContextMenuPattern, SelectedMenus[i-1]), $"Context menu {SelectedMenus[i-1]}").ClickSubItem(SelectedMenus[i]);
            }
        }

        protected ApplicationMenu AddMenu(string menuName)
        {
            SelectedMenus.Add(menuName);
            return this;
        }

        public File File => new File();
        public Run Run => new Run();
    }
}
