using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataAccessLayer
{
    public partial class TransactionsView
    {
        public string TransactionNumber
        {
            get
            {
                BarCodes.UPCA.cUPCA barcode = new BarCodes.UPCA.cUPCA();
                string txnnumber = TransactionId.ToString().PadLeft(11, '0');
                string finaltxn = txnnumber + barcode.GetCheckSum(txnnumber).ToString();
                return finaltxn;
            }
        }
    }
}
