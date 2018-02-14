using demo.framework.Utils.Vk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VkApi.Api
{
    class ApiRequests
    {
        public string CreatedPostId { get; private set; }

        public void PostMessageOnWall(string message)
        {
            var responseJson = VkApiUtil.SendRequest(VkMethod.PostOnWall, RequestType.POST, new[] { $"message={message}" });
            CreatedPostId = responseJson.response.post_id;
        }

        public void EditPostOnWall(string newMessage, string postId, dynamic jsonUploadedPhoto)
        {
            string photoId = jsonUploadedPhoto.response[0].id;
            var responseJson = VkApiUtil.SendRequest(VkMethod.EditPost, RequestType.POST, 
                new[] { $"message={newMessage}", $"post_id={postId}", $"attachments={photoId}" });
            string response = responseJson.response;
            Assert.AreEqual("1", response, "Post edited successfully");
        }

        public dynamic UploadPhotoOnServer(string filePath, string fileName)
        {
            var jsonWithUploadServer = VkApiUtil.SendRequest(VkMethod.GetWallUploadServer, RequestType.POST);
            string uploadUri = jsonWithUploadServer.response.upload_url;
            var jsonWithUploadedPhoto = VkApiUtil.UploadPhotoOnServer(uploadUri, filePath, fileName);
            string photo = jsonWithUploadedPhoto.photo;
            string server = jsonWithUploadedPhoto.server;
            string hash = jsonWithUploadedPhoto.hash;
            return VkApiUtil.SendRequest(VkMethod.SaveWallPhotoOnServer, RequestType.POST, new[] { $"photo={photo}", $"server={server}", $"hash={hash}" });
        }

        public dynamic CreateCommentToWallPost(string postId, string message) => 
                VkApiUtil.SendRequest(VkMethod.CreateCommentOnWallPost, 
                RequestType.POST, new[] { $"post_id={postId}", $"message={message}" });

        public bool IsPostLiked(string postId)
        {
            var jsonResponse = VkApiUtil.SendRequest(VkMethod.IsPostLiked, RequestType.POST, new[] {"type=post", $"item_id={postId}"});
            string liked = jsonResponse.response;
            return liked.Equals("1");
        }

        public void DeletePost(string postId)
        {
            var jsonResponse = VkApiUtil.SendRequest(VkMethod.DeleteWallPost, RequestType.POST, new []{ $"post_id={postId}" });
            string postDeleted = jsonResponse.response;
            Assert.AreEqual("1", postDeleted, "Post deleted successfully");
        }
    }
}
