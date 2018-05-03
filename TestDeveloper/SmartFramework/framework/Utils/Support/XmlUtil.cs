﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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

        public static XElement IsTagTagValue(XmlDocument xmlDocument, string tagNameToSort, string tagNameSortBy)
        {
            var root = XElement.Load(new XmlNodeReader(xmlDocument));

            var orderedtabs = root.Elements(tagNameToSort)
                .OrderBy(xtab => (int)xtab.Element(tagNameSortBy))
                .ToArray();

            root.RemoveAll();
            foreach (var tab in orderedtabs)
            {
                root.Add(tab);
            }
            return root;
        }

        public static List<T> GetTestsFromXml<T>(XmlDocument xmlDocument)
        {
            var tests = new List<T>();
            foreach (var test in xmlDocument.GetElementsByTagName("test"))
            {
                var xmlReader = new XmlNodeReader((XmlNode)test);
                var serializer = new XmlSerializer(typeof(T));
                tests.Add((T) serializer.Deserialize(xmlReader));
            }

            return tests;
        }

    }
}
