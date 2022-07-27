using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UI_Layer.ErrorNotification
{
    public class ErrorNotifier : INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errorsDict = new Dictionary<string, List<string>>();
        public IEnumerable GetErrors(string propertyName)
        {
            return errorsDict.GetValueOrDefault(propertyName, null);
        }
        public bool HasErrors => errorsDict.Any();
        public void AddError(string PropertyName, string ErrorMessage)
        {
            if (!errorsDict.ContainsKey(PropertyName))
            {
                errorsDict.Add(PropertyName, new List<string>());
            }
            errorsDict[PropertyName].Add(ErrorMessage);
            OnErrorChanged(PropertyName);
        }
        public void ClearError(string PropertyName)
        {
            errorsDict.Remove(PropertyName);
            OnErrorChanged(PropertyName);
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void OnErrorChanged(string PropertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(PropertyName));
        }
    }
}
