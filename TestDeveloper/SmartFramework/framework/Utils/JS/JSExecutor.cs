using System.Drawing;
using OpenQA.Selenium;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.JS
{
    public class JSExecutor : BaseEntity
    {
        public static void JavaScriptClickByCoordinates(Point coords)
        {
            var script = $"document.elementFromPoint ({coords.X}, {coords.Y}).click ();";
            Log.Info("== Javascript click by coordinates ==");
            (Browser.Instance as IJavaScriptExecutor)?.ExecuteScript(script);
        }
    }
}
