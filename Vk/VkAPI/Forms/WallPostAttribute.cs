namespace VkAPI.Forms
{
    internal class WallPostAttribute
    {
        private WallPostAttribute(string value) => Value = value;

        private string Value { get;}

        public static WallPostAttribute PostAuthor => new WallPostAttribute("//h5[@class='post_author']/a");

        public static WallPostAttribute PostMessage => new WallPostAttribute("//div[contains(@class, 'wall_post_text')]");

        public static WallPostAttribute PostCommentAuthor(string commentId) => new WallPostAttribute($"//div[@id='{commentId}']//a[@class='author']");

        public static WallPostAttribute PostCommentText(string commentId) => new WallPostAttribute($"//div[@id='{commentId}']//div[@class='wall_reply_text']");

        public override string ToString() => Value;
    }
}
