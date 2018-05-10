using smart.framework.BaseEntities;
using smart.framework.Utils.Support;
using TestProject.Enums;

namespace TestProject.Models
{
    public class AddedTest : BaseModel
    {
        public string TestName { get; set; }
        public string Status { get; set; }
        public string TestMethod { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Environment { get; set; }
        public string Browser { get; set; }

        public static AddedTest GetRandomModel() => new AddedTest
        {
            TestName = Randoms.GetRandomString(10),
            Status = Randoms.GetEnumRandomValue(typeof(PortalStatus)).ToString(),
            TestMethod = Randoms.GetRandomString(10),
            StartTime = Randoms.GetRandomDate("yyyy-mm-dd hh:mm:ss"),
            EndTime = Randoms.GetRandomDate("yyyy-mm-dd hh:mm:ss"),
            Environment = Randoms.GetRandomString(10),
            Browser = Randoms.GetRandomString(10)
        };
    }
}
