using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CheckManager
{
    public partial class Cheque
    {
        public Voucher Voucher { get; set; }

        public List<Distribution> Distribution { get; set; }
    }
}