using System;
using System.Collections.Generic;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
//using System.Data.Entity.Validation;
using System.Linq;
using System.Data;
using System.IO;
using QBPOSFC3Lib;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickBooks
{
    public class QBPOS: IDisposable
    {
        static QBPOSSessionManager sessionManager = null;
        bool sessionBegun = false;
        bool connectionOpen = false;
        string qbposfile = Properties.Settings.Default.QBCompanyFile  ;

        private static object syncRoot = new Object();
        private static volatile QBPOS instance;
        public static QBPOS Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new QBPOS();
                    }
                }

                return instance;
            }
        }

        private QBPOS()
        {
            ///////////////////////// Do not attempt per instance manager cuz of constant dialog box
            sessionManager = new QBPOSSessionManager();
            BeginSession();
        }

        private void CloseSession()
        {
            if (sessionManager != null)
            {
                sessionManager.EndSession();
                sessionBegun = false;
                sessionManager.CloseConnection();
            }
            connectionOpen = false;
        }

        private void BeginSession()
        {
            //sessionManager.OpenConnection("1", "Insight QBBulk");
            //short majorVersion;
            //short minorVersion;
            //ENReleaseLevel releaseLevel;
            //short releaseNumber;
            //sessionManager.GetVersion(out majorVersion, out minorVersion, out releaseLevel, out releaseNumber);
            try
            {
                
                sessionManager.OpenConnection("1", "QS2QBPost");
                connectionOpen = true;
                sessionManager.BeginSession("Computer Name=server;Company Data=hills and valley gd;Version=11");//
                //sessionManager.BeginSession("");
                
                sessionBegun = true;
             
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
              
                if (sessionBegun)
                {
                    sessionManager.EndSession();
                }
                if (connectionOpen)
                {
                    sessionManager.CloseConnection();
                }
            }
        }

        public List<ItemInventoryRet> GetInventoryItemQuery(int days = 1)
        {
            
           

            if (sessionManager != null)
            {
                IMsgSetRequest ItemModifiedInventoryRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                IMsgSetRequest ItemCreatedInventoryRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                ItemModifiedInventoryRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                ItemCreatedInventoryRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                ItemInventoryViewModel inventoryVM = new ItemInventoryViewModel();
                inventoryVM.BuildModifiedItemInventoryQuery(ItemModifiedInventoryRequestMsgSet, days);
                inventoryVM.BuildCreatedItemInventoryQuery(ItemCreatedInventoryRequestMsgSet, days);
                //if(sessionBegun == false)
                //    BeginSession();
                ////Send the request and get the response from QuickBooks
                IMsgSetResponse ModifiedItemsLst = sessionManager.DoRequests(ItemModifiedInventoryRequestMsgSet);
                IMsgSetResponse CreatedItemsLst = sessionManager.DoRequests(ItemCreatedInventoryRequestMsgSet);
                //if(sessionBegun == true)
                
                var lst = new List<ItemInventoryRet>();
                var tlst = new TrackableCollection<ItemInventoryRet>(null);

                if (ModifiedItemsLst != null)
                    tlst = inventoryVM.WalkItemInventoryQueryRs(ModifiedItemsLst);

                if(tlst != null) 
                    lst.AddRange(tlst.ToList());

                if (CreatedItemsLst != null)
                    tlst = inventoryVM.WalkItemInventoryQueryRs(CreatedItemsLst);

                if (tlst != null) 
                    lst.AddRange(tlst.ToList());

                return lst.GroupBy(x => x.ListID).Select(grp => grp.First()).ToList();
            }
            return new List<ItemInventoryRet>();
        }

        public void DoSalesReceipt()
        {
            //Create the message set request object to hold our request
            
            if (sessionManager != null)
            {
                IMsgSetRequest SalesReceiptRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                SalesReceiptRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                SalesReceiptViewModel SalesReceiptVM = new SalesReceiptViewModel();
                SalesReceiptVM.BuildSalesReceiptQueryRq(SalesReceiptRequestMsgSet);

                // BeginSession();

                IMsgSetResponse SalesReceiptResponseMsgSet = sessionManager.DoRequests(SalesReceiptRequestMsgSet);

          

                SalesReceiptVM.WalkSalesReceiptQueryRs(SalesReceiptResponseMsgSet);
            }
        }

        public SalesReceiptRet AddSalesReceipt(SalesReceipt salesreceipt)
        {
            //Create the message set request object to hold our request
           
            if (sessionManager != null)
            {
                IMsgSetRequest SalesReceiptRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                SalesReceiptRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                SalesReceiptViewModel SalesReceiptVM = new SalesReceiptViewModel();
                SalesReceiptVM.BuildSalesReceiptAddRq(SalesReceiptRequestMsgSet,
                    SalesReceiptVM.BuildSalesReceipt(salesreceipt));

                try
                {
                    return GetQBSalesReceipt(salesreceipt, SalesReceiptRequestMsgSet, SalesReceiptVM);
                }
                catch (COMException ce)
                {
                    // MessageBox.Show("QuickBooks Problem: " + ce.Message);
                    return new SalesReceiptRet() {Comments = "QuickBooks Problem: " + ce.Message };
                }
            }
            return new SalesReceiptRet();
        }

        private SalesReceiptRet GetQBSalesReceipt(SalesReceipt salesreceipt, IMsgSetRequest SalesReceiptRequestMsgSet, SalesReceiptViewModel SalesReceiptVM)
        {
            //if(sessionBegun == false)
            //BeginSession();

            IMsgSetResponse SalesReceiptResponseMsgSet = sessionManager.DoRequests(SalesReceiptRequestMsgSet);

            //if(sessionBegun == true)
          

            return SalesReceiptVM.WalkSalesReceiptAddRs(SalesReceiptResponseMsgSet, salesreceipt);
        }
  
       

        public void DoInventoryAdjustment()
        {
            //IMsgSetRequest InventoryAdjustmentRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
            //InventoryAdjustmentRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
            //InventoryAdjustmentViewModel AdjVM = new InventoryAdjustmentViewModel();
            //AdjVM.BuildInventoryAdjustmentQueryRq(InventoryAdjustmentRequestMsgSet);

            //BeginSession();

            //IMsgSetResponse InventoryAdjustmentResponseMsgSet = sessionManager.DoRequests(InventoryAdjustmentRequestMsgSet);

            //CloseSession();

            //AdjVM.WalkInventoryAdjustmentQueryRs(InventoryAdjustmentResponseMsgSet);

        }


        public void Dispose()
        {
           CloseSession();
        }

        public async Task<List<ItemInventoryRet>> GetAllInventoryQuery()
        {
            
            if (sessionManager != null)
            {
                var ItemInventoryRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                ItemInventoryRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                var inventoryVM = new ItemInventoryViewModel();
                int increment = 1000;
                int toItemNumber = 0;
                int fromItemNumber = 0;
                string errstring = null;

         
                var lst = new List<ItemInventoryRet>();

                while (errstring == null)
                {

                    toItemNumber += increment;
                    ItemInventoryRequestMsgSet.ClearRequests();
                    inventoryVM.BuildItemInventoryQueryRq(ItemInventoryRequestMsgSet, fromItemNumber, toItemNumber);
                    fromItemNumber = toItemNumber;
                    ItemInventoryRequestMsgSet.Verify(out errstring);

                    IMsgSetResponse ItemInventoryResponseMsgSet = null;
                    ItemInventoryResponseMsgSet = sessionManager.DoRequests(ItemInventoryRequestMsgSet);
                    var responseStatus = new Tuple<string, TrackableCollection<ItemInventoryRet>>("",null);
                    if (ItemInventoryResponseMsgSet != null)
                        responseStatus = await inventoryVM.WalkItemInventoryQueryRsAsync(ItemInventoryResponseMsgSet).ConfigureAwait(false);

                
                    if (errstring != null || responseStatus.Item1 == "0") break;
                    lst.AddRange(responseStatus.Item2);
                }
             
                return lst;
            }
            return new List<ItemInventoryRet>();
        }
    }
}