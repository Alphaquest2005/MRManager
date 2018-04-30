using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RMSDataAccessLayer
{
   public partial class TransactionEntryBase:IDataErrorInfo
    {
        partial void CustomStartup()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && (e.PropertyName == "Quantity" || e.PropertyName == "Price"))
            {
                NotifyPropertyChanged("Amount");
                NotifyPropertyChanged("SalesTax");

            }
        }

        [NotMapped]
        [IgnoreDataMember]
       public string TransactionEntryNumber
       {
           get
           {
               string value = TransactionId.ToString();
               //if (value != null && value.ToString().Replace("0", "") != "")
               //    value = value.ToString().Remove(0, value.ToString().IndexOfAny("123456789".ToCharArray()));
               if (EntryNumber == null)
               {
                   return value;
               }
               else
               {
                   return value + "-" + EntryNumber.ToString();
               }
               
            }
           set { NotifyPropertyChanged("TransactionEntryNumber"); }
       }
        [NotMapped]
        [IgnoreDataMember]
        public virtual double Amount
        {
            get { return ((Quantity * Price * (1 + SalesTaxPercent) - Discount.GetValueOrDefault())); }
        }
        decimal salestax = 0;
        //if tax is not manually set return calculated tax
        [NotMapped]
        [IgnoreDataMember]
        public double SalesTax
        {
            get
            {

                if (Amount != 0 && SalesTaxPercent != 0 && Taxable == true)
                {
                    // divide for VAT inclusive Multiply for VAT Exclusive
                    return Amount - (Amount * (1 + SalesTaxPercent));
                }
                else
                {
                     return 0;
                }
               
              
            }
            set
            {
                SalesTaxPercent = value;
            }
        }

        #region "Validation"
        Dictionary<string, string> m_validationErrors = new Dictionary<string, string>();
       

       public void AddError(string col, string msg)
        {
            if (!m_validationErrors.ContainsKey(col))
            {
                m_validationErrors.Add(col, msg);
            }
        }
        public void RemoveError(string col)
        {
            if (m_validationErrors.ContainsKey(col))
            {
                m_validationErrors.Remove(col);
            }
        }
        public string ValidationErrorMsg { get; set; }
        public virtual string Error
        {
            get
            {
                if (m_validationErrors.Count > 0)
                {
                    return ValidationErrorMsg;
                }
                else
                {
                    return null;
                }
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (m_validationErrors.ContainsKey(columnName))
                {
                    return m_validationErrors[columnName];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion



    }
}
