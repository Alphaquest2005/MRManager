using RMSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BarCodes;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using NSubstitute;
using SalesRegion.Messages;
using SimpleMvvmToolkit;

namespace SalesRegion
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        public SalesView(SalesVM salesVM)
        {
            InitializeComponent();
            DataContext = salesVM;
            salesvm = salesVM;
            SalesPad.SalesVM = salesVM;
            SalesPad.SalesView = this;
            
            
           
         
            ((INotifyCollectionChanged)SalesLst.Items).CollectionChanged += SalesView_CollectionChanged;

                      
            SalesLst.SelectionChanged += SalesLst_SelectionChanged;
            SalesLst.LayoutUpdated += SalesLst_LayoutUpdated;
                       
            HideReceipt();
            ShowTransaction();

            SetUpSalesPad();
            ObservableObject<object> viewRegionContext =
               RegionContext.GetObservableContext(this);
            viewRegionContext.PropertyChanged += this.ViewRegionContext_OnPropertyChangedEvent;

        }

        private void ViewRegionContext_OnPropertyChangedEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                var context = (ObservableObject<object>)sender;
                salesvm.TransactionData = (RMSDataAccessLayer.TransactionBase)context.Value;
            }
        }

        private void SetUpSalesPad()
        {
            Canvas.SetTop(SalesLst, 0);
            SalesPad.Margin = SalesPadMargin;
            Canvas.SetTop(SalesPad, 0);
            this.SalesLst.SelectedIndex = 0;
            SetSalesPadtoSelectedItem();
        }

        void salesVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SearchList")
            {
                SalesPad.SearchListCtl.ItemsSource = salesvm.SearchList;
            }
            if (e.PropertyName == "TransactionData")
            {
                SalesLst.SelectedIndex = 0;
                pkey = Key.Up;
            }
           
        }



        //void salesVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "TransactionData")
        //    {
        //        SalesPad.Transaction = salesvm.TransactionData;
        //    }
        //}

        //void SalesPad_TransactionChanged(object sender, EventArgs e)
        //{
        //    if (salesvm.TransactionData != SalesPad.Transaction)
        //    {
        //        salesvm.TransactionData = SalesPad.Transaction;
        //        SalesLst.SelectedIndex = 0;
        //    }
        //}


        void SalesLst_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetTop(SalesPad, SalesLst.ActualHeight + 8);
        }

     
        

        void SalesView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                SalesLst.ScrollIntoView(e.NewItems[0]);
                SalesLst.SelectedItem = e.NewItems[0];
                salesvm.CurrentTransactionEntry =(RMSDataAccessLayer.TransactionEntryBase) SalesLst.SelectedItem;
            }
            else if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                SetUpSalesPad();
               // SalesLst_SelectionChanged(null, null);
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
            //if (pkey != Key.Down && pkey != Key.Up && pkey != Key.None)
            //{
                if ( SalesLst.SelectedIndex != -1)
                {
                    Canvas.SetTop(SalesLst, 0);
                    Canvas.SetTop(SalesPad, 0);
                    SetSalesPadtoSelectedItem();
                    pkey = Key.None;
                    return;
                }
            //}
            //if (pkey == Key.Up)
            //{
            //    SalesPad.Margin = SalesPadMargin;
            //    SalesLst.UpdateLayout();

            //    if (padPos == PadPosition.Middle && SalesLst.SelectedIndex == -1)
            //    {

            //        Canvas.SetTop(SalesPad, 0);
            //        Canvas.SetTop(SalesLst, SalesPad.ActualHeight + 8);
            //        //SetSalesPadtoSelectedItem();
            //        if(SalesLst.SelectedIndex == -1) padPos = PadPosition.Above;
            //        //return;
            //    }
            //    if (padPos == PadPosition.Middle && SalesLst.SelectedIndex != -1)
            //    {
            //        Canvas.SetTop(SalesLst, 0);
            //        Canvas.SetTop(SalesPad, 0);
            //        SetSalesPadtoSelectedItem();
                    
            //    }

            //    if (SalesLst.SelectedIndex != -1 && padPos == PadPosition.Above)
            //    {
            //        // set the index to the last one and goto that one
            //        Canvas.SetTop(SalesLst, 0);
            //        //SalesLst.SelectedIndex = SalesLst.Items.Count - 1;
            //        SetSalesPadtoSelectedItem();
            //        padPos = PadPosition.Middle;
            //    }

            //    if (padPos == PadPosition.Below)
            //    {
            //        // set the index to the last one and goto that one

            //        Canvas.SetTop(SalesLst, 0);
            //        Canvas.SetTop(SalesPad, 0);
            //        SetSalesPadtoSelectedItem();
            //        padPos = PadPosition.Middle;
            //        SalesLst.SelectedIndex = SalesLst.Items.Count - 1;
                    
                    
                   
            //    }
            //    pkey = Key.None;
            //}
            //if (pkey == Key.Down)
            //{
            //    MoveSalesPadDown();
            //    pkey = Key.None;
            //}
            

           // pkey = Key.None;
            //}


        }

        public void MoveSalesPadDown()
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

   
       public SalesVM salesvm;
        FrameworkElement f;
        void SalesLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Rect r = ((FrameworkElement)SalesLst.ItemContainerGenerator.ContainerFromItem(SalesLst.SelectedItem)).TransformToAncestor(ppcan).TransformBounds(new Rect(0,0,0,0));
            //SalesPad.Margin = new Thickness(r.Left, r.Top, r.Right, r.Bottom);
            
            if (salesvm.TransactionData == null) return;
            if (SalesLst.SelectedItem == null) return;

            salesvm.CurrentTransactionEntry = (RMSDataAccessLayer.TransactionEntryBase)SalesLst.SelectedItem;
            if (padPos != PadPosition.Middle) return;
            
            
           // f = (FrameworkElement)SalesLst.ItemContainerGenerator.ContainerFromItem(SalesLst.SelectedItem);
           
                SalesPad_LayoutUpdated(null, null);
              //  SetSalesPadtoSelectedItem();
                //if (f != null)
                //{
                //    f.LayoutUpdated += f_LayoutUpdated;
                //}
            
        }

        void f_LayoutUpdated(object sender, EventArgs e)
        {
           
                SetSalesPadtoSelectedItem();
          
        }

        private void SetSalesPadtoSelectedItem()
        {

            SalesLst.UpdateLayout();
            f = (FrameworkElement)SalesLst.ItemContainerGenerator.ContainerFromItem(SalesLst.SelectedItem);
            if (f != null )//&& f.Parent != null
            {
                Rect r = ((FrameworkElement)f).TransformToAncestor(ppcan).TransformBounds(new Rect(0, 0, 0, 0));
                SalesPad.Margin = new Thickness(r.Left, r.Top, r.Right, r.Bottom);
            }
            
        }

       public SalesRegion.SalesPadTransState SalesPadState = SalesPadTransState.Transaction;
       public Key pkey;
        public void ppcan_PreviewKeyDown_1(object sender, KeyEventArgs e)
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

           
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.P)
            {
                if (SalesPadState != SalesPadTransState.Receipt)
                {
                    HideCurrentSalesPadState();
                    // unhide the colums to print
                    ShowReceipt();
                    salesvm.Print(ref SalesPad.ReceiptGrd);
                    e.Handled = true;
                    //hide it back
                }
                else
                {
                    salesvm.Print(ref SalesPad.ReceiptGrd);
                    e.Handled = true;
                }
               
            }

            pkey = e.Key;
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
            SalesLst.SelectedIndex = 0;
            if (SalesPadState == SalesPadTransState.Receipt)//SalesPad.TotalsCol.Width == new GridLength(0) && SalesPad.PaymentCol.Width == new GridLength(0)
            {
                HideReceipt();
                ShowTransaction();
                return;
            }
            if (SalesPadState == SalesPadTransState.Transaction)
            {
                salesvm.GoToPreviousTransaction();
            }
        }

        public void HideReceipt()
        {
            SalesPad.ReceiptCol.Width = new GridLength(0);
            SalesPad.ReceiptGrd.Visibility = System.Windows.Visibility.Hidden;
            SalesPad.PrintCol.Width = new GridLength(0);
            SalesPad.PrintGrd.Visibility = System.Windows.Visibility.Hidden;
        }
        public void ShowReceipt()
        {
            SalesPad.ReceiptCol.Width = new GridLength(400);
            SalesPad.ReceiptGrd.Visibility = System.Windows.Visibility.Visible;
            SalesPad.PrintCol.Width = new GridLength(200);
            SalesPad.PrintGrd.Visibility = System.Windows.Visibility.Visible;
            SalesTaskPad.Instance.MoveToNextControl(SalesPad.PrintGrd);
            SalesPadState = SalesPadTransState.Receipt;
            
            
        }

        public void ShowTransaction()
        {
            SalesPad.EntryCol.Width = new GridLength(408);
            SalesPad.TransactionGrd.Visibility = System.Windows.Visibility.Visible;
            SalesPad.TotalsCol.Width = new GridLength(200);
            SalesPad.TotalsGrd.Visibility = System.Windows.Visibility.Visible;
            SalesTaskPad.Instance.MoveToNextControl(SalesPad.TransactionGrd);
            SalesPadState = SalesPadTransState.Transaction;

        }

        public void GotoNextSalesStep(Key e)
        {
            SalesLst.SelectedIndex = 0;
            NextPhamacySalesSteps(e);
        }

        private void NextPhamacySalesSteps(Key e)
        {
            if (SalesPadState == SalesPadTransState.Receipt && (e == Key.Right || e == Key.Enter))
            {
                HideReceipt();
                salesvm.CloseTransaction();
                ShowTransaction();
                return;
            }


            if (SalesPadState == SalesPadTransState.Transaction)
            {
                //if (salesvm.TransactionData != null && salesvm.TransactionData.GetType() == typeof(Prescription))
                //{
                //    var p = salesvm.TransactionData as Prescription;
                //    if (p.Doctor == null)
                //    {
                //        MessageBox.Show("Please Select a doctor");
                //        return;
                //    }
                //    if (p.Patient == null)
                //    {
                //        MessageBox.Show("Please Select a Patient");
                //        return;
                //    }
                //}
                
                    HideTransaction();
                   
                    ShowReceipt();
               }

        }

        public void HideTransaction()
        {
            SalesPad.EntryCol.Width = new GridLength(0);
            SalesPad.TransactionGrd.Visibility = System.Windows.Visibility.Hidden;
            SalesPad.TotalsCol.Width = new GridLength(0);
            SalesPad.TotalsGrd.Visibility = System.Windows.Visibility.Hidden;
            SalesTaskPad.Instance.MoveToNextControl(SalesPad.ReceiptGrd);
        }


        public void GoToNextTransactionEntry()
        {
           

            if (SalesLst.SelectedIndex == SalesLst.Items.Count - 1)
            {
                

               padPos = PadPosition.Below;
               salesvm.CurrentTransactionEntry = null;
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

        private void UserControl_Unloaded_1(object sender, RoutedEventArgs e)
        {
            salesvm.CreateNewPrescription();
        }

        private void EditDoctorTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(salesvm.Doctor != null)
            SalesPad.ItemEditor.Content = salesvm.Doctor;
        }

        private void EditPatientTB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (salesvm.Patient != null)
                SalesPad.ItemEditor.Content = salesvm.Patient;
        }

        private void DeleteTranBtn_Click(object sender, RoutedEventArgs e)
        {
            salesvm.DeleteCurrentTransaction();
        }

        private void AutoRepeatBtn_Click(object sender, RoutedEventArgs e)
        {
            salesvm.AutoRepeat();
            SetUpSalesPad();
        }


        public void Notify(string token, object sender, NotificationEventArgs e)
        {
            MessageBus.Default.Notify(token, sender, e);
        }
    }
}