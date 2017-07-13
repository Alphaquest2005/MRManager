﻿

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AllocationDS.Business.Entities
{
    public partial class xcuda_PreviousItem
    {

        [IgnoreDataMember]
        [NotMapped]
        public string DutyFreePaid
        {
            get
            {
                if (xcuda_Item == null) return null;
                if (xcuda_Item.AsycudaDocument == null) return null;
                if (xcuda_Item.AsycudaDocument.Type_of_declaration == null) return null;
                return xcuda_Item.AsycudaDocument.Type_of_declaration == "EX" ? "Duty Free" : "Duty Paid";
            }
        }




    }
}

