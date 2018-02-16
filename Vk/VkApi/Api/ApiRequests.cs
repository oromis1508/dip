using demo.framework.Utils.Vk;

namespace VkApi.Api
{
    internal class ApiRequests
    {
        public string PostMessageOnWall(string message)
        {
            var responseJson = VkApiUtil.SendRequest(VkMethod.PostOnWall, RequestType.POST, new[] { $"message={message}" });
            return responseJson.response.post_id.ToString();
        }

        public void EditPostOnWall(string newMessage, string postId, string idUploadedPhoto) => VkApiUtil.SendRequest(VkMethod.EditPost, RequestType.POST,
                new[] { $"message={newMessage}", $"post_id={postId}", $"attachments={idUploadedPhoto}" });

        public string UploadPhotoOnServer(string filePath, string fileName)
        {
            var jsonWithUploadServer = VkApiUtil.SendRequest(VkMethod.GetWallUploadServer, RequestType.POST);
            string uploadUri = jsonWithUploadServer.response.upload_url;
            var jsonWithUploadedPhoto = VkApiUtil.UploadPhotoOnServer(uploadUri, filePath, fileName);
            string photo = jsonWithUploadedPhoto.photo;
            string server = jsonWithUploadedPhoto.server;
            string hash = jsonWithUploadedPhoto.hash;
            return VkApiUtil.SendRequest(VkMethod.SaveWallPhotoOnServer, RequestType.POST, new[] { $"photo={photo}", $"server={server}", $"hash={hash}" }).response[0].id.ToString();
        }

        public string CreateCommentToWallPost(string postId, string message) => 
                VkApiUtil.SendRequest(VkMethod.CreateCommentOnWallPost, 
                RequestType.POST, new[] { $"post_id={postId}", $"message={message}" }).response.cid.ToString();

        public bool IsPostLiked(string postId, string userId)
        {
            var jsonResponse = VkApiUtil.SendRequest(VkMethod.IsPostLiked, RequestType.POST, new[] {"type=post", $"item_id={postId}", $"user_id={userId}" });
            string liked = jsonResponse.response;
            return liked.Equals("1");
        }

        public void DeletePost(string postId) => VkApiUtil.SendRequest(VkMethod.DeleteWallPost, RequestType.POST, new[] { $"post_id={postId}" });
    }
}
