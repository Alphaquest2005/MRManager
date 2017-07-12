using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.Common.UI;
using OversShortQS.Client.Entities;
using OversShortQS.Client.Repositories;
using SimpleMvvmToolkit;

using WaterNut.QuerySpace.OversShortQS.ViewModels;
using System.Windows;



namespace WaterNut.QuerySpace.OversShortQS.ViewModels
{
    public class OversShortsModel : OversShortEXViewModel_AutoGen
    {
          private static readonly OversShortsModel instance;
          static OversShortsModel()
        {
            instance = new OversShortsModel();

        }

          public static OversShortsModel Instance
        {
            get { return instance; }
        }

        private bool _viewDocData = false;

        public bool ViewDocData
        {
            get { return _viewDocData; }
            set
            {
                _viewDocData = value;
                base.FilterData();

            }
        }
        private bool _applyEX9Bucket = false;
        public bool ApplyEX9Bucket
        {
            get { return _applyEX9Bucket; }

            set
            {
                _applyEX9Bucket = value;
                if (_applyEX9Bucket == true)
                {
                   BreakOnMonthYear = false;
                   NotifyPropertyChanged(x => this.BreakOnMonthYear);
                }
            }
        }

        public bool OverWriteExisting { get; set; }

        public  bool BreakOnMonthYear { get; set; }

        private string referenceNumberFilter;
        public string ReferenceNumberFilter
        {
            get { return referenceNumberFilter;  }
            set
            {
                referenceNumberFilter = value;
                FilterData();
                NotifyPropertyChanged(x => this.ReferenceNumberFilter);
            }
        }

        private string cNumberFilter;
        public string CNumberFilter
        {
            get { return cNumberFilter; }
            set
            {
                cNumberFilter = value;
                FilterData();
                NotifyPropertyChanged(x => this.CNumberFilter);
            }
        }

        public override void FilterData()
        {
            var res = GetAutoPropertyFilterString();
            if (!string.IsNullOrEmpty(ReferenceNumberFilter))
            {
                res.Append(string.Format(" && OverShortSuggestedDocuments.ReferenceNumber.Contains(\"{0}\")",ReferenceNumberFilter));
            }

            if (!string.IsNullOrEmpty(ReferenceNumberFilter))
            {
                res.Append(string.Format(" && OverShortSuggestedDocuments.CNumber.Contains(\"{0}\")", CNumberFilter));
            }
            FilterData(res);
        }

        internal async Task Import()
        {
            if (CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx == null)
            {
                MessageBox.Show("Please Select a Document Set before Importing.");
                return;
            }
            Microsoft.Win32.OpenFileDialog od = new Microsoft.Win32.OpenFileDialog();
            od.Title = "Import Sales";
            od.DefaultExt = ".csv";
            od.Filter = "CSV Files (.csv)|*.csv";
            od.Multiselect = true;
            Nullable<bool> result = od.ShowDialog();
            if (result == true)
            {
                foreach (string f in od.FileNames)
                {
                    await OversShortEXRepository.Instance.Import(f, "OversShort", CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId, OverWriteExisting).ConfigureAwait(false);
                }
            }
            MessageBox.Show("Complete");
            //ViewAll();
        }

        internal async Task CreateOSEntries()
        {

            if (CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx == null)
            {
                MessageBox.Show("Please Select a Asycuda Document Set before proceding");
                return;
            }

            var selOS = SelectedOversShorts.ToList();

            if (selOS.Any() == false)
            {
                MessageBox.Show("Please Select Overs/Shorts before proceding");
                return;
            }

            await OversShortEXRepository.Instance.CreateOversOps(SelectedOversShorts.Select(x => x.OversShortsId),
                CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId).ConfigureAwait(false);

            await
                OversShortEXRepository.Instance.CreateShortsEx9(SelectedOversShorts.Select(x => x.OversShortsId),
                    CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId, BreakOnMonthYear,
                    ApplyEX9Bucket).ConfigureAwait(false);

            StatusModel.StopStatusUpdate();

            MessageBus.Default.BeginNotify(CoreEntities.MessageToken.AsycudaDocumentsChanged, this, new NotificationEventArgs(QuerySpace.CoreEntities.MessageToken.AsycudaDocumentsChanged));
            MessageBus.Default.BeginNotify(CoreEntities.MessageToken.AsycudaDocumentSetExsChanged, this, new NotificationEventArgs(QuerySpace.CoreEntities.MessageToken.AsycudaDocumentSetExsChanged));


            MessageBox.Show("Complete");
        }

        internal async Task SaveCNumber(string p)
        {
            await OversShortEXRepository.Instance.SaveCNumber(SelectedOversShorts.Select(x => x.OversShortsId), p).ConfigureAwait(false);

            MessageBus.Default.BeginNotify(MessageToken.OversShortsChanged, this,
             new NotificationEventArgs(MessageToken.OversShortsChanged));
        }

        internal async Task AutoMatch()
        {
            await OversShortEXRepository.Instance.AutoMatch(SelectedOversShorts.Select(x => x.OversShortsId)).ConfigureAwait(false);
        }

        internal async Task RemoveSelectedOverShorts()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete all Selected Items?",
              "Delete selected Items", MessageBoxButton.YesNo);
            List<OversShortEX> lst = null;
            if (res == MessageBoxResult.Yes)

                lst = Instance.SelectedOversShorts.ToList();
            if (lst == null) return;

            await OversShortEXRepository.Instance.RemoveSelectedOverShorts(SelectedOversShorts.Select(x => x.OversShortsId)).ConfigureAwait(false);

            MessageBus.Default.BeginNotify(MessageToken.OversShortsChanged, this,
              new NotificationEventArgs(MessageToken.OversShortsChanged));
            MessageBus.Default.BeginNotify(MessageToken.SelectedOversShortsChanged, this,
                new NotificationEventArgs(MessageToken.OversShortsChanged));
        }

        internal async Task SaveReferenceNumber(string p)
        {
            await OversShortEXRepository.Instance.SaveReferenceNumber(SelectedOversShorts.Select(x => x.OversShortsId), p).ConfigureAwait(false);
           
            MessageBus.Default.BeginNotify(MessageToken.OversShortsChanged, this,
                 new NotificationEventArgs(MessageToken.OversShortsChanged));
        }

        internal async Task MatchEntries()
        {
            await OversShortEXRepository.Instance.MatchEntries(SelectedOversShorts.Select(x => x.OversShortsId)).ConfigureAwait(false);
        }
    }
}