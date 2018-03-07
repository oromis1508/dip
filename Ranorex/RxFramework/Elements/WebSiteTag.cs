namespace RxFramework.Elements
{
    public class WebSiteTag : BaseElement
    {
        public WebSiteTag(string locator, string name) : base(locator, name)
        {
        }

        public string TagInnerText => GetElement<Ranorex.WebElement>().InnerText;
    }
}
