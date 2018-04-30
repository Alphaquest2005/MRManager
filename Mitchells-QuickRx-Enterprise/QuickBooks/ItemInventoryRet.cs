
namespace POSAI_mvvm.QuickBooks
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRet")]
    public partial class ItemInventoryRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string listIDField;

        private System.DateTime timeCreatedField;

        private System.DateTime timeModifiedField;

        private string aLUField;

        private string attributeField;

        private string cOGSAccountField;

        private decimal costField;

        private string departmentCodeField;

        private string departmentListIDField;

        private string desc1Field;

        private string desc2Field;

        private string incomeAccountField;

        private bool isBelowReorderField;

        private bool isEligibleForCommissionField;

        private bool isPrintingTagsField;

        private bool isUnorderableField;

        private bool hasPicturesField;

        private bool isEligibleForRewardsField;

        private bool isWebItemField;

        private decimal itemNumberField;

        private string itemTypeField;

        private string lastReceivedField;

        private decimal marginPercentField;

        private decimal markupPercentField;

        private decimal mSRPField;

        private decimal onHandStore01Field;

        private decimal onHandStore02Field;

        private decimal onHandStore03Field;

        private decimal onHandStore04Field;

        private decimal onHandStore05Field;

        private decimal onHandStore06Field;

        private decimal onHandStore07Field;

        private decimal onHandStore08Field;

        private decimal onHandStore09Field;

        private decimal onHandStore10Field;

        private decimal onHandStore11Field;

        private decimal onHandStore12Field;

        private decimal onHandStore13Field;

        private decimal onHandStore14Field;

        private decimal onHandStore15Field;

        private decimal onHandStore16Field;

        private decimal onHandStore17Field;

        private decimal onHandStore18Field;

        private decimal onHandStore19Field;

        private decimal onHandStore20Field;

        private decimal reorderPointStore01Field;

        private decimal reorderPointStore02Field;

        private decimal reorderPointStore03Field;

        private decimal reorderPointStore04Field;

        private decimal reorderPointStore05Field;

        private decimal reorderPointStore06Field;

        private decimal reorderPointStore07Field;

        private decimal reorderPointStore08Field;

        private decimal reorderPointStore09Field;

        private decimal reorderPointStore10Field;

        private decimal reorderPointStore11Field;

        private decimal reorderPointStore12Field;

        private decimal reorderPointStore13Field;

        private decimal reorderPointStore14Field;

        private decimal reorderPointStore15Field;

        private decimal reorderPointStore16Field;

        private decimal reorderPointStore17Field;

        private decimal reorderPointStore18Field;

        private decimal reorderPointStore19Field;

        private decimal reorderPointStore20Field;

        private string orderByUnitField;

        private decimal orderCostField;

        private decimal price1Field;

        private decimal price2Field;

        private decimal price3Field;

        private decimal price4Field;

        private decimal price5Field;

        private decimal quantityOnCustomerOrderField;

        private decimal quantityOnHandField;

        private decimal quantityOnOrderField;

        private decimal quantityOnPendingOrderField;

        private ItemInventoryRetAvailableQty availableQtyField;

        private decimal reorderPointField;

        private string sellByUnitField;

        private string serialFlagField;

        private string sizeField;

        private string storeExchangeStatusField;

        private string taxCodeField;

        private string unitOfMeasureField;

        private string uPCField;

        private string vendorCodeField;

        private string vendorListIDField;

        private string webDescField;

        private decimal webPriceField;

        private string manufacturerField;

        private decimal weightField;

        private string webSKUField;

        private string keywordsField;

        private string webCategoriesField;

        private ItemInventoryRetUnitOfMeasure1 unitOfMeasure1Field;

        private ItemInventoryRetUnitOfMeasure2 unitOfMeasure2Field;

        private ItemInventoryRetUnitOfMeasure3 unitOfMeasure3Field;

        private ItemInventoryRetVendorInfo2 vendorInfo2Field;

        private ItemInventoryRetVendorInfo3 vendorInfo3Field;

        private ItemInventoryRetVendorInfo4 vendorInfo4Field;

        private ItemInventoryRetVendorInfo5 vendorInfo5Field;

        private ItemInventoryRetDataExtRet dataExtRetField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public ItemInventoryRet()
        {
            this.dataExtRetField = new ItemInventoryRetDataExtRet();
            this.vendorInfo5Field = new ItemInventoryRetVendorInfo5();
            this.vendorInfo4Field = new ItemInventoryRetVendorInfo4();
            this.vendorInfo3Field = new ItemInventoryRetVendorInfo3();
            this.vendorInfo2Field = new ItemInventoryRetVendorInfo2();
            this.unitOfMeasure3Field = new ItemInventoryRetUnitOfMeasure3();
            this.unitOfMeasure2Field = new ItemInventoryRetUnitOfMeasure2();
            this.unitOfMeasure1Field = new ItemInventoryRetUnitOfMeasure1();
            this.availableQtyField = new ItemInventoryRetAvailableQty();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ListID
        {
            get
            {
                return this.listIDField;
            }
            set
            {
                if (((this.listIDField == null)
                            || (listIDField.Equals(value) != true)))
                {
                    this.listIDField = value;
                    this.OnPropertyChanged("ListID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeCreated
        {
            get
            {
                return this.timeCreatedField;
            }
            set
            {
                if ((timeCreatedField.Equals(value) != true))
                {
                    this.timeCreatedField = value;
                    this.OnPropertyChanged("TimeCreated", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeModified
        {
            get
            {
                return this.timeModifiedField;
            }
            set
            {
                if ((timeModifiedField.Equals(value) != true))
                {
                    this.timeModifiedField = value;
                    this.OnPropertyChanged("TimeModified", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                if (((this.attributeField == null)
                            || (attributeField.Equals(value) != true)))
                {
                    this.attributeField = value;
                    this.OnPropertyChanged("Attribute", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string COGSAccount
        {
            get
            {
                return this.cOGSAccountField;
            }
            set
            {
                if (((this.cOGSAccountField == null)
                            || (cOGSAccountField.Equals(value) != true)))
                {
                    this.cOGSAccountField = value;
                    this.OnPropertyChanged("COGSAccount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                if ((costField.Equals(value) != true))
                {
                    this.costField = value;
                    this.OnPropertyChanged("Cost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DepartmentCode
        {
            get
            {
                return this.departmentCodeField;
            }
            set
            {
                if (((this.departmentCodeField == null)
                            || (departmentCodeField.Equals(value) != true)))
                {
                    this.departmentCodeField = value;
                    this.OnPropertyChanged("DepartmentCode", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DepartmentListID
        {
            get
            {
                return this.departmentListIDField;
            }
            set
            {
                if (((this.departmentListIDField == null)
                            || (departmentListIDField.Equals(value) != true)))
                {
                    this.departmentListIDField = value;
                    this.OnPropertyChanged("DepartmentListID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Desc1
        {
            get
            {
                return this.desc1Field;
            }
            set
            {
                if (((this.desc1Field == null)
                            || (desc1Field.Equals(value) != true)))
                {
                    this.desc1Field = value;
                    this.OnPropertyChanged("Desc1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Desc2
        {
            get
            {
                return this.desc2Field;
            }
            set
            {
                if (((this.desc2Field == null)
                            || (desc2Field.Equals(value) != true)))
                {
                    this.desc2Field = value;
                    this.OnPropertyChanged("Desc2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IncomeAccount
        {
            get
            {
                return this.incomeAccountField;
            }
            set
            {
                if (((this.incomeAccountField == null)
                            || (incomeAccountField.Equals(value) != true)))
                {
                    this.incomeAccountField = value;
                    this.OnPropertyChanged("IncomeAccount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsBelowReorder
        {
            get
            {
                return this.isBelowReorderField;
            }
            set
            {
                if ((isBelowReorderField.Equals(value) != true))
                {
                    this.isBelowReorderField = value;
                    this.OnPropertyChanged("IsBelowReorder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsEligibleForCommission
        {
            get
            {
                return this.isEligibleForCommissionField;
            }
            set
            {
                if ((isEligibleForCommissionField.Equals(value) != true))
                {
                    this.isEligibleForCommissionField = value;
                    this.OnPropertyChanged("IsEligibleForCommission", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsPrintingTags
        {
            get
            {
                return this.isPrintingTagsField;
            }
            set
            {
                if ((isPrintingTagsField.Equals(value) != true))
                {
                    this.isPrintingTagsField = value;
                    this.OnPropertyChanged("IsPrintingTags", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsUnorderable
        {
            get
            {
                return this.isUnorderableField;
            }
            set
            {
                if ((isUnorderableField.Equals(value) != true))
                {
                    this.isUnorderableField = value;
                    this.OnPropertyChanged("IsUnorderable", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasPictures
        {
            get
            {
                return this.hasPicturesField;
            }
            set
            {
                if ((hasPicturesField.Equals(value) != true))
                {
                    this.hasPicturesField = value;
                    this.OnPropertyChanged("HasPictures", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsEligibleForRewards
        {
            get
            {
                return this.isEligibleForRewardsField;
            }
            set
            {
                if ((isEligibleForRewardsField.Equals(value) != true))
                {
                    this.isEligibleForRewardsField = value;
                    this.OnPropertyChanged("IsEligibleForRewards", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsWebItem
        {
            get
            {
                return this.isWebItemField;
            }
            set
            {
                if ((isWebItemField.Equals(value) != true))
                {
                    this.isWebItemField = value;
                    this.OnPropertyChanged("IsWebItem", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ItemNumber
        {
            get
            {
                return this.itemNumberField;
            }
            set
            {
                if ((itemNumberField.Equals(value) != true))
                {
                    this.itemNumberField = value;
                    this.OnPropertyChanged("ItemNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ItemType
        {
            get
            {
                return this.itemTypeField;
            }
            set
            {
                if (((this.itemTypeField == null)
                            || (itemTypeField.Equals(value) != true)))
                {
                    this.itemTypeField = value;
                    this.OnPropertyChanged("ItemType", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastReceived
        {
            get
            {
                return this.lastReceivedField;
            }
            set
            {
                if (((this.lastReceivedField == null)
                            || (lastReceivedField.Equals(value) != true)))
                {
                    this.lastReceivedField = value;
                    this.OnPropertyChanged("LastReceived", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MarginPercent
        {
            get
            {
                return this.marginPercentField;
            }
            set
            {
                if ((marginPercentField.Equals(value) != true))
                {
                    this.marginPercentField = value;
                    this.OnPropertyChanged("MarginPercent", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MarkupPercent
        {
            get
            {
                return this.markupPercentField;
            }
            set
            {
                if ((markupPercentField.Equals(value) != true))
                {
                    this.markupPercentField = value;
                    this.OnPropertyChanged("MarkupPercent", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MSRP
        {
            get
            {
                return this.mSRPField;
            }
            set
            {
                if ((mSRPField.Equals(value) != true))
                {
                    this.mSRPField = value;
                    this.OnPropertyChanged("MSRP", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore01
        {
            get
            {
                return this.onHandStore01Field;
            }
            set
            {
                if ((onHandStore01Field.Equals(value) != true))
                {
                    this.onHandStore01Field = value;
                    this.OnPropertyChanged("OnHandStore01", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore02
        {
            get
            {
                return this.onHandStore02Field;
            }
            set
            {
                if ((onHandStore02Field.Equals(value) != true))
                {
                    this.onHandStore02Field = value;
                    this.OnPropertyChanged("OnHandStore02", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore03
        {
            get
            {
                return this.onHandStore03Field;
            }
            set
            {
                if ((onHandStore03Field.Equals(value) != true))
                {
                    this.onHandStore03Field = value;
                    this.OnPropertyChanged("OnHandStore03", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore04
        {
            get
            {
                return this.onHandStore04Field;
            }
            set
            {
                if ((onHandStore04Field.Equals(value) != true))
                {
                    this.onHandStore04Field = value;
                    this.OnPropertyChanged("OnHandStore04", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore05
        {
            get
            {
                return this.onHandStore05Field;
            }
            set
            {
                if ((onHandStore05Field.Equals(value) != true))
                {
                    this.onHandStore05Field = value;
                    this.OnPropertyChanged("OnHandStore05", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore06
        {
            get
            {
                return this.onHandStore06Field;
            }
            set
            {
                if ((onHandStore06Field.Equals(value) != true))
                {
                    this.onHandStore06Field = value;
                    this.OnPropertyChanged("OnHandStore06", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore07
        {
            get
            {
                return this.onHandStore07Field;
            }
            set
            {
                if ((onHandStore07Field.Equals(value) != true))
                {
                    this.onHandStore07Field = value;
                    this.OnPropertyChanged("OnHandStore07", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore08
        {
            get
            {
                return this.onHandStore08Field;
            }
            set
            {
                if ((onHandStore08Field.Equals(value) != true))
                {
                    this.onHandStore08Field = value;
                    this.OnPropertyChanged("OnHandStore08", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 33)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore09
        {
            get
            {
                return this.onHandStore09Field;
            }
            set
            {
                if ((onHandStore09Field.Equals(value) != true))
                {
                    this.onHandStore09Field = value;
                    this.OnPropertyChanged("OnHandStore09", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 34)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore10
        {
            get
            {
                return this.onHandStore10Field;
            }
            set
            {
                if ((onHandStore10Field.Equals(value) != true))
                {
                    this.onHandStore10Field = value;
                    this.OnPropertyChanged("OnHandStore10", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 35)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore11
        {
            get
            {
                return this.onHandStore11Field;
            }
            set
            {
                if ((onHandStore11Field.Equals(value) != true))
                {
                    this.onHandStore11Field = value;
                    this.OnPropertyChanged("OnHandStore11", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 36)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore12
        {
            get
            {
                return this.onHandStore12Field;
            }
            set
            {
                if ((onHandStore12Field.Equals(value) != true))
                {
                    this.onHandStore12Field = value;
                    this.OnPropertyChanged("OnHandStore12", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 37)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore13
        {
            get
            {
                return this.onHandStore13Field;
            }
            set
            {
                if ((onHandStore13Field.Equals(value) != true))
                {
                    this.onHandStore13Field = value;
                    this.OnPropertyChanged("OnHandStore13", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 38)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore14
        {
            get
            {
                return this.onHandStore14Field;
            }
            set
            {
                if ((onHandStore14Field.Equals(value) != true))
                {
                    this.onHandStore14Field = value;
                    this.OnPropertyChanged("OnHandStore14", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 39)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore15
        {
            get
            {
                return this.onHandStore15Field;
            }
            set
            {
                if ((onHandStore15Field.Equals(value) != true))
                {
                    this.onHandStore15Field = value;
                    this.OnPropertyChanged("OnHandStore15", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 40)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore16
        {
            get
            {
                return this.onHandStore16Field;
            }
            set
            {
                if ((onHandStore16Field.Equals(value) != true))
                {
                    this.onHandStore16Field = value;
                    this.OnPropertyChanged("OnHandStore16", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 41)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore17
        {
            get
            {
                return this.onHandStore17Field;
            }
            set
            {
                if ((onHandStore17Field.Equals(value) != true))
                {
                    this.onHandStore17Field = value;
                    this.OnPropertyChanged("OnHandStore17", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 42)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore18
        {
            get
            {
                return this.onHandStore18Field;
            }
            set
            {
                if ((onHandStore18Field.Equals(value) != true))
                {
                    this.onHandStore18Field = value;
                    this.OnPropertyChanged("OnHandStore18", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 43)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore19
        {
            get
            {
                return this.onHandStore19Field;
            }
            set
            {
                if ((onHandStore19Field.Equals(value) != true))
                {
                    this.onHandStore19Field = value;
                    this.OnPropertyChanged("OnHandStore19", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 44)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OnHandStore20
        {
            get
            {
                return this.onHandStore20Field;
            }
            set
            {
                if ((onHandStore20Field.Equals(value) != true))
                {
                    this.onHandStore20Field = value;
                    this.OnPropertyChanged("OnHandStore20", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 45)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore01
        {
            get
            {
                return this.reorderPointStore01Field;
            }
            set
            {
                if ((reorderPointStore01Field.Equals(value) != true))
                {
                    this.reorderPointStore01Field = value;
                    this.OnPropertyChanged("ReorderPointStore01", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 46)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore02
        {
            get
            {
                return this.reorderPointStore02Field;
            }
            set
            {
                if ((reorderPointStore02Field.Equals(value) != true))
                {
                    this.reorderPointStore02Field = value;
                    this.OnPropertyChanged("ReorderPointStore02", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 47)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore03
        {
            get
            {
                return this.reorderPointStore03Field;
            }
            set
            {
                if ((reorderPointStore03Field.Equals(value) != true))
                {
                    this.reorderPointStore03Field = value;
                    this.OnPropertyChanged("ReorderPointStore03", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 48)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore04
        {
            get
            {
                return this.reorderPointStore04Field;
            }
            set
            {
                if ((reorderPointStore04Field.Equals(value) != true))
                {
                    this.reorderPointStore04Field = value;
                    this.OnPropertyChanged("ReorderPointStore04", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 49)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore05
        {
            get
            {
                return this.reorderPointStore05Field;
            }
            set
            {
                if ((reorderPointStore05Field.Equals(value) != true))
                {
                    this.reorderPointStore05Field = value;
                    this.OnPropertyChanged("ReorderPointStore05", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 50)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore06
        {
            get
            {
                return this.reorderPointStore06Field;
            }
            set
            {
                if ((reorderPointStore06Field.Equals(value) != true))
                {
                    this.reorderPointStore06Field = value;
                    this.OnPropertyChanged("ReorderPointStore06", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 51)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore07
        {
            get
            {
                return this.reorderPointStore07Field;
            }
            set
            {
                if ((reorderPointStore07Field.Equals(value) != true))
                {
                    this.reorderPointStore07Field = value;
                    this.OnPropertyChanged("ReorderPointStore07", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 52)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore08
        {
            get
            {
                return this.reorderPointStore08Field;
            }
            set
            {
                if ((reorderPointStore08Field.Equals(value) != true))
                {
                    this.reorderPointStore08Field = value;
                    this.OnPropertyChanged("ReorderPointStore08", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 53)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore09
        {
            get
            {
                return this.reorderPointStore09Field;
            }
            set
            {
                if ((reorderPointStore09Field.Equals(value) != true))
                {
                    this.reorderPointStore09Field = value;
                    this.OnPropertyChanged("ReorderPointStore09", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 54)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore10
        {
            get
            {
                return this.reorderPointStore10Field;
            }
            set
            {
                if ((reorderPointStore10Field.Equals(value) != true))
                {
                    this.reorderPointStore10Field = value;
                    this.OnPropertyChanged("ReorderPointStore10", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 55)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore11
        {
            get
            {
                return this.reorderPointStore11Field;
            }
            set
            {
                if ((reorderPointStore11Field.Equals(value) != true))
                {
                    this.reorderPointStore11Field = value;
                    this.OnPropertyChanged("ReorderPointStore11", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 56)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore12
        {
            get
            {
                return this.reorderPointStore12Field;
            }
            set
            {
                if ((reorderPointStore12Field.Equals(value) != true))
                {
                    this.reorderPointStore12Field = value;
                    this.OnPropertyChanged("ReorderPointStore12", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 57)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore13
        {
            get
            {
                return this.reorderPointStore13Field;
            }
            set
            {
                if ((reorderPointStore13Field.Equals(value) != true))
                {
                    this.reorderPointStore13Field = value;
                    this.OnPropertyChanged("ReorderPointStore13", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 58)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore14
        {
            get
            {
                return this.reorderPointStore14Field;
            }
            set
            {
                if ((reorderPointStore14Field.Equals(value) != true))
                {
                    this.reorderPointStore14Field = value;
                    this.OnPropertyChanged("ReorderPointStore14", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 59)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore15
        {
            get
            {
                return this.reorderPointStore15Field;
            }
            set
            {
                if ((reorderPointStore15Field.Equals(value) != true))
                {
                    this.reorderPointStore15Field = value;
                    this.OnPropertyChanged("ReorderPointStore15", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 60)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore16
        {
            get
            {
                return this.reorderPointStore16Field;
            }
            set
            {
                if ((reorderPointStore16Field.Equals(value) != true))
                {
                    this.reorderPointStore16Field = value;
                    this.OnPropertyChanged("ReorderPointStore16", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 61)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore17
        {
            get
            {
                return this.reorderPointStore17Field;
            }
            set
            {
                if ((reorderPointStore17Field.Equals(value) != true))
                {
                    this.reorderPointStore17Field = value;
                    this.OnPropertyChanged("ReorderPointStore17", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 62)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore18
        {
            get
            {
                return this.reorderPointStore18Field;
            }
            set
            {
                if ((reorderPointStore18Field.Equals(value) != true))
                {
                    this.reorderPointStore18Field = value;
                    this.OnPropertyChanged("ReorderPointStore18", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 63)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore19
        {
            get
            {
                return this.reorderPointStore19Field;
            }
            set
            {
                if ((reorderPointStore19Field.Equals(value) != true))
                {
                    this.reorderPointStore19Field = value;
                    this.OnPropertyChanged("ReorderPointStore19", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 64)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPointStore20
        {
            get
            {
                return this.reorderPointStore20Field;
            }
            set
            {
                if ((reorderPointStore20Field.Equals(value) != true))
                {
                    this.reorderPointStore20Field = value;
                    this.OnPropertyChanged("ReorderPointStore20", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 65)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderByUnit
        {
            get
            {
                return this.orderByUnitField;
            }
            set
            {
                if (((this.orderByUnitField == null)
                            || (orderByUnitField.Equals(value) != true)))
                {
                    this.orderByUnitField = value;
                    this.OnPropertyChanged("OrderByUnit", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 66)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OrderCost
        {
            get
            {
                return this.orderCostField;
            }
            set
            {
                if ((orderCostField.Equals(value) != true))
                {
                    this.orderCostField = value;
                    this.OnPropertyChanged("OrderCost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 67)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price1
        {
            get
            {
                return this.price1Field;
            }
            set
            {
                if ((price1Field.Equals(value) != true))
                {
                    this.price1Field = value;
                    this.OnPropertyChanged("Price1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 68)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price2
        {
            get
            {
                return this.price2Field;
            }
            set
            {
                if ((price2Field.Equals(value) != true))
                {
                    this.price2Field = value;
                    this.OnPropertyChanged("Price2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 69)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price3
        {
            get
            {
                return this.price3Field;
            }
            set
            {
                if ((price3Field.Equals(value) != true))
                {
                    this.price3Field = value;
                    this.OnPropertyChanged("Price3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 70)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price4
        {
            get
            {
                return this.price4Field;
            }
            set
            {
                if ((price4Field.Equals(value) != true))
                {
                    this.price4Field = value;
                    this.OnPropertyChanged("Price4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 71)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price5
        {
            get
            {
                return this.price5Field;
            }
            set
            {
                if ((price5Field.Equals(value) != true))
                {
                    this.price5Field = value;
                    this.OnPropertyChanged("Price5", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 72)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnCustomerOrder
        {
            get
            {
                return this.quantityOnCustomerOrderField;
            }
            set
            {
                if ((quantityOnCustomerOrderField.Equals(value) != true))
                {
                    this.quantityOnCustomerOrderField = value;
                    this.OnPropertyChanged("QuantityOnCustomerOrder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 73)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnHand
        {
            get
            {
                return this.quantityOnHandField;
            }
            set
            {
                if ((quantityOnHandField.Equals(value) != true))
                {
                    this.quantityOnHandField = value;
                    this.OnPropertyChanged("QuantityOnHand", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 74)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnOrder
        {
            get
            {
                return this.quantityOnOrderField;
            }
            set
            {
                if ((quantityOnOrderField.Equals(value) != true))
                {
                    this.quantityOnOrderField = value;
                    this.OnPropertyChanged("QuantityOnOrder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 75)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnPendingOrder
        {
            get
            {
                return this.quantityOnPendingOrderField;
            }
            set
            {
                if ((quantityOnPendingOrderField.Equals(value) != true))
                {
                    this.quantityOnPendingOrderField = value;
                    this.OnPropertyChanged("QuantityOnPendingOrder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 76)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetAvailableQty AvailableQty
        {
            get
            {
                return this.availableQtyField;
            }
            set
            {
                if (((this.availableQtyField == null)
                            || (availableQtyField.Equals(value) != true)))
                {
                    this.availableQtyField = value;
                    this.OnPropertyChanged("AvailableQty", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 77)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ReorderPoint
        {
            get
            {
                return this.reorderPointField;
            }
            set
            {
                if ((reorderPointField.Equals(value) != true))
                {
                    this.reorderPointField = value;
                    this.OnPropertyChanged("ReorderPoint", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 78)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SellByUnit
        {
            get
            {
                return this.sellByUnitField;
            }
            set
            {
                if (((this.sellByUnitField == null)
                            || (sellByUnitField.Equals(value) != true)))
                {
                    this.sellByUnitField = value;
                    this.OnPropertyChanged("SellByUnit", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 79)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SerialFlag
        {
            get
            {
                return this.serialFlagField;
            }
            set
            {
                if (((this.serialFlagField == null)
                            || (serialFlagField.Equals(value) != true)))
                {
                    this.serialFlagField = value;
                    this.OnPropertyChanged("SerialFlag", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 80)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                if (((this.sizeField == null)
                            || (sizeField.Equals(value) != true)))
                {
                    this.sizeField = value;
                    this.OnPropertyChanged("Size", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 81)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StoreExchangeStatus
        {
            get
            {
                return this.storeExchangeStatusField;
            }
            set
            {
                if (((this.storeExchangeStatusField == null)
                            || (storeExchangeStatusField.Equals(value) != true)))
                {
                    this.storeExchangeStatusField = value;
                    this.OnPropertyChanged("StoreExchangeStatus", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 82)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TaxCode
        {
            get
            {
                return this.taxCodeField;
            }
            set
            {
                if (((this.taxCodeField == null)
                            || (taxCodeField.Equals(value) != true)))
                {
                    this.taxCodeField = value;
                    this.OnPropertyChanged("TaxCode", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 83)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                if (((this.unitOfMeasureField == null)
                            || (unitOfMeasureField.Equals(value) != true)))
                {
                    this.unitOfMeasureField = value;
                    this.OnPropertyChanged("UnitOfMeasure", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 84)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 85)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VendorCode
        {
            get
            {
                return this.vendorCodeField;
            }
            set
            {
                if (((this.vendorCodeField == null)
                            || (vendorCodeField.Equals(value) != true)))
                {
                    this.vendorCodeField = value;
                    this.OnPropertyChanged("VendorCode", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 86)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VendorListID
        {
            get
            {
                return this.vendorListIDField;
            }
            set
            {
                if (((this.vendorListIDField == null)
                            || (vendorListIDField.Equals(value) != true)))
                {
                    this.vendorListIDField = value;
                    this.OnPropertyChanged("VendorListID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 87)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebDesc
        {
            get
            {
                return this.webDescField;
            }
            set
            {
                if (((this.webDescField == null)
                            || (webDescField.Equals(value) != true)))
                {
                    this.webDescField = value;
                    this.OnPropertyChanged("WebDesc", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 88)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal WebPrice
        {
            get
            {
                return this.webPriceField;
            }
            set
            {
                if ((webPriceField.Equals(value) != true))
                {
                    this.webPriceField = value;
                    this.OnPropertyChanged("WebPrice", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 89)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                if (((this.manufacturerField == null)
                            || (manufacturerField.Equals(value) != true)))
                {
                    this.manufacturerField = value;
                    this.OnPropertyChanged("Manufacturer", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 90)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                if ((weightField.Equals(value) != true))
                {
                    this.weightField = value;
                    this.OnPropertyChanged("Weight", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 91)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebSKU
        {
            get
            {
                return this.webSKUField;
            }
            set
            {
                if (((this.webSKUField == null)
                            || (webSKUField.Equals(value) != true)))
                {
                    this.webSKUField = value;
                    this.OnPropertyChanged("WebSKU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 92)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keywords
        {
            get
            {
                return this.keywordsField;
            }
            set
            {
                if (((this.keywordsField == null)
                            || (keywordsField.Equals(value) != true)))
                {
                    this.keywordsField = value;
                    this.OnPropertyChanged("Keywords", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 93)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebCategories
        {
            get
            {
                return this.webCategoriesField;
            }
            set
            {
                if (((this.webCategoriesField == null)
                            || (webCategoriesField.Equals(value) != true)))
                {
                    this.webCategoriesField = value;
                    this.OnPropertyChanged("WebCategories", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 94)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetUnitOfMeasure1 UnitOfMeasure1
        {
            get
            {
                return this.unitOfMeasure1Field;
            }
            set
            {
                if (((this.unitOfMeasure1Field == null)
                            || (unitOfMeasure1Field.Equals(value) != true)))
                {
                    this.unitOfMeasure1Field = value;
                    this.OnPropertyChanged("UnitOfMeasure1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 95)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetUnitOfMeasure2 UnitOfMeasure2
        {
            get
            {
                return this.unitOfMeasure2Field;
            }
            set
            {
                if (((this.unitOfMeasure2Field == null)
                            || (unitOfMeasure2Field.Equals(value) != true)))
                {
                    this.unitOfMeasure2Field = value;
                    this.OnPropertyChanged("UnitOfMeasure2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 96)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetUnitOfMeasure3 UnitOfMeasure3
        {
            get
            {
                return this.unitOfMeasure3Field;
            }
            set
            {
                if (((this.unitOfMeasure3Field == null)
                            || (unitOfMeasure3Field.Equals(value) != true)))
                {
                    this.unitOfMeasure3Field = value;
                    this.OnPropertyChanged("UnitOfMeasure3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 97)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo2 VendorInfo2
        {
            get
            {
                return this.vendorInfo2Field;
            }
            set
            {
                if (((this.vendorInfo2Field == null)
                            || (vendorInfo2Field.Equals(value) != true)))
                {
                    this.vendorInfo2Field = value;
                    this.OnPropertyChanged("VendorInfo2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 98)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo3 VendorInfo3
        {
            get
            {
                return this.vendorInfo3Field;
            }
            set
            {
                if (((this.vendorInfo3Field == null)
                            || (vendorInfo3Field.Equals(value) != true)))
                {
                    this.vendorInfo3Field = value;
                    this.OnPropertyChanged("VendorInfo3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 99)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo4 VendorInfo4
        {
            get
            {
                return this.vendorInfo4Field;
            }
            set
            {
                if (((this.vendorInfo4Field == null)
                            || (vendorInfo4Field.Equals(value) != true)))
                {
                    this.vendorInfo4Field = value;
                    this.OnPropertyChanged("VendorInfo4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 100)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo5 VendorInfo5
        {
            get
            {
                return this.vendorInfo5Field;
            }
            set
            {
                if (((this.vendorInfo5Field == null)
                            || (vendorInfo5Field.Equals(value) != true)))
                {
                    this.vendorInfo5Field = value;
                    this.OnPropertyChanged("VendorInfo5", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 101)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetDataExtRet DataExtRet
        {
            get
            {
                return this.dataExtRetField;
            }
            set
            {
                if (((this.dataExtRetField == null)
                            || (dataExtRetField.Equals(value) != true)))
                {
                    this.dataExtRetField = value;
                    this.OnPropertyChanged("DataExtRet", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRet));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRet object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRet);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRet Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRet object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRet);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRet LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRet object
        /// </summary>
        public virtual ItemInventoryRet Clone()
        {
            return ((ItemInventoryRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetAvailableQty")]
    public partial class ItemInventoryRetAvailableQty : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal storeNumberField;

        private decimal quantityOnOrderField;

        private decimal quantityOnCustomerOrderField;

        private decimal quantityOnPendingOrderField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal StoreNumber
        {
            get
            {
                return this.storeNumberField;
            }
            set
            {
                if ((storeNumberField.Equals(value) != true))
                {
                    this.storeNumberField = value;
                    this.OnPropertyChanged("StoreNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnOrder
        {
            get
            {
                return this.quantityOnOrderField;
            }
            set
            {
                if ((quantityOnOrderField.Equals(value) != true))
                {
                    this.quantityOnOrderField = value;
                    this.OnPropertyChanged("QuantityOnOrder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnCustomerOrder
        {
            get
            {
                return this.quantityOnCustomerOrderField;
            }
            set
            {
                if ((quantityOnCustomerOrderField.Equals(value) != true))
                {
                    this.quantityOnCustomerOrderField = value;
                    this.OnPropertyChanged("QuantityOnCustomerOrder", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QuantityOnPendingOrder
        {
            get
            {
                return this.quantityOnPendingOrderField;
            }
            set
            {
                if ((quantityOnPendingOrderField.Equals(value) != true))
                {
                    this.quantityOnPendingOrderField = value;
                    this.OnPropertyChanged("QuantityOnPendingOrder", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetAvailableQty));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetAvailableQty object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetAvailableQty object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetAvailableQty object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetAvailableQty obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetAvailableQty);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetAvailableQty obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetAvailableQty Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetAvailableQty)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetAvailableQty Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetAvailableQty)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetAvailableQty object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetAvailableQty object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetAvailableQty object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetAvailableQty obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetAvailableQty);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetAvailableQty obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetAvailableQty obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetAvailableQty LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetAvailableQty LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetAvailableQty object
        /// </summary>
        public virtual ItemInventoryRetAvailableQty Clone()
        {
            return ((ItemInventoryRetAvailableQty)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetUnitOfMeasure1")]
    public partial class ItemInventoryRetUnitOfMeasure1 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal mSRPField;

        private decimal numberOfBaseUnitsField;

        private decimal price1Field;

        private decimal price2Field;

        private decimal price3Field;

        private decimal price4Field;

        private decimal price5Field;

        private string unitOfMeasureField;

        private string uPCField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MSRP
        {
            get
            {
                return this.mSRPField;
            }
            set
            {
                if ((mSRPField.Equals(value) != true))
                {
                    this.mSRPField = value;
                    this.OnPropertyChanged("MSRP", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal NumberOfBaseUnits
        {
            get
            {
                return this.numberOfBaseUnitsField;
            }
            set
            {
                if ((numberOfBaseUnitsField.Equals(value) != true))
                {
                    this.numberOfBaseUnitsField = value;
                    this.OnPropertyChanged("NumberOfBaseUnits", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price1
        {
            get
            {
                return this.price1Field;
            }
            set
            {
                if ((price1Field.Equals(value) != true))
                {
                    this.price1Field = value;
                    this.OnPropertyChanged("Price1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price2
        {
            get
            {
                return this.price2Field;
            }
            set
            {
                if ((price2Field.Equals(value) != true))
                {
                    this.price2Field = value;
                    this.OnPropertyChanged("Price2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price3
        {
            get
            {
                return this.price3Field;
            }
            set
            {
                if ((price3Field.Equals(value) != true))
                {
                    this.price3Field = value;
                    this.OnPropertyChanged("Price3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price4
        {
            get
            {
                return this.price4Field;
            }
            set
            {
                if ((price4Field.Equals(value) != true))
                {
                    this.price4Field = value;
                    this.OnPropertyChanged("Price4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price5
        {
            get
            {
                return this.price5Field;
            }
            set
            {
                if ((price5Field.Equals(value) != true))
                {
                    this.price5Field = value;
                    this.OnPropertyChanged("Price5", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                if (((this.unitOfMeasureField == null)
                            || (unitOfMeasureField.Equals(value) != true)))
                {
                    this.unitOfMeasureField = value;
                    this.OnPropertyChanged("UnitOfMeasure", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetUnitOfMeasure1));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure1 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetUnitOfMeasure1 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure1 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure1 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure1);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure1 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetUnitOfMeasure1 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetUnitOfMeasure1)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetUnitOfMeasure1 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetUnitOfMeasure1)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure1 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetUnitOfMeasure1 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure1 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetUnitOfMeasure1 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure1);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure1 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure1 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetUnitOfMeasure1 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetUnitOfMeasure1 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetUnitOfMeasure1 object
        /// </summary>
        public virtual ItemInventoryRetUnitOfMeasure1 Clone()
        {
            return ((ItemInventoryRetUnitOfMeasure1)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetUnitOfMeasure2")]
    public partial class ItemInventoryRetUnitOfMeasure2 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal mSRPField;

        private decimal numberOfBaseUnitsField;

        private decimal price1Field;

        private decimal price2Field;

        private decimal price3Field;

        private decimal price4Field;

        private decimal price5Field;

        private string unitOfMeasureField;

        private string uPCField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MSRP
        {
            get
            {
                return this.mSRPField;
            }
            set
            {
                if ((mSRPField.Equals(value) != true))
                {
                    this.mSRPField = value;
                    this.OnPropertyChanged("MSRP", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal NumberOfBaseUnits
        {
            get
            {
                return this.numberOfBaseUnitsField;
            }
            set
            {
                if ((numberOfBaseUnitsField.Equals(value) != true))
                {
                    this.numberOfBaseUnitsField = value;
                    this.OnPropertyChanged("NumberOfBaseUnits", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price1
        {
            get
            {
                return this.price1Field;
            }
            set
            {
                if ((price1Field.Equals(value) != true))
                {
                    this.price1Field = value;
                    this.OnPropertyChanged("Price1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price2
        {
            get
            {
                return this.price2Field;
            }
            set
            {
                if ((price2Field.Equals(value) != true))
                {
                    this.price2Field = value;
                    this.OnPropertyChanged("Price2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price3
        {
            get
            {
                return this.price3Field;
            }
            set
            {
                if ((price3Field.Equals(value) != true))
                {
                    this.price3Field = value;
                    this.OnPropertyChanged("Price3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price4
        {
            get
            {
                return this.price4Field;
            }
            set
            {
                if ((price4Field.Equals(value) != true))
                {
                    this.price4Field = value;
                    this.OnPropertyChanged("Price4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price5
        {
            get
            {
                return this.price5Field;
            }
            set
            {
                if ((price5Field.Equals(value) != true))
                {
                    this.price5Field = value;
                    this.OnPropertyChanged("Price5", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                if (((this.unitOfMeasureField == null)
                            || (unitOfMeasureField.Equals(value) != true)))
                {
                    this.unitOfMeasureField = value;
                    this.OnPropertyChanged("UnitOfMeasure", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetUnitOfMeasure2));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure2 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetUnitOfMeasure2 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure2 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure2 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure2);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure2 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetUnitOfMeasure2 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetUnitOfMeasure2)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetUnitOfMeasure2 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetUnitOfMeasure2)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure2 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetUnitOfMeasure2 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure2 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetUnitOfMeasure2 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure2);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure2 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure2 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetUnitOfMeasure2 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetUnitOfMeasure2 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetUnitOfMeasure2 object
        /// </summary>
        public virtual ItemInventoryRetUnitOfMeasure2 Clone()
        {
            return ((ItemInventoryRetUnitOfMeasure2)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetUnitOfMeasure3")]
    public partial class ItemInventoryRetUnitOfMeasure3 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal mSRPField;

        private decimal numberOfBaseUnitsField;

        private decimal price1Field;

        private decimal price2Field;

        private decimal price3Field;

        private decimal price4Field;

        private decimal price5Field;

        private string unitOfMeasureField;

        private string uPCField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MSRP
        {
            get
            {
                return this.mSRPField;
            }
            set
            {
                if ((mSRPField.Equals(value) != true))
                {
                    this.mSRPField = value;
                    this.OnPropertyChanged("MSRP", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal NumberOfBaseUnits
        {
            get
            {
                return this.numberOfBaseUnitsField;
            }
            set
            {
                if ((numberOfBaseUnitsField.Equals(value) != true))
                {
                    this.numberOfBaseUnitsField = value;
                    this.OnPropertyChanged("NumberOfBaseUnits", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price1
        {
            get
            {
                return this.price1Field;
            }
            set
            {
                if ((price1Field.Equals(value) != true))
                {
                    this.price1Field = value;
                    this.OnPropertyChanged("Price1", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price2
        {
            get
            {
                return this.price2Field;
            }
            set
            {
                if ((price2Field.Equals(value) != true))
                {
                    this.price2Field = value;
                    this.OnPropertyChanged("Price2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price3
        {
            get
            {
                return this.price3Field;
            }
            set
            {
                if ((price3Field.Equals(value) != true))
                {
                    this.price3Field = value;
                    this.OnPropertyChanged("Price3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price4
        {
            get
            {
                return this.price4Field;
            }
            set
            {
                if ((price4Field.Equals(value) != true))
                {
                    this.price4Field = value;
                    this.OnPropertyChanged("Price4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price5
        {
            get
            {
                return this.price5Field;
            }
            set
            {
                if ((price5Field.Equals(value) != true))
                {
                    this.price5Field = value;
                    this.OnPropertyChanged("Price5", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                if (((this.unitOfMeasureField == null)
                            || (unitOfMeasureField.Equals(value) != true)))
                {
                    this.unitOfMeasureField = value;
                    this.OnPropertyChanged("UnitOfMeasure", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetUnitOfMeasure3));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure3 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetUnitOfMeasure3 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure3 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure3 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure3);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetUnitOfMeasure3 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetUnitOfMeasure3 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetUnitOfMeasure3)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetUnitOfMeasure3 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetUnitOfMeasure3)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetUnitOfMeasure3 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetUnitOfMeasure3 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetUnitOfMeasure3 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetUnitOfMeasure3 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetUnitOfMeasure3);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure3 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetUnitOfMeasure3 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetUnitOfMeasure3 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetUnitOfMeasure3 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetUnitOfMeasure3 object
        /// </summary>
        public virtual ItemInventoryRetUnitOfMeasure3 Clone()
        {
            return ((ItemInventoryRetUnitOfMeasure3)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo2")]
    public partial class ItemInventoryRetVendorInfo2 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal orderCostField;

        private string uPCField;

        private ItemInventoryRetVendorInfo2VendorListID vendorListIDField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public ItemInventoryRetVendorInfo2()
        {
            this.vendorListIDField = new ItemInventoryRetVendorInfo2VendorListID();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OrderCost
        {
            get
            {
                return this.orderCostField;
            }
            set
            {
                if ((orderCostField.Equals(value) != true))
                {
                    this.orderCostField = value;
                    this.OnPropertyChanged("OrderCost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo2VendorListID VendorListID
        {
            get
            {
                return this.vendorListIDField;
            }
            set
            {
                if (((this.vendorListIDField == null)
                            || (vendorListIDField.Equals(value) != true)))
                {
                    this.vendorListIDField = value;
                    this.OnPropertyChanged("VendorListID", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo2));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo2 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo2 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo2 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo2 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo2);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo2 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo2 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo2)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo2 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo2)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo2 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo2 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo2 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo2 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo2);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo2 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo2 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo2 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo2 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo2 object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo2 Clone()
        {
            return ((ItemInventoryRetVendorInfo2)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo2VendorListID")]
    public partial class ItemInventoryRetVendorInfo2VendorListID : System.ComponentModel.INotifyPropertyChanged
    {

        private string useMacroField;

        private string valueField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string useMacro
        {
            get
            {
                return this.useMacroField;
            }
            set
            {
                if (((this.useMacroField == null)
                            || (useMacroField.Equals(value) != true)))
                {
                    this.useMacroField = value;
                    this.OnPropertyChanged("useMacro", value);
                }
            }
        }

        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                if (((this.valueField == null)
                            || (valueField.Equals(value) != true)))
                {
                    this.valueField = value;
                    this.OnPropertyChanged("Value", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo2VendorListID));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo2VendorListID object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo2VendorListID object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo2VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo2VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo2VendorListID);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo2VendorListID obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo2VendorListID Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo2VendorListID)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo2VendorListID Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo2VendorListID)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo2VendorListID object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo2VendorListID object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo2VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo2VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo2VendorListID);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo2VendorListID obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo2VendorListID obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo2VendorListID LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo2VendorListID LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo2VendorListID object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo2VendorListID Clone()
        {
            return ((ItemInventoryRetVendorInfo2VendorListID)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo3")]
    public partial class ItemInventoryRetVendorInfo3 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal orderCostField;

        private string uPCField;

        private ItemInventoryRetVendorInfo3VendorListID vendorListIDField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public ItemInventoryRetVendorInfo3()
        {
            this.vendorListIDField = new ItemInventoryRetVendorInfo3VendorListID();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OrderCost
        {
            get
            {
                return this.orderCostField;
            }
            set
            {
                if ((orderCostField.Equals(value) != true))
                {
                    this.orderCostField = value;
                    this.OnPropertyChanged("OrderCost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo3VendorListID VendorListID
        {
            get
            {
                return this.vendorListIDField;
            }
            set
            {
                if (((this.vendorListIDField == null)
                            || (vendorListIDField.Equals(value) != true)))
                {
                    this.vendorListIDField = value;
                    this.OnPropertyChanged("VendorListID", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo3));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo3 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo3 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo3 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo3 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo3);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo3 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo3 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo3)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo3 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo3)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo3 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo3 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo3 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo3 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo3);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo3 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo3 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo3 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo3 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo3 object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo3 Clone()
        {
            return ((ItemInventoryRetVendorInfo3)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo3VendorListID")]
    public partial class ItemInventoryRetVendorInfo3VendorListID : System.ComponentModel.INotifyPropertyChanged
    {

        private string useMacroField;

        private string valueField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string useMacro
        {
            get
            {
                return this.useMacroField;
            }
            set
            {
                if (((this.useMacroField == null)
                            || (useMacroField.Equals(value) != true)))
                {
                    this.useMacroField = value;
                    this.OnPropertyChanged("useMacro", value);
                }
            }
        }

        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                if (((this.valueField == null)
                            || (valueField.Equals(value) != true)))
                {
                    this.valueField = value;
                    this.OnPropertyChanged("Value", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo3VendorListID));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo3VendorListID object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo3VendorListID object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo3VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo3VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo3VendorListID);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo3VendorListID obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo3VendorListID Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo3VendorListID)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo3VendorListID Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo3VendorListID)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo3VendorListID object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo3VendorListID object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo3VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo3VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo3VendorListID);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo3VendorListID obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo3VendorListID obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo3VendorListID LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo3VendorListID LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo3VendorListID object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo3VendorListID Clone()
        {
            return ((ItemInventoryRetVendorInfo3VendorListID)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo4")]
    public partial class ItemInventoryRetVendorInfo4 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal orderCostField;

        private string uPCField;

        private ItemInventoryRetVendorInfo4VendorListID vendorListIDField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public ItemInventoryRetVendorInfo4()
        {
            this.vendorListIDField = new ItemInventoryRetVendorInfo4VendorListID();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OrderCost
        {
            get
            {
                return this.orderCostField;
            }
            set
            {
                if ((orderCostField.Equals(value) != true))
                {
                    this.orderCostField = value;
                    this.OnPropertyChanged("OrderCost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo4VendorListID VendorListID
        {
            get
            {
                return this.vendorListIDField;
            }
            set
            {
                if (((this.vendorListIDField == null)
                            || (vendorListIDField.Equals(value) != true)))
                {
                    this.vendorListIDField = value;
                    this.OnPropertyChanged("VendorListID", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo4));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo4 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo4 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo4 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo4 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo4);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo4 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo4 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo4)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo4 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo4)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo4 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo4 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo4 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo4 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo4);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo4 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo4 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo4 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo4 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo4 object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo4 Clone()
        {
            return ((ItemInventoryRetVendorInfo4)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo4VendorListID")]
    public partial class ItemInventoryRetVendorInfo4VendorListID : System.ComponentModel.INotifyPropertyChanged
    {

        private string useMacroField;

        private string valueField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string useMacro
        {
            get
            {
                return this.useMacroField;
            }
            set
            {
                if (((this.useMacroField == null)
                            || (useMacroField.Equals(value) != true)))
                {
                    this.useMacroField = value;
                    this.OnPropertyChanged("useMacro", value);
                }
            }
        }

        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                if (((this.valueField == null)
                            || (valueField.Equals(value) != true)))
                {
                    this.valueField = value;
                    this.OnPropertyChanged("Value", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo4VendorListID));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo4VendorListID object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo4VendorListID object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo4VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo4VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo4VendorListID);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo4VendorListID obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo4VendorListID Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo4VendorListID)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo4VendorListID Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo4VendorListID)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo4VendorListID object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo4VendorListID object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo4VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo4VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo4VendorListID);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo4VendorListID obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo4VendorListID obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo4VendorListID LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo4VendorListID LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo4VendorListID object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo4VendorListID Clone()
        {
            return ((ItemInventoryRetVendorInfo4VendorListID)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo5")]
    public partial class ItemInventoryRetVendorInfo5 : System.ComponentModel.INotifyPropertyChanged
    {

        private string aLUField;

        private decimal orderCostField;

        private string uPCField;

        private ItemInventoryRetVendorInfo5VendorListID vendorListIDField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public ItemInventoryRetVendorInfo5()
        {
            this.vendorListIDField = new ItemInventoryRetVendorInfo5VendorListID();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ALU
        {
            get
            {
                return this.aLUField;
            }
            set
            {
                if (((this.aLUField == null)
                            || (aLUField.Equals(value) != true)))
                {
                    this.aLUField = value;
                    this.OnPropertyChanged("ALU", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OrderCost
        {
            get
            {
                return this.orderCostField;
            }
            set
            {
                if ((orderCostField.Equals(value) != true))
                {
                    this.orderCostField = value;
                    this.OnPropertyChanged("OrderCost", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UPC
        {
            get
            {
                return this.uPCField;
            }
            set
            {
                if (((this.uPCField == null)
                            || (uPCField.Equals(value) != true)))
                {
                    this.uPCField = value;
                    this.OnPropertyChanged("UPC", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ItemInventoryRetVendorInfo5VendorListID VendorListID
        {
            get
            {
                return this.vendorListIDField;
            }
            set
            {
                if (((this.vendorListIDField == null)
                            || (vendorListIDField.Equals(value) != true)))
                {
                    this.vendorListIDField = value;
                    this.OnPropertyChanged("VendorListID", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo5));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo5 object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo5 object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo5 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo5 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo5);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo5 obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo5 Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo5)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo5 Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo5)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo5 object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo5 object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo5 object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo5 obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo5);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo5 obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo5 obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo5 LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo5 LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo5 object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo5 Clone()
        {
            return ((ItemInventoryRetVendorInfo5)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetVendorInfo5VendorListID")]
    public partial class ItemInventoryRetVendorInfo5VendorListID : System.ComponentModel.INotifyPropertyChanged
    {

        private string useMacroField;

        private string valueField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string useMacro
        {
            get
            {
                return this.useMacroField;
            }
            set
            {
                if (((this.useMacroField == null)
                            || (useMacroField.Equals(value) != true)))
                {
                    this.useMacroField = value;
                    this.OnPropertyChanged("useMacro", value);
                }
            }
        }

        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                if (((this.valueField == null)
                            || (valueField.Equals(value) != true)))
                {
                    this.valueField = value;
                    this.OnPropertyChanged("Value", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetVendorInfo5VendorListID));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo5VendorListID object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetVendorInfo5VendorListID object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo5VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo5VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo5VendorListID);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetVendorInfo5VendorListID obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetVendorInfo5VendorListID Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetVendorInfo5VendorListID)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetVendorInfo5VendorListID Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetVendorInfo5VendorListID)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetVendorInfo5VendorListID object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetVendorInfo5VendorListID object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetVendorInfo5VendorListID object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetVendorInfo5VendorListID obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetVendorInfo5VendorListID);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo5VendorListID obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetVendorInfo5VendorListID obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetVendorInfo5VendorListID LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetVendorInfo5VendorListID LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetVendorInfo5VendorListID object
        /// </summary>
        public virtual ItemInventoryRetVendorInfo5VendorListID Clone()
        {
            return ((ItemInventoryRetVendorInfo5VendorListID)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemInventoryRetDataExtRet")]
    public partial class ItemInventoryRetDataExtRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string ownerIDField;

        private string dataExtNameField;

        private string dataExtTypeField;

        private string dataExtValueField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OwnerID
        {
            get
            {
                return this.ownerIDField;
            }
            set
            {
                if (((this.ownerIDField == null)
                            || (ownerIDField.Equals(value) != true)))
                {
                    this.ownerIDField = value;
                    this.OnPropertyChanged("OwnerID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DataExtName
        {
            get
            {
                return this.dataExtNameField;
            }
            set
            {
                if (((this.dataExtNameField == null)
                            || (dataExtNameField.Equals(value) != true)))
                {
                    this.dataExtNameField = value;
                    this.OnPropertyChanged("DataExtName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DataExtType
        {
            get
            {
                return this.dataExtTypeField;
            }
            set
            {
                if (((this.dataExtTypeField == null)
                            || (dataExtTypeField.Equals(value) != true)))
                {
                    this.dataExtTypeField = value;
                    this.OnPropertyChanged("DataExtType", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DataExtValue
        {
            get
            {
                return this.dataExtValueField;
            }
            set
            {
                if (((this.dataExtValueField == null)
                            || (dataExtValueField.Equals(value) != true)))
                {
                    this.dataExtValueField = value;
                    this.OnPropertyChanged("DataExtValue", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemInventoryRetDataExtRet));
                }
                return serializer;
            }
        }

        [XmlIgnore()]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if ((this.changeTrackerField == null))
                {
                    this.changeTrackerField = new ObjectChangeTracker(this);
                }
                return changeTrackerField;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName, object value)
        {
            this.ChangeTracker.RecordCurrentValue(propertyName, value);
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current ItemInventoryRetDataExtRet object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an ItemInventoryRetDataExtRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output ItemInventoryRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out ItemInventoryRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetDataExtRet);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out ItemInventoryRetDataExtRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static ItemInventoryRetDataExtRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((ItemInventoryRetDataExtRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ItemInventoryRetDataExtRet Deserialize(System.IO.Stream s)
        {
            return ((ItemInventoryRetDataExtRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current ItemInventoryRetDataExtRet object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an ItemInventoryRetDataExtRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output ItemInventoryRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out ItemInventoryRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(ItemInventoryRetDataExtRet);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetDataExtRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out ItemInventoryRetDataExtRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ItemInventoryRetDataExtRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static ItemInventoryRetDataExtRet LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this ItemInventoryRetDataExtRet object
        /// </summary>
        public virtual ItemInventoryRetDataExtRet Clone()
        {
            return ((ItemInventoryRetDataExtRet)(this.MemberwiseClone()));
        }
        #endregion
    }
}
