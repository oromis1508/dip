using RxFramework.BaseEntities;

namespace FormsAndLocators
{
    public partial class TextEditor : BaseForm
    {
        public void PrintText(string text) => _textEditor.SetText(text);

        public string ActiveTabName => _activeTab.TabName;

        public string DisplayedText => _textEditor.Text;
    }
}
