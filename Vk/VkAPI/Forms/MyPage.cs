using System;
using demo.framework;
using demo.framework.Elements;
using demo.framework.forms;
using OpenQA.Selenium;

namespace VkAPI.Forms
{
    internal class MyPage : BaseForm
    {
        private const string PageNameClass = "page_name";
        public string UserId { get; } = Browser.CurrentUri.Split(new[] {"id"}, StringSplitOptions.None)[1];

        private readonly Label _lblPageName = new Label(By.ClassName(PageNameClass), "Page name");


        public MyPage() : base(By.ClassName(PageNameClass), "My page")
        {
        }

        public string PageName => _lblPageName.Text;

        public string VkWallPostAttribute(string postId, WallPostAttribute wallPostAttribute)
            => new VkWallPost(postId, UserId).GetPostAttribute(wallPostAttribute);

        public void LikePost(string postId) => new VkWallPost(postId, UserId).LikePost();

        public bool IsPostExist(string postId) => new VkWallPost(postId, UserId).IsPresent();
    }
}
