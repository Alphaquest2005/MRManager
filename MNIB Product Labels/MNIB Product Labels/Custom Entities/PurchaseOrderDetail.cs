using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BarCodes;


namespace MNIB_Product_Labels
{
    public partial class PurchaseOrderDetail: INotifyPropertyChanged
    { 
        public string Barcode
        {
            get
            {
                var tz = new TransPreZeroConverter();
                string val = null;
                val = Convert.ToString(tz.Convert(PurchaseOrderNo.Replace("-","") + "0"+ LineNumber.ToString(), typeof(string), null, null));
                return val;
            }
        }

        

        private int labelQty = 1;

        public int LabelQty
        {
            get { return labelQty; }
            set
            {
                labelQty = value;
                OnPropertyChanged("LabelQty");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
