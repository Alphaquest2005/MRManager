using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckManager
{
    public partial class Voucher
    {
        public string Status
        {
            get
            {
                if (Prepared == null) return "UnPrepared";
                if(Prepared != null && Prepared.Signatures != Authorizeds.Count) return "UnAuthorized";
                
                if (Prepared != null && Prepared.Signatures == Authorizeds.Count && (Disbursed == null || Disbursed.PayeeId == 0)) return "UnDisbursed";
                if (Prepared != null && Prepared.Signatures == Authorizeds.Count && Disbursed != null && Disbursed.PayeeId != 0) return "Disbursed";
                if (Prepared != null && Prepared.Signatures == Authorizeds.Count) return "Authorized";
                return "Prepared";
            }
        }

        public string Action
        {
            get
            {
                switch (Status)
                {
                    case "UnPrepared":
                        return "Prepare";
                    case "UnAuthorized":
                        return "Authorize";
                    case "UnDisbursed":
                        return "Disburse";
                    default:
                        return "";

                }
            }
        }
    }
}
