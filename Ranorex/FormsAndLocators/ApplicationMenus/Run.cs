namespace FormsAndLocators.ApplicationMenus
{
    public class Run : ApplicationMenu
    {
        public Run() : base("Run")
        {
        }

        public ApplicationMenu RunIn => AddMenu("Run");
        public ApplicationMenu LaunchInFirefox => AddMenu("Launch in Firefox");
        public ApplicationMenu LaunchInIe => AddMenu("Launch in IE");
        public ApplicationMenu LaunchInChrome => AddMenu("Launch in Chrome");
        public ApplicationMenu LaunchInSafari => AddMenu("Launch in Safari");
        public ApplicationMenu GetPhpHelp => AddMenu("Get php help");
        public ApplicationMenu WikipediaSearch => AddMenu("Wikipedia Search");
        public ApplicationMenu OpenFileInAnotherInstance => AddMenu("Open file in another instance");
        public ApplicationMenu SendViaOutlook => AddMenu("Send via Outlook");
        public ApplicationMenu ModifyShortcut => AddMenu("Modify Shortcut");
    }
}
