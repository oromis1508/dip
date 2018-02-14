using System;
using System.Drawing;
using System.Net;
using demo.framework;
using demo.framework.Elements;
using demo.framework.forms;
using OpenQA.Selenium;
using VkApi;

namespace VkAPI.Forms
{
    internal class MyPage : BaseForm
    {
        private const string PageNameClass = "page_name";

        public string UserId { get; }

        private readonly Label _lblPageName = new Label(By.ClassName(PageNameClass), "Page name");


        public MyPage() : base(By.ClassName(PageNameClass), "My page")
        {
            UserId = Browser.CurrentUri.Split(new[] { "id" }, StringSplitOptions.None)[1];
        }

        public string PageName => _lblPageName.Text;

        public string VkWallPostAttribute(string postId, WallPostAttribute wallPostAttribute)
            => new VkWallPost(postId, UserId).GetPostAttribute(wallPostAttribute);

        public void LikePost(string postId)
        {
            new VkWallPost(postId, UserId).LikePost();
        }

        public bool IsImageValid(string postId, string imageHref, string sourceImage)
        {
            var imageUri = new VkWallPost(postId, UserId).GetImageURI(imageHref);
            var sourceImageBitmap = Resource.wall;

            var request = WebRequest.Create(imageUri);
            var imageOnWallBitmap = Bitmap.FromStream(request.GetResponse().GetResponseStream());
            return sourceImageBitmap.Equals(imageOnWallBitmap);
        }

        public bool IsPostExist(string postId) => new VkWallPost(postId, UserId).IsDeleted();
    }
}
