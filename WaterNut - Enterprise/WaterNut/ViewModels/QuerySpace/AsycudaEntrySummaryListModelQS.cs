using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using CoreEntities.Client.Entities;
using CoreEntities.Client.Repositories;
using PreviousDocumentQS.Client.Entities;
using SimpleMvvmToolkit;


namespace WaterNut.QuerySpace.CoreEntities.ViewModels
{
    public partial class AsycudaDocumentItemsModel : AsycudaDocumentItemViewModel_AutoGen 
	{
        private static readonly AsycudaDocumentItemsModel instance;
        static AsycudaDocumentItemsModel()
        {
            instance = new AsycudaDocumentItemsModel()
            {
                ViewCurrentAsycudaDocument = true,
                StartRegistrationDateFilter = DateTime.MinValue,
                EndRegistrationDateFilter = DateTime.MinValue
            };
        }

        public static AsycudaDocumentItemsModel Instance
        {
            get { return instance; }
        }

        private AsycudaDocumentItemsModel()
        {
            RegisterToReceiveMessages<AsycudaDocument>(MessageToken.CurrentAsycudaDocumentChanged, OnCurrentAsycudaDocumentChanged2);
            RegisterToReceiveMessages<PreviousItemsEx>(PreviousDocumentQS.MessageToken.CurrentPreviousItemsExChanged, OnCurrentPreviousItemsExChanged);
        }

        private void OnCurrentPreviousItemsExChanged(object sender, NotificationEventArgs<PreviousItemsEx> e)
        {
            if (e.Data != null)
            {
                CNumberFilter = e.Data.Prev_reg_nbr;
                LineNumberFilter = Convert.ToInt16(e.Data.Current_item_number);
                ViewCurrentDocumentOnly = false;
            }
        }

        private void OnCurrentAsycudaDocumentChanged2(object sender, NotificationEventArgs<AsycudaDocument> e)
        {
           // if (e.Data != null) CNumberFilter = e.Data.CNumber;
        }

        private bool _viewCurrentDocumentOnly = true;
        public bool ViewCurrentDocumentOnly
        {
            get
            {
                return _viewCurrentDocumentOnly;
            }
            set
            {
                _viewCurrentDocumentOnly = value;
                NotifyPropertyChanged(x => ViewCurrentDocumentOnly);
                FilterData();
            }
        }


        bool _viewIM4 = true;
        public bool ViewIM4
        {
            get
            {
                return _viewIM4;
            }
            set
            {
                _viewIM4 = value;
                NotifyPropertyChanged(x => ViewIM4);
                FilterData();
            }
        }

        bool _viewIM7 = true;
        public bool ViewIM7
        {
            get
            {
                return _viewIM7;
            }
            set
            {
                _viewIM7 = value;
                NotifyPropertyChanged(x => ViewIM7);
                FilterData();

            }
        }

        bool _viewEx9 = true;
        public bool ViewEx9
        {
            get
            {
                return _viewEx9;
            }
            set
            {
                _viewEx9 = value;
                NotifyPropertyChanged(x => ViewEx9);
                FilterData();
            }
        }

        bool _viewIM9 = true;
        public bool ViewIM9
        {
            get
            {
                return _viewIM9;
            }
            set
            {
                _viewIM9 = value;
                NotifyPropertyChanged(x => ViewIM9);
                FilterData();
            }
        }

        bool _viewInvalidHSCode = false;
        public bool ViewInvalidHSCode
        {
            get
            {
                return _viewInvalidHSCode;
            }
            set
            {
                _viewInvalidHSCode = value;
                NotifyPropertyChanged(x => ViewInvalidHSCode);
                FilterData();
            }
        }

        public override void FilterData()
        {
            var res = GetAutoPropertyFilterString();
            var navexp = new StringBuilder();
            if (ViewIM7 == true)
            {
                navexp.Append("|| DocumentType == \"IM7\"");
            }
            if (ViewEx9 == true)
            {
              navexp.Append("|| DocumentType == \"EX9\"");
            }
            if (ViewIM9 == true)
            {
                navexp.Append("|| DocumentType == \"IM9\"");
            }
            if (ViewIM4 == true)
            {
              navexp.Append("|| DocumentType == \"IM4\"");
            }

            if (ViewCurrentDocumentOnly)
            {
                if (BaseViewModel.Instance.CurrentAsycudaDocument != null)
                {
                     navexp.Clear();
                     navexp.Append(string.Format("&& ASYCUDA_Id == {0}",
                     BaseViewModel.Instance.CurrentAsycudaDocument.ASYCUDA_Id));
                }
                //else
                //{
                //    MessageBox.Show("Please Select Document");
                //}
            }

            if (ViewInvalidHSCode)
            {
                    res.Append(string.Format("|| InvalidHSCode == {0}",
                    ViewInvalidHSCode));
            }
            if(navexp.Length > 0)
                vloader.SetNavigationExpression("AsycudaDocument",  navexp.ToString().Substring(2) );
            
            FilterData(res);

        }

        internal async Task RemoveItem()
        {
            if (SelectedAsycudaDocumentItems.Count > 0)
            {
                var res = MessageBox.Show("Are you sure you want to remove these selected items?", "Delete Items",
                    MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    await AsycudaDocumentItemRepository.Instance.RemoveSelectedItems(SelectedAsycudaDocumentItems.ToList()).ConfigureAwait(false);
                   

                    MessageBus.Default.BeginNotify(MessageToken.AsycudaDocumentItemsChanged, null,
                                    new NotificationEventArgs(MessageToken.AsycudaDocumentItemsChanged));
                   
                }
            }
            
        }

        internal async Task RemoveSelectedItems(List<AsycudaDocumentItem> list)
        {
              var res = MessageBox.Show("Do you want to remove the selected items?", "Confirm Delete", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                await AsycudaDocumentItemRepository.Instance.RemoveSelectedItems(list).ConfigureAwait(false);

                MessageBus.Default.BeginNotify(MessageToken.AsycudaDocumentsChanged, null,
                            new NotificationEventArgs(MessageToken.AsycudaDocumentsChanged));

                MessageBus.Default.BeginNotify(MessageToken.AsycudaDocumentItemsChanged, null,
                                new NotificationEventArgs(MessageToken.AsycudaDocumentItemsChanged));

                MessageBus.Default.BeginNotify(QuerySpace.PreviousDocumentQS.MessageToken.PreviousDocumentItemsChanged, null,
                                new NotificationEventArgs(QuerySpace.PreviousDocumentQS.MessageToken.PreviousDocumentItemsChanged));

                MessageBox.Show("Complete");
            }
        }

        internal async Task SaveDocumentItem(AsycudaDocumentItem asycudaDocumentItem)
        {
            await
                AsycudaDocumentItemRepository.Instance.SaveAsycudaDocumentItem(new AsycudaDocumentItem(asycudaDocumentItem.ChangeTracker.GetChanges().FirstOrDefault()))
                    .ConfigureAwait(false);
            MessageBox.Show("Complete");
        }
    }
}