
namespace demo.framework.Utils.Vk
{
    public class VkMethod
    {
        private VkMethod(string value) => Value = value; 

        public string Value { get; set; }

        public static VkMethod PostOnWall => new VkMethod("wall.post");

        public static VkMethod EditPost => new VkMethod("wall.edit");

        public static VkMethod GetWallUploadServer => new VkMethod("photos.getWallUploadServer");

        public static VkMethod SaveWallPhotoOnServer => new VkMethod("photos.saveWallPhoto");

        public static VkMethod CreateCommentOnWallPost => new VkMethod("wall.createComment");

        public static VkMethod IsPostLiked => new VkMethod("likes.isLiked");

        public static VkMethod DeleteWallPost => new VkMethod("wall.delete");

        public override string ToString() => Value;
    }
}
