

namespace POSAI_mvvm
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
    using POSAI_mvvm.QuickBooks;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "InventoryQtyAdjustmentRet")]
    public partial class InventoryQtyAdjustmentRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string txnIDField;

        private System.DateTime timeCreatedField;

        private System.DateTime timeModifiedField;

        private string associateField;

        private string commentsField;

        private decimal costDifferenceField;

        private string historyDocStatusField;

        private int inventoryAdjustmentNumberField;

        private string inventoryAdjustmentSourceField;

        private int itemsCountField;

        private decimal newQuantityField;

        private decimal oldQuantityField;

        private decimal qtyDifferenceField;

        private string quickBooksFlagField;

        private string reasonField;

        private string storeExchangeStatusField;

        private int storeNumberField;

        private System.DateTime txnDateField;

        private string txnStateField;

        private int workstationField;

        private TrackableCollection<InventoryQtyAdjustmentItemRet> inventoryQtyAdjustmentItemRetField;

        private InventoryQtyAdjustmentRetDataExtRet dataExtRetField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public InventoryQtyAdjustmentRet()
        {
            this.dataExtRetField = new InventoryQtyAdjustmentRetDataExtRet();
            this.inventoryQtyAdjustmentItemRetField = new TrackableCollection<POSAI_mvvm.InventoryQtyAdjustmentItemRet>(null);
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TxnID
        {
            get
            {
                return this.txnIDField;
            }
            set
            {
                if (((this.txnIDField == null)
                            || (txnIDField.Equals(value) != true)))
                {
                    this.txnIDField = value;
                    this.OnPropertyChanged("TxnID", value);
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
        public string Associate
        {
            get
            {
                return this.associateField;
            }
            set
            {
                if (((this.associateField == null)
                            || (associateField.Equals(value) != true)))
                {
                    this.associateField = value;
                    this.OnPropertyChanged("Associate", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                if (((this.commentsField == null)
                            || (commentsField.Equals(value) != true)))
                {
                    this.commentsField = value;
                    this.OnPropertyChanged("Comments", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CostDifference
        {
            get
            {
                return this.costDifferenceField;
            }
            set
            {
                if ((costDifferenceField.Equals(value) != true))
                {
                    this.costDifferenceField = value;
                    this.OnPropertyChanged("CostDifference", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HistoryDocStatus
        {
            get
            {
                return this.historyDocStatusField;
            }
            set
            {
                if (((this.historyDocStatusField == null)
                            || (historyDocStatusField.Equals(value) != true)))
                {
                    this.historyDocStatusField = value;
                    this.OnPropertyChanged("HistoryDocStatus", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int InventoryAdjustmentNumber
        {
            get
            {
                return this.inventoryAdjustmentNumberField;
            }
            set
            {
                if ((inventoryAdjustmentNumberField.Equals(value) != true))
                {
                    this.inventoryAdjustmentNumberField = value;
                    this.OnPropertyChanged("InventoryAdjustmentNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InventoryAdjustmentSource
        {
            get
            {
                return this.inventoryAdjustmentSourceField;
            }
            set
            {
                if (((this.inventoryAdjustmentSourceField == null)
                            || (inventoryAdjustmentSourceField.Equals(value) != true)))
                {
                    this.inventoryAdjustmentSourceField = value;
                    this.OnPropertyChanged("InventoryAdjustmentSource", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ItemsCount
        {
            get
            {
                return this.itemsCountField;
            }
            set
            {
                if (((this.itemsCountField == null)
                            || (itemsCountField.Equals(value) != true)))
                {
                    this.itemsCountField = value;
                    this.OnPropertyChanged("ItemsCount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal NewQuantity
        {
            get
            {
                return this.newQuantityField;
            }
            set
            {
                if ((newQuantityField.Equals(value) != true))
                {
                    this.newQuantityField = value;
                    this.OnPropertyChanged("NewQuantity", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal OldQuantity
        {
            get
            {
                return this.oldQuantityField;
            }
            set
            {
                if ((oldQuantityField.Equals(value) != true))
                {
                    this.oldQuantityField = value;
                    this.OnPropertyChanged("OldQuantity", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QtyDifference
        {
            get
            {
                return this.qtyDifferenceField;
            }
            set
            {
                if ((qtyDifferenceField.Equals(value) != true))
                {
                    this.qtyDifferenceField = value;
                    this.OnPropertyChanged("QtyDifference", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string QuickBooksFlag
        {
            get
            {
                return this.quickBooksFlagField;
            }
            set
            {
                if (((this.quickBooksFlagField == null)
                            || (quickBooksFlagField.Equals(value) != true)))
                {
                    this.quickBooksFlagField = value;
                    this.OnPropertyChanged("QuickBooksFlag", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                if (((this.reasonField == null)
                            || (reasonField.Equals(value) != true)))
                {
                    this.reasonField = value;
                    this.OnPropertyChanged("Reason", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StoreNumber
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

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 17)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TxnDate
        {
            get
            {
                return this.txnDateField;
            }
            set
            {
                if ((txnDateField.Equals(value) != true))
                {
                    this.txnDateField = value;
                    this.OnPropertyChanged("TxnDate", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TxnState
        {
            get
            {
                return this.txnStateField;
            }
            set
            {
                if (((this.txnStateField == null)
                            || (txnStateField.Equals(value) != true)))
                {
                    this.txnStateField = value;
                    this.OnPropertyChanged("TxnState", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Workstation
        {
            get
            {
                return this.workstationField;
            }
            set
            {
                if ((workstationField.Equals(value) != true))
                {
                    this.workstationField = value;
                    this.OnPropertyChanged("Workstation", value);
                }
            }
        }




        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TrackableCollection<InventoryQtyAdjustmentItemRet> InventoryQtyAdjustmentItemRet
        {
            get
            {
                return this.inventoryQtyAdjustmentItemRetField;
            }
            set
            {
                if (((this.inventoryQtyAdjustmentItemRetField == null)
                            || (inventoryQtyAdjustmentItemRetField.Equals(value) != true)))
                {
                    this.inventoryQtyAdjustmentItemRetField = value;
                    this.OnPropertyChanged("InventoryQtyAdjustmentItemRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public InventoryQtyAdjustmentRetDataExtRet DataExtRet
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(InventoryQtyAdjustmentRet));
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
        /// Serializes current InventoryQtyAdjustmentRet object into an XML document
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
        /// Deserializes workflow markup into an InventoryQtyAdjustmentRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out InventoryQtyAdjustmentRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentRet);
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

        public static bool Deserialize(string xml, out InventoryQtyAdjustmentRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static InventoryQtyAdjustmentRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((InventoryQtyAdjustmentRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static InventoryQtyAdjustmentRet Deserialize(System.IO.Stream s)
        {
            return ((InventoryQtyAdjustmentRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current InventoryQtyAdjustmentRet object into file
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
        /// Deserializes xml markup from file into an InventoryQtyAdjustmentRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out InventoryQtyAdjustmentRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentRet);
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

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static InventoryQtyAdjustmentRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static InventoryQtyAdjustmentRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this InventoryQtyAdjustmentRet object
        /// </summary>
        public virtual InventoryQtyAdjustmentRet Clone()
        {
            return ((InventoryQtyAdjustmentRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "InventoryQtyAdjustmentItemRet")]
    public partial class InventoryQtyAdjustmentItemRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string listIDField;

        private decimal newQuantityField;

        private decimal numberOfBaseUnitsField;

        private decimal oldQuantityField;

        private decimal qtyDifferenceField;

        private string serialNumberField;

        private string unitOfMeasureField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

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

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal NewQuantity
        {
            get
            {
                return this.newQuantityField;
            }
            set
            {
                if ((newQuantityField.Equals(value) != true))
                {
                    this.newQuantityField = value;
                    this.OnPropertyChanged("NewQuantity", value);
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
        public decimal OldQuantity
        {
            get
            {
                return this.oldQuantityField;
            }
            set
            {
                if ((oldQuantityField.Equals(value) != true))
                {
                    this.oldQuantityField = value;
                    this.OnPropertyChanged("OldQuantity", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal QtyDifference
        {
            get
            {
                return this.qtyDifferenceField;
            }
            set
            {
                if ((qtyDifferenceField.Equals(value) != true))
                {
                    this.qtyDifferenceField = value;
                    this.OnPropertyChanged("QtyDifference", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SerialNumber
        {
            get
            {
                return this.serialNumberField;
            }
            set
            {
                if (((this.serialNumberField == null)
                            || (serialNumberField.Equals(value) != true)))
                {
                    this.serialNumberField = value;
                    this.OnPropertyChanged("SerialNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
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

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(InventoryQtyAdjustmentItemRet));
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
        /// Serializes current InventoryQtyAdjustmentItemRet object into an XML document
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
        /// Deserializes workflow markup into an InventoryQtyAdjustmentItemRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentItemRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out InventoryQtyAdjustmentItemRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentItemRet);
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

        public static bool Deserialize(string xml, out InventoryQtyAdjustmentItemRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static InventoryQtyAdjustmentItemRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((InventoryQtyAdjustmentItemRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static InventoryQtyAdjustmentItemRet Deserialize(System.IO.Stream s)
        {
            return ((InventoryQtyAdjustmentItemRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current InventoryQtyAdjustmentItemRet object into file
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
        /// Deserializes xml markup from file into an InventoryQtyAdjustmentItemRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentItemRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out InventoryQtyAdjustmentItemRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentItemRet);
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

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentItemRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentItemRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static InventoryQtyAdjustmentItemRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static InventoryQtyAdjustmentItemRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this InventoryQtyAdjustmentItemRet object
        /// </summary>
        public virtual InventoryQtyAdjustmentItemRet Clone()
        {
            return ((InventoryQtyAdjustmentItemRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "InventoryQtyAdjustmentRetDataExtRet")]
    public partial class InventoryQtyAdjustmentRetDataExtRet : System.ComponentModel.INotifyPropertyChanged
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(InventoryQtyAdjustmentRetDataExtRet));
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
        /// Serializes current InventoryQtyAdjustmentRetDataExtRet object into an XML document
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
        /// Deserializes workflow markup into an InventoryQtyAdjustmentRetDataExtRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out InventoryQtyAdjustmentRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentRetDataExtRet);
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

        public static bool Deserialize(string xml, out InventoryQtyAdjustmentRetDataExtRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public new static InventoryQtyAdjustmentRetDataExtRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((InventoryQtyAdjustmentRetDataExtRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static InventoryQtyAdjustmentRetDataExtRet Deserialize(System.IO.Stream s)
        {
            return ((InventoryQtyAdjustmentRetDataExtRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current InventoryQtyAdjustmentRetDataExtRet object into file
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
        /// Deserializes xml markup from file into an InventoryQtyAdjustmentRetDataExtRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output InventoryQtyAdjustmentRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out InventoryQtyAdjustmentRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(InventoryQtyAdjustmentRetDataExtRet);
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

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentRetDataExtRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out InventoryQtyAdjustmentRetDataExtRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static InventoryQtyAdjustmentRetDataExtRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public new static InventoryQtyAdjustmentRetDataExtRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this InventoryQtyAdjustmentRetDataExtRet object
        /// </summary>
        public virtual InventoryQtyAdjustmentRetDataExtRet Clone()
        {
            return ((InventoryQtyAdjustmentRetDataExtRet)(this.MemberwiseClone()));
        }
        #endregion
    }
}
