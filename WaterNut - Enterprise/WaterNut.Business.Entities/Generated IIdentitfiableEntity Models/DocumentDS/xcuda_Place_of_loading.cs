﻿// <autogenerated>
//   This file was generated by T4 code generator AllBusinessModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using CoreEntities.Business.Entities;
using Core.Common.Data.Contracts;
using System;

namespace DocumentDS.Business.Entities
{
    public partial class xcuda_Place_of_loading: IIdentifiableEntity
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
                return this.Place_of_loading_Id.ToString();  // this.Place_of_loading_Id == null?"0":
            }
            set
            {
                this.Place_of_loading_Id = Convert.ToInt32(value);
            }
        }



         #endregion
    }
   
}
		