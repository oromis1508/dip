using RxFramework.BaseEntities;

namespace FormsAndLocators
{
    public partial class FileSave : BaseForm
    {
        public void SaveFile(string filePath)
        {
            _tflFileName.SetText(filePath);
            _btnSave.Click();
        }
    }
}
