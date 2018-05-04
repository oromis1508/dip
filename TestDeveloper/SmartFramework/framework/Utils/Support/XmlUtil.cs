using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.Support
{
    public static class XmlUtil
    {
        public static XmlDocument GetXmlWithoutRootNode(Stream xmlStream)
        {
            var streamString = new StreamReader(xmlStream).ReadToEnd();
            if (streamString.StartsWith("<"))
            {
                var normalXml = $"<root>{streamString}</root>";
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(normalXml);
                return xmlDocument;
            }

            BaseEntity.Log.Fatal("Invalid format of xml");
            throw new FormatException("Invalid format of xml");
        }

        public static List<T> GetObjectsFromXml<T>(XmlDocument xmlDocument)
        {
            var tests = new List<T>();
            foreach (var test in xmlDocument.GetElementsByTagName(typeof(T).Name))
            {
                var xmlReader = new XmlNodeReader((XmlNode)test);
                var serializer = new XmlSerializer(typeof(T));
                tests.Add((T) serializer.Deserialize(xmlReader));
            }

            return tests;
        }

        public static XmlDocument OpenXmlDocument(string pathToXml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToXml);
            return xmlDocument;
        }
    }
}
