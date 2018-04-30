using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Data;

namespace RMSDataAccessLayer
{
    public partial class PrescriptionEntry : ISearchItem
    {
        partial void CustomStartup2()
        {
            PropertyChanged += PrescriptionEntry_PropertyChanged;
            
        }

        private void PrescriptionEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Quantity)) NotifyPropertyChanged(nameof(RepeatInfo));
            if (e.PropertyName == nameof(Repeat))
            {
               
                NotifyPropertyChanged(nameof(RepeatInfo));
            }
            if (e.PropertyName == nameof(RepeatQuantity)) NotifyPropertyChanged(nameof(RepeatInfo));
        }

        public int Total => Convert.ToInt32((Repeat + 1) * (RepeatQuantity??0));//Convert.ToInt32(Quantity)

        public int TotalTaken
        {
            get
            {
                
                var lst = new List<PrescriptionEntry>();
                if (this.Transaction?.ParentTransaction != null)
                {
                    lst.AddRange(this.Transaction.ParentTransaction.Transactions.SelectMany(x => x.TransactionEntries).Where(x => x.TransactionEntryId < TransactionEntryId).Where(z => z.TransactionEntryItem.ItemId == TransactionEntryItem.ItemId).OfType<PrescriptionEntry>());
                        lst.AddRange(this.Transaction.ParentTransaction.TransactionEntries.Where(z => z.TransactionEntryItem.ItemId == TransactionEntryItem.ItemId).OfType<PrescriptionEntry>());
                }
                
                return lst.Sum(x => Convert.ToInt32(x.Quantity));
            }
        }

        public int Remaining => Total - TotalTaken;

        public string RepeatInfo
        {
            get
            {
                
                var r = 0;
                if (Quantity == 0) return "";
                var repeatQuantity = RepeatQuantity.HasValue == false ? Convert.ToInt32(Quantity) : RepeatQuantity.Value;
                if (repeatQuantity == 0) return "";
                var repeat = Math.DivRem(Remaining - Convert.ToInt32(Quantity), repeatQuantity, out r);//

                var rstr = "";
                if (r > 0) rstr = $"Bal:{r} |";

                var repeatstr = "";
                if (repeat > 0) repeatstr = $"Repeat: {repeat} X {RepeatQuantity ?? Convert.ToInt32(Quantity)}  ";

                var totalstr = (rstr + repeatstr);

                return string.IsNullOrEmpty(totalstr)?totalstr:totalstr.Substring(0,totalstr.Length - 2);
            }
            set
            {
                NotifyPropertyChanged();
            }
        }

        public int Remainder => Convert.ToInt32(RepeatQuantity ?? Convert.ToInt32(Quantity)) - Convert.ToInt32(Quantity);

        #region ISearchItem Members
        [NotMapped]
        [IgnoreDataMember]
        public string SearchCriteria
        {
            get
            {
                return DisplayName + "|";
            }
            set
            {
               
            }
        }
        [NotMapped]
        [IgnoreDataMember]
        public string DisplayName
        {
            get { return ""; } //this.Item.DisplayName; 
            }
        [NotMapped]
        [IgnoreDataMember]
        public string Key
        {
            get { return TransactionEntryNumber; }
        }

        #endregion
    }
}
