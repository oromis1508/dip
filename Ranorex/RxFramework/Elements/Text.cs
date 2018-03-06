using System.Windows;

namespace RxFramework.Elements
{
    public class Text : BaseElement
    {
        private string _locator;
        private string _name;

        public Text(string locator, string name) : base(locator, name)
        {
            _locator = locator;
            _name = name;
        }

        public string GetText()
        {
            return GetElement<Ranorex.Text>().TextValue;
        }

        public string GetTextViaHotKeys()
        {
            PressKeys<Ranorex.Text>(HotKeys.CtrlA);
            PressKeys<Ranorex.Text>(HotKeys.CtrlC);
            return Clipboard.GetText();
        }
    }
}
