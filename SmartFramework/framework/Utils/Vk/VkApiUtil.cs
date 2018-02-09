using System.Linq;
using System.Net;
using demo.framework.Utils.Vk;

namespace demo.framework.Utils
{
    public class VkApiUtil
    {
        private string vkMethodsURI = @"https://api.vk.com/method/";
        private string access_token = $"access_token={RunConfigurator.GetValue("token")}";

        public string ContentType { get; set; }

        public VkApiUtil()
        {
            ContentType = "application/JSON";
        }

        public string GetRequest(VkMethod method, RequestType requestType, string[] parameters) {
            {
                var requestString = $"{vkMethodsURI}{method}?";

                requestString = parameters.Aggregate(requestString, (current, parameter) => current + $"{parameter}&");

                requestString += access_token;

                var request = (HttpWebRequest) WebRequest.Create(requestString);
                request.Method = requestType.ToString();
                request.ContentType = ContentType;

                return request.GetResponse();
            }
        }
}
