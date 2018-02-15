using System.Configuration;

namespace demo.framework.Utils {
    
    public class Configuration {
        //Settings
        private const string Timeout = "Timeout";
        private const string BaseUrl = "BaseUrl";
        private const string Browser = "Browser";


        public static string GetParameterValue(string key) {
            return ConfigurationManager.AppSettings.Get(key);
        }

        private static void SetParameterValue(string key, string value) {
            ConfigurationManager.AppSettings.Set(key, value);
        }

        //============================================== Settings ====================================================
        public static string GetTimeout() {
            return GetParameterValue(Timeout);
        }

        public static string GetBaseUrl()
        {
            return GetParameterValue(BaseUrl);
        }

        public static string GetBrowser()
        {
            return GetParameterValue(Browser);
        }

    }
}