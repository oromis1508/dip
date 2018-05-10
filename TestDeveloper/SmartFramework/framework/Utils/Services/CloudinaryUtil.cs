using System.IO;
using Cloudinary;
using Cloudinary.Results;

namespace smart.framework.Utils.Services
{
    public class CloudinaryUtil
    {
        public static string CloudName { get; set; } = "dmy2hakdu";
        public static string ApiKey { get; set; } = "268336191758197";
        public static string ApiSecret { get; set; } = "OHFfJWUnCXrwnGUmTagFksWpjeY";

        public static UploadResult UploadPhoto(string fileName, Stream imageInputStream)
        {
            var uploader = new Uploader(new AccountConfiguration(CloudName, ApiKey, ApiSecret));
            var uploadInformation = new UploadInformation(fileName, imageInputStream);
            var res = uploader.Upload(uploadInformation);
            return res;
        }

    }
}
