using demo.framework.Elements;
using OpenQA.Selenium;

namespace VkAPI.Forms
{
    internal class VkWallPost : BaseElement
    {
        public VkWallPost(string postId, string authorId) : base(By.Id($"post{authorId}_{postId}"), $"post{authorId}_{postId}")
        {
        }

        public string GetPostAttribute(WallPostAttribute wallPostAttribute) 
            => GetElement().FindElementByXPath(wallPostAttribute.ToString()).Text;

        public void LikePost() => GetElement().FindElementByXPath("//span[contains(@class, 'post_like_link')]").Click();
    }
}
