using Ranorex;

namespace RxFramework.Elements
{
    public class UnknownElement : BaseElement
    {
        public UnknownElement(string locator, string name) : base(locator, name)
        {
        }

        public void SetText(string text) => SetText<Unknown>(text);

        public string Text => GetTextViaHotKeys<Unknown>();
    }
}
