﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using CoreEntities.Client.DTO;
//using WaterNut.Client.DTO;

using System;

namespace SalesDataQS.Client.DTO
{
    public partial class AsycudaDocumentSetEntryData
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
               return this.Id.ToString(); // this.Id == null?"0":                        
            }
            set
            {
                this.Id = Convert.ToInt32(value);
            }
        }



         #endregion
    }
   
}
		