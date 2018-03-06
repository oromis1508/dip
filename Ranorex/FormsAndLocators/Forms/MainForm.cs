using RxFramework;
using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class MainForm : BaseForm
    {
        public void PressButtons(string buttons)
        {
            foreach (var button in buttons)
            {
                new Button(string.Format(_buttonLocatorPattern, _buttonsAndLocators[button.ToString()]), $"Button {button}").Press();
            }
        }

        public string GetResult()
        {
            return _resultText.GetText().Replace("Display is ", "");
        }
    }
}
