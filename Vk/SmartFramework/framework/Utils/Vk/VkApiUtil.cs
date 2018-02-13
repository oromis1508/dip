using System.IO;
using System.Linq;
using System.Net;
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

            return GetResponse(request);
        }

        private static dynamic GetResponse(WebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var streamReader = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }
            BaseEntity.Log.Fatal("Error of getting the response");
            return null;
        }

        public static dynamic UploadFileOnServer(string uri, string file, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = RequestType.POST.ToString();

            var requestStream = httpWebRequest.GetRequestStream();
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            var buffer = new byte[8192];
            var bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            requestStream.Close();

            return GetResponse(httpWebRequest);
        }
    }
}
