
namespace demo.framework.Utils.Vk
{
    public class VkMethod
    {
        private VkMethod(string value) { Value = value; }

        public string Value { get; set; }

        public static VkMethod PostOnWall => new VkMethod("wall.post");

        public override string ToString() => Value;
    }
}
