﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using CoreEntities.Client.DTO;
//using WaterNut.Client.DTO;

using System;

namespace CounterPointQS.Client.DTO
{
    public partial class CounterPointSalesDetails
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
               return this.INVNO.ToString(); // this.INVNO == null?"0":                        
            }
            set
            {
                this.INVNO = Convert.ToString(value);
            }
        }



         #endregion
    }
   
}
		