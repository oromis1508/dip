using System.IO;
using System.Text;
using smart.framework.Utils.Html;

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

            return Requests.PostRequest(requestString, contentType, data).GetResponseStream(); ;
        }
    }
}
