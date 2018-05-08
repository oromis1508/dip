using smart.framework.BaseEntities;

namespace TestProject.Models
{
    public class test : BaseModel
    {
        public string duration { get; set; }
        public string method { get; set; }
        public string name { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string status { get; set; }
    }
}
