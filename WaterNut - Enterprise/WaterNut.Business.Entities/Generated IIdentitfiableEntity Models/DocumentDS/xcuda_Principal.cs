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
    public partial class xcuda_Principal: IIdentifiableEntity
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
                return this.Principal_Id.ToString();  // this.Principal_Id == null?"0":
            }
            set
            {
                this.Principal_Id = Convert.ToInt32(value);
            }
        }



         #endregion
    }
   
}
		