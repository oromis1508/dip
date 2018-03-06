using System.Collections.Generic;
using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class MainForm
    {
        private Text _resultText = new Text("/winapp[@packagename='Microsoft.WindowsCalculator']/text[@automationid='CalculatorResults']", "result text");
        private string _buttonLocatorPattern = "/winapp[@packagename='Microsoft.WindowsCalculator']/?/?/button[@automationid='{0}']";
        private Dictionary<string, string> _buttonsAndLocators = new Dictionary<string, string>
        {
            {"0","num0Button"},
            {"1","num1Button"},
            {"2","num2Button"},
            {"3","num3Button"},
            {"4","num4Button"},
            {"5","num5Button"},
            {"6","num6Button"},
            {"7","num7Button"},
            {"8","num8Button"},
            {"9","num9Button"},
            {"+","plusButton"},
            {"-","minusButton"},
            {"=","equalButton"},
        };
    }
}
