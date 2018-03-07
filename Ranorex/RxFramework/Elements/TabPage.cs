namespace RxFramework.Elements
{
    public class TabPage : BaseElement
    {
        public TabPage(string locator, string name) : base(locator, name)
        {
        }

        public string TabName => GetElement<Ranorex.TabPage>().Title;
    }
}
