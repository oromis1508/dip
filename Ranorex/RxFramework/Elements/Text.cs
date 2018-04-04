namespace RxFramework.Elements
{
    public class Text : BaseElement
    {
        public Text(string locator, string name) : base(locator, name)
        {
        }

        public string GetText() => GetElement<Ranorex.Text>().TextValue;

        public string TextViaHotKeys => GetTextViaHotKeys<Ranorex.Text>();

        public void SetText(string text) => SetText<Ranorex.Text>(text);
    }
}
