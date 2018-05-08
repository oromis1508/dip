using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using smart.framework.Utils.Html;
using smart.framework.Utils.TestUtils;

namespace smart.framework.Utils.Services
{
    public class TestRailUtil
    {
        private static readonly string TestRailUrl = Configuration.TestRailUrl;
        private static readonly NetworkCredential RailCreds = new NetworkCredential(Configuration.TestRailLogin, Configuration.TestRailPassword);
        private const string TestRailPostContentType = "application/json";

        public static void AddResultForCase(string runId, string caseId, TestRailResultCodes status, string comment="")
        {
            var requestString = $"{TestRailUrl}?/api/v2/add_result_for_case/{runId}/{caseId}";

            var postData = new Dictionary<string, object>
            {
                { "status_id", (int)status },
                { "comment", comment }
            };
            var jsonString = JsonConvert.SerializeObject(postData, Formatting.Indented);
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, jsonString);

            var data = Encoding.ASCII.GetBytes($"{{\"status_id\": {(int)status},\"comment\": \"{comment}\"}}");

            Requests.PostRequest(requestString, TestRailPostContentType, data, RailCreds);
        }

        public static string AddRun(string projectId)
        {
            var requestString = $"{TestRailUrl}?/api/v2/add_run/{projectId}";
            var response = Requests.PostRequest(requestString, TestRailPostContentType, Encoding.ASCII.GetBytes(""), RailCreds);
            var streamReader = new StreamReader(response.GetResponseStream());
            dynamic result = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            return (string)result.id;

        }

        public static string GetImageStringForAddToComment(string imageUri) => $"![]({imageUri})";
    }
}
