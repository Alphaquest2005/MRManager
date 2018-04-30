//using QBFC8Lib;

//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Validation;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace QuickBooks
//{
//    class InventoryAdjustmentViewModel
//    {
//        public void WalkInventoryAdjustmentQueryRs(IMsgSetResponse responseMsgSet)
//        {
//            if (responseMsgSet == null) return;

//            IResponseList responseList = responseMsgSet.ResponseList;
//            if (responseList == null) return;

//            //if we sent only one request, there is only one response, we'll walk the list for this sample
//            for (int i = 0; i < responseList.Count; i++)
//            {
//                IResponse response = responseList.GetAt(i);
//                //check the status code of the response, 0=ok, >0 is warning
//                if (response.StatusCode >= 0)
//                {
//                    //the request-specific response is in the details, make sure we have some
//                    if (response.Detail != null)
//                    {
//                        //make sure the response is the type we're expecting
//                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
//                        if (responseType == ENResponseType.rtInventoryQtyAdjustmentQueryRs)
//                        {
//                            //upcast to more specific type here, this is safe because we checked with response.Type check above
//                            IInventoryQtyAdjustmentRetList InventoryQtyAdjustmentRet = (IInventoryQtyAdjustmentRetList)response.Detail;
//                            SaveToDataBase(WalkInventoryQtyAdjustmentRet(InventoryQtyAdjustmentRet));
//                        }
//                    }
//                }
//            }
//        }

//        private void SaveToDataBase(TrackableCollection<InventoryQtyAdjustmentRet> trackableCollection)
//        {
//            try
//            {
//                QBPOSEntities db = new QBPOSEntities();




//                foreach (InventoryQtyAdjustmentRet qbadj in trackableCollection)
//                {
//                    RemoveExistingAdjustment(db, qbadj);

//                    QuickBooks.InventoryAdjustment adj = new InventoryAdjustment();
//                    adj.AdjustmentNumber = qbadj.InventoryAdjustmentNumber.ToString();
//                    adj.AdjustmentSource = qbadj.InventoryAdjustmentSource;
//                    adj.TxnDate = qbadj.TxnDate;
//                    adj.TxnID = qbadj.TxnID;

//                    foreach (var qbadjitm in qbadj.InventoryQtyAdjustmentItemRet)
//                    {
//                        InventoryAdjustmentItem adjitm = new InventoryAdjustmentItem();
//                        adjitm.ItemListID = qbadjitm.ListID;
//                        adjitm.NewQuantity = qbadjitm.NewQuantity;
//                        adjitm.OldQuantity = qbadjitm.OldQuantity;
//                        adjitm.QuantityDifference = qbadjitm.QtyDifference;
//                        adj.InventoryAdjustmentItems.Add(adjitm);
//                    }
//                    db.InventoryAdjustments.Add(adj);
//                }
//                db.SaveChanges();
//                MessageBox.Show("Inventory Adjustment Import Complete");
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }
//        }

//        private static void RemoveExistingAdjustment(QBPOSEntities db, InventoryQtyAdjustmentRet qbadj)
//        {
//            var xadj = (from i in db.InventoryAdjustments
//                        where i.TxnID == qbadj.TxnID
//                        select i).FirstOrDefault();
//            if (xadj == null) return;
//            db.InventoryAdjustments.Remove(xadj);
//        }


//        TrackableCollection<InventoryQtyAdjustmentRet> WalkInventoryQtyAdjustmentRet(QBFC8Lib.IInventoryAdjustmentRetList InventoryQtyAdjustmentRetList)
//        {
//            TrackableCollection<InventoryQtyAdjustmentRet> AdjList = new TrackableCollection<InventoryQtyAdjustmentRet>(null);
//            if (InventoryQtyAdjustmentRetList.Count == 0) return AdjList;

//            for (int i = 0; i < InventoryQtyAdjustmentRetList.Count; i++)
//            {
//                InventoryQtyAdjustmentRet adj = new InventoryQtyAdjustmentRet();
//               QBFC8Lib.IInventoryAdjustmentRet QBadj = InventoryQtyAdjustmentRetList.GetAt(i);


//                if (QBadj == null) continue;

//                //Go through all the elements of IQBadjList
//                //Get value of TxnID
//                if (QBadj.TxnID != null)
//                {
//                    adj.TxnID = (string)QBadj.TxnID.GetValue();
//                }
//                //Get value of TimeCreated
//                if (QBadj.TimeCreated != null)
//                {
//                    adj.TimeCreated = (DateTime)QBadj.TimeCreated.GetValue();
//                }
//                //Get value of TimeModified
//                if (QBadj.TimeModified != null)
//                {
//                    adj.TimeModified = (DateTime)QBadj.TimeModified.GetValue();
//                }
//                //Get value of Associate
//                if (QBadj.Associate != null)
//                {
//                    adj.Associate = (string)QBadj.Associate.GetValue();
//                }
//                //Get value of Comments
//                if (QBadj.Comments != null)
//                {
//                    adj.Comments = (string)QBadj.Comments.GetValue();
//                }
//                //Get value of CostDifference
//                if (QBadj.CostDifference != null)
//                {
//                    adj.CostDifference = (decimal)QBadj.CostDifference.GetValue();
//                }
//                //Get value of HistoryDocStatus
//                if (QBadj.HistoryDocStatus != null)
//                {
//                    adj.HistoryDocStatus = ((ENHistoryDocStatus)QBadj.HistoryDocStatus.GetValue()).ToString();
//                }
//                //Get value of InventoryAdjustmentNumber
//                if (QBadj.InventoryAdjustmentNumber != null)
//                {
//                    adj.InventoryAdjustmentNumber = (int)QBadj.InventoryAdjustmentNumber.GetValue();
//                }
//                //Get value of InventoryAdjustmentSource
//                if (QBadj.InventoryAdjustmentSource != null)
//                {
//                    adj.InventoryAdjustmentSource = ((ENInventoryAdjustmentSource)QBadj.InventoryAdjustmentSource.GetValue()).ToString();
//                }
//                //Get value of ItemsCount
//                if (QBadj.ItemsCount != null)
//                {
//                    adj.ItemsCount = (int)QBadj.ItemsCount.GetValue();
//                }
//                //Get value of NewQuantity
//                if (QBadj.NewQuantity != null)
//                {
//                    adj.NewQuantity = (int)QBadj.NewQuantity.GetValue();
//                }
//                //Get value of OldQuantity
//                if (QBadj.OldQuantity != null)
//                {
//                    adj.OldQuantity = (int)QBadj.OldQuantity.GetValue();
//                }
//                //Get value of QtyDifference
//                if (QBadj.QtyDifference != null)
//                {
//                    adj.QtyDifference = (int)QBadj.QtyDifference.GetValue();
//                }
//                //Get value of QuickBooksFlag
//                if (QBadj.QuickBooksFlag != null)
//                {
//                    adj.QuickBooksFlag = ((ENQuickBooksFlag)QBadj.QuickBooksFlag.GetValue()).ToString();
//                }
//                //Get value of Reason
//                if (QBadj.Reason != null)
//                {
//                    adj.Reason = (string)QBadj.Reason.GetValue();
//                }
//                //Get value of StoreExchangeStatus
//                if (QBadj.StoreExchangeStatus != null)
//                {
//                    adj.StoreExchangeStatus = ((ENStoreExchangeStatus)QBadj.StoreExchangeStatus.GetValue()).ToString();
//                }
//                //Get value of StoreNumber
//                if (QBadj.StoreNumber != null)
//                {
//                    adj.StoreNumber = (int)QBadj.StoreNumber.GetValue();
//                }
//                //Get value of TxnDate
//                if (QBadj.TxnDate != null)
//                {
//                    adj.TxnDate = (DateTime)QBadj.TxnDate.GetValue();
//                }
//                //Get value of TxnState
//                if (QBadj.TxnState != null)
//                {
//                    adj.TxnState = ((ENTxnState)QBadj.TxnState.GetValue()).ToString();
//                }
//                //Get value of Workstation
//                if (QBadj.Workstation != null)
//                {
//                    adj.Workstation = (int)QBadj.Workstation.GetValue();
//                }
//                if (QBadj.InventoryQtyAdjustmentItemRetList != null)
//                {

//                    adj.InventoryQtyAdjustmentItemRet = new TrackableCollection<InventoryQtyAdjustmentItemRet>(null);

//                    for (int i348 = 0; i348 < QBadj.InventoryAdjustmentLineRetList.Count; i348++)
//                    {

//                        QBFC8Lib.IInventoryAdjustmentLineRet InventoryQtyAdjustmentItemRet = QBadj.InventoryAdjustmentLineRetList.GetAt(i348);
//                        InventoryQtyAdjustmentItemRet adjItm = new InventoryQtyAdjustmentItemRet();
//                        //Get value of ListID
//                        if (InventoryQtyAdjustmentItemRet.ItemRef.ListID != null)
//                        {
//                            adjItm.ListID = (string)InventoryQtyAdjustmentItemRet.ItemRef.ListID.GetValue();
//                        }
//                        //Get value of NewQuantity
//                        if (InventoryQtyAdjustmentItemRet.NewQuantity != null)
//                        {
//                            adjItm.NewQuantity = (int)InventoryQtyAdjustmentItemRet.NewQuantity.GetValue();
//                        }
//                        //Get value of NumberOfBaseUnits
//                        if (InventoryQtyAdjustmentItemRet.NumberOfBaseUnits != null)
//                        {
//                            adjItm.NumberOfBaseUnits = (int)InventoryQtyAdjustmentItemRet.NumberOfBaseUnits.GetValue();
//                        }
//                        //Get value of OldQuantity
//                        if (InventoryQtyAdjustmentItemRet.OldQuantity != null)
//                        {
//                            adjItm.OldQuantity = (int)InventoryQtyAdjustmentItemRet.OldQuantity.GetValue();
//                        }
//                        //Get value of QtyDifference
//                        if (InventoryQtyAdjustmentItemRet.QtyDifference != null)
//                        {
//                            adjItm.QtyDifference = (int)InventoryQtyAdjustmentItemRet.QtyDifference.GetValue();
//                        }
//                        //Get value of SerialNumber
//                        if (InventoryQtyAdjustmentItemRet.SerialNumber != null)
//                        {
//                            adjItm.SerialNumber = (string)InventoryQtyAdjustmentItemRet.SerialNumber.GetValue();
//                        }
//                        //Get value of UnitOfMeasure
//                        if (InventoryQtyAdjustmentItemRet.UnitOfMeasure != null)
//                        {
//                            adjItm.UnitOfMeasure = (string)InventoryQtyAdjustmentItemRet.UnitOfMeasure.GetValue();
//                        }
//                        adj.InventoryQtyAdjustmentItemRet.Add(adjItm);
//                    }
//                }
//                //if (QBadj.DataExtRetList != null)
//                //{
//                //    for (int i356 = 0; i356 < QBadj.DataExtRetList.Count; i356++)
//                //    {
//                //        IDataExtRet DataExtRet = QBadj.DataExtRetList.GetAt(i356);
//                //        //Get value of OwnerID
//                //        string OwnerID357 = (string)DataExtRet.OwnerID.GetValue();
//                //        //Get value of DataExtName
//                //        string DataExtName358 = (string)DataExtRet.DataExtName.GetValue();
//                //        //Get value of DataExtType
//                //        ENDataExtType DataExtType359 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
//                //        //Get value of DataExtValue
//                //        string DataExtValue360 = (string)DataExtRet.DataExtValue.GetValue();
//                //    }
//                //}

//                AdjList.Add(adj);
//            }
//            return AdjList;
//        }


//        public void BuildInventoryAdjustmentQueryRq(IMsgSetRequest InventoryAdjusmentRequestMsgSet)
//        {
//            IInventoryQtyAdjustmentQuery InventoryQtyAdjustmentQueryRq = InventoryAdjusmentRequestMsgSet.AppendInventoryQtyAdjustmentQueryRq();

//            //Set field value for MatchNumericCriterion
//            InventoryQtyAdjustmentQueryRq.ORTxnDateFilters.TxnDateFilter.MatchNumericCriterion.SetValue(ENMatchNumericCriterion.mncGreaterThanEqual);
//            //Set field value for TxnDate
//            InventoryQtyAdjustmentQueryRq.ORTxnDateFilters.TxnDateFilter.TxnDate.SetValue(DateTime.Parse("9/1/2012"));
//        }
//    }
//}
