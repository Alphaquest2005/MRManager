//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WaterNutDB
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class xcuda_ASYCUDA_ExtendedProperties
    {
        public xcuda_ASYCUDA_ExtendedProperties()
        {
            this.AutoUpdate = true;
        }
    
        public int ASYCUDA_Id { get; set; }
        public Nullable<int> AsycudaDocumentSetId { get; set; }
        public Nullable<int> FileNumber { get; set; }
        public Nullable<bool> IsManuallyAssessed { get; set; }
        public string CNumber { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string ReferenceNumber { get; set; }
        public Nullable<bool> AutoUpdate { get; set; }
        public Nullable<int> Customs_ProcedureId { get; set; }
        public Nullable<int> Document_Type_Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> ExportTemplate_Id { get; set; }
        public string BLNumber { get; set; }
        public Nullable<System.DateTime> EffectiveRegistrationDate { get; set; }
        public Nullable<int> WeightApportionMethod { get; set; }
        public Nullable<int> FreightApportionMethod { get; set; }
        public Nullable<bool> DoNotAllocate { get; set; }
    
        public virtual AsycudaDocumentSet AsycudaDocumentSet { get; set; }
        public virtual xcuda_ASYCUDA xcuda_ASYCUDA { get; set; }
        public virtual Customs_Procedure Customs_Procedure { get; set; }
        public virtual Document_Type Document_Type { get; set; }
        public virtual ExportTemplate ExportTemplate { get; set; }
    }
}