using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class TextEditor
    {
        public TextEditor() : base("/form[@processname='notepad++']", "Notepad text editor")
        {
            _textEditor = new UnknownElement($"{FormLocator}/element[@class='Scintilla']", FormName);
            _activeTab = new TabPage($"{FormLocator}//tabpage[selected='true']", "Active tab with editor");
        }

        private readonly UnknownElement _textEditor;
        private readonly TabPage _activeTab;
    }
}
