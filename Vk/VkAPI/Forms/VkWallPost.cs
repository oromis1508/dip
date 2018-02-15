using System;
using System.Text.RegularExpressions;
using demo.framework.Elements;
using demo.framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace VkAPI.Forms
{
    internal class VkWallPost : BaseElement
    {
        public VkWallPost(string postId, string authorId) : base(By.Id($"post{authorId}_{postId}"), $"post{authorId}_{postId}")
        {
        }

        private IWebElement WaitForChildren(string xPath)
        {
            IWebElement webElement = null;
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    var webElements = GetElement().FindElementsByXPath(xPath);
                    if (webElements.Count > 0)
                    {
                        webElement = webElements[0];
                        return true;
                    }
                    return false;
                });
            }
            catch (TimeoutException)
            {
                Log.Fatal($"Element with locator: '{xPath}' does not exists!");
            }
            return webElement;
        }

        public string GetPostAttribute(WallPostAttribute wallPostAttribute)
        {
            return WaitForChildren(wallPostAttribute.ToString()).Text;
        }



        public void LikePost()
        {
            GetElement().FindElementByXPath("//span[contains(@class, 'post_like_link')]").Click();
            WaitForChildren("//span[contains(@class, 'post_like_count') and text()='1']");
        }

        public bool IsDeleted()
        {
            IWebElement webElement = null;
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting => !IsPresent());
                return true;
            }
            catch (TimeoutException)
            {
                Log.Fatal($"Post with: '{GetLocator()}' was not delete!");
                return false;
            }
        }

        public string GetImageUri(string imageHref)
        {
            var imageStyle = WaitForChildren(WallPostAttribute.PostImage(imageHref).ToString()).GetAttribute("style");
            var s = Regex.Matches(imageStyle, ".*url\\((.*)\\).*", RegexOptions.None);
            return Regex.Matches(imageStyle, ".*url\\(\"(.*)\"\\).*", RegexOptions.None)[0].Groups[1].Value;
        }
    }
}
