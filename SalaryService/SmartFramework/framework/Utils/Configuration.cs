using System.Configuration;

namespace demo.framework.Utils {
    
    public class Configuration {
        //Settings
        private const string Timeout = "Timeout";
        private const string BaseUrl = "BaseUrl";
        private const string Browser = "Browser";
        private const string WebServiceUri = "webServiceURI";

        public static string GetParameterValue(string key) => ConfigurationManager.AppSettings.Get(key);

        private static void SetParameterValue(string key, string value) => ConfigurationManager.AppSettings.Set(key, value);

        //============================================== Settings ====================================================
        public static string GetTimeout() => GetParameterValue(Timeout);

        public static string GetWebServiceUri() => GetParameterValue(WebServiceUri);

        public static string GetBaseUrl() => GetParameterValue(BaseUrl);

        public static string GetBrowser() => GetParameterValue(Browser);
    }
}