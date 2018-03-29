using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class IEWindow
    {
        public IEWindow() : base("/form[@processname='iexplore']", "Internet Explorer window")
        {
            _pageTagXpath = $"{FormLocator}//dom[@page='{{0}}']//{{1}}";
            _btnCloseApp = new Button($"{FormLocator}//button[@accessiblename='Close']", "Button to close IE window");
        }

        private readonly string _pageTagXpath;
        private readonly Button _btnCloseApp;
    }
}
