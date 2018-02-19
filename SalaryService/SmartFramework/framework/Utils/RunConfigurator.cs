using System.Xml;

namespace demo.framework.Utils
{
    public class RunConfigurator
    {   
        private static readonly XmlDocument XmlDoc = new XmlDocument(); // Create an XML document object
            
        public static string GetValue(string tag)
        {   
            XmlDoc.Load("../../Resources/run.xml"); // Load the XML document from the specified file
            var browser = XmlDoc.GetElementsByTagName(tag);
            return browser[0].InnerText;
        }
    }
}
