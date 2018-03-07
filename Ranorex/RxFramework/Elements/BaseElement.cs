using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using Ranorex;
using RxFramework.Utils;
using Duration = Ranorex.Duration;

namespace RxFramework.Elements
{
    public abstract class BaseElement
    {
        protected string Locator;
        protected string Name;

        private readonly Dictionary<HotKeys, string> _hotKeys = new Dictionary<HotKeys, string>
        {
            {HotKeys.CtrlA,"{ControlKey DOWN}{aKey}{ControlKey UP}" },
            { HotKeys.CtrlC,"{ControlKey DOWN}{cKey}{ControlKey UP}"}
        };

        protected BaseElement(string locator, string name)
        {
            Locator = locator;
            Name = name;
        }

        protected T GetElement<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {Name} with locator {Locator}");
            return Host.Local.FindSingle<T>(Locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWait"])));
        }

        internal bool IsExist<T>() where T : Adapter
        {
            Logger.Instance.Info($"looking for element {Name} with locator {Locator}");
            return Host.Local.TryFindSingle(Locator, Duration.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["appWaitIsExist"])), out T el);
        }

        protected void PressKeys<T>(string keys) where T : Adapter
        {
            Logger.Instance.Info($"Pressing keys {keys} for element {Name}");
            GetElement<T>().PressKeys(keys);
        }
        protected void PressKeys<T>(HotKeys keys) where T : Adapter
        {
            PressKeys<T>(_hotKeys[keys]);
        }

        protected void Click<T>() where T : Adapter
        {
            GetElement<T>().Click();
            Logger.Instance.Info($"Click on element {Name} with locator {Locator}");
        }

        public void SetText<T>(string text) where T : Adapter
        {
            GetElement<T>().PressKeys(text);
            Logger.Instance.Info($"Set text {text} into element {Name}");
        }

        public string GetTextViaHotKeys<T>() where T : Adapter
        {
            PressKeys<T>(HotKeys.CtrlA);
            PressKeys<T>(HotKeys.CtrlC);
            return Clipboard.GetText();
        }
    }
}
