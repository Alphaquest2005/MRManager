﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using CoreEntities.Client.DTO;
//using WaterNut.Client.DTO;

using System;

namespace EntryDataQS.Client.DTO
{
    public partial class ContainerEx
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
               return this.Container_Id.ToString(); // this.Container_Id == null?"0":                        
            }
            set
            {
                this.Container_Id = Convert.ToInt32(value);
            }
        }



         #endregion
    }
   
}
		