using demo.framework.Utils.Vk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VkAPI
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
            var ownerId = jsonUploadedPhoto.owner_id;
            var photoId = jsonUploadedPhoto.id;
            var responseJson = VkApiUtil.SendRequest(VkMethod.EditPost, RequestType.POST, 
                new[] { $"message={newMessage}", $"post_id={postId}", $"attachments=photo{ownerId}_{photoId}" });
            Assert.AreEqual("1", responseJson.response, "Post eddited successfully");
        }

        public dynamic UploadPhotoOnServer(string photoPath)
        {
            var jsonWithUploadServer = VkApiUtil.SendRequest(VkMethod.GetWallUploadServer, RequestType.POST);
            var jsonWithUploadedPhoto = VkApiUtil.UploadFileOnServer(jsonWithUploadServer.upload_url, photoPath, "multipart/form-data");
            return VkApiUtil.SendRequest(VkMethod.SaveWallPhotoOnServer, RequestType.POST, new[] { $"photo={jsonWithUploadedPhoto.photo}" });
        }

        public dynamic CreateCommentToWallPost(string postId, string message) => 
                VkApiUtil.SendRequest(VkMethod.CreateCommentOnWallPost, 
                RequestType.POST, new[] { $"post_id={postId}", $"message={message}" });

        public bool IsPostLiked(string postId)
        {
            var jsonResponse = VkApiUtil.SendRequest(VkMethod.IsPostLiked, RequestType.POST, new[] {"type=post", $"item_id={postId}"});
            return jsonResponse.response.liked.Equals("1");
        }

        public void DeletePost(string postId)
        {
            var jsonResponse = VkApiUtil.SendRequest(VkMethod.DeleteWallPost, RequestType.POST, new []{ $"post_id={postId}" });
            Assert.AreEqual("1", jsonResponse.response, "Post deleted successfully");
        }
    }
}
