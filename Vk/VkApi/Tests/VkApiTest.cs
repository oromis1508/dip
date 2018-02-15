using System.IO;
using demo.framework.BaseEntities;
using demo.framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VkApi.Api;
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
            var news = new NewsForm();
            news.NavigateMyPage();

            Log.Step(3, "Add a post on wall by the api request");
            var apiRequests = new ApiRequests();
            var postingMessage = RandomString.Generate(_maxSizeRandomString);
            apiRequests.PostMessageOnWall(postingMessage);
            var myPage = new MyPage();
            var usingPostId = apiRequests.CreatedPostId;
            Asserts.Assert.AreEqual(myPage.PageName, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostAuthor), "Check the post author");
            Asserts.Assert.AreEqual(postingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage), "Check the post message");

            Log.Step(4, "Edit added the post message and add photo");
            var editingMessage = RandomString.Generate(_maxSizeRandomString);
            var jsonUploadedPhotoOnServer =
                apiRequests.UploadPhotoOnServer(Path.Combine(Directory.GetCurrentDirectory(), "Resources"),
                    _uploadedFileName);
            apiRequests.EditPostOnWall(editingMessage, usingPostId, jsonUploadedPhotoOnServer);

            var imageHref = $"/photo{myPage.UserId}_{jsonUploadedPhotoOnServer.response[0].pid}";
            var sourceImageUri = Path.Combine(Directory.GetCurrentDirectory(), "Resources", _uploadedFileName);
            Asserts.Assert.AreEqual(editingMessage, myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostMessage), "Check on the post message edited correctly");
            Asserts.SoftAssert.IsTrue(myPage.IsImageValid(usingPostId, imageHref, sourceImageUri), "Image added to post correctly");

            Log.Step(5, "Add a comment to the post");
            var commentMessage = RandomString.Generate(_maxSizeRandomString);
            var jsonWithCommentId = apiRequests.CreateCommentToWallPost(usingPostId, commentMessage);
            var commentId = $"post{myPage.UserId}_{jsonWithCommentId.response.cid}";
            Asserts.Assert.AreEqual(commentMessage,
                myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentText(commentId)), "Check the comment to post message");
            Asserts.Assert.AreEqual(myPage.PageName,
                myPage.VkWallPostAttribute(usingPostId, WallPostAttribute.PostCommentAuthor(commentId)), "Check the comment to post author");

            Log.Step(6, "Like the post");
            myPage.LikePost(usingPostId);
            Asserts.Assert.IsTrue(apiRequests.IsPostLiked(usingPostId), $"Post with id {usingPostId} liked");

            Log.Step(7, "Delete the post");
            apiRequests.DeletePost(usingPostId);
            Asserts.Assert.IsFalse(myPage.IsPostExist(usingPostId), $"Post with id {usingPostId} deleted");
        }
    }
}
