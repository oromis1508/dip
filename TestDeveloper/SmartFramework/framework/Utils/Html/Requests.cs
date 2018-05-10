using System;
using System.Net;
using System.Web;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.Html
{
    internal class Requests
    {
        public static HttpWebResponse PostRequest(string url, string contentType, byte[] data, NetworkCredential credentials = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = RequestType.POST.ToString();
            request.ContentType = contentType;
            request.ContentLength = data.Length;

            if (credentials != null)
            {
                var encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials.UserName + ":" + credentials.Password));
                request.Headers.Add("Authorization", "Basic " + encoded);
            }

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }

            BaseEntity.Log.Fatal("Error of getting the response");
            throw new HttpException("Error of getting the response");
        }
    }
}
