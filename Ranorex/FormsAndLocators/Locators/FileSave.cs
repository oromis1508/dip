using RxFramework.Elements;

namespace FormsAndLocators
{
    public partial class FileSave
    {
        public FileSave() : base("/form[@title='Save As']", "Save file form")
        {
            _tflFileName = new Text($"{FormLocator}//text[@controlid='1148']", "Text field for file name");
            _btnSave = new Button($"{FormLocator}//button[@controlid='1']", "Button for file save");
        }

        private readonly Text _tflFileName;
        private readonly Button _btnSave;
    }
}
