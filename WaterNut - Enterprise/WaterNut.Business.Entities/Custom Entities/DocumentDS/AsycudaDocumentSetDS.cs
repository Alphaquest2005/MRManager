using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;


namespace DocumentDS.Business.Entities
{
    public enum DocumentSetApportionMethod
    {
        ByValue,
        Equal
    }

    public partial class AsycudaDocumentSet//: IHasEntryTimeStamp
    {



        [IgnoreDataMember]
        [NotMapped]
        public IEnumerable<xcuda_ASYCUDA> Documents
        {
            get
            {
                var alist = from a in xcuda_ASYCUDA_ExtendedProperties.Where(x => x.xcuda_ASYCUDA != null)
                            select a.xcuda_ASYCUDA;
                return new List<xcuda_ASYCUDA>(alist);
            }
        }

        
        private void UpdateDocuments()
        {
            var xlist = xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null);

            for (var i = 0; i < xlist.Count(); i++)
            {
                var xe = xlist.ElementAt(i);
                if (xe.AutoUpdate == null || xe.AutoUpdate == true)
                {
                    var cdoc = xe.xcuda_ASYCUDA;
                    //cdoc.SetupProperties();

                    cdoc.xcuda_Declarant.Number = Declarant_Reference_Number;


                    cdoc.xcuda_Identification.xcuda_Type.Type_of_declaration = Document_Type.Type_of_declaration;
                    cdoc.xcuda_Identification.xcuda_Type.Declaration_gen_procedure_code = Document_Type.Declaration_gen_procedure_code;
                    cdoc.xcuda_ASYCUDA_ExtendedProperties.Customs_ProcedureId = Customs_ProcedureId;
                    cdoc.xcuda_ASYCUDA_ExtendedProperties.BLNumber = BLNumber;

                    cdoc.xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSet.Description = Description;
                    cdoc.xcuda_General_information.xcuda_Country.Country_first_destination = Country_of_origin_code;
                    cdoc.xcuda_Valuation.xcuda_Gs_Invoice.Currency_code = Currency_Code;
                    cdoc.xcuda_Valuation.xcuda_Gs_Invoice.Currency_rate = Convert.ToSingle(Exchange_Rate);

                }
            }
        }
        
        public void SetFileNumber()
        {
            var xlist = xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null);
            var seed = Convert.ToInt32(StartingFileCount);
            for (var i = 0; i < xlist.Count(); i++)
            {
                var xe = xlist.ElementAt(i);
                xe.FileNumber = seed + i + 1;
              

            }
        }


      

        //decimal totalGrossWeight = 0;
        //public decimal TotalGrossWeight
        //{
        //    get
        //    {
        //        if (this.AsycudaDocumentSetId == 0) return 0;
        //        return Convert.ToDecimal(xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null).Sum(x => x.TotalGrossWeight));
        //    }
        //    set
        //    {
        //        totalGrossWeight = SetDocumentTotalGrossWeights(value);
        //    }
        //}

        //private decimal SetDocumentTotalGrossWeights(decimal value)
        //{
        //    // Set the Document Weights
        //    if (value == 0) return 0;
        //    decimal totweight = 0;
        //    decimal w = 0;
        //    var xlist = xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null && xe.AutoUpdate != false);

        //    for (var i = 0; i < xlist.Count(); i++)
        //    {
        //        var xe = xlist.ElementAt(i);

        //        //switch (WeightApportionMethod)
        //        //{
        //        //    case DocumentSetApportionMethod.ByValue:
        //        //        w = SetWeightByValue(value, xe.xcuda_ASYCUDA);
        //        //        break;
        //        //    case DocumentSetApportionMethod.Equal:
        //        //        w = value / xlist.Count();
        //        //        break;
        //        //    default:
        //        //        break;
        //        //}

        //        xe.TotalGrossWeight = w;
        //        totweight += w;
        //    }
        //    return totweight;
        //}

        //private decimal SetWeightByValue(decimal totalweight, xcuda_ASYCUDA doc)
        //{
        //    var totalsetvalue = this.EntryData.Sum(x => x.EntryDataDetails.Sum(z => z.Cost * z.Quantity));
        //    var rate = totalweight/totalsetvalue;
        //    var docval = doc.EntryData.Sum(x => x.EntryDataDetails.Sum(z => z.Quantity * z.Cost));
        //    return docval * rate;
        //}

    }
}
