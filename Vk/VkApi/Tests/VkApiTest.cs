using System.Drawing;
using System.IO;
using demo.framework.BaseEntities;
using demo.framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VkApi.Api;
using VkApi.Forms;
using VkAPI.Forms;

namespace VkAPI.Tests
{
    [TestClass]
    public class VkApiTest : BaseTest
    {
        private readonly string _username = RunConfigurator.GetValue("username");
        private readonly string _password = RunConfigurator.GetValue("password");
        private readonly int _maxSizeRandomString = int.Parse(RunConfigurator.GetValue("maxSizeRandomString"));
        private readonly string _uploadedFileName = RunConfigurator.GetValue("uploadedFileName");

        [TestMethod]
        public void VkPostTest()
        {
            Log.Step(1, $"Login as the user {_username} with the password {_password}");
            var authForm = new MainAuthForm();
            authForm.LogIn(_username, _password);

            Log.Step(2, "Navigate to the my page");
            var menu = new NavigateMenu();
            menu.NavigatePage(NavigateMenuItem.MyPage);

            Log.Step(3, "Add a post on wall by the api request");
            var apiRequests = new ApiRequests();
            var postingMessage = RandomString.Generate(_maxSizeRandomString);
            var usingPostId = apiRequests.PostMessageOnWall(postingMessage);

            Log.Step(4, "Check adding the message on the wall correctly");
            var myPage = new MyPage();
            Asserts.AreEqual(myPage.PageName, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostAuthor), "Check the post author");
            Asserts.AreEqual(postingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage), "Check the post message");

            Log.Step(5, "Edit added the post message and add photo");
            var editingMessage = RandomString.Generate(_maxSizeRandomString);
            var idUploadedPhoto =
                apiRequests.UploadPhotoOnServer(Path.Combine(Directory.GetCurrentDirectory(), "Resources"),
                    _uploadedFileName);
            apiRequests.EditPostOnWall(editingMessage, usingPostId, idUploadedPhoto);

            Log.Step(6, "Check the post editing correctly");
            var sourceImage = Path.Combine(Directory.GetCurrentDirectory(), "Resources", _uploadedFileName);
            var sourceImageBitmap = new Bitmap(Image.FromFile(sourceImage));
            var pageImageBitmap = myPage.GetBitmapOfImage(usingPostId, idUploadedPhoto.Split('_')[1]);
            Asserts.AreEqual(editingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage), "Check on the post message edited correctly");
            Asserts.AreEqual(sourceImageBitmap, pageImageBitmap, "Image added to post correctly", true);

            Log.Step(7, "Add a comment to the post");
            var commentMessage = RandomString.Generate(_maxSizeRandomString);
            var commentPostId = apiRequests.CreateCommentToWallPost(usingPostId, commentMessage);

            Log.Step(8, "Check adding comment to the post correctly");
            var commentId = $"post{MyPage.UserId}_{commentPostId}";
            Asserts.AreEqual(commentMessage,
                myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentText(commentId)), "Check the comment to post message");
            Asserts.AreEqual(myPage.PageName,
                myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentAuthor(commentId)), "Check the comment to post author");

            Log.Step(9, "Like the post");
            myPage.LikePost(usingPostId);

            Log.Step(10, "Check on the post liked by the right user");
            Asserts.IsTrue(apiRequests.IsPostLiked(usingPostId, MyPage.UserId), $"Post with id {usingPostId} liked");

            Log.Step(11, "Delete the post");
            apiRequests.DeletePost(usingPostId);

            Log.Step(12, "Check on the post deleted");
            Asserts.IsFalse(myPage.IsPostExist(usingPostId), $"Post with id {usingPostId} deleted");
        }
    }
}
