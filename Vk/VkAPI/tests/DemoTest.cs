using System.IO;
using demo.framework;
using demo.framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VkApi.Api;
using VkAPI.Forms;

namespace VkAPI.Tests
{
    [TestClass]
    public class DemoTest : BaseTest
    {
        private readonly string _username = RunConfigurator.GetValue("username");
        private readonly string _password = RunConfigurator.GetValue("password");
        private readonly int _maxSizeRandomString = int.Parse(RunConfigurator.GetValue("maxSizeRandomString"));
        private readonly string _uploadedFileName = RunConfigurator.GetValue("uploadedFileName");

        [TestMethod]
        public void RunTest()
        {
            Log.Step(1);
            var authForm = new MainAuthForm();
            authForm.LogIn(_username, _password);

            Log.Step(2);
            var news = new NewsForm();
            news.NavigateMyPage();

            Log.Step(3);
            var apiRequests = new ApiRequests();
            var postingMessage = RandomString.Generate(_maxSizeRandomString);
            apiRequests.PostMessageOnWall(postingMessage);
            var myPage = new MyPage();
            var usingPostId = apiRequests.CreatedPostId;
            Assert.AreEqual(myPage.PageName, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostAuthor));
            Assert.AreEqual(postingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage));

            Log.Step(4);
            var editingMessage = RandomString.Generate(_maxSizeRandomString);
            var jsonUploadedPhotoOnServer = apiRequests.UploadPhotoOnServer(Path.Combine(Directory.GetCurrentDirectory(), "Resources"), _uploadedFileName);
            apiRequests.EditPostOnWall(editingMessage, usingPostId, jsonUploadedPhotoOnServer);


            var imageHref = $"/photo{myPage.UserId}_{jsonUploadedPhotoOnServer.response[0].pid}";
            var sourceImageURI = Path.Combine(Directory.GetCurrentDirectory(), "Resources", _uploadedFileName);
            Assert.AreEqual(editingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage));
            Assert.IsTrue(myPage.IsImageValid(usingPostId, imageHref, sourceImageURI), "Image added to post correctly");

            Log.Step(5);
            var commentMessage = RandomString.Generate(_maxSizeRandomString);
            var jsonWithCommentId = apiRequests.CreateCommentToWallPost(usingPostId, commentMessage);
            var commentId = $"post{myPage.UserId}_{jsonWithCommentId.response.cid}";
            Assert.AreEqual(commentMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentText(commentId)));
            Assert.AreEqual(myPage.PageName, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentAuthor(commentId)));

            Log.Step(6);
            myPage.LikePost(usingPostId);
            Assert.IsTrue(apiRequests.IsPostLiked(usingPostId), $"Post with id {usingPostId} liked");

            Log.Step(7);
            apiRequests.DeletePost(usingPostId);
            Assert.IsFalse(myPage.IsPostExist(usingPostId));
        }
    }
}
