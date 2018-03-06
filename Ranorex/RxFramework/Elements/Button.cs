namespace RxFramework.Elements
{
    public class Button : BaseElement
    {
        private string _locator;
        private string _name;
        public Button(string locator, string name) : base(locator, name)
        {
            _locator = locator;
            _name = name;
        }

        public void Click()
        {
            GetElement<Ranorex.Button>().Click();
            Logger.Instance.Info($"Click on element {_name} with locator {_locator}");
        }

        public void Press()
        {
            GetElement<Ranorex.Button>().Press();
            Logger.Instance.Info($"Click on element {_name} with locator {_locator}");
        }
    }
}
