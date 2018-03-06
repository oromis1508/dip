namespace RxFramework.Elements
{
    public class Button : BaseElement
    {
        public Button(string locator, string name) : base(locator, name)
        {
        }

        public void Click()
        {
            GetElement<Ranorex.Button>().Click();
            Logger.Instance.Info($"Click on element {Name} with locator {Locator}");
        }

        public void Press()
        {
            GetElement<Ranorex.Button>().Press();
            Logger.Instance.Info($"Click on element {Name} with locator {Locator}");
        }
    }
}
