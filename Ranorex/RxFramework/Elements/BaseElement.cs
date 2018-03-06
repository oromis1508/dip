using System.Collections.Generic;
using System.Configuration;
using Ranorex;

namespace RxFramework.Elements
{
    public abstract class BaseElement
    {
        protected string Locator;
        protected string Name;

        private Dictionary<HotKeys, string> hotKeys = new Dictionary<HotKeys, string>
        {
            {HotKeys.CtrlA,"{ControlKey DOWN}{aKey}{ControlKey UP}" },
            { HotKeys.CtrlC,"{ControlKey DOWN}{cKey}{ControlKey UP}"}
        };
        protected BaseElement(string locator, string name)
        {
            Locator = locator;
            Name = name;
        }

        public T GetElement<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {Name} with locator {Locator}");
            return Host.Local.FindSingle<T>(Locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWait"])));
        }

        public bool IsExist<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {Name} with locator {Locator}");
            return Host.Local.TryFindSingle(Locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWaitIsExist"])), out T el);
        }

        public void PressKeys<T>(string keys) where T : Adapter
        {
            Logger.Instance.Info($"Pressing keys {keys} for element {Name}");
            GetElement<T>().PressKeys(keys);
        }
        public void PressKeys<T>(HotKeys keys) where T : Adapter
        {
            PressKeys<T>(hotKeys[keys]);
        }
    }
}
