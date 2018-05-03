using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TestProject.Entities
{
    public class test
    {
        public string duration { get; set; }
        public string method { get; set; }
        public string name { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string status { get; set; }

        public override bool Equals(object obj) => GetType().GetFields().SequenceEqual(obj.GetType().GetFields());

        public override string ToString()
        {
            var result = "";
            foreach (var field in GetType().GetFields())
            {
                result += $"{field.Name}{field.GetValue(null)}";
            }

            return result;
        }
    }
}
