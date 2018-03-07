namespace RxFramework.Elements
{
    public class Button : BaseElement
    {
        public Button(string locator, string name) : base(locator, name)
        {
        }

        public void Click() => Click<Ranorex.Button>();
    }
}
