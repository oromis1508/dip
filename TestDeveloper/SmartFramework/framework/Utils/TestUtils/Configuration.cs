using System;
using System.Configuration;

namespace smart.framework.Utils.TestUtils {
    
    public class Configuration {

        public static string GetParameterValue(string key) => ConfigurationManager.AppSettings.Get(key);

        private static void SetParameterValue(string key, string value) => ConfigurationManager.AppSettings.Set(key, value);

        public static string Timeout => GetParameterValue("Timeout");

        public static string BaseUrl => GetParameterValue("BaseUrl");

        public static string Variant => GetParameterValue("Variant");

        public static string PortalLogin => GetParameterValue("PortalLogin");

        public static string PortalPassword => GetParameterValue("PortalPassword");

        public static string LocalBrowser => GetParameterValue("LocalBrowser");

        public static string OpenedProject => GetParameterValue("OpenedProject");
        
        public static string Browser => Environment.GetEnvironmentVariable("BrowserName");
        public static bool IsRemote => false;
    }
}