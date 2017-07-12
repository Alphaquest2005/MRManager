﻿// <autogenerated>
//   This file was generated by T4 code generator AllClientModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using CoreEntities.Client.Entities;
using CoreEntities.Client.Services;
//using WaterNut.Client.Services;
using SalesDataQS.Client.Services;

using System;

namespace SalesDataQS.Client.Entities
{
    public partial class AsycudaDocumentSetEntryData
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
                return this.Id.ToString();//this.Id == null?"0":			
            }
            set
            {
                this.Id = Convert.ToInt32(value);
            }
        }
        public string SalesDataEntityName
        {
            get
            {
                return this.SalesData == null ? "" : this.SalesData.EntityName;
            }
            set
            {
                                if (string.IsNullOrEmpty(value)) return;
                string[] vals = value.Split(',');
               
                    using (SalesDataClient ctx = new SalesDataClient())
                    {
                        var dto = ctx.GetSalesDatas().Result.AsEnumerable().FirstOrDefault(x => x.EntityName == value);
                        

                        if ( dto == null)
                        {
                            this.SalesData = (SalesData)new SalesData().CreateEntityFromString(value);
							
							this.Id = Convert.ToInt32(this.SalesData.EntryDataId);
                            this.TrackingState=TrackableEntities.TrackingState.Modified;
                           NotifyPropertyChanged("AddSalesData");
                        }
                        else
                        {
                            var obj = new SalesData(dto);
                           if (this.SalesData == null || this.SalesData.EntityId != obj.EntityId) this.SalesData = obj;
                           
                        }
                         


                    }
            
            }

      }



         #endregion
    }
   
}
		