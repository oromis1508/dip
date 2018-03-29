using Ranorex;
using RxFramework.Utils;
using Text = RxFramework.Elements.Text;

namespace RxFramework.BaseEntities
{
    public abstract class BaseForm
    {
        protected string FormLocator;
        protected string FormName;

        protected BaseForm(string formLocator, string formName)
        {
            FormLocator = formLocator;
            FormName = formName;
            IsFormExist();
        }

        public bool IsFormExist()
        {
            var isForm = new Text(FormLocator, FormName).IsExist<Form>();
            if (isForm)
            {
                Logger.Instance.Info($"Form {FormName} was opened");
            }
            return isForm;
        }
    }
}
