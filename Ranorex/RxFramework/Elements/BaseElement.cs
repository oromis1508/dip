using System.Collections.Generic;
using System.Configuration;
using Ranorex;

namespace RxFramework.Elements
{
    public abstract class BaseElement
    {
        private string _locator;
        private string _name;

        private Dictionary<HotKeys, string> hotKeys = new Dictionary<HotKeys, string>
        {
            {HotKeys.CtrlA,"{ControlKey DOWN}{aKey}{ControlKey UP}" },
            { HotKeys.CtrlC,"{ControlKey DOWN}{cKey}{ControlKey UP}"}
        };
        protected BaseElement(string locator, string name)
        {
            _locator = locator;
            _name = name;
        }

        public T GetElement<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {_name} with locator {_locator}");
            return Host.Local.FindSingle<T>(_locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWait"])));
        }

        public bool IsExist<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {_name} with locator {_locator}");
            return Host.Local.TryFindSingle(_locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWaitIsExist"])), out T el);
        }

        public void PressKeys<T>(string keys) where T : Adapter
        {
            Logger.Instance.Info($"Pressing keys {keys} for element {_name}");
            GetElement<T>().PressKeys(keys);
        }
        public void PressKeys<T>(HotKeys keys) where T : Adapter
        {
            PressKeys<T>(hotKeys[keys]);
        }
    }
}
