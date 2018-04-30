using RMSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;



namespace SalesRegion
{

    /// <summary>
    /// Interaction logic for SalesTaskPad.xaml
    /// </summary>
    public partial class SalesTaskPad : UserControl
    {
        private static readonly SalesTaskPad _instance;
        static SalesTaskPad()
        {
            _instance = new SalesTaskPad();
        }

        public static SalesTaskPad Instance
        {
            get { return _instance; }
        }
        public SalesTaskPad()
        {
            InitializeComponent();
            this.DataContextChanged += SalesTaskPad_DataContextChanged;
           

        }

        void SalesLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReBindItemEditor();

        }


        void SalesTaskPad_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SearchBox.Focus();
            ReBindTranStatusTxt();
        }

        private void ReBindTranStatusTxt()
        {
            Binding myBinding = new Binding("Status");
            myBinding.Source = SalesVM.Instance.TransactionData;
            ((FrameworkElement)SalesPad.FindName("TransStatusTxt")).SetBinding(TextBlock.TextProperty, myBinding);
        }


        private void ReBindItemEditor()
        {

            ReBindTranStatusTxt();
            Binding myBinding = new Binding("SelectedItem");
            myBinding.Source = SalesView.SalesLst;
            myBinding.Mode = BindingMode.OneWay;

            ((FrameworkElement)SalesPad.FindName("ItemEditor")).SetBinding(ContentControl.ContentProperty, myBinding);
        }





        public TransactionBase Transaction
        {
            get {return SalesVM.Instance.TransactionData;}
        }


        private void SalesPad_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

            var uie = e.OriginalSource as Control;

            if (uie == null) uie = SearchBox as Control;
            if (uie.Name == "PART_FilterBox")
            {
                if (e.Key == Key.Enter)//pkey == Key.Enter &&
                {
                    SearchBox.RaiseFilterEvent();
                    if (SearchListCtl.Items.Count == 1)
                        SearchListCtl.SelectedIndex = 0;
                    (uie as TextBox).Text = "";
                    //e.Handled = true;
                    MoveToNextControl(uie);
                }

                return;
            }

            if (e.Key == Key.Enter)
            {
                // e.Handled = true;
                MoveToNextControl(uie);
            }
            pkey = e.Key;
        }

        public void MoveToNextControl(object sender)
        {
            UIElement uie = sender as UIElement;
            uie.MoveFocus(
            new TraversalRequest(
            FocusNavigationDirection.Next));
        }

        private void FilterControl_Direction_1(object sender, DirectionEventArgs e)
        {
            if (e.Direction == DirectionEnum.Down)
            {
                SearchListCtl.SelectedIndex += 1;
            }
            if (e.Direction == DirectionEnum.Up && SearchListCtl.SelectedIndex > -1)
            {
                SearchListCtl.SelectedIndex -= 1;
            }
            if (e.Direction == DirectionEnum.Right)
            {

                MoveToNextControl(sender);
            }
        }
         Key pkey;
        private void SearchBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                DeleteTransactionEntry();
            }

            if (SearchListCtl.Items.Count == 1)
                SearchListCtl.SelectedIndex = 0;

            if (e.Key == Key.Enter)
            {
                //if (SearchListCtl.SelectedItem == null)
                //{
                //    SalesVM.GetSearchResults(SearchBox.FilterText);
                //    return;
                //}
                //else
                //{
                //select the item
                LocalProcesItem(SearchListCtl.SelectedItem);
                MoveToNextControl(sender);
                //}


            }
           

            pkey = e.Key;

        }

        private void DeleteTransactionEntry()
        {

            if (SalesVM.Instance.TransactionData != null && SalesVM.Instance.CurrentTransactionEntry != null)
            {
                SalesVM.Instance.DeleteTransactionEntry<TransactionEntryBase>(SalesVM.Instance.CurrentTransactionEntry);
            }
        }

        private void LocalProcesItem(object itm)
        {

            if (itm == null) return;

            if (edititem == true)
            {
                ItemEditor.Content = itm;
                edititem = false;
                return;
            }


            if (itm.GetType() == typeof (RMSDataAccessLayer.SearchItem))
            {
                switch (((ISearchItem) itm).DisplayName)
                {

                    case "Add Patient":


                        Patient p = SalesVM.Instance.CreateNewPatient();
                        //   SalesVM.rms.Persons.AddObject(p);
                        ItemEditor.Content = p;

                        break;
                    case "Add Doctor":
                        Doctor d = SalesVM.Instance.CreateNewDoctor();
                        //  SalesVM.rms.Persons.AddObject(d);
                        ItemEditor.Content = d;
                        break;
                   
                    default:
                        ItemEditor.Content = ((RMSDataAccessLayer.SearchItem) itm).SearchObject;
                        break;
                }
            }



            else
            {
                if (showPatientPrescriptions == true)
                {
                    if (itm.GetType() == typeof (RMSDataAccessLayer.Patient))
                    {
                        ItemEditor.Content = SalesVM.Instance.GetPatientTransactionList(itm as Patient);
                        showPatientPrescriptions = false;
                    }

                    if (itm.GetType() == typeof (RMSDataAccessLayer.Doctor))
                    {
                        ItemEditor.Content = SalesVM.Instance.GetDoctorTransactionList(itm as Doctor);
                        showPatientPrescriptions = false;
                    }
                }
                else
                {
                    SalesVM.ProcessSearchListItem(itm);
                    MoveToNextControl(ItemEditor);
                }
            }
        }


        SalesView _SalesView;
        public SalesView SalesView
        {
            get
            {
                return _SalesView;
            }
            set
            {
                _SalesView = value;
                SalesView.SalesLst.SelectionChanged += SalesLst_SelectionChanged;
            }
        }

        SalesVM _SalesVM;
        public SalesVM SalesVM
        {
            get
            {
                return _SalesVM;
            }
            set
            {
                _SalesVM = value;
                SearchListCtl.ItemsSource = SalesVM.SearchList;
                // SalesVM.TransactionData.PropertyChanged += TransactionData_PropertyChanged;


            }
        }






        private void SearchBox_Filter_1(object sender, FilterEventArgs e)
        {
            if (e.FilterText != "" && e.IsFilterApplied == false)
            {
                this.SalesVM.GetSearchResults(SearchBox.FilterText);
                ShowSearchList();

            }
            if (e.FilterText == "")
            {
                HideSearchList();


            }

        }

        private void HideSearchList()
        {
            SearchListCtl.Visibility = System.Windows.Visibility.Collapsed;
            SearchListCtl.Focusable = false;
            SearchListCtl.SelectedIndex = -1;
        }

        private void ShowSearchList()
        {
            SearchListCtl.Visibility = System.Windows.Visibility.Visible;
            SearchListCtl.Focusable = true;
            if (SearchListCtl.Items.Count == 1)
                SearchListCtl.SelectedIndex = 0;
        }



        private void TextBox_GotKeyboardFocus_1(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        public void PrintBtn_Click_1(object sender, RoutedEventArgs e)
        {


            if (ReceiptCol.Width == new GridLength(0))
            {
                // unhide the colums to print
                ReceiptCol.Width = new GridLength(400);
                ReceiptGrd.UpdateLayout();
                Print();
                ReceiptCol.Width = new GridLength(0);
                //hide it back
            }
            else
            {
                Print();
            }

        }


        private void Print()
        {
            if (typeof(QuickPrescription).IsInstanceOfType(SalesVM.TransactionData) || typeof(Prescription).IsInstanceOfType(SalesVM.TransactionData))
            {

                ListView plist = Common.FindChild<ListView>(ReceiptGrd, "PrescriptionEntriesRptLst");// (ListView)this.FindName("PrescriptionEntriesRptLst");
                if (plist != null)
                    PrintListItems(plist);
            }

            else
            {
                SalesVM.Print(ref ReceiptGrd);
            }
        }

        private void PrintListItems(ListView plist)
        {
            dynamic lst;
            if (plist.SelectedItems.Count == 0)
            {
                lst = plist.Items;
            }
            else
            {
                lst = plist.SelectedItems;
            }

            foreach (var item in lst)
            {
                FrameworkElement fi = (FrameworkElement)plist.ItemContainerGenerator.ContainerFromItem(item);

                SalesVM.Print(ref fi);
            }
        }



        private void NewTransaction(object sender, RoutedEventArgs e)
        {
           // SalesVM.NewTransaction();
            
        }
         bool focusswitch;
        private void TransStatusTxt_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (focusswitch == true)
            {
                ReBindTranStatusTxt();
            }

            focusswitch = !focusswitch;

        }

        private void ListView_SourceUpdated_1(object sender, DataTransferEventArgs e)
        {

        }
        int TemplateHeight = 192;
        private void PrescriptionEntriesRptLst_LayoutUpdated(object sender, EventArgs e)
        {
            if (SalesVM.TransactionData != null && SalesView.SalesPadState == SalesPadTransState.Receipt)
                ((FrameworkElement)this.FindName("ReceiptGrd")).Height = TemplateHeight * SalesVM.TransactionData.TransactionEntries.Count;
        }

        private void SearchListCtl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            LocalProcesItem(SearchListCtl.SelectedItem);
            MoveToNextControl(sender);
        }

        private void SearchListCtl_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

            LocalProcesItem(SearchListCtl.SelectedItem);
            MoveToNextControl(sender);
            SearchBox.FilterText = "";
            HideSearchList();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SearchListCtl.Visibility == System.Windows.Visibility.Visible)
            {
                HideSearchList();
            }
            else
            {
                ShowSearchList();
            }
        }

        private void NextEntry_Click(object sender, RoutedEventArgs e)
        {
            SalesView.pkey = Key.Down;
            SalesView.padPos = SalesRegion.SalesView.PadPosition.Middle;
            SalesView.GoToNextTransactionEntry();

        }

        private void DeleteTranBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteTransactionEntry();
        }

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalesView.GotoPreviousSalesStep();
        }

        private void NextBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalesView.GotoNextSalesStep(Key.Right);
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SalesVM.GoToTransaction((e.AddedItems[0] as TransactionBase).TransactionId);
                ReBindItemEditor();
            }
        }

        private void SavePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (ItemEditor.Content is Patient)
            {

                SalesVM.Instance.SavePatient( (Patient) ItemEditor.Content);
            }
            SalesVM.Instance.SaveTransaction();
            //if (SalesVM.Instance.SaveTransaction() == false)
            //{
            //    ReBindItemEditor();
            //    return;
            //}
            LocalProcesItem(ItemEditor.Content);
            ReBindItemEditor();
        }
        bool edititem = false;
        private void EditItemTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            edititem = true;
        }

        bool showPatientPrescriptions = false;
        private void PatientPrescriptionBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            showPatientPrescriptions = true;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemEditor.Content = null;
            ReBindItemEditor();
            // SalesVM.DiscardChanges();
        }

        private void PostToQB_Click(object sender, RoutedEventArgs e)
        {
            SalesVM.PostQBSale();
            NextBtn_MouseLeftButtonDown(sender, null);
        }



        private void PrintBtn_RightClick(object sender, MouseButtonEventArgs e)
        {
            ListView plist = Common.FindChild<ListView>(ReceiptGrd, "PrescriptionEntriesRptLst");// (ListView)this.FindName("PrescriptionEntriesRptLst");
            if (plist != null)
            {
                plist.SelectAll();
                PrintBtn_Click_1(sender, e);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView plist = Common.FindChild<ListView>(ReceiptGrd, "PrescriptionEntriesRptLst");// (ListView)this.FindName("PrescriptionEntriesRptLst");
            if (plist != null)
            {
                plist.SelectedItems.Clear();
            }
        }

        private void PharmacistCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    Cashier c = (e.AddedItems[0] as Cashier);
            //    if (c != null && SalesVM.TransactionData != null)
            //        SalesVM.TransactionData.PharmacistId = c.Id;
            //}
            // SalesVM.OnStaticPropertyChanged("PharmacistId");
        }
















    }


}
