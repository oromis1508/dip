using System.IO;
using System.Net;
using System.Xml;
using demo.framework.Utils.SalaryWebService;

namespace demo.framework.Utils
{
    public class SoapUtil
    {
        private static string GetSoapMessage(WebServiceMethod method)
        {
            var request = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                          "<soap:Envelope xmlns:xsi=" +
                          "\"http://www.w3.org/2001/XMLSchema-instance\" " +
                          "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                          "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
                          $"  <soap:Body>\r\n    <{method.Name} xmlns=\"Service\">\r\n" +
                          method.Body +
                          $"    </{method.Name}>\r\n  </soap:Body>\r\n</soap:Envelope>";
            return request;
        }

        public static XmlDocument SendMessage(string webServiceName, WebServiceMethod webServiceMethod)
        {
            var soapMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                          "<soap:Envelope xmlns:xsi=" +
                          "\"http://www.w3.org/2001/XMLSchema-instance\" " +
                          "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                          "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
                          $"  <soap:Body>\r\n    <{webServiceMethod.Name} xmlns=\"Service\">\r\n" +
                              webServiceMethod.Body +
                          $"    </{webServiceMethod.Name}>\r\n  </soap:Body>\r\n</soap:Envelope>";

            var webRequest = (HttpWebRequest)WebRequest.Create(webServiceName);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            webRequest.SendChunked = true;
            webRequest.Headers.Add("SOAPAction", $"Service/{webServiceMethod.Name}");

            var stream = webRequest.GetRequestStream();
            var writer = new StreamWriter(stream);
            writer.Write(soapMessage);
            writer.Close();
            var response = webRequest.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseString);
            return xmlDocument;
        }
    }
}