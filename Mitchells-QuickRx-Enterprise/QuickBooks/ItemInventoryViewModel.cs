using QBPOSFC3Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSAI_mvvm.QuickBooks
{
    public class ItemInventoryViewModel
    {

        public void BuildItemInventoryQueryRq(IMsgSetRequest ItemInventoryRequestMsgSet)
        {
            IItemInventoryQuery ItemInventoryQueryRq = ItemInventoryRequestMsgSet.AppendItemInventoryQueryRq();
            //Set attributes
            //Set field value for MatchNumericCriterion
            //ItemInventoryQueryRq.ORTimeCreatedFilters.TimeCreatedFilter.MatchNumericCriterion.SetValue(ENMatchNumericCriterion.mncGreaterThanEqual);
            ////Set field value for TimeCreated
            //ItemInventoryQueryRq.ORTimeCreatedFilters.TimeCreatedFilter.TimeCreated.SetValue(DateTime.Parse("1/1/2000"), false);
        }

        public void WalkItemInventoryQueryRs(IMsgSetResponse responseMsgSet)
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
                        if (responseType == ENResponseType.rtItemInventoryQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            IItemInventoryRetList ItemInventoryRet = (IItemInventoryRetList)response.Detail;
                            SaveInventoryToDB(WalkItemInventoryRet(ItemInventoryRet));
                        }
                    }
                }
            }
        }

        private void SaveInventoryToDB(TrackableCollection<ItemInventoryRet> trackableCollection)
        {

            POSAIData.POSAIEntities db = new POSAIData.POSAIEntities();
            RemoveAllIDbInventoryItems(db);
            foreach (ItemInventoryRet itm in trackableCollection)
            {
                POSAIData.InventoryItem i = new POSAIData.InventoryItem();
                i.ALU = itm.ALU;
                i.Attribute = itm.Attribute;
                i.DepartmentCode = itm.DepartmentCode;
                i.ItemDesc1 = itm.Desc1;
                i.ItemDesc2 = itm.Desc2;
                i.ItemNumber = System.Convert.ToInt32(itm.ItemNumber);
                i.ItemType = itm.ItemType;
                i.ListID = itm.ListID;
                i.Size = itm.Size;

                db.InventoryItems.Add(i);

            }
            try
            {
                db.SaveChanges();
                System.Windows.MessageBox.Show("Inventory Items Import Complete");
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
        }

        private static void RemoveAllIDbInventoryItems(POSAIData.POSAIEntities db)
        {
            foreach (POSAIData.InventoryItem eitm in db.InventoryItems)
            {
                db.InventoryItems.Remove(eitm);
            }
        }
        POSAI_mvvm.QuickBooks.TrackableCollection<ItemInventoryRet> WalkItemInventoryRet(IItemInventoryRetList ItemInventoryRetList)
        {
            TrackableCollection<ItemInventoryRet> itmList = new TrackableCollection<QuickBooks.ItemInventoryRet>(null);

            for (int i = 0; i < ItemInventoryRetList.Count; i++)
            {
                IItemInventoryRet positm = ItemInventoryRetList.GetAt(i);
                ItemInventoryRet itm = new ItemInventoryRet();

                if (positm == null) continue;

                //Go through all the elements of IpositmList
                //Get value of ListID
                if (positm.ListID != null)
                {
                    itm.ListID = (string)positm.ListID.GetValue();
                }

                //Get value of ALU
                if (positm.ALU != null)
                {
                    itm.ALU = (string)positm.ALU.GetValue();
                }
                //Get value of Attribute
                if (positm.Attribute != null)
                {
                    itm.Attribute = (string)positm.Attribute.GetValue();
                }
                //Get value of DepartmentCode
                if (positm.DepartmentCode != null)
                {
                    itm.DepartmentCode = (string)positm.DepartmentCode.GetValue();
                }
                //Get value of Desc1
                if (positm.Desc1 != null)
                {
                    itm.Desc1 = (string)positm.Desc1.GetValue();
                }
                //Get value of Desc2
                if (positm.Desc2 != null)
                {
                    itm.Desc2 = (string)positm.Desc2.GetValue();
                }
                //Get value of ItemNumber
                if (positm.ItemNumber != null)
                {
                    itm.ItemNumber = (int)positm.ItemNumber.GetValue();
                }
                //Get value of ItemType
                if (positm.ItemType != null)
                {
                    itm.ItemType = ((ENItemType)positm.ItemType.GetValue()).ToString();
                }
                //Get value of Size
                if (positm.Size != null)
                {
                    itm.Size = (string)positm.Size.GetValue();
                }

                #region "More Properties"
                //        //Get value of TimeCreated
                //if (ItemInventoryRet.TimeCreated != null)
                //{
                //DateTime TimeCreated78 = (DateTime)ItemInventoryRet.TimeCreated.GetValue();
                //}
                ////Get value of TimeModified
                //if (ItemInventoryRet.TimeModified != null)
                //{
                //DateTime TimeModified79 = (DateTime)ItemInventoryRet.TimeModified.GetValue();
                //}
                ////Get value of COGSAccount
                //if (ItemInventoryRet.COGSAccount != null)
                //{
                //string COGSAccount82 = (string)ItemInventoryRet.COGSAccount.GetValue();
                //}
                ////Get value of Cost
                //if (ItemInventoryRet.Cost != null)
                //{
                //double Cost83 = (double)ItemInventoryRet.Cost.GetValue();
                //}
                ////Get value of DepartmentListID
                //if (ItemInventoryRet.DepartmentListID != null)
                //{
                //string DepartmentListID85 = (string)ItemInventoryRet.DepartmentListID.GetValue();
                //}
                ////Get value of IncomeAccount
                //if (ItemInventoryRet.IncomeAccount != null)
                //{
                //string IncomeAccount88 = (string)ItemInventoryRet.IncomeAccount.GetValue();
                //}
                ////Get value of IsBelowReorder
                //if (ItemInventoryRet.IsBelowReorder != null)
                //{
                //bool IsBelowReorder89 = (bool)ItemInventoryRet.IsBelowReorder.GetValue();
                //}
                ////Get value of IsEligibleForCommission
                //if (ItemInventoryRet.IsEligibleForCommission != null)
                //{
                //bool IsEligibleForCommission90 = (bool)ItemInventoryRet.IsEligibleForCommission.GetValue();
                //}
                ////Get value of IsPrintingTags
                //if (ItemInventoryRet.IsPrintingTags != null)
                //{
                //bool IsPrintingTags91 = (bool)ItemInventoryRet.IsPrintingTags.GetValue();
                //}
                ////Get value of IsUnorderable
                //if (ItemInventoryRet.IsUnorderable != null)
                //{
                //bool IsUnorderable92 = (bool)ItemInventoryRet.IsUnorderable.GetValue();
                //}
                ////Get value of HasPictures
                //if (ItemInventoryRet.HasPictures != null)
                //{
                //bool HasPictures93 = (bool)ItemInventoryRet.HasPictures.GetValue();
                //}
                ////Get value of IsEligibleForRewards
                //if (ItemInventoryRet.IsEligibleForRewards != null)
                //{
                //bool IsEligibleForRewards94 = (bool)ItemInventoryRet.IsEligibleForRewards.GetValue();
                //}
                ////Get value of IsWebItem
                //if (ItemInventoryRet.IsWebItem != null)
                //{
                //bool IsWebItem95 = (bool)ItemInventoryRet.IsWebItem.GetValue();
                //}
                ////Get value of LastReceived
                //if (ItemInventoryRet.LastReceived != null)
                //{
                //DateTime LastReceived98 = (DateTime)ItemInventoryRet.LastReceived.GetValue();
                //}
                ////Get value of MarginPercent
                //if (ItemInventoryRet.MarginPercent != null)
                //{
                //int MarginPercent99 = (int)ItemInventoryRet.MarginPercent.GetValue();
                //}
                ////Get value of MarkupPercent
                //if (ItemInventoryRet.MarkupPercent != null)
                //{
                //int MarkupPercent100 = (int)ItemInventoryRet.MarkupPercent.GetValue();
                //}
                ////Get value of MSRP
                //if (ItemInventoryRet.MSRP != null)
                //{
                //double MSRP101 = (double)ItemInventoryRet.MSRP.GetValue();
                //}
                ////Get value of OnHandStore01
                //if (ItemInventoryRet.OnHandStore01 != null)
                //{
                //int OnHandStore01102 = (int)ItemInventoryRet.OnHandStore01.GetValue();
                //}
                ////Get value of OnHandStore02
                //if (ItemInventoryRet.OnHandStore02 != null)
                //{
                //int OnHandStore02103 = (int)ItemInventoryRet.OnHandStore02.GetValue();
                //}
                ////Get value of OnHandStore03
                //if (ItemInventoryRet.OnHandStore03 != null)
                //{
                //int OnHandStore03104 = (int)ItemInventoryRet.OnHandStore03.GetValue();
                //}
                ////Get value of OnHandStore04
                //if (ItemInventoryRet.OnHandStore04 != null)
                //{
                //int OnHandStore04105 = (int)ItemInventoryRet.OnHandStore04.GetValue();
                //}
                ////Get value of OnHandStore05
                //if (ItemInventoryRet.OnHandStore05 != null)
                //{
                //int OnHandStore05106 = (int)ItemInventoryRet.OnHandStore05.GetValue();
                //}
                ////Get value of OnHandStore06
                //if (ItemInventoryRet.OnHandStore06 != null)
                //{
                //int OnHandStore06107 = (int)ItemInventoryRet.OnHandStore06.GetValue();
                //}
                ////Get value of OnHandStore07
                //if (ItemInventoryRet.OnHandStore07 != null)
                //{
                //int OnHandStore07108 = (int)ItemInventoryRet.OnHandStore07.GetValue();
                //}
                ////Get value of OnHandStore08
                //if (ItemInventoryRet.OnHandStore08 != null)
                //{
                //int OnHandStore08109 = (int)ItemInventoryRet.OnHandStore08.GetValue();
                //}
                ////Get value of OnHandStore09
                //if (ItemInventoryRet.OnHandStore09 != null)
                //{
                //int OnHandStore09110 = (int)ItemInventoryRet.OnHandStore09.GetValue();
                //}
                ////Get value of OnHandStore10
                //if (ItemInventoryRet.OnHandStore10 != null)
                //{
                //int OnHandStore10111 = (int)ItemInventoryRet.OnHandStore10.GetValue();
                //}
                ////Get value of OnHandStore11
                //if (ItemInventoryRet.OnHandStore11 != null)
                //{
                //int OnHandStore11112 = (int)ItemInventoryRet.OnHandStore11.GetValue();
                //}
                ////Get value of OnHandStore12
                //if (ItemInventoryRet.OnHandStore12 != null)
                //{
                //int OnHandStore12113 = (int)ItemInventoryRet.OnHandStore12.GetValue();
                //}
                ////Get value of OnHandStore13
                //if (ItemInventoryRet.OnHandStore13 != null)
                //{
                //int OnHandStore13114 = (int)ItemInventoryRet.OnHandStore13.GetValue();
                //}
                ////Get value of OnHandStore14
                //if (ItemInventoryRet.OnHandStore14 != null)
                //{
                //int OnHandStore14115 = (int)ItemInventoryRet.OnHandStore14.GetValue();
                //}
                ////Get value of OnHandStore15
                //if (ItemInventoryRet.OnHandStore15 != null)
                //{
                //int OnHandStore15116 = (int)ItemInventoryRet.OnHandStore15.GetValue();
                //}
                ////Get value of OnHandStore16
                //if (ItemInventoryRet.OnHandStore16 != null)
                //{
                //int OnHandStore16117 = (int)ItemInventoryRet.OnHandStore16.GetValue();
                //}
                ////Get value of OnHandStore17
                //if (ItemInventoryRet.OnHandStore17 != null)
                //{
                //int OnHandStore17118 = (int)ItemInventoryRet.OnHandStore17.GetValue();
                //}
                ////Get value of OnHandStore18
                //if (ItemInventoryRet.OnHandStore18 != null)
                //{
                //int OnHandStore18119 = (int)ItemInventoryRet.OnHandStore18.GetValue();
                //}
                ////Get value of OnHandStore19
                //if (ItemInventoryRet.OnHandStore19 != null)
                //{
                //int OnHandStore19120 = (int)ItemInventoryRet.OnHandStore19.GetValue();
                //}
                ////Get value of OnHandStore20
                //if (ItemInventoryRet.OnHandStore20 != null)
                //{
                //int OnHandStore20121 = (int)ItemInventoryRet.OnHandStore20.GetValue();
                //}
                ////Get value of ReorderPointStore01
                //if (ItemInventoryRet.ReorderPointStore01 != null)
                //{
                //int ReorderPointStore01122 = (int)ItemInventoryRet.ReorderPointStore01.GetValue();
                //}
                ////Get value of ReorderPointStore02
                //if (ItemInventoryRet.ReorderPointStore02 != null)
                //{
                //int ReorderPointStore02123 = (int)ItemInventoryRet.ReorderPointStore02.GetValue();
                //}
                ////Get value of ReorderPointStore03
                //if (ItemInventoryRet.ReorderPointStore03 != null)
                //{
                //int ReorderPointStore03124 = (int)ItemInventoryRet.ReorderPointStore03.GetValue();
                //}
                ////Get value of ReorderPointStore04
                //if (ItemInventoryRet.ReorderPointStore04 != null)
                //{
                //int ReorderPointStore04125 = (int)ItemInventoryRet.ReorderPointStore04.GetValue();
                //}
                ////Get value of ReorderPointStore05
                //if (ItemInventoryRet.ReorderPointStore05 != null)
                //{
                //int ReorderPointStore05126 = (int)ItemInventoryRet.ReorderPointStore05.GetValue();
                //}
                ////Get value of ReorderPointStore06
                //if (ItemInventoryRet.ReorderPointStore06 != null)
                //{
                //int ReorderPointStore06127 = (int)ItemInventoryRet.ReorderPointStore06.GetValue();
                //}
                ////Get value of ReorderPointStore07
                //if (ItemInventoryRet.ReorderPointStore07 != null)
                //{
                //int ReorderPointStore07128 = (int)ItemInventoryRet.ReorderPointStore07.GetValue();
                //}
                ////Get value of ReorderPointStore08
                //if (ItemInventoryRet.ReorderPointStore08 != null)
                //{
                //int ReorderPointStore08129 = (int)ItemInventoryRet.ReorderPointStore08.GetValue();
                //}
                ////Get value of ReorderPointStore09
                //if (ItemInventoryRet.ReorderPointStore09 != null)
                //{
                //int ReorderPointStore09130 = (int)ItemInventoryRet.ReorderPointStore09.GetValue();
                //}
                ////Get value of ReorderPointStore10
                //if (ItemInventoryRet.ReorderPointStore10 != null)
                //{
                //int ReorderPointStore10131 = (int)ItemInventoryRet.ReorderPointStore10.GetValue();
                //}
                ////Get value of ReorderPointStore11
                //if (ItemInventoryRet.ReorderPointStore11 != null)
                //{
                //int ReorderPointStore11132 = (int)ItemInventoryRet.ReorderPointStore11.GetValue();
                //}
                ////Get value of ReorderPointStore12
                //if (ItemInventoryRet.ReorderPointStore12 != null)
                //{
                //int ReorderPointStore12133 = (int)ItemInventoryRet.ReorderPointStore12.GetValue();
                //}
                ////Get value of ReorderPointStore13
                //if (ItemInventoryRet.ReorderPointStore13 != null)
                //{
                //int ReorderPointStore13134 = (int)ItemInventoryRet.ReorderPointStore13.GetValue();
                //}
                ////Get value of ReorderPointStore14
                //if (ItemInventoryRet.ReorderPointStore14 != null)
                //{
                //int ReorderPointStore14135 = (int)ItemInventoryRet.ReorderPointStore14.GetValue();
                //}
                ////Get value of ReorderPointStore15
                //if (ItemInventoryRet.ReorderPointStore15 != null)
                //{
                //int ReorderPointStore15136 = (int)ItemInventoryRet.ReorderPointStore15.GetValue();
                //}
                ////Get value of ReorderPointStore16
                //if (ItemInventoryRet.ReorderPointStore16 != null)
                //{
                //int ReorderPointStore16137 = (int)ItemInventoryRet.ReorderPointStore16.GetValue();
                //}
                ////Get value of ReorderPointStore17
                //if (ItemInventoryRet.ReorderPointStore17 != null)
                //{
                //int ReorderPointStore17138 = (int)ItemInventoryRet.ReorderPointStore17.GetValue();
                //}
                ////Get value of ReorderPointStore18
                //if (ItemInventoryRet.ReorderPointStore18 != null)
                //{
                //int ReorderPointStore18139 = (int)ItemInventoryRet.ReorderPointStore18.GetValue();
                //}
                ////Get value of ReorderPointStore19
                //if (ItemInventoryRet.ReorderPointStore19 != null)
                //{
                //int ReorderPointStore19140 = (int)ItemInventoryRet.ReorderPointStore19.GetValue();
                //}
                ////Get value of ReorderPointStore20
                //if (ItemInventoryRet.ReorderPointStore20 != null)
                //{
                //int ReorderPointStore20141 = (int)ItemInventoryRet.ReorderPointStore20.GetValue();
                //}
                ////Get value of OrderByUnit
                //if (ItemInventoryRet.OrderByUnit != null)
                //{
                //string OrderByUnit142 = (string)ItemInventoryRet.OrderByUnit.GetValue();
                //}
                ////Get value of OrderCost
                //if (ItemInventoryRet.OrderCost != null)
                //{
                //double OrderCost143 = (double)ItemInventoryRet.OrderCost.GetValue();
                //}
                ////Get value of Price1
                //if (ItemInventoryRet.Price1 != null)
                //{
                //double Price1144 = (double)ItemInventoryRet.Price1.GetValue();
                //}
                ////Get value of Price2
                //if (ItemInventoryRet.Price2 != null)
                //{
                //double Price2145 = (double)ItemInventoryRet.Price2.GetValue();
                //}
                ////Get value of Price3
                //if (ItemInventoryRet.Price3 != null)
                //{
                //double Price3146 = (double)ItemInventoryRet.Price3.GetValue();
                //}
                ////Get value of Price4
                //if (ItemInventoryRet.Price4 != null)
                //{
                //double Price4147 = (double)ItemInventoryRet.Price4.GetValue();
                //}
                ////Get value of Price5
                //if (ItemInventoryRet.Price5 != null)
                //{
                //double Price5148 = (double)ItemInventoryRet.Price5.GetValue();
                //}
                ////Get value of QuantityOnCustomerOrder
                //if (ItemInventoryRet.QuantityOnCustomerOrder != null)
                //{
                //int QuantityOnCustomerOrder149 = (int)ItemInventoryRet.QuantityOnCustomerOrder.GetValue();
                //}
                ////Get value of QuantityOnHand
                //if (ItemInventoryRet.QuantityOnHand != null)
                //{
                //int QuantityOnHand150 = (int)ItemInventoryRet.QuantityOnHand.GetValue();
                //}
                ////Get value of QuantityOnOrder
                //if (ItemInventoryRet.QuantityOnOrder != null)
                //{
                //int QuantityOnOrder151 = (int)ItemInventoryRet.QuantityOnOrder.GetValue();
                //}
                ////Get value of QuantityOnPendingOrder
                //if (ItemInventoryRet.QuantityOnPendingOrder != null)
                //{
                //int QuantityOnPendingOrder152 = (int)ItemInventoryRet.QuantityOnPendingOrder.GetValue();
                //}
                //if (ItemInventoryRet.AvailableQtyList != null)
                //{
                //for (int i153 = 0; i153 < ItemInventoryRet.AvailableQtyList.Count; i153++)
                //{
                //IAvailableQty AvailableQty = ItemInventoryRet.AvailableQtyList.GetAt(i153);
                ////Get value of StoreNumber
                //if (AvailableQty.StoreNumber != null)
                //{
                //int StoreNumber154 = (int)AvailableQty.StoreNumber.GetValue();
                //}
                ////Get value of QuantityOnOrder
                //if (AvailableQty.QuantityOnOrder != null)
                //{
                //int QuantityOnOrder155 = (int)AvailableQty.QuantityOnOrder.GetValue();
                //}
                ////Get value of QuantityOnCustomerOrder
                //if (AvailableQty.QuantityOnCustomerOrder != null)
                //{
                //int QuantityOnCustomerOrder156 = (int)AvailableQty.QuantityOnCustomerOrder.GetValue();
                //}
                ////Get value of QuantityOnPendingOrder
                //if (AvailableQty.QuantityOnPendingOrder != null)
                //{
                //int QuantityOnPendingOrder157 = (int)AvailableQty.QuantityOnPendingOrder.GetValue();
                //}
                //}
                //}
                ////Get value of ReorderPoint
                //if (ItemInventoryRet.ReorderPoint != null)
                //{
                //int ReorderPoint158 = (int)ItemInventoryRet.ReorderPoint.GetValue();
                //}
                ////Get value of SellByUnit
                //if (ItemInventoryRet.SellByUnit != null)
                //{
                //string SellByUnit159 = (string)ItemInventoryRet.SellByUnit.GetValue();
                //}
                ////Get value of SerialFlag
                //if (ItemInventoryRet.SerialFlag != null)
                //{
                //ENSerialFlag SerialFlag160 = (ENSerialFlag)ItemInventoryRet.SerialFlag.GetValue();
                //}

                ////Get value of StoreExchangeStatus
                //if (ItemInventoryRet.StoreExchangeStatus != null)
                //{
                //ENStoreExchangeStatus StoreExchangeStatus162 = (ENStoreExchangeStatus)ItemInventoryRet.StoreExchangeStatus.GetValue();
                //}
                ////Get value of TaxCode
                //if (ItemInventoryRet.TaxCode != null)
                //{
                //string TaxCode163 = (string)ItemInventoryRet.TaxCode.GetValue();
                //}
                ////Get value of UnitOfMeasure
                //if (ItemInventoryRet.UnitOfMeasure != null)
                //{
                //string UnitOfMeasure164 = (string)ItemInventoryRet.UnitOfMeasure.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.UPC != null)
                //{
                //string UPC165 = (string)ItemInventoryRet.UPC.GetValue();
                //}
                ////Get value of VendorCode
                //if (ItemInventoryRet.VendorCode != null)
                //{
                //string VendorCode166 = (string)ItemInventoryRet.VendorCode.GetValue();
                //}
                ////Get value of VendorListID
                //if (ItemInventoryRet.VendorListID != null)
                //{
                //string VendorListID167 = (string)ItemInventoryRet.VendorListID.GetValue();
                //}
                ////Get value of WebDesc
                //if (ItemInventoryRet.WebDesc != null)
                //{
                //string WebDesc168 = (string)ItemInventoryRet.WebDesc.GetValue();
                //}
                ////Get value of WebPrice
                //if (ItemInventoryRet.WebPrice != null)
                //{
                //double WebPrice169 = (double)ItemInventoryRet.WebPrice.GetValue();
                //}
                ////Get value of Manufacturer
                //if (ItemInventoryRet.Manufacturer != null)
                //{
                //string Manufacturer170 = (string)ItemInventoryRet.Manufacturer.GetValue();
                //}
                ////Get value of Weight
                //if (ItemInventoryRet.Weight != null)
                //{
                //IQBFloatType Weight171 = (IQBFloatType)ItemInventoryRet.Weight.GetValue();
                //}
                ////Get value of WebSKU
                //if (ItemInventoryRet.WebSKU != null)
                //{
                //string WebSKU172 = (string)ItemInventoryRet.WebSKU.GetValue();
                //}
                ////Get value of Keywords
                //if (ItemInventoryRet.Keywords != null)
                //{
                //string Keywords173 = (string)ItemInventoryRet.Keywords.GetValue();
                //}
                ////Get value of WebCategories
                //if (ItemInventoryRet.WebCategories != null)
                //{
                //string WebCategories174 = (string)ItemInventoryRet.WebCategories.GetValue();
                //}
                //if (ItemInventoryRet.UnitOfMeasure1 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.UnitOfMeasure1.ALU != null)
                //{
                //string ALU175 = (string)ItemInventoryRet.UnitOfMeasure1.ALU.GetValue();
                //}
                ////Get value of MSRP
                //if (ItemInventoryRet.UnitOfMeasure1.MSRP != null)
                //{
                //double MSRP176 = (double)ItemInventoryRet.UnitOfMeasure1.MSRP.GetValue();
                //}
                ////Get value of NumberOfBaseUnits
                //if (ItemInventoryRet.UnitOfMeasure1.NumberOfBaseUnits != null)
                //{
                //int NumberOfBaseUnits177 = (int)ItemInventoryRet.UnitOfMeasure1.NumberOfBaseUnits.GetValue();
                //}
                ////Get value of Price1
                //if (ItemInventoryRet.UnitOfMeasure1.Price1 != null)
                //{
                //double Price1178 = (double)ItemInventoryRet.UnitOfMeasure1.Price1.GetValue();
                //}
                ////Get value of Price2
                //if (ItemInventoryRet.UnitOfMeasure1.Price2 != null)
                //{
                //double Price2179 = (double)ItemInventoryRet.UnitOfMeasure1.Price2.GetValue();
                //}
                ////Get value of Price3
                //if (ItemInventoryRet.UnitOfMeasure1.Price3 != null)
                //{
                //double Price3180 = (double)ItemInventoryRet.UnitOfMeasure1.Price3.GetValue();
                //}
                ////Get value of Price4
                //if (ItemInventoryRet.UnitOfMeasure1.Price4 != null)
                //{
                //double Price4181 = (double)ItemInventoryRet.UnitOfMeasure1.Price4.GetValue();
                //}
                ////Get value of Price5
                //if (ItemInventoryRet.UnitOfMeasure1.Price5 != null)
                //{
                //double Price5182 = (double)ItemInventoryRet.UnitOfMeasure1.Price5.GetValue();
                //}
                ////Get value of UnitOfMeasure
                //if (ItemInventoryRet.UnitOfMeasure1.UnitOfMeasure != null)
                //{
                //string UnitOfMeasure183 = (string)ItemInventoryRet.UnitOfMeasure1.UnitOfMeasure.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.UnitOfMeasure1.UPC != null)
                //{
                //string UPC184 = (string)ItemInventoryRet.UnitOfMeasure1.UPC.GetValue();
                //}
                //}
                //if (ItemInventoryRet.UnitOfMeasure2 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.UnitOfMeasure2.ALU != null)
                //{
                //string ALU185 = (string)ItemInventoryRet.UnitOfMeasure2.ALU.GetValue();
                //}
                ////Get value of MSRP
                //if (ItemInventoryRet.UnitOfMeasure2.MSRP != null)
                //{
                //double MSRP186 = (double)ItemInventoryRet.UnitOfMeasure2.MSRP.GetValue();
                //}
                ////Get value of NumberOfBaseUnits
                //if (ItemInventoryRet.UnitOfMeasure2.NumberOfBaseUnits != null)
                //{
                //int NumberOfBaseUnits187 = (int)ItemInventoryRet.UnitOfMeasure2.NumberOfBaseUnits.GetValue();
                //}
                ////Get value of Price1
                //if (ItemInventoryRet.UnitOfMeasure2.Price1 != null)
                //{
                //double Price1188 = (double)ItemInventoryRet.UnitOfMeasure2.Price1.GetValue();
                //}
                ////Get value of Price2
                //if (ItemInventoryRet.UnitOfMeasure2.Price2 != null)
                //{
                //double Price2189 = (double)ItemInventoryRet.UnitOfMeasure2.Price2.GetValue();
                //}
                ////Get value of Price3
                //if (ItemInventoryRet.UnitOfMeasure2.Price3 != null)
                //{
                //double Price3190 = (double)ItemInventoryRet.UnitOfMeasure2.Price3.GetValue();
                //}
                ////Get value of Price4
                //if (ItemInventoryRet.UnitOfMeasure2.Price4 != null)
                //{
                //double Price4191 = (double)ItemInventoryRet.UnitOfMeasure2.Price4.GetValue();
                //}
                ////Get value of Price5
                //if (ItemInventoryRet.UnitOfMeasure2.Price5 != null)
                //{
                //double Price5192 = (double)ItemInventoryRet.UnitOfMeasure2.Price5.GetValue();
                //}
                ////Get value of UnitOfMeasure
                //if (ItemInventoryRet.UnitOfMeasure2.UnitOfMeasure != null)
                //{
                //string UnitOfMeasure193 = (string)ItemInventoryRet.UnitOfMeasure2.UnitOfMeasure.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.UnitOfMeasure2.UPC != null)
                //{
                //string UPC194 = (string)ItemInventoryRet.UnitOfMeasure2.UPC.GetValue();
                //}
                //}
                //if (ItemInventoryRet.UnitOfMeasure3 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.UnitOfMeasure3.ALU != null)
                //{
                //string ALU195 = (string)ItemInventoryRet.UnitOfMeasure3.ALU.GetValue();
                //}
                ////Get value of MSRP
                //if (ItemInventoryRet.UnitOfMeasure3.MSRP != null)
                //{
                //double MSRP196 = (double)ItemInventoryRet.UnitOfMeasure3.MSRP.GetValue();
                //}
                ////Get value of NumberOfBaseUnits
                //if (ItemInventoryRet.UnitOfMeasure3.NumberOfBaseUnits != null)
                //{
                //int NumberOfBaseUnits197 = (int)ItemInventoryRet.UnitOfMeasure3.NumberOfBaseUnits.GetValue();
                //}
                ////Get value of Price1
                //if (ItemInventoryRet.UnitOfMeasure3.Price1 != null)
                //{
                //double Price1198 = (double)ItemInventoryRet.UnitOfMeasure3.Price1.GetValue();
                //}
                ////Get value of Price2
                //if (ItemInventoryRet.UnitOfMeasure3.Price2 != null)
                //{
                //double Price2199 = (double)ItemInventoryRet.UnitOfMeasure3.Price2.GetValue();
                //}
                ////Get value of Price3
                //if (ItemInventoryRet.UnitOfMeasure3.Price3 != null)
                //{
                //double Price3200 = (double)ItemInventoryRet.UnitOfMeasure3.Price3.GetValue();
                //}
                ////Get value of Price4
                //if (ItemInventoryRet.UnitOfMeasure3.Price4 != null)
                //{
                //double Price4201 = (double)ItemInventoryRet.UnitOfMeasure3.Price4.GetValue();
                //}
                ////Get value of Price5
                //if (ItemInventoryRet.UnitOfMeasure3.Price5 != null)
                //{
                //double Price5202 = (double)ItemInventoryRet.UnitOfMeasure3.Price5.GetValue();
                //}
                ////Get value of UnitOfMeasure
                //if (ItemInventoryRet.UnitOfMeasure3.UnitOfMeasure != null)
                //{
                //string UnitOfMeasure203 = (string)ItemInventoryRet.UnitOfMeasure3.UnitOfMeasure.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.UnitOfMeasure3.UPC != null)
                //{
                //string UPC204 = (string)ItemInventoryRet.UnitOfMeasure3.UPC.GetValue();
                //}
                //}
                //if (ItemInventoryRet.VendorInfo2 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.VendorInfo2.ALU != null)
                //{
                //string ALU205 = (string)ItemInventoryRet.VendorInfo2.ALU.GetValue();
                //}
                ////Get value of OrderCost
                //if (ItemInventoryRet.VendorInfo2.OrderCost != null)
                //{
                //double OrderCost206 = (double)ItemInventoryRet.VendorInfo2.OrderCost.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.VendorInfo2.UPC != null)
                //{
                //string UPC207 = (string)ItemInventoryRet.VendorInfo2.UPC.GetValue();
                //}
                ////Get value of VendorListID
                //string VendorListID208 = (string)ItemInventoryRet.VendorInfo2.VendorListID.GetValue();
                //}
                //if (ItemInventoryRet.VendorInfo3 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.VendorInfo3.ALU != null)
                //{
                //string ALU209 = (string)ItemInventoryRet.VendorInfo3.ALU.GetValue();
                //}
                ////Get value of OrderCost
                //if (ItemInventoryRet.VendorInfo3.OrderCost != null)
                //{
                //double OrderCost210 = (double)ItemInventoryRet.VendorInfo3.OrderCost.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.VendorInfo3.UPC != null)
                //{
                //string UPC211 = (string)ItemInventoryRet.VendorInfo3.UPC.GetValue();
                //}
                ////Get value of VendorListID
                //string VendorListID212 = (string)ItemInventoryRet.VendorInfo3.VendorListID.GetValue();
                //}
                //if (ItemInventoryRet.VendorInfo4 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.VendorInfo4.ALU != null)
                //{
                //string ALU213 = (string)ItemInventoryRet.VendorInfo4.ALU.GetValue();
                //}
                ////Get value of OrderCost
                //if (ItemInventoryRet.VendorInfo4.OrderCost != null)
                //{
                //double OrderCost214 = (double)ItemInventoryRet.VendorInfo4.OrderCost.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.VendorInfo4.UPC != null)
                //{
                //string UPC215 = (string)ItemInventoryRet.VendorInfo4.UPC.GetValue();
                //}
                ////Get value of VendorListID
                //string VendorListID216 = (string)ItemInventoryRet.VendorInfo4.VendorListID.GetValue();
                //}
                //if (ItemInventoryRet.VendorInfo5 != null)
                //{
                ////Get value of ALU
                //if (ItemInventoryRet.VendorInfo5.ALU != null)
                //{
                //string ALU217 = (string)ItemInventoryRet.VendorInfo5.ALU.GetValue();
                //}
                ////Get value of OrderCost
                //if (ItemInventoryRet.VendorInfo5.OrderCost != null)
                //{
                //double OrderCost218 = (double)ItemInventoryRet.VendorInfo5.OrderCost.GetValue();
                //}
                ////Get value of UPC
                //if (ItemInventoryRet.VendorInfo5.UPC != null)
                //{
                //string UPC219 = (string)ItemInventoryRet.VendorInfo5.UPC.GetValue();
                //}
                ////Get value of VendorListID
                //string VendorListID220 = (string)ItemInventoryRet.VendorInfo5.VendorListID.GetValue();
                //}
                //if (ItemInventoryRet.DataExtRetList != null)
                //{
                //for (int i221 = 0; i221 < ItemInventoryRet.DataExtRetList.Count; i221++)
                //{
                //IDataExtRet DataExtRet = ItemInventoryRet.DataExtRetList.GetAt(i221);
                ////Get value of OwnerID
                //string OwnerID222 = (string)DataExtRet.OwnerID.GetValue();
                ////Get value of DataExtName
                //string DataExtName223 = (string)DataExtRet.DataExtName.GetValue();
                ////Get value of DataExtType
                //ENDataExtType DataExtType224 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                ////Get value of DataExtValue
                //string DataExtValue225 = (string)DataExtRet.DataExtValue.GetValue();
                //}
                //}
                #endregion

                itmList.Add(itm);
            }
            return itmList;
        }

    }
}
