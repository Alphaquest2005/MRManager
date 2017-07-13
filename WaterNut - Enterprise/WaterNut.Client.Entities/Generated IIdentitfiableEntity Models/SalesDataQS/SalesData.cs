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
    public partial class SalesData
    {
       
       #region IIdentifiable Entities
        public override string EntityId
        {
            get
            {
                return this.EntryDataId.ToString();//this.EntryDataId == null?"0":			
            }
            set
            {
                this.EntryDataId = Convert.ToString(value);
            }
        }
        public string AsycudaDocumentEntityName
        {
            get
            {
 
                if(this.AsycudaDocument == null) UpdateAsycudaDocument();
                return this.AsycudaDocument == null ? "" : this.AsycudaDocument.EntityName;
            }
            set
            {
                                if (string.IsNullOrEmpty(value)) return;
                string[] vals = value.Split(',');
               
                    using (AsycudaDocumentClient ctx = new AsycudaDocumentClient())
                    {
                        var dto = ctx.GetAsycudaDocuments().Result.AsEnumerable().FirstOrDefault(x => x.EntityName == value);
                        

                        if ( dto == null)
                        {
                            this.AsycudaDocument = (AsycudaDocument)new AsycudaDocument().CreateEntityFromString(value);
							
							this.AsycudaDocumentId = Convert.ToInt32(this.AsycudaDocument.ASYCUDA_Id);
                            this.TrackingState=TrackableEntities.TrackingState.Modified;
                           NotifyPropertyChanged("AddAsycudaDocument");
                        }
                        else
                        {
                            var obj = new AsycudaDocument(dto);
                           if (this.AsycudaDocument == null || this.AsycudaDocument.EntityId != obj.EntityId) this.AsycudaDocument = obj;
                           
                        }
                         


                    }
            
            }

      }



         #endregion
    }
   
}
		