using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class TextEditor
    {
        Button btn = new Button("/form[@title='new 2 - Notepad++']/element[@instance='0']", "textEditor");
    }
}
