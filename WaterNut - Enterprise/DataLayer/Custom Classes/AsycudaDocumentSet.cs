using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace WaterNut.DataLayer
{
    public enum DocumentSetApportionMethod
        {
            ByValue,
            Equal
        }

    public partial class AsycudaDocumentSet: IHasEntryTimeStamp
    {

        

        public AsycudaDocumentSet()
        {
            xcuda_ASYCUDA_ExtendedProperties.AssociationChanged +=xcuda_ASYCUDA_ExtendedProperties_AssociationChanged;

        }

        private void xcuda_ASYCUDA_ExtendedProperties_AssociationChanged(object sender, CollectionChangeEventArgs e)
        {
          if (xcuda_ASYCUDA_ExtendedProperties != null)
            {
                SetFileNumber();
                OnPropertyChanged("AsycudaDocuments");
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
                   cdoc.SetupProperties();

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
            for (var i = 0; i < xlist.Count() ; i++)
            {
                var xe = xlist.ElementAt(i);
                xe.FileNumber = seed + i + 1;
                //if (xe.xcuda_ASYCUDAReference.IsLoaded == false)
                //{
                //    xe.xcuda_ASYCUDAReference.Load();
                //}
                //if (xe.xcuda_ASYCUDA.xcuda_DeclarantReference.IsLoaded == false && xe.xcuda_ASYCUDA.EntityState != System.Data.EntityState.Added)
                //    xe.xcuda_ASYCUDA.xcuda_DeclarantReference.Load();

                //if (xe.xcuda_ASYCUDA.xcuda_Declarant != null)
                //    xe.xcuda_ASYCUDA.xcuda_Declarant.Number = xe.xcuda_ASYCUDA.xcuda_Declarant.Number.Substring(0,xe.xcuda_ASYCUDA.xcuda_Declarant.Number.Length - 3) + "-F" + xe.FileNumber.ToString();

            }
        }

        public ObservableCollection<xcuda_ASYCUDA> Documents
        {
            get
            {
                var alist = from a in xcuda_ASYCUDA_ExtendedProperties.Where( x => x.xcuda_ASYCUDA != null)
                            select a.xcuda_ASYCUDA;
                return new ObservableCollection<xcuda_ASYCUDA>(alist);
            }
        }



       decimal totalGrossWeight = 0;
        public decimal TotalGrossWeight
        {
            get
            {
                return Convert.ToDecimal(xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null).Sum(x => x.TotalGrossWeight));
            }
            set
            {
                totalGrossWeight = SetDocumentTotalGrossWeights(value);
            }
        }

        private decimal SetDocumentTotalGrossWeights(decimal value)
        {
            // Set the Document Weights
            if (value == 0) return 0;
            decimal totweight = 0;
            decimal w = 0;
            var xlist = xcuda_ASYCUDA_ExtendedProperties.Where(xe => xe.xcuda_ASYCUDA != null && xe.AutoUpdate != false);

            for (var i = 0; i < xlist.Count(); i++)
            {
                var xe = xlist.ElementAt(i);

                //switch (WeightApportionMethod)
                //{
                //    case DocumentSetApportionMethod.ByValue:
                //        w = SetWeightByValue(value, xe.xcuda_ASYCUDA);
                //        break;
                //    case DocumentSetApportionMethod.Equal:
                //        w = value / xlist.Count();
                //        break;
                //    default:
                //        break;
                //}

                xe.TotalGrossWeight = w;
                totweight += w;
            }
            return totweight;
        }

       
       
    }
}
