using demo.framework.Utils.SalaryWebService;

namespace demo.framework.Utils
{
    public class SoapUtil
    {
        public static string GetSoapMessage(WebServiceMethod method)
        {
            var request = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                          "<soap:Envelope xmlns:xsi=" +
                          "\"http://www.w3.org/2001/XMLSchema-instance\" " +
                          "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                          "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
                          "  <soap:Body>\r\n" +
                          $"    <{method.Name} xmlns=\"Service\">\r\n" +
                          method.Body +
                          $"    </{method.Name}>\r\n" +
                          "  </soap:Body>\r\n" +
                          "</soap:Envelope>";

            return request;
        }
    }
}