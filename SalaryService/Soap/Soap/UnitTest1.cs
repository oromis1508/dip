using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using demo.framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soap
{
    [TestClass]
    public class UnitTest1
    {
        [Serializable]
        class Person
    {
        public int newID { get; set; }
        public string newPrivate_id { get; set; }
        public string newFirst_name;
        public string newLast_name;
        public string newMiddle_name;
        public int newExp;
        public int newProfession_id;


        public Person(int newID, string newPrivate_id, string newFirst_name, string newLast_name, string newMiddle_name, int newExp, int newProfession_id)
        {
            this.newID = newID;
            this.newPrivate_id = newPrivate_id;
            this.newFirst_name = newFirst_name;
            this.newLast_name = newLast_name;
            this.newMiddle_name = newMiddle_name;
            this.newExp = newExp;
            this.newProfession_id = newProfession_id;
        }
    }

        [TestMethod]
        public void TestMethod1()
        {
/*            var ut = new SoapUtil();
            var doc = ut.GetSoapMessage();
            //var xmlstr = doc.InnerXml;
            //var bytes = new UTF8Encoding().GetBytes(xmlstr);

            var trying = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                         "<soap:Envelope xmlns:xsi=" +
                         "\"http://www.w3.org/2001/XMLSchema-instance\" " +
                         "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                         "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
                         "  <soap:Body>\r\n    <AddNewEmployee xmlns=\"Service\">\r\n" +
                         "      <newID>78941242</newID>\r\n" +
                         "      <newPrivate_id>f3h7wqef</newPrivate_id>\r\n" +
                         "      <newFirst_name>GFees</newFirst_name>\r\n" +
                         "      <newLast_name>RETESDF</newLast_name>\r\n" +
                         "      <newMiddle_name>GRfrgr</newMiddle_name>\r\n" +
                         "      <newExp>8</newExp>\r\n" +
                         "      <newProfession_id>4</newProfession_id>\r\n" +
                         "    </AddNewEmployee>\r\n  </soap:Body>\r\n</soap:Envelope>";


            var webRequest = (HttpWebRequest)WebRequest.Create(@"http://172.20.68.55:1060/service/Service.asmx");
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            webRequest.ContentLength = trying.Length;
            webRequest.Headers.Add("SOAPAction", "Service/AddNewEmployee");

            var stream = webRequest.GetRequestStream();
            var writer = new StreamWriter(stream);
            writer.Write(trying);
            writer.Close();
            var response = webRequest.GetResponse();
            var xmlres = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Debug.WriteLine(response);*/
            DbUtil.ConnectDatabase();
            DbUtil.GetResponse("SELECT first_name FROM employees WHERE Id = 1");
        }
    }
}
