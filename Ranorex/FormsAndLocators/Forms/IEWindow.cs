using RxFramework.BaseEntities;
using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class IEWindow : BaseForm
    {
        public string GetPageTagValue(string pageName, string tagName) => 
            new WebSiteTag(string.Format(_pageTagXpath, pageName, tagName), $"Tag {tagName} for page {pageName}").TagInnerText;

        public void CloseApp() => _btnCloseApp.Click();
    }
}
