﻿using QBPOSFC3Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickBooks
{
    public class SalesReceiptViewModel
    {

        public void BuildSalesReceiptQueryRq(IMsgSetRequest requestMsgSet)
        {
            if (requestMsgSet == null) return;
            ISalesReceiptQuery SalesReceiptQueryRq = requestMsgSet.AppendSalesReceiptQueryRq();

            ////Set field value for MatchNumericCriterion
            //SalesReceiptQueryRq.ORTxnDateFilters.TxnDateFilter.MatchNumericCriterion.SetValue(ENMatchNumericCriterion.mncGreaterThanEqual);
            ////Set field value for TimeCreated
            //SalesReceiptQueryRq.ORTxnDateFilters.TxnDateFilter.TxnDate.SetValue(DateTime.Parse("01/01/2013"));

        }

        public void BuildSalesReceiptAddRq(IMsgSetRequest requestMsgSet, SalesReceiptRet salesitm )
        {
           

                ISalesReceiptAdd SalesReceiptAddRq = requestMsgSet.AppendSalesReceiptAddRq();
                //Set attributes
                //Set field value for defMacro
               // SalesReceiptAddRq.defMacro.SetValue("IQBStringType");
                //Set field value for Associate
                SalesReceiptAddRq.Associate.SetValue(salesitm.Associate);
                //Set field value for Cashier
                SalesReceiptAddRq.Cashier.SetValue(salesitm.Cashier);
                //Set field value for Comments
                SalesReceiptAddRq.Comments.SetValue(salesitm.Comments);
                //Set field value for CustomerListID
                //if (salesitm.CustomerListID != null)
                //SalesReceiptAddRq.CustomerListID.SetValue(salesitm.CustomerListID);
                //Set attributes

                //Set field value for StoreNumber
                SalesReceiptAddRq.StoreNumber.SetValue(System.Convert.ToInt16(salesitm.StoreNumber));

                ////Set field value for TaxCategory
                //SalesReceiptAddRq.TaxCategory.SetValue(salesitm.TaxCategory);

                //Set field value for SalesReceiptType
                SalesReceiptAddRq.SalesReceiptType.SetValue((ENSalesReceiptType)System.Convert.ToInt16(salesitm.SalesReceiptType));
                ////Set field value for TrackingNumber
                //SalesReceiptAddRq.TrackingNumber.SetValue(salesitm.TrackingNumber);
                //Set field value for TxnDate
                SalesReceiptAddRq.TxnDate.SetValue(salesitm.TxnDate);
                //Set field value for Workstation
                SalesReceiptAddRq.Workstation.SetValue(System.Convert.ToInt16(salesitm.Workstation));
                //Set field value for TxnState
                SalesReceiptAddRq.TxnState.SetValue((ENTxnState)System.Convert.ToInt16(salesitm.TxnState));

                SalesReceiptAddRq.DiscountPercent.SetValue(Convert.ToSingle(salesitm.DiscountPercent));

                SalesReceiptAddRq.TrackingNumber.SetValue(salesitm.TrackingNumber);

                foreach (var item in salesitm.SalesReceiptItems)
                {
                    ISalesReceiptItemAdd SalesReceiptItemAdd1 = SalesReceiptAddRq.SalesReceiptItemAddList.Append();
                    //Set field value for ListID
                    SalesReceiptItemAdd1.ListID.SetValue(item.ListID);
                    //Set attributes
                    //Set field value for useMacro
                   // SalesReceiptItemAdd1.useMacro.SetValue("IQBStringType");
                    //Set field value for ALU
                    //SalesReceiptItemAdd1.ALU.SetValue(item.ALU);
                    ////Set field value for Associate
                    //SalesReceiptItemAdd1.Associate.SetValue(item.Associate);
                    ////Set field value for Desc1
                    //SalesReceiptItemAdd1.Desc1.SetValue(item.Desc1);
                    //Set field value for Price
                   // SalesReceiptItemAdd1.Price.SetValue(System.Convert.ToDouble(item.Price));
                    //Set field value for Qty
                    SalesReceiptItemAdd1.Qty.SetValue(System.Convert.ToDouble(item.Qty));
                    ////Set field value for Size
                    //SalesReceiptItemAdd1.Size.SetValue(item.Size);
                    //Set field value for TaxCode
                   // SalesReceiptItemAdd1.TaxCode.SetValue(item.TaxCode);
                    ////Set field value for UnitOfMeasure
                    //SalesReceiptItemAdd1.UnitOfMeasure.SetValue(item.UnitOfMeasure);
                    ////Set field value for UPC
                    //SalesReceiptItemAdd1.UPC.SetValue(item.UPC);
                }

                ITenderCashAdd TenderCashAdd3 = SalesReceiptAddRq.TenderCashAddList.Append();
                //Set field value for TenderAmount
                TenderCashAdd3.TenderAmount.SetValue(System.Convert.ToDouble(salesitm.TenderCashRet.TenderAmount));
                




//                //Set field value for useMacro
//                SalesReceiptAddRq.useMacro.SetValue("IQBStringType");
//                //Set field value for Discount
//                SalesReceiptAddRq.Discount.SetValue(System.Convert.ToDouble(salesitm.Discount));
//                //Set field value for DiscountPercent
//                SalesReceiptAddRq.DiscountPercent.SetValue("IQBFloatType");
//                //Set field value for PriceLevelNumber
//                SalesReceiptAddRq.PriceLevelNumber.SetValue((ENPriceLevelNumber)salesitm.PriceLevelNumber);
//                //Set field value for QuickBooksFlag
//                SalesReceiptAddRq.QuickBooksFlag.SetValue((ENQuickBooksFlag)salesitm.QuickBooksFlag);
//                //Set field value for SalesOrderTxnID
//                SalesReceiptAddRq.SalesOrderTxnID.SetValue(salesitm.SalesOrderTxnID);
//                //Set attributes
//                //Set field value for useMacro
//                SalesReceiptAddRq.useMacro.SetValue("IQBStringType");
                
// //Set field value for PromoCode
//                SalesReceiptAddRq.PromoCode.SetValue(salesitm.PromoCode);
////Set field value for ShipDate
//                SalesReceiptAddRq.ShipDate.SetValue(salesitm.ShipDate);
//  //Set field value for TipReceiver
//                SalesReceiptAddRq.TipReceiver.SetValue(salesitm.TipReceiver);
//               
                
//                //Set field value for AddressName
//                SalesReceiptAddRq.ShippingInformation.AddressName.SetValue(salesitm.ShippingInformation.AddressName);
//                //Set field value for City
//                SalesReceiptAddRq.ShippingInformation.City.SetValue(salesitm.ShippingInformation.City);
//                //Set field value for CompanyName
//                SalesReceiptAddRq.ShippingInformation.CompanyName.SetValue(salesitm.ShippingInformation.CompanyName);
//                //Set field value for Country
//                SalesReceiptAddRq.ShippingInformation.Country.SetValue(salesitm.ShippingInformation.Country);
//                //Set field value for FullName
//                SalesReceiptAddRq.ShippingInformation.FullName.SetValue(salesitm.ShippingInformation.FullName);
//                //Set field value for Phone
//                SalesReceiptAddRq.ShippingInformation.Phone.SetValue("ab");
//                //Set field value for Phone2
//                SalesReceiptAddRq.ShippingInformation.Phone2.SetValue("ab");
//                //Set field value for Phone3
//                SalesReceiptAddRq.ShippingInformation.Phone3.SetValue("ab");
//                //Set field value for Phone4
//                SalesReceiptAddRq.ShippingInformation.Phone4.SetValue("ab");
//                //Set field value for PostalCode
//                SalesReceiptAddRq.ShippingInformation.PostalCode.SetValue("ab");
//                //Set field value for ShipBy
//                SalesReceiptAddRq.ShippingInformation.ShipBy.SetValue("ab");
//                //Set field value for Shipping
//                SalesReceiptAddRq.ShippingInformation.Shipping.SetValue(10.01);
//                //Set field value for State
//                SalesReceiptAddRq.ShippingInformation.State.SetValue("ab");
//                //Set field value for Street
//                SalesReceiptAddRq.ShippingInformation.Street.SetValue("ab");
//                //Set field value for Street2
//                SalesReceiptAddRq.ShippingInformation.Street2.SetValue("ab");
                

//                //Set field value for Attribute
//                SalesReceiptItemAdd1.Attribute.SetValue("ab");
//                //Set field value for Commission
//                SalesReceiptItemAdd1.Commission.SetValue(10.01);
//                //Set field value for Desc2
//                SalesReceiptItemAdd1.Desc2.SetValue("ab");
//                //Set field value for Discount
//                SalesReceiptItemAdd1.Discount.SetValue(10.01);
//                //Set field value for DiscountPercent
//                SalesReceiptItemAdd1.DiscountPercent.SetValue("IQBFloatType");
//                //Set field value for DiscountType
//                SalesReceiptItemAdd1.DiscountType.SetValue("ab");
//                //Set field value for ExtendedPrice
//                SalesReceiptItemAdd1.ExtendedPrice.SetValue(10.01);
                
//                //Set field value for SerialNumber
//                SalesReceiptItemAdd1.SerialNumber.SetValue("ab");
               
//                ITenderAccountAdd TenderAccountAdd2 = SalesReceiptAddRq.TenderAccountAddList.Append();
//                //Set field value for TenderAmount
//                TenderAccountAdd2.TenderAmount.SetValue(10.01);
//                //Set field value for TipAmount
//                TenderAccountAdd2.TipAmount.SetValue(10.01);
//                ITenderCashAdd TenderCashAdd3 = SalesReceiptAddRq.TenderCashAddList.Append();
//                //Set field value for TenderAmount
//                TenderCashAdd3.TenderAmount.SetValue(10.01);
//                ITenderCheckAdd TenderCheckAdd4 = SalesReceiptAddRq.TenderCheckAddList.Append();
//                //Set field value for CheckNumber
//                TenderCheckAdd4.CheckNumber.SetValue("ab");
//                //Set field value for TenderAmount
//                TenderCheckAdd4.TenderAmount.SetValue(10.01);
//                ITenderCreditCardAdd TenderCreditCardAdd5 = SalesReceiptAddRq.TenderCreditCardAddList.Append();
//                //Set field value for CardName
//                TenderCreditCardAdd5.CardName.SetValue("ab");
//                //Set field value for TenderAmount
//                TenderCreditCardAdd5.TenderAmount.SetValue(10.01);
//                //Set field value for TipAmount
//                TenderCreditCardAdd5.TipAmount.SetValue(10.01);
//                ITenderDebitCardAdd TenderDebitCardAdd6 = SalesReceiptAddRq.TenderDebitCardAddList.Append();
//                //Set field value for Cashback
//                TenderDebitCardAdd6.Cashback.SetValue(10.01);
//                //Set field value for TenderAmount
//                TenderDebitCardAdd6.TenderAmount.SetValue(10.01);
//                ITenderDepositAdd TenderDepositAdd7 = SalesReceiptAddRq.TenderDepositAddList.Append();
//                //Set field value for TenderAmount
//                TenderDepositAdd7.TenderAmount.SetValue(10.01);
//                ITenderGiftAdd TenderGiftAdd8 = SalesReceiptAddRq.TenderGiftAddList.Append();
//                //Set field value for GiftCertificateNumber
//                TenderGiftAdd8.GiftCertificateNumber.SetValue("ab");
//                //Set field value for TenderAmount
//                TenderGiftAdd8.TenderAmount.SetValue(10.01);
//                ITenderGiftCardAdd TenderGiftCardAdd9 = SalesReceiptAddRq.TenderGiftCardAddList.Append();
//                //Set field value for TenderAmount
//                TenderGiftCardAdd9.TenderAmount.SetValue(10.01);
//                //Set field value for TipAmount
//                TenderGiftCardAdd9.TipAmount.SetValue(10.01);
//                //Set field value for IncludeRetElementList
//                //May create more than one of these if needed
//                SalesReceiptAddRq.IncludeRetElementList.Add("ab");
            
        }
        const double VatRate = 1.15;
        public SalesReceiptRet BuildSalesReceipt(SalesReceipt salesreceipt)
        {
            SalesReceiptRet sr = new SalesReceiptRet();
           
            sr.Associate = salesreceipt.Associate;
            //sr.CustomerListID = null;
            sr.Cashier = salesreceipt.Cashier;
            sr.Comments = salesreceipt.Comments;
            sr.StoreNumber = salesreceipt.StoreNumber;
            sr.Workstation = salesreceipt.Workstation;
            sr.TxnState = salesreceipt.TxnState;
            sr.TxnID = salesreceipt.TxnID;
            sr.SalesReceiptType = salesreceipt.SalesReceiptType;
            sr.TxnDate = salesreceipt.TxnDate;
            sr.TrackingNumber = salesreceipt.TrackingNumber;
            sr.DiscountPercent = Convert.ToDecimal(string.IsNullOrEmpty(salesreceipt.Discount)?"0":salesreceipt.Discount);

            //double total = 0;
            //foreach (var item in salesreceipt.SalesReceiptDetails)
            //{
            //    if (item.TaxCode == "VAT")
            //    {
            //        total += (System.Convert.ToDouble(item.QtySold) * (double)item.Price) * VatRate;
            //    }
            //    else
            //    {
            //        total += (System.Convert.ToDouble(item.QtySold) * (double)item.Price);
            //    }
            //}


            //sr.TenderCashRet = new SalesReceiptRetTenderCashRet() { TenderAmount = Math.Round((Decimal)total,2,MidpointRounding.AwayFromZero)};
           // sr.= 5.865;
            sr.SalesReceiptNumber = salesreceipt.SalesReceiptNumber;
            foreach (var item in salesreceipt.SalesReceiptDetails)
            {
                QuickBooks.SalesReceiptRetSalesReceiptItemRet sritm = new SalesReceiptRetSalesReceiptItemRet();

                sritm.ListID = item.ItemListID;
                //sritm.Price =(Decimal) item.Price;
                sritm.Qty =  item.QtySold.ToString();
               // sritm.TaxCode = item.TaxCode;                

                sr.SalesReceiptItems.Add(sritm);
            }
            return sr;
        }

        public SalesReceiptRet WalkSalesReceiptAddRs(IMsgSetResponse responseMsgSet, SalesReceipt salesreceipt)
        {
            if (responseMsgSet == null) return null;

            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return null;

            //if we sent only one request, there is only one response, we'll walk the list for this sample
            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                //check the status code of the response, 0=ok, >0 is warning
                if (response.StatusCode == 0)
                {
                    //the request-specific response is in the details, make sure we have some
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtSalesReceiptAddRs)
                        {
                            ////upcast to more specific type here, this is safe because we checked with response.Type check above
                            ISalesReceiptRet SalesReceiptRet = (ISalesReceiptRet)response.Detail;
                            SalesReceiptRet result = GetSaleReceipt(SalesReceiptRet);
                            //salesreceipt.TxnID = result.TxnID;
                            //salesreceipt.SalesReceiptNumber = result.SalesReceiptNumber;
                            return result;
                           
                        }
                    }
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString() + " - " + response.StatusMessage);
                    return null;
                }
            }
            return null;
        }

        public void WalkSalesReceiptQueryRs(IMsgSetResponse responseMsgSet)
        {
            if (responseMsgSet == null) return;

            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return;

            //if we sent only one request, there is only one response, we'll walk the list for this sample
            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                //check the status code of the response, 0=ok, >0 is warning
                if (response.StatusCode >= 0)
                {
                    //the request-specific response is in the details, make sure we have some
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtSalesReceiptQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            ISalesReceiptRetList SalesReceiptRet = (ISalesReceiptRetList)response.Detail;
                            SaveToDataBase(WalkSalesReceiptRet(SalesReceiptRet));


                        }
                    }
                }
            }
        }

        private void SaveToDataBase(TrackableCollection<SalesReceiptRet> trackableCollection)
        {
            try
            {


                QBPOSEntities db = new QBPOSEntities();
                foreach (SalesReceiptRet saleReceipt in trackableCollection)
                {
                    SalesReceipt sr = new SalesReceipt();

                    RemoveExistingDbReceipts(db, saleReceipt);

                    sr.SalesReceiptNumber = saleReceipt.SalesReceiptNumber;
                    sr.TxnDate = saleReceipt.TxnDate;

                    // do details
                    if (saleReceipt.SalesReceiptItems != null)
                    {
                        foreach (QuickBooks.SalesReceiptRetSalesReceiptItemRet saleReceiptDetail in saleReceipt.SalesReceiptItems)
                        {
                            SalesReceiptDetail itm = new SalesReceiptDetail();
                            itm.ItemListID = saleReceiptDetail.ListID;
                            itm.Tax = saleReceiptDetail.TaxAmount;
                            itm.QtySold = System.Convert.ToDecimal(saleReceiptDetail.Qty);
                            itm.ItemKey = saleReceiptDetail.Desc1 + "|" + saleReceiptDetail.Desc2 + "|" + saleReceiptDetail.Attribute + "|" + saleReceiptDetail.Size;
                            itm.ItemALU = saleReceiptDetail.ALU;
                            itm.ItemDesc1 = saleReceiptDetail.Desc1;
                            itm.ItemDesc2 = saleReceiptDetail.Desc2;
                            sr.SalesReceiptDetails.Add(itm);
                        }
                    }
                    db.SalesReceipts.Add(sr);
                }

                db.SaveChanges();
                MessageBox.Show("Sale Receipt Import Complete");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void RemoveExistingDbReceipts(QBPOSEntities db, SalesReceiptRet saleReceipt)
        {
            SalesReceipt xr = (SalesReceipt)(from er in db.SalesReceipts
                                             where er.SalesReceiptNumber == saleReceipt.SalesReceiptNumber
                                             select er).FirstOrDefault();

            if (xr != null)
            {
                db.SalesReceipts.Remove(xr);
                // db.SaveChanges();
            }
        }


        QuickBooks.TrackableCollection<SalesReceiptRet> WalkSalesReceiptRet(ISalesReceiptRetList SalesReceiptRetList)
        {
            QuickBooks.TrackableCollection<SalesReceiptRet> xSaleReceiptList = new TrackableCollection<SalesReceiptRet>(null);
            if (SalesReceiptRetList == null) return xSaleReceiptList;




            for (int i = 0; i < SalesReceiptRetList.Count; i++)
            {
                ISalesReceiptRet SalesReceiptRet = SalesReceiptRetList.GetAt(i);

                QuickBooks.SalesReceiptRet xSale = GetSaleReceipt(SalesReceiptRet);

                xSaleReceiptList.Add(xSale);
            }

            return xSaleReceiptList;
        }

        private static QuickBooks.SalesReceiptRet GetSaleReceipt(ISalesReceiptRet SalesReceiptRet)
        {
            QuickBooks.SalesReceiptRet xSale = new SalesReceiptRet();


            //Go through all the elements of ISalesReceiptRetList
            // Get value of TxnID
            if (SalesReceiptRet.TxnID != null)
            {
                xSale.TxnID = (string)SalesReceiptRet.TxnID.GetValue();
            }
            //Get value of TimeCreated
            if (SalesReceiptRet.TimeCreated != null)
            {
                xSale.TimeCreated = (DateTime)SalesReceiptRet.TimeCreated.GetValue();
            }
            //Get value of TimeModified
            if (SalesReceiptRet.TimeModified != null)
            {
                xSale.TimeModified = (DateTime)SalesReceiptRet.TimeModified.GetValue();
            }
            //Get value of Associate
            if (SalesReceiptRet.Associate != null)
            {
                xSale.Associate = (string)SalesReceiptRet.Associate.GetValue();
            }
            //Get value of Cashier
            if (SalesReceiptRet.Cashier != null)
            {
                xSale.Cashier = (string)SalesReceiptRet.Cashier.GetValue();
            }
            //Get value of Comments
            if (SalesReceiptRet.Comments != null)
            {
                xSale.Comments = (string)SalesReceiptRet.Comments.GetValue();
            }
            //Get value of CustomerListID
            if (SalesReceiptRet.CustomerListID != null)
            {
                xSale.CustomerListID = (string)SalesReceiptRet.CustomerListID.GetValue();
            }
            //Get value of Discount
            if (SalesReceiptRet.Discount != null)
            {
                xSale.Discount = (decimal)SalesReceiptRet.Discount.GetValue();
            }
            //Get value of DiscountPercent
            if (SalesReceiptRet.DiscountPercent != null)
            {
                xSale.DiscountPercent = (decimal)SalesReceiptRet.DiscountPercent.GetValue();
            }
            //Get value of HistoryDocStatus
            if (SalesReceiptRet.HistoryDocStatus != null)
            {
                xSale.HistoryDocStatus = ((ENHistoryDocStatus)SalesReceiptRet.HistoryDocStatus.GetValue()).ToString();
            }
            //Get value of ItemsCount
            if (SalesReceiptRet.ItemsCount != null)
            {
                xSale.ItemsCount = ((int)SalesReceiptRet.ItemsCount.GetValue()).ToString();
            }
            //Get value of PriceLevelNumber
            if (SalesReceiptRet.PriceLevelNumber != null)
            {
                xSale.PriceLevelNumber = (int)SalesReceiptRet.PriceLevelNumber.GetValue();//ENPriceLevelNumber
            }
            //Get value of PromoCode
            if (SalesReceiptRet.PromoCode != null)
            {
                xSale.PromoCode = (string)SalesReceiptRet.PromoCode.GetValue();
            }
            //Get value of QuickBooksFlag
            if (SalesReceiptRet.QuickBooksFlag != null)
            {
                xSale.QuickBooksFlag = ((ENQuickBooksFlag)SalesReceiptRet.QuickBooksFlag.GetValue()).ToString();
            }
            //Get value of SalesOrderTxnID
            if (SalesReceiptRet.SalesOrderTxnID != null)
            {
                xSale.SalesOrderTxnID = (string)SalesReceiptRet.SalesOrderTxnID.GetValue();
            }
            //Get value of SalesReceiptNumber
            if (SalesReceiptRet.SalesReceiptNumber != null)
            {
                xSale.SalesReceiptNumber = ((int)SalesReceiptRet.SalesReceiptNumber.GetValue()).ToString();
            }
            //Get value of SalesReceiptType
            if (SalesReceiptRet.SalesReceiptType != null)
            {
                xSale.SalesReceiptType = ((ENSalesReceiptType)SalesReceiptRet.SalesReceiptType.GetValue()).ToString();
            }
            //Get value of ShipDate
            if (SalesReceiptRet.ShipDate != null)
            {
                xSale.ShipDate = (DateTime)SalesReceiptRet.ShipDate.GetValue();
            }
            //Get value of StoreExchangeStatus
            if (SalesReceiptRet.StoreExchangeStatus != null)
            {
                xSale.StoreExchangeStatus = ((ENStoreExchangeStatus)SalesReceiptRet.StoreExchangeStatus.GetValue()).ToString();
            }
            //Get value of StoreNumber
            if (SalesReceiptRet.StoreNumber != null)
            {
                xSale.StoreNumber = ((int)SalesReceiptRet.StoreNumber.GetValue()).ToString();
            }
            //Get value of Subtotal
            if (SalesReceiptRet.Subtotal != null)
            {
                xSale.Subtotal = (Decimal)SalesReceiptRet.Subtotal.GetValue();
            }
            //Get value of TaxAmount
            if (SalesReceiptRet.TaxAmount != null)
            {
                xSale.TaxAmount = ((decimal)SalesReceiptRet.TaxAmount.GetValue());
            }
            //Get value of TaxCategory
            if (SalesReceiptRet.TaxCategory != null)
            {
                xSale.TaxCategory = (string)SalesReceiptRet.TaxCategory.GetValue();
            }
            //Get value of TaxPercentage
            if (SalesReceiptRet.TaxPercentage != null)
            {
                xSale.TaxPercentage = (decimal)SalesReceiptRet.TaxPercentage.GetValue();
            }
            //Get value of TenderType
            //if (SalesReceiptRet.TenderType != null)
            //{
            //    ENTenderType TenderType328 = (ENTenderType)SalesReceiptRet.TenderType.GetValue();
            //}
            //Get value of TipReceiver
            //if (SalesReceiptRet.TipReceiver != null)
            //{
            //    string TipReceiver329 = (string)SalesReceiptRet.TipReceiver.GetValue();
            //}
            //Get value of Total
            if (SalesReceiptRet.Total != null)
            {
                xSale.Total = (decimal)SalesReceiptRet.Total.GetValue();
            }
            //Get value of TrackingNumber
            if (SalesReceiptRet.TrackingNumber != null)
            {
                xSale.TrackingNumber = (string)SalesReceiptRet.TrackingNumber.GetValue();
            }
            //Get value of TxnDate
            if (SalesReceiptRet.TxnDate != null)
            {
                xSale.TxnDate = (DateTime)SalesReceiptRet.TxnDate.GetValue();
            }
            #region "dm code"

            ////Get value of TxnState
            //if (SalesReceiptRet.TxnState != null)
            //{
            //    ENTxnState TxnState333 = (ENTxnState)SalesReceiptRet.TxnState.GetValue();
            //}
            ////Get value of Workstation
            //if (SalesReceiptRet.Workstation != null)
            //{
            //    int Workstation334 = (int)SalesReceiptRet.Workstation.GetValue();
            //}
            //if (SalesReceiptRet.BillingInformation != null)
            //{
            //    //Get value of City
            //    if (SalesReceiptRet.BillingInformation.City != null)
            //    {
            //        string City335 = (string)SalesReceiptRet.BillingInformation.City.GetValue();
            //    }
            //    //Get value of CompanyName
            //    if (SalesReceiptRet.BillingInformation.CompanyName != null)
            //    {
            //        string CompanyName336 = (string)SalesReceiptRet.BillingInformation.CompanyName.GetValue();
            //    }
            //    //Get value of Country
            //    if (SalesReceiptRet.BillingInformation.Country != null)
            //    {
            //        string Country337 = (string)SalesReceiptRet.BillingInformation.Country.GetValue();
            //    }
            //    //Get value of FirstName
            //    if (SalesReceiptRet.BillingInformation.FirstName != null)
            //    {
            //        string FirstName338 = (string)SalesReceiptRet.BillingInformation.FirstName.GetValue();
            //    }
            //    //Get value of LastName
            //    if (SalesReceiptRet.BillingInformation.LastName != null)
            //    {
            //        string LastName339 = (string)SalesReceiptRet.BillingInformation.LastName.GetValue();
            //    }
            //    //Get value of Phone
            //    if (SalesReceiptRet.BillingInformation.Phone != null)
            //    {
            //        string Phone340 = (string)SalesReceiptRet.BillingInformation.Phone.GetValue();
            //    }
            //    //Get value of Phone2
            //    if (SalesReceiptRet.BillingInformation.Phone2 != null)
            //    {
            //        string Phone2341 = (string)SalesReceiptRet.BillingInformation.Phone2.GetValue();
            //    }
            //    //Get value of Phone3
            //    if (SalesReceiptRet.BillingInformation.Phone3 != null)
            //    {
            //        string Phone3342 = (string)SalesReceiptRet.BillingInformation.Phone3.GetValue();
            //    }
            //    //Get value of Phone4
            //    if (SalesReceiptRet.BillingInformation.Phone4 != null)
            //    {
            //        string Phone4343 = (string)SalesReceiptRet.BillingInformation.Phone4.GetValue();
            //    }
            //    //Get value of PostalCode
            //    if (SalesReceiptRet.BillingInformation.PostalCode != null)
            //    {
            //        string PostalCode344 = (string)SalesReceiptRet.BillingInformation.PostalCode.GetValue();
            //    }
            //    //Get value of Salutation
            //    if (SalesReceiptRet.BillingInformation.Salutation != null)
            //    {
            //        string Salutation345 = (string)SalesReceiptRet.BillingInformation.Salutation.GetValue();
            //    }
            //    //Get value of State
            //    if (SalesReceiptRet.BillingInformation.State != null)
            //    {
            //        string State346 = (string)SalesReceiptRet.BillingInformation.State.GetValue();
            //    }
            //    //Get value of Street
            //    if (SalesReceiptRet.BillingInformation.Street != null)
            //    {
            //        string Street347 = (string)SalesReceiptRet.BillingInformation.Street.GetValue();
            //    }
            //    //Get value of Street2
            //    if (SalesReceiptRet.BillingInformation.Street2 != null)
            //    {
            //        string Street2348 = (string)SalesReceiptRet.BillingInformation.Street2.GetValue();
            //    }
            //    //Get value of WebNumber
            //    if (SalesReceiptRet.BillingInformation.WebNumber != null)
            //    {
            //        string WebNumber349 = (string)SalesReceiptRet.BillingInformation.WebNumber.GetValue();
            //    }
            //}
            //if (SalesReceiptRet.ShippingInformation != null)
            //{
            //    //Get value of AddressName
            //    if (SalesReceiptRet.ShippingInformation.AddressName != null)
            //    {
            //        string AddressName350 = (string)SalesReceiptRet.ShippingInformation.AddressName.GetValue();
            //    }
            //    //Get value of City
            //    if (SalesReceiptRet.ShippingInformation.City != null)
            //    {
            //        string City351 = (string)SalesReceiptRet.ShippingInformation.City.GetValue();
            //    }
            //    //Get value of CompanyName
            //    if (SalesReceiptRet.ShippingInformation.CompanyName != null)
            //    {
            //        string CompanyName352 = (string)SalesReceiptRet.ShippingInformation.CompanyName.GetValue();
            //    }
            //    //Get value of Country
            //    if (SalesReceiptRet.ShippingInformation.Country != null)
            //    {
            //        string Country353 = (string)SalesReceiptRet.ShippingInformation.Country.GetValue();
            //    }
            //    //Get value of FullName
            //    if (SalesReceiptRet.ShippingInformation.FullName != null)
            //    {
            //        string FullName354 = (string)SalesReceiptRet.ShippingInformation.FullName.GetValue();
            //    }
            //    //Get value of Phone
            //    if (SalesReceiptRet.ShippingInformation.Phone != null)
            //    {
            //        string Phone355 = (string)SalesReceiptRet.ShippingInformation.Phone.GetValue();
            //    }
            //    //Get value of Phone2
            //    if (SalesReceiptRet.ShippingInformation.Phone2 != null)
            //    {
            //        string Phone2356 = (string)SalesReceiptRet.ShippingInformation.Phone2.GetValue();
            //    }
            //    //Get value of Phone3
            //    if (SalesReceiptRet.ShippingInformation.Phone3 != null)
            //    {
            //        string Phone3357 = (string)SalesReceiptRet.ShippingInformation.Phone3.GetValue();
            //    }
            //    //Get value of Phone4
            //    if (SalesReceiptRet.ShippingInformation.Phone4 != null)
            //    {
            //        string Phone4358 = (string)SalesReceiptRet.ShippingInformation.Phone4.GetValue();
            //    }
            //    //Get value of PostalCode
            //    if (SalesReceiptRet.ShippingInformation.PostalCode != null)
            //    {
            //        string PostalCode359 = (string)SalesReceiptRet.ShippingInformation.PostalCode.GetValue();
            //    }
            //    //Get value of ShipBy
            //    if (SalesReceiptRet.ShippingInformation.ShipBy != null)
            //    {
            //        string ShipBy360 = (string)SalesReceiptRet.ShippingInformation.ShipBy.GetValue();
            //    }
            //    //Get value of Shipping
            //    if (SalesReceiptRet.ShippingInformation.Shipping != null)
            //    {
            //        double Shipping361 = (double)SalesReceiptRet.ShippingInformation.Shipping.GetValue();
            //    }
            //    //Get value of State
            //    if (SalesReceiptRet.ShippingInformation.State != null)
            //    {
            //        string State362 = (string)SalesReceiptRet.ShippingInformation.State.GetValue();
            //    }
            //    //Get value of Street
            //    if (SalesReceiptRet.ShippingInformation.Street != null)
            //    {
            //        string Street363 = (string)SalesReceiptRet.ShippingInformation.Street.GetValue();
            //    }
            //    //Get value of Street2
            //    if (SalesReceiptRet.ShippingInformation.Street2 != null)
            //    {
            //        string Street2364 = (string)SalesReceiptRet.ShippingInformation.Street2.GetValue();
            //    }
            //}
            #endregion
            if (SalesReceiptRet.SalesReceiptItemRetList != null)
            {
                for (int i365 = 0; i365 < SalesReceiptRet.SalesReceiptItemRetList.Count; i365++)
                {
                    xSale.SalesReceiptItems = new TrackableCollection<SalesReceiptRetSalesReceiptItemRet>(null);
                    QuickBooks.SalesReceiptRetSalesReceiptItemRet xSaleReceiptItem = new SalesReceiptRetSalesReceiptItemRet();
                    ISalesReceiptItemRet SalesReceiptItemRet = SalesReceiptRet.SalesReceiptItemRetList.GetAt(i365);
                    //Get value of ListID
                    if (SalesReceiptItemRet.ListID != null)
                    {
                        xSaleReceiptItem.ListID = (string)SalesReceiptItemRet.ListID.GetValue();
                    }
                    //Get value of ALU
                    if (SalesReceiptItemRet.ALU != null)
                    {
                        xSaleReceiptItem.ALU = (string)SalesReceiptItemRet.ALU.GetValue();
                    }
                    //Get value of Associate
                    if (SalesReceiptItemRet.Associate != null)
                    {
                        xSaleReceiptItem.Associate = (string)SalesReceiptItemRet.Associate.GetValue();
                    }
                    //Get value of Attribute
                    if (SalesReceiptItemRet.Attribute != null)
                    {
                        xSaleReceiptItem.Attribute = (string)SalesReceiptItemRet.Attribute.GetValue();
                    }
                    //Get value of Commission
                    if (SalesReceiptItemRet.Commission != null)
                    {
                        xSaleReceiptItem.Commission = (decimal)SalesReceiptItemRet.Commission.GetValue();
                    }
                    //Get value of Cost
                    if (SalesReceiptItemRet.Cost != null)
                    {
                        xSaleReceiptItem.Cost = (decimal)SalesReceiptItemRet.Cost.GetValue();
                    }
                    //Get value of Desc1
                    if (SalesReceiptItemRet.Desc1 != null)
                    {
                        xSaleReceiptItem.Desc1 = (string)SalesReceiptItemRet.Desc1.GetValue();
                    }
                    //Get value of Desc2
                    if (SalesReceiptItemRet.Desc2 != null)
                    {
                        xSaleReceiptItem.Desc2 = (string)SalesReceiptItemRet.Desc2.GetValue();
                    }
                    //Get value of Discount
                    if (SalesReceiptItemRet.Discount != null)
                    {
                        xSaleReceiptItem.Discount = (decimal)SalesReceiptItemRet.Discount.GetValue();
                    }
                    //Get value of DiscountPercent
                    if (SalesReceiptItemRet.DiscountPercent != null)
                    {
                        xSaleReceiptItem.DiscountPercent = (decimal)SalesReceiptItemRet.DiscountPercent.GetValue();
                    }
                    //Get value of DiscountType
                    if (SalesReceiptItemRet.DiscountType != null)
                    {
                        xSaleReceiptItem.DiscountType = (string)SalesReceiptItemRet.DiscountType.GetValue();
                    }
                    //Get value of DiscountSource
                    if (SalesReceiptItemRet.DiscountSource != null)
                    {
                        xSaleReceiptItem.DiscountSource = ((ENDiscountSource)SalesReceiptItemRet.DiscountSource.GetValue()).ToString();
                    }
                    //Get value of ExtendedPrice
                    if (SalesReceiptItemRet.ExtendedPrice != null)
                    {
                        xSaleReceiptItem.ExtendedPrice = (decimal)SalesReceiptItemRet.ExtendedPrice.GetValue();
                    }
                    //Get value of ExtendedTax
                    if (SalesReceiptItemRet.ExtendedTax != null)
                    {
                        xSaleReceiptItem.ExtendedTax = (decimal)SalesReceiptItemRet.ExtendedTax.GetValue();
                    }
                    //Get value of ItemNumber
                    if (SalesReceiptItemRet.ItemNumber != null)
                    {
                        xSaleReceiptItem.ItemNumber = ((int)SalesReceiptItemRet.ItemNumber.GetValue()).ToString();
                    }
                    //Get value of NumberOfBaseUnits
                    if (SalesReceiptItemRet.NumberOfBaseUnits != null)
                    {
                        xSaleReceiptItem.NumberOfBaseUnits = ((int)SalesReceiptItemRet.NumberOfBaseUnits.GetValue()).ToString();
                    }
                    //Get value of Price
                    if (SalesReceiptItemRet.Price != null)
                    {
                        xSaleReceiptItem.Price = (decimal)SalesReceiptItemRet.Price.GetValue();
                    }
                    //Get value of PriceLevelNumber
                    if (SalesReceiptItemRet.PriceLevelNumber != null)
                    {
                        xSaleReceiptItem.PriceLevelNumber = ((ENPriceLevelNumber)SalesReceiptItemRet.PriceLevelNumber.GetValue()).ToString();
                    }
                    //Get value of Qty
                    if (SalesReceiptItemRet.Qty != null)
                    {
                        xSaleReceiptItem.Qty = ((int)SalesReceiptItemRet.Qty.GetValue()).ToString();
                    }
                    //Get value of SerialNumber
                    if (SalesReceiptItemRet.SerialNumber != null)
                    {
                        xSaleReceiptItem.SerialNumber = (string)SalesReceiptItemRet.SerialNumber.GetValue();
                    }
                    //Get value of Size
                    if (SalesReceiptItemRet.Size != null)
                    {
                        xSaleReceiptItem.Size = (string)SalesReceiptItemRet.Size.GetValue();
                    }
                    //Get value of TaxAmount
                    if (SalesReceiptItemRet.TaxAmount != null)
                    {
                        xSaleReceiptItem.TaxAmount = (decimal)SalesReceiptItemRet.TaxAmount.GetValue();
                    }
                    //Get value of TaxCode
                    if (SalesReceiptItemRet.TaxCode != null)
                    {
                        xSaleReceiptItem.TaxCode = (string)SalesReceiptItemRet.TaxCode.GetValue();
                    }
                    //Get value of TaxPercentage
                    if (SalesReceiptItemRet.TaxPercentage != null)
                    {
                        xSaleReceiptItem.TaxPercentage = (decimal)SalesReceiptItemRet.TaxPercentage.GetValue();
                    }
                    //Get value of UnitOfMeasure
                    if (SalesReceiptItemRet.UnitOfMeasure != null)
                    {
                        xSaleReceiptItem.UnitOfMeasure = (string)SalesReceiptItemRet.UnitOfMeasure.GetValue();
                    }
                    //Get value of UPC
                    if (SalesReceiptItemRet.UPC != null)
                    {
                        xSaleReceiptItem.UPC = (string)SalesReceiptItemRet.UPC.GetValue();
                    }
                    //Get value of WebDesc
                    if (SalesReceiptItemRet.WebDesc != null)
                    {
                        xSaleReceiptItem.WebDesc = (string)SalesReceiptItemRet.WebDesc.GetValue();
                    }
                    //Get value of Manufacturer
                    if (SalesReceiptItemRet.Manufacturer != null)
                    {
                        xSaleReceiptItem.Manufacturer = (string)SalesReceiptItemRet.Manufacturer.GetValue();
                    }
                    //Get value of Weight
                    if (SalesReceiptItemRet.Weight != null)
                    {
                        xSaleReceiptItem.Weight = (decimal)SalesReceiptItemRet.Weight.GetValue();
                    }
                    //Get value of WebSKU
                    if (SalesReceiptItemRet.WebSKU != null)
                    {
                        xSaleReceiptItem.WebSKU = (string)SalesReceiptItemRet.WebSKU.GetValue();
                    }
                    xSale.SalesReceiptItems.Add(xSaleReceiptItem);
                }

            }

            #region "morecode"

            //if (SalesReceiptRet.TenderAccountRetList != null)
            //{
            //    for (int i396 = 0; i396 < SalesReceiptRet.TenderAccountRetList.Count; i396++)
            //    {
            //        ITenderAccountRet TenderAccountRet = SalesReceiptRet.TenderAccountRetList.GetAt(i396);
            //        //Get value of TenderAmount
            //        if (TenderAccountRet.TenderAmount != null)
            //        {
            //            double TenderAmount397 = (double)TenderAccountRet.TenderAmount.GetValue();
            //        }
            //        //Get value of TipAmount
            //        if (TenderAccountRet.TipAmount != null)
            //        {
            //            double TipAmount398 = (double)TenderAccountRet.TipAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderCashRetList != null)
            //{
            //    for (int i399 = 0; i399 < SalesReceiptRet.TenderCashRetList.Count; i399++)
            //    {
            //        ITenderCashRet TenderCashRet = SalesReceiptRet.TenderCashRetList.GetAt(i399);
            //        //Get value of TenderAmount
            //        if (TenderCashRet.TenderAmount != null)
            //        {
            //            double TenderAmount400 = (double)TenderCashRet.TenderAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderCheckRetList != null)
            //{
            //    for (int i401 = 0; i401 < SalesReceiptRet.TenderCheckRetList.Count; i401++)
            //    {
            //        ITenderCheckRet TenderCheckRet = SalesReceiptRet.TenderCheckRetList.GetAt(i401);
            //        //Get value of CheckNumber
            //        if (TenderCheckRet.CheckNumber != null)
            //        {
            //            string CheckNumber402 = (string)TenderCheckRet.CheckNumber.GetValue();
            //        }
            //        //Get value of TenderAmount
            //        if (TenderCheckRet.TenderAmount != null)
            //        {
            //            double TenderAmount403 = (double)TenderCheckRet.TenderAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderCreditCardRetList != null)
            //{
            //    for (int i404 = 0; i404 < SalesReceiptRet.TenderCreditCardRetList.Count; i404++)
            //    {
            //        ITenderCreditCardRet TenderCreditCardRet = SalesReceiptRet.TenderCreditCardRetList.GetAt(i404);
            //        //Get value of CardName
            //        if (TenderCreditCardRet.CardName != null)
            //        {
            //            string CardName405 = (string)TenderCreditCardRet.CardName.GetValue();
            //        }
            //        //Get value of TenderAmount
            //        if (TenderCreditCardRet.TenderAmount != null)
            //        {
            //            double TenderAmount406 = (double)TenderCreditCardRet.TenderAmount.GetValue();
            //        }
            //        //Get value of TipAmount
            //        if (TenderCreditCardRet.TipAmount != null)
            //        {
            //            double TipAmount407 = (double)TenderCreditCardRet.TipAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderDebitCardRetList != null)
            //{
            //    for (int i408 = 0; i408 < SalesReceiptRet.TenderDebitCardRetList.Count; i408++)
            //    {
            //        ITenderDebitCardRet TenderDebitCardRet = SalesReceiptRet.TenderDebitCardRetList.GetAt(i408);
            //        //Get value of Cashback
            //        if (TenderDebitCardRet.Cashback != null)
            //        {
            //            double Cashback409 = (double)TenderDebitCardRet.Cashback.GetValue();
            //        }
            //        //Get value of TenderAmount
            //        if (TenderDebitCardRet.TenderAmount != null)
            //        {
            //            double TenderAmount410 = (double)TenderDebitCardRet.TenderAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderDepositRetList != null)
            //{
            //    for (int i411 = 0; i411 < SalesReceiptRet.TenderDepositRetList.Count; i411++)
            //    {
            //        ITenderDepositRet TenderDepositRet = SalesReceiptRet.TenderDepositRetList.GetAt(i411);
            //        //Get value of TenderAmount
            //        if (TenderDepositRet.TenderAmount != null)
            //        {
            //            double TenderAmount412 = (double)TenderDepositRet.TenderAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderGiftRetList != null)
            //{
            //    for (int i413 = 0; i413 < SalesReceiptRet.TenderGiftRetList.Count; i413++)
            //    {
            //        ITenderGiftRet TenderGiftRet = SalesReceiptRet.TenderGiftRetList.GetAt(i413);
            //        //Get value of GiftCertificateNumber
            //        if (TenderGiftRet.GiftCertificateNumber != null)
            //        {
            //            string GiftCertificateNumber414 = (string)TenderGiftRet.GiftCertificateNumber.GetValue();
            //        }
            //        //Get value of TenderAmount
            //        if (TenderGiftRet.TenderAmount != null)
            //        {
            //            double TenderAmount415 = (double)TenderGiftRet.TenderAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.TenderGiftCardRetList != null)
            //{
            //    for (int i416 = 0; i416 < SalesReceiptRet.TenderGiftCardRetList.Count; i416++)
            //    {
            //        ITenderGiftCardRet TenderGiftCardRet = SalesReceiptRet.TenderGiftCardRetList.GetAt(i416);
            //        //Get value of TenderAmount
            //        if (TenderGiftCardRet.TenderAmount != null)
            //        {
            //            double TenderAmount417 = (double)TenderGiftCardRet.TenderAmount.GetValue();
            //        }
            //        //Get value of TipAmount
            //        if (TenderGiftCardRet.TipAmount != null)
            //        {
            //            double TipAmount418 = (double)TenderGiftCardRet.TipAmount.GetValue();
            //        }
            //    }
            //}
            //if (SalesReceiptRet.DataExtRetList != null)
            //{
            //    for (int i419 = 0; i419 < SalesReceiptRet.DataExtRetList.Count; i419++)
            //    {
            //        IDataExtRet DataExtRet = SalesReceiptRet.DataExtRetList.GetAt(i419);
            //        //Get value of OwnerID
            //        string OwnerID420 = (string)DataExtRet.OwnerID.GetValue();
            //        //Get value of DataExtName
            //        string DataExtName421 = (string)DataExtRet.DataExtName.GetValue();
            //        //Get value of DataExtType
            //        ENDataExtType DataExtType422 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
            //        //Get value of DataExtValue
            //        string DataExtValue423 = (string)DataExtRet.DataExtValue.GetValue();
            //    }
            //}
            #endregion
            return xSale;
        }


    }
}
