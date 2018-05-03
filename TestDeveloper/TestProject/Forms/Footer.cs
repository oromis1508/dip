using System.Text.RegularExpressions;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;

namespace TestProject.Forms
{
    public class Footer : BaseForm
    {
        private Label lblVariant = new Label(By.XPath("//footer//span"), "text with the number of variant");

        public Footer() : base(By.TagName("footer"), "page's footer")
        {
        }

        public string GetVariantNumber()
        {
            var variantText = lblVariant.Text;
            return Regex.Match(variantText, ".*Version: (\\d)\\D*").Groups[1].Value;
        }
    }
}
