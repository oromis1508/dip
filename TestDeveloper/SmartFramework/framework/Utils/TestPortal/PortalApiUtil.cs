using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.TestPortal
{
    public static class PortalApiUtil
    {
        private const string MethodsUri = @"http://portal.com:8080/api";

        public static Stream SendRequest(PortalMethod method, 
                                          string[] parameters = null, 
                                          string contentType = "application/x-www-form-urlencoded", 
                                          RequestType requestType = RequestType.POST)
        {
            var requestString = $"{MethodsUri}{method}";

            var postData = "";
            if (parameters != null)
            {
                for (var i=0; i<parameters.Length; i++)
                {
                    postData += parameters[i];
                    if (i + 1 != parameters.Length)
                    {
                        postData += "&";
                    }
                }
            }
            var data = Encoding.ASCII.GetBytes(postData);

            var request = (HttpWebRequest) WebRequest.Create(requestString);
            request.Method = requestType.ToString();
            request.ContentType = contentType;
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.GetResponseStream();
/*
                var streamReader = new StreamReader(responseStream);
                return streamReader.ReadToEnd();
*/
            }

            BaseEntity.Log.Fatal("Error of getting the response");
            return null;       
        }
    }
}
