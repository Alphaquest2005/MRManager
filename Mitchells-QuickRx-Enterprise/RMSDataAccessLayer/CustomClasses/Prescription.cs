using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMSDataAccessLayer
{
    public partial class Prescription: ISearchItem
    {
     

       public new string SearchCriteria
       {
           get
           {
               return base.SearchCriteria;
                    //return String.Format("{0}|{1}|{2}",base.SearchCriteria ?? "",
                    //                         Patient != null? Patient.SearchCriteria:"",
                    //                         Doctor != null? Doctor.SearchCriteria:"")
           }
       }


        //public new string Status
        // {
        //     get
        //     {
        //         if (DoctorId == null)
        //             return "Please select Doctor";
        //         if (PatientId == null)
        //             return "Please select Patient";

        //         return base.Status;
        //     }
        //     set
        //     {
        //         base.Status = value;
        //         NotifyPropertyChanged("Status");
        //     }
        // }
    }
}
