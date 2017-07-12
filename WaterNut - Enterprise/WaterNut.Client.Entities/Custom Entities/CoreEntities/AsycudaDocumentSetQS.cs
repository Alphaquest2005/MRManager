using EntryDataQS.Client.Entities;
using EntryDataQS.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;



namespace CoreEntities.Client.Entities
{
    public enum DocumentSetApportionMethod
        {
            ByValue,
            Equal
        }

    public partial class AsycudaDocumentSetEx//: IHasEntryTimeStamp
    {

        //public ObservableCollection<EntryDataEx> DocumentEntryData
        //{
        //    get
        //    {
        //        using (var ctx = new EntryDataExClient())
        //        {
        //            var alist = ctx.GetEntryDataExByExpressionNav("All", new Dictionary<string, string>() { { "AsycudaDocuments", string.Format("AsycudaDocumentSetId == {0}",AsycudaDocumentSetId) } }).Result.Select(x => new EntryDataEx(x));
        //            return new ObservableCollection<EntryDataEx>(alist);
        //        }

        //    }
        //}

        //public ObservableCollection<EntryDataEx> EntryData
        //{
        //    get
        //    {
        //        using (EntryDataExClient ctx = new EntryDataExClient())
        //        {
        //            var res = ctx.GetEntryDataExByAsycudaDocumentSetId(this.AsycudaDocumentSetId.ToString()).Result.Select(x => new EntryDataEx(x));
        //            if (res != null)
        //            {
        //                return new ObservableCollection<EntryDataEx>(res);
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }

        //    }
        //}


        public Nullable<double> TotalFreight
        {
            get { return this.asycudadocumentsetex.TotalFreight; }
            set
            {
                if (value == this.asycudadocumentsetex.TotalFreight) return;
                this.asycudadocumentsetex.TotalFreight = value;
                if (this.DTO.TrackingState == TrackableEntities.TrackingState.Unchanged) this.DTO.TrackingState = TrackableEntities.TrackingState.Modified;
                NotifyPropertyChanged("TotalFreight");
            }
        }
    }
}
