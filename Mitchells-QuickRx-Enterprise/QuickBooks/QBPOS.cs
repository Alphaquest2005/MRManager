using System;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data;
using System.IO;
using QBPOSFC3Lib;
using System.Windows;

namespace POSAI_mvvm.QuickBooks
{
    public class QBPOS
    {
        public void DoSalesReceiptQuery()
        {
            bool sessionBegun = false;
            bool connectionOpen = false;
            QBPOSSessionManager sessionManager = null;

            //try
            //{
                // get qbpos filename
                string qbposfile = "";

                //Create the session Manager object
                sessionManager = new QBPOSSessionManager();

IMsgSetRequest ItemInventoryRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                ItemInventoryRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                ItemInventoryViewModel inventoryVM = new ItemInventoryViewModel();
                inventoryVM.BuildItemInventoryQueryRq(ItemInventoryRequestMsgSet);

                //Create the message set request object to hold our request
                IMsgSetRequest SalesReceiptRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                SalesReceiptRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                SalesReceiptViewModel SalesReceiptVM = new SalesReceiptViewModel();
                SalesReceiptVM.BuildSalesReceiptQueryRq(SalesReceiptRequestMsgSet);

                

                IMsgSetRequest InventoryAdjustmentRequestMsgSet = sessionManager.CreateMsgSetRequest(3, 0);
                InventoryAdjustmentRequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
                InventoryAdjustmentViewModel AdjVM = new InventoryAdjustmentViewModel();
                AdjVM.BuildInventoryAdjustmentQueryRq(InventoryAdjustmentRequestMsgSet);



                //Connect to QuickBooks and begin a session
                sessionManager.OpenConnection("1", "Insight POSAI");
                short majorVersion;
                short minorVersion;
                ENReleaseLevel releaseLevel;
                short releaseNumber;
                sessionManager.GetVersion(out majorVersion, out minorVersion, out releaseLevel, out releaseNumber);



                connectionOpen = true;
                sessionManager.BeginSession(qbposfile);
                sessionBegun = true;

                //Send the request and get the response from QuickBooks
                IMsgSetResponse ItemInventoryResponseMsgSet = sessionManager.DoRequests(ItemInventoryRequestMsgSet);   
            
                IMsgSetResponse SalesReceiptResponseMsgSet = sessionManager.DoRequests(SalesReceiptRequestMsgSet);

                IMsgSetResponse InventoryAdjustmentResponseMsgSet = sessionManager.DoRequests(InventoryAdjustmentRequestMsgSet);
                //End the session and close the connection to QuickBooks
                sessionManager.EndSession();
                sessionBegun = false;
                sessionManager.CloseConnection();
                connectionOpen = false;

                inventoryVM.WalkItemInventoryQueryRs(ItemInventoryResponseMsgSet);
                SalesReceiptVM.WalkSalesReceiptQueryRs(SalesReceiptResponseMsgSet);
                
                AdjVM.WalkInventoryAdjustmentQueryRs(InventoryAdjustmentResponseMsgSet);

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, "Error");
            //    if (sessionBegun)
            //    {
            //        sessionManager.EndSession();
            //    }
            //    if (connectionOpen)
            //    {
            //        sessionManager.CloseConnection();
            //    }
            //}
        }

        

        



    }
}