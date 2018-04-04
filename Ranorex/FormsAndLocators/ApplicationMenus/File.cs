namespace FormsAndLocators.ApplicationMenus
{
    public class File : ApplicationMenu
    {
        public File() : base("File")
        {
        }

        public ApplicationMenu New => AddMenu("New");
        public ApplicationMenu SaveAs => AddMenu("Save As");
        public ApplicationMenu Exit => AddMenu("Exit");
    }
}
