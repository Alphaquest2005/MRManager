using RMSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Common.Core.Logging;
using log4netWrapper;
using SimpleMvvmToolkit;

namespace SalesRegion
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
            //DataContext = SalesVM.Instance;
           // SalesVM = SalesVM.Instance;
           // SalesPad.SalesVM = SalesVM.Instance;
            if (SalesPad != null) SalesPad.SalesView = this;


            if (SalesLst != null)
            {
                var notifyCollectionChanged = (INotifyCollectionChanged)SalesLst.Items;
                if (notifyCollectionChanged != null)
                    notifyCollectionChanged.CollectionChanged += SalesView_CollectionChanged;


                SalesLst.SelectionChanged += SalesLst_SelectionChanged;
                //  SalesPad.LayoutUpdated += SalesPad_LayoutUpdated;
                //SalesLst.SizeChanged += SalesLst_SizeChanged;
            
                SalesLst.LayoutUpdated += SalesLst_LayoutUpdated;
            }
            // salesVM.ParentCanvas = ppcan;
           
            
            HideReceipt();
            ShowTransaction();

            SetUpSalesPad();
           

        }


        private void SetUpSalesPad()
        {
            if (SalesLst != null)
            {
                Canvas.SetTop(SalesLst, 0);
                if (SalesPad != null)
                {
                    SalesPad.Margin = SalesPadMargin;
                    Canvas.SetTop(SalesPad, 0);
                }
                this.SalesLst.SelectedIndex = 0;
            }
            SetSalesPadtoSelectedItem();
        }

       



        void SalesView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {

            if (e != null && e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    if (SalesLst != null)
                    {
                        SalesLst.ScrollIntoView(e.NewItems[0]);
                        SalesLst.SelectedItem = e.NewItems[0];
                    }
                }
                SalesVM.Instance.TransactionData.CurrentTransactionEntry = (PrescriptionEntry)SalesLst.SelectedItem;
            }
            else if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                SetUpSalesPad();
               // SalesLst_SelectionChanged(null, null);
            }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

       public enum PadPosition
        {
            Above,
            Middle,
            Below,
        }
        public PadPosition padPos = PadPosition.Middle;
        void SalesLst_LayoutUpdated(object sender, EventArgs e)
        {
            SalesPad_LayoutUpdated(null, null);
        }
        Thickness SalesPadMargin = new Thickness(0, 0, 0, 0);
        void SalesPad_LayoutUpdated(object sender, EventArgs e)
        {
           
                if ( SalesLst.SelectedIndex != -1)
                {
                    Canvas.SetTop(SalesLst, 0);
                    Canvas.SetTop(SalesPad, 0);
                    SetSalesPadtoSelectedItem();
                    pkey = Key.None;
                    return;
                }
        }

        public void MoveSalesPadDown()
        {
            try
            {
            SalesLst.UpdateLayout();
            SalesPad.Margin = SalesPadMargin;


            if (padPos == PadPosition.Above && SalesLst.SelectedIndex != -1)
            {
                Canvas.SetTop(SalesLst, 0);
                SetSalesPadtoSelectedItem();
                padPos = PadPosition.Middle;
            }

            if (padPos == PadPosition.Middle)
            {
                if (SalesLst.SelectedIndex == -1)
                {
                    Canvas.SetTop(SalesLst, 0);
                    // SalesPad.Margin = SalesPadMargin;
                    Canvas.SetTop(SalesPad, SalesLst.ActualHeight + 8);
                    SetSalesPadtoSelectedItem();
                    if (SalesLst.SelectedIndex == -1) padPos = PadPosition.Below;
                }
                else
                {
                    Canvas.SetTop(SalesLst, 0);
                    SetSalesPadtoSelectedItem();
                }
              
            }

            if (SalesLst.SelectedIndex != -1 && padPos == PadPosition.Below)
            {
                Canvas.SetTop(SalesLst, 0);
                Canvas.SetTop(SalesPad, 0);
                SalesPad.Margin = SalesPadMargin;
                SetSalesPadtoSelectedItem();
                if (SalesLst.SelectedIndex == 0) padPos = PadPosition.Middle;
               // padPos = PadPosition.Below;
            }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        
        FrameworkElement f;
        void SalesLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            
            if (SalesVM.Instance.TransactionData == null) return;
            if (SalesLst.SelectedItem == null) return;

            SalesVM.Instance.TransactionData.CurrentTransactionEntry = (PrescriptionEntry)SalesLst.SelectedItem;
            if (padPos != PadPosition.Middle) return;
            SalesPad_LayoutUpdated(null, null);
            
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private void SetSalesPadtoSelectedItem()
        {
            try
            {

            SalesLst.UpdateLayout();
            f = (FrameworkElement)SalesLst.ItemContainerGenerator.ContainerFromItem(SalesLst.SelectedItem);
            if (f != null )//&& f.Parent != null
            {
                var transformToAncestor = ((FrameworkElement)f).TransformToAncestor(ppcan);
                if (transformToAncestor != null)
                {
                    Rect r = transformToAncestor.TransformBounds(new Rect(0, 0, 0, 0));
                    SalesPad.Margin = new Thickness(r.Left, r.Top, r.Right, r.Bottom);
                }
            }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

       public SalesPadTransState SalesPadState = SalesPadTransState.Transaction;
       public Key pkey;
        public void ppcan_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            var uie = e.OriginalSource as Control;

            if (uie == null) uie = SalesPad.SearchBox as Control;

            if ( uie.Name == "PART_FilterBox" && ((uie as TextBox).Text != "") && (e.Key == Key.Up || e.Key == Key.Down))
            {
               
                return;
            }

            if (uie.Name == "PrintBtn" && e.Key == Key.Enter)
            {
                if (pkey == Key.Enter) GotoNextSalesStep(e.Key);
                e.Handled = true;
                pkey = e.Key;
                return;
            }

           
            //if (e.KeyboardDevice != null && (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.P))
            //{
            //    if (SalesPadState != SalesPadTransState.Receipt)
            //    {
            //        HideCurrentSalesPadState();
            //        // unhide the colums to print
            //        ShowReceipt();
            //        SalesVM.Instance.Print(ref SalesPad.ReceiptGrd);
            //        e.Handled = true;
            //        //hide it back
            //    }
            //    else
            //    {
            //        SalesVM.Instance.Print(ref SalesPad.ReceiptGrd);
            //        e.Handled = true;
            //    }
               
            //}

            pkey = e.Key;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private void HideCurrentSalesPadState()
        {
            if (SalesPadState == SalesPadTransState.Transaction) HideTransaction();
            
            if (SalesPadState == SalesPadTransState.Receipt) HideReceipt();
        }


        public void GotoPreviousSalesStep()
        {
            PreviousPharmacySaleSteps();
        }

        private void PreviousPharmacySaleSteps()
        {
            try
            {
            SalesLst.SelectedIndex = 0;
            if (SalesPadState == SalesPadTransState.Receipt)//SalesPad.TotalsCol.Width == new GridLength(0) && SalesPad.PaymentCol.Width == new GridLength(0)
            {
                HideReceipt();
                ShowTransaction();
                return;
            }
            if (SalesPadState == SalesPadTransState.Transaction)
            {
                SalesVM.Instance.GoToPreviousTransaction();
                
            }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void HideReceipt()
        {
            if (SalesPad != null)
            {
                SalesPad.ReceiptCol.Width = new GridLength(0);
                SalesPad.ReceiptGrd.Visibility = System.Windows.Visibility.Hidden;
                SalesPad.PrintCol.Width = new GridLength(0);
                SalesPad.PrintGrd.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        public void ShowReceipt()
        {
            if (SalesPad != null)
            {
                SalesPad.ReceiptCol.Width = new GridLength(400);
                SalesPad.ReceiptGrd.Visibility = System.Windows.Visibility.Visible;
                SalesPad.PrintCol.Width = new GridLength(200);
                SalesPad.PrintGrd.Visibility = System.Windows.Visibility.Visible;
                SalesTaskPad.Instance.MoveToNextControl(SalesPad.PrintGrd);
            }
            SalesPadState = SalesPadTransState.Receipt;
            
            
        }

        public void ShowTransaction()
        {
            if (SalesPad != null)
            {
                SalesPad.EntryCol.Width = new GridLength(408);
                SalesPad.TransactionGrd.Visibility = System.Windows.Visibility.Visible;
                SalesPad.TotalsCol.Width = new GridLength(200);
                SalesPad.TotalsGrd.Visibility = System.Windows.Visibility.Visible;
                SalesTaskPad.Instance.MoveToNextControl(SalesPad.TransactionGrd);
            }
            SalesPadState = SalesPadTransState.Transaction;

        }

        public void GotoNextSalesStep(Key e)
        {
            SalesLst.SelectedIndex = 0;
            NextPhamacySalesSteps(e);
        }

        private void NextPhamacySalesSteps(Key e)
        {
            try
            {
            if (SalesPadState == SalesPadTransState.Receipt && (e == Key.Right || e == Key.Enter))
            {
                HideReceipt();
                SalesVM.Instance.CloseTransaction();
                ShowTransaction();
                return;
            }


            if (SalesPadState == SalesPadTransState.Transaction)
            {
                if (SalesVM.Instance.TransactionData != null && SalesVM.Instance.TransactionData.GetType() == typeof(Prescription))
                {
                    var p = SalesVM.Instance.TransactionData as Prescription;
                    if (p.Doctor == null)
                    {
                        MessageBox.Show("Please Select a doctor");
                        return;
                    }
                    if (p.Patient == null)
                    {
                        MessageBox.Show("Please Select a Patient");
                        return;
                    }
                }

                HideTransaction();
                if (SalesVM.Instance.TransactionData != null && SalesVM.Instance.TransactionData.Status == null)
                {
                    if (!SalesVM.Instance.SaveTransaction())
                    {
                         MessageBox.Show("Saving Transaction Failed Try again!");
                        //return;
                    };
                        //ShowReceipt();
                    }
                ShowReceipt();
               }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void HideTransaction()
        {
            if (SalesPad != null)
            {
                SalesPad.EntryCol.Width = new GridLength(0);
                SalesPad.TransactionGrd.Visibility = System.Windows.Visibility.Hidden;
                SalesPad.TotalsCol.Width = new GridLength(0);
                SalesPad.TotalsGrd.Visibility = System.Windows.Visibility.Hidden;
                SalesTaskPad.Instance.MoveToNextControl(SalesPad.ReceiptGrd);
            }
        }


        public void GoToNextTransactionEntry()
        {
            try
            {
            if (SalesVM.Instance.TransactionData == null) return;
            
            if(!SalesVM.Instance.SaveTransaction()) return;

                if (SalesLst.SelectedIndex == SalesLst.Items.Count - 1)
            {
                

               padPos = PadPosition.Below;
               SalesVM.Instance.TransactionData.CurrentTransactionEntry = null;
               SalesLst.SelectedItem = null;
               Canvas.SetTop(SalesLst, 0);
               SalesLst.UpdateLayout();
               Canvas.SetTop(SalesPad, 0);
               SalesPad.Margin = SalesPadMargin;
               Canvas.SetTop(SalesPad, SalesLst.ActualHeight + 8);
               SetSalesPadtoSelectedItem();
            }
            else
            {
                SalesLst.SelectedIndex += 1; // selected index don't change when more than list

            }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private void GoToPreviousTransactionEntry(KeyEventArgs e)
        {
            e.Handled = true;
            if (SalesLst.SelectedIndex != -1)
            {
                SalesLst.SelectedIndex -= 1;
            }
            else
            {
                SalesLst.SelectedIndex += 1;
            }
        }


        private void EditDoctorTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((SalesVM.Instance.TransactionData as Prescription).Doctor != null)
                SalesPad.ItemEditor.Content = (SalesVM.Instance.TransactionData as Prescription).Doctor;
        }

        private void EditPatientTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((SalesVM.Instance.TransactionData as Prescription).Patient != null)
                SalesPad.ItemEditor.Content = (SalesVM.Instance.TransactionData as Prescription).Patient;
        }

        private void DeleteTranBtn_Click(object sender, RoutedEventArgs e)
        {
            SalesVM.Instance.DeleteCurrentTransaction();
        }

        private void AutoRepeatBtn_Click(object sender, RoutedEventArgs e)
        {
            SalesVM.Instance.AutoRepeat();
            SetUpSalesPad();
        }


        public void Notify(string token, object sender, NotificationEventArgs e)
        {
            MessageBus.Default.Notify(token, sender, e);
        }

        private void GoToMaster(object sender, RoutedEventArgs e)
        {
           var trans = SalesVM.Instance.TransactionData as Prescription;
           if(trans.ParentTransactionId.GetValueOrDefault() > 0) SalesVM.Instance.GoToTransaction(trans.ParentTransactionId.GetValueOrDefault());
        }
    }
}