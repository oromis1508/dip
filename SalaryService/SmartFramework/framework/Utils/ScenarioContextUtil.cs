using TechTalk.SpecFlow;

namespace demo.framework.Utils
{
    public static class ScenarioContextUtil
    {
        public static void UpdateContext(string key, object newValue)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                ScenarioContext.Current.Remove(key);
            }
            ScenarioContext.Current.Add(key, newValue);
        }
    }
}
