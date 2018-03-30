using System;
using System.Drawing;
using System.Net;
using demo.framework.BaseEntities;
using demo.framework.Elements;
using demo.framework.Utils;
using OpenQA.Selenium;

namespace VkAPI.Forms
{
    internal class MyPage : BaseForm
    {
        private const string PageNameClass = "page_name";
        private readonly string _photoLocator = $"/photo{UserId}_{{0}}";

        private readonly Label _lblPageName = new Label(By.ClassName(PageNameClass), "Page name");

        public static string UserId { get; } = Browser.CurrentUri.Split(new[] { "id" }, StringSplitOptions.None)[1];
        public string PageName => _lblPageName.Text;

        public MyPage() : base(By.ClassName(PageNameClass), "My page")
        {
        }

        public string VkWallPostAttribute(string postId, WallPostAttribute wallPostAttribute)
            => new VkWallPost(postId, UserId).GetPostAttribute(wallPostAttribute);

        public void LikePost(string postId) => new VkWallPost(postId, UserId).LikePost();

        public Bitmap GetBitmapOfImage(string postId, string photoId)
        {
            var imageHref = string.Format(_photoLocator, photoId);
            var imageUri = new VkWallPost(postId, UserId).GetImageUri(imageHref);
            var request = WebRequest.Create(imageUri);
            return new Bitmap(Image.FromStream(request.GetResponse().GetResponseStream()));
        }

        public bool IsPostExist(string postId) => !new VkWallPost(postId, UserId).IsDeleted();
    }
}
