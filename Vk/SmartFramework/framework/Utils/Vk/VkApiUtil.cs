using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace demo.framework.Utils.Vk
{
    public static class VkApiUtil
    {
        private const string VkMethodsUri = @"https://api.vk.com/method/";
        private static readonly string AccessToken = $"access_token={RunConfigurator.GetValue("token")}";

        public static string ContentType { get; set; } = "application/JSON";

        public static dynamic SendRequest(VkMethod method, RequestType requestType, string[] parameters = null)
        {
            var requestString = $"{VkMethodsUri}{method}?";

            if (parameters != null)
            {
                requestString = parameters.Aggregate(requestString, (current, parameter) => current + $"{parameter}&");
            }

            requestString += AccessToken;

            var request = (HttpWebRequest) WebRequest.Create(requestString);
            request.Method = requestType.ToString();
            request.ContentType = ContentType;
            request.ContentLength = 0;

            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var streamReader = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }

            BaseEntity.Log.Fatal("Error of getting the response");
            return null;       
        }

        public static dynamic UploadPhotoOnServer(string uri, string filePath, string fileName)
        {
            var form = new MultipartFormDataContent
            {
                {new ByteArrayContent(File.ReadAllBytes(Path.Combine(filePath, fileName))), "photo", fileName}
            };

            var httpClient = new HttpClient();
            var response = httpClient.PostAsync(uri, form).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject(responseString);
        }
    }
}
