using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using TrackableEntities.Client;

namespace RMSDataAccessLayer
{
    public class BaseEntity<T>:EntityBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>>
      _validationErrors = new Dictionary<string, ICollection<string>>();
        protected void ValidateModelProperty (object sender ,object value,[CallerMemberName] string propertyName = "null")
        {
            //if (_validationErrors.ContainsKey(propertyName))
            //    _validationErrors.Remove(propertyName);

            //PropertyInfo propertyInfo = sender.GetType().GetProperty(propertyName);
            //IList<string> validationErrors =
            //      (from validationAttribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>()
            //       where !validationAttribute.IsValid(value)
            //       select validationAttribute.FormatErrorMessage(string.Empty))
            //       .ToList();

            //_validationErrors.Add(propertyName, validationErrors);
            //RaiseErrorsChanged(propertyName);
       
            _validationErrors.Clear();
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(sender, null, null);
            if (!Validator.TryValidateObject(sender, validationContext, validationResults, true))
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string property = validationResult.MemberNames.ElementAt(0);
                    if (_validationErrors.ContainsKey(property))
                    {
                        _validationErrors[property].Add(validationResult.ErrorMessage);
                    }
                    else
                    {
                        _validationErrors.Add(property, new List<string> { validationResult.ErrorMessage });
                    }
                    RaiseErrorsChanged(property);
                }
            }

    }
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)
                || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }
        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors => (this._validationErrors.Count > 0);
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}