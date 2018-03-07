using Ranorex;

namespace RxFramework.Elements
{
    public class Scintilla : BaseElement
    {
        public Scintilla(string locator, string name) : base(locator, name)
        {
        }

        public void SetText(string text) => SetText<Unknown>(text);

        public string Text => GetTextViaHotKeys<Unknown>();
    }
}
