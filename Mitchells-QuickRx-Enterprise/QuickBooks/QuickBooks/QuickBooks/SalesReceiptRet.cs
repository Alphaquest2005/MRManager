
namespace QuickBooks
{

    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;


    #region PropertyValueState class
    public class PropertyValueState
    {

        public string PropertyName { get; set; }
        public object OriginalValue { get; set; }
        public object CurrentValue { get; set; }
        public ObjectState State { get; set; }
    }
    #endregion

    #region ObjectStateChangingEventArgs class
    public class ObjectStateChangingEventArgs : EventArgs
    {

        public ObjectState NewState { get; set; }
    }
    #endregion

    #region ObjectList class
    public class ObjectList : List<object>
    {
    }
    #endregion

    #region ObjectState enum
    public enum ObjectState
    {

        Unchanged,

        Added,

        Modified,

        Deleted,
    }
    #endregion

    #region NotifyTrackableCollectionChangedEventHandler class
    public delegate void NotifyTrackableCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e, string propertyName);
    #endregion

    #region Tracking changes class
    public class ObjectChangeTracker : System.ComponentModel.INotifyPropertyChanged
    {

        #region  Fields
        private bool isDeserializingField;
        private ObjectState originalobjectStateField = ObjectState.Added;
        private bool isInitilizedField = false;
        private readonly ObservableCollection<PropertyValueState> changeSetsField = new ObservableCollection<PropertyValueState>();
        private Delegate collectionChangedDelegateField = null;

        private bool objectTrackingEnabledField;
        private readonly object trackedObjectField;

        private PropertyValueStatesDictionary propertyValueStatesFields;
        //private ValueStatesDictionary valueStatesField;

        private ObjectsAddedToCollectionProperties objectsAddedToCollectionsField = new ObjectsAddedToCollectionProperties();
        private ObjectsRemovedFromCollectionProperties objectsRemovedFromCollectionsField = new ObjectsRemovedFromCollectionProperties();
        private ObjectsOriginalFromCollectionProperties objectsOriginalFromCollectionsField = new ObjectsOriginalFromCollectionProperties();
        #endregion

        public ObjectChangeTracker(object trackedObject)
        {
            trackedObjectField = trackedObject;
        }

        #region Events

        public event EventHandler<ObjectStateChangingEventArgs> ObjectStateChanging;

        #endregion

        protected virtual void OnObjectStateChanging(ObjectState newState)
        {
            if (ObjectStateChanging != null)
            {
                ObjectStateChanging(this, new ObjectStateChangingEventArgs() { NewState = newState });
            }
        }

        /// <summary>
        /// indicate current state
        /// </summary>
        private ObjectState stateField;

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public ObjectState State
        {
            get
            {
                return stateField;
            }
        }

        /// <summary>
        /// Updates the state of the change.
        /// </summary>
        private void UpdateChangeState()
        {
            ObjectState resultState = ObjectState.Added;
            this.changeSetsField.Clear();
            if (this.originalobjectStateField == ObjectState.Added)
            {
                if (stateField != ObjectState.Added)
                {
                    stateField = ObjectState.Added;
                    OnPropertyChanged("State");
                    OnObjectStateChanging(stateField);
                }
                return;
            }
            foreach (var propertyValuestate in PropertyValueStates)
            {
                if (propertyValuestate.Value.State == ObjectState.Modified)
                {
                    changeSetsField.Add(propertyValuestate.Value);
                    resultState = ObjectState.Modified;
                }
            }

            if (ObjectsRemovedFromCollectionProperties.Count > 0)
            {
                foreach (var objectsRemovedFromCollectionProperty in ObjectsRemovedFromCollectionProperties)
                {
                    foreach (var objectsRemoved in objectsRemovedFromCollectionProperty.Value)
                    {
                        changeSetsField.Add(new PropertyValueState() { PropertyName = objectsRemovedFromCollectionProperty.Key, State = ObjectState.Deleted, CurrentValue = objectsRemoved.ToString() });
                    }
                }
                resultState = ObjectState.Modified;
            }

            if (ObjectsAddedToCollectionProperties.Count > 0)
            {
                foreach (var objectsAddedFromCollectionProperty in ObjectsAddedToCollectionProperties)
                {
                    foreach (var objectsAdded in objectsAddedFromCollectionProperty.Value)
                    {
                        changeSetsField.Add(new PropertyValueState() { PropertyName = objectsAddedFromCollectionProperty.Key, State = ObjectState.Added, CurrentValue = objectsAdded.ToString() });
                    }
                }
                resultState = ObjectState.Modified;
            }

            if (resultState == ObjectState.Modified)
            {
                if (stateField != ObjectState.Modified)
                {
                    stateField = ObjectState.Modified;
                    OnPropertyChanged("State");
                    OnObjectStateChanging(stateField);
                }
                return;
            }
            if (stateField != this.originalobjectStateField)
            {
                stateField = this.originalobjectStateField;
                OnPropertyChanged("State");
                OnObjectStateChanging(stateField);
            }
            return;
        }

        public ObservableCollection<PropertyValueState> ChangeSets
        {
            get
            {
                return this.changeSetsField;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [change tracking enabled].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [change tracking enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ObjectTrackingEnabled
        {
            get { return objectTrackingEnabledField; }
        }

        /// <summary>
        /// Starts record changes.
        /// </summary>
        public void StartTracking()
        {
            objectTrackingEnabledField = true;
            ResetTracking();
        }

        /// <summary>
        /// Starts record changes.
        /// </summary>
        public void StartTracking(bool keepLastRecords)
        {
            objectTrackingEnabledField = true;
            if (!keepLastRecords)
                StartTracking();
        }

        /// <summary>
        /// Starts record changes.
        /// </summary>
        public void StopTracking()
        {
            objectTrackingEnabledField = false;
        }

        // Resets the ObjectChangeTracker to the Unchanged state and
        // clears the original values as well as the record of changes
        // to collection properties
        public void ResetTracking()
        {
            if (this.objectTrackingEnabledField)
            {
                this.originalobjectStateField = ObjectState.Unchanged;
                ResetOriginalValue();
                ObjectsAddedToCollectionProperties.Clear();
                ObjectsRemovedFromCollectionProperties.Clear();
                ObjectsOriginalFromCollectionProperties.Clear();
                InitOriginalValue();
                UpdateChangeState();
            }
        }
        /// <summary>
        /// Inits the original value.
        /// </summary>
        private void InitOriginalValue()
        {
            var type = trackedObjectField.GetType();
            var propertyies = type.GetProperties();

            if (!isInitilizedField)
            {
                foreach (var propertyInfo in propertyies)
                {
                    if (!propertyInfo.Name.Equals("ChangeTracker") && !propertyInfo.Name.Equals("Item"))
                    {
                        object o = propertyInfo.GetValue(trackedObjectField, null);
                        if (propertyInfo.PropertyType.Name.Contains("TrackableCollection"))
                        {
                            var eventInfo = propertyInfo.PropertyType.GetEvent("TrackableCollectionChanged");
                            if (eventInfo != null)
                            {
                                Type tDelegate = eventInfo.EventHandlerType;
                                if (tDelegate != null)
                                {
                                    if (collectionChangedDelegateField == null)
                                        collectionChangedDelegateField = Delegate.CreateDelegate(tDelegate, this, "TrackableCollectionChanged");

                                    // Get the "add" accessor of the event and invoke it late bound, passing in the delegate instance. This is equivalent
                                    // to using the += operator in C#. The instance on which the "add" accessor is invoked.
                                    MethodInfo addHandler = eventInfo.GetAddMethod();
                                    Object[] addHandlerArgs = { collectionChangedDelegateField };
                                    addHandler.Invoke(o, addHandlerArgs);
                                }
                            }

                            var collection = o as IEnumerable;
                            if (collection != null)
                            {
                                foreach (var item in collection)
                                {
                                    RecordOriginalToCollectionProperties(propertyInfo.Name, item);
                                }
                            }
                        }
                        else
                        {
                            RecordCurrentValue(propertyInfo.Name, o);
                        }
                    }
                }
                isInitilizedField = true;
            }
        }

        /// <summary>
        /// Resets the original value.
        /// </summary>
        private void ResetOriginalValue()
        {
            PropertyValueStates.Clear();
            //ValueStates.Clear();

            if (isInitilizedField)
            {
                var type = trackedObjectField.GetType();
                var propertyies = type.GetProperties();
                foreach (var propertyInfo in propertyies)
                {
                    if (!propertyInfo.Name.Equals("ChangeTracker") && !propertyInfo.Name.Equals("Item"))
                    {
                        object o = propertyInfo.GetValue(trackedObjectField, null);
                        if (propertyInfo.PropertyType.Name.Contains("TrackableCollection"))
                        {
                            var eventInfo = propertyInfo.PropertyType.GetEvent("TrackableCollectionChanged");
                            if (eventInfo != null)
                            {
                                Type tDelegate = eventInfo.EventHandlerType;
                                if (tDelegate != null)
                                {
                                    if (collectionChangedDelegateField != null)
                                    {
                                        // Get the "remove" accessor of the event and invoke it latebound, passing in the delegate instance. This is equivalent
                                        // to using the += operator in C#. The instance on which the "add" accessor is invoked.
                                        MethodInfo removeHandler = eventInfo.GetRemoveMethod();
                                        Object[] addHandlerArgs = { collectionChangedDelegateField };
                                        removeHandler.Invoke(o, addHandlerArgs);
                                    }
                                }
                            }
                        }
                    }
                }
                isInitilizedField = false;
            }
        }

        /// <summary>
        /// Trackables the collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyTrackableCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <param name="propertyName">Name of the property.</param>
        private void TrackableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e, string propertyName)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        //todo:implémenter la récursivité sur l'ajout des élements dans la collection
                        //var project = newItem as IObjectWithChangeTracker;
                        //if (project != null)
                        //{
                        //    if (this.ChangeTrackingEnabled)
                        //    {
                        //        project.ChangeTracker.Start();
                        //    }
                        //}
                        RecordAdditionToCollectionProperties(propertyName, newItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var odlItem in e.OldItems)
                    {
                        RecordRemovalFromCollectionProperties(propertyName, odlItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        // Cas d'un Clear sur la collection.
                        // Vide le cache des modifications pour la collection.
                        if (ObjectsRemovedFromCollectionProperties.ContainsKey(propertyName))
                        {
                            ObjectsRemovedFromCollectionProperties.Remove(propertyName);
                        }

                        if (ObjectsAddedToCollectionProperties.ContainsKey(propertyName))
                        {
                            ObjectsAddedToCollectionProperties.Remove(propertyName);
                        }

                        // Tranfère les données de départ dans les élements supprimés.
                        if (ObjectsOriginalFromCollectionProperties.Count > 0)
                        {
                            foreach (var item in ObjectsOriginalFromCollectionProperties[propertyName])
                            {
                                RecordRemovalFromCollectionProperties(propertyName, item);
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        // Comment gérer le cas d'un changement d'instance dans la liste ?
                    }
                    break;
            }
            UpdateChangeState();
        }

        #region public properties
        // Returns the removed objects to collection valued properties that were changed.
        [DataMember]
        public ObjectsRemovedFromCollectionProperties ObjectsRemovedFromCollectionProperties
        {
            get { return objectsRemovedFromCollectionsField ?? (objectsRemovedFromCollectionsField = new ObjectsRemovedFromCollectionProperties()); }
        }

        // Returns the original values for properties that were changed.
        [DataMember]
        public PropertyValueStatesDictionary PropertyValueStates
        {
            get { return propertyValueStatesFields ?? (propertyValueStatesFields = new PropertyValueStatesDictionary()); }
        }

        // Returns the added objects to collection valued properties that were changed.
        [DataMember]
        public ObjectsAddedToCollectionProperties ObjectsAddedToCollectionProperties
        {
            get { return objectsAddedToCollectionsField ?? (objectsAddedToCollectionsField = new ObjectsAddedToCollectionProperties()); }
        }

        // Returns the added objects to collection valued properties that were changed.
        [DataMember]
        public ObjectsOriginalFromCollectionProperties ObjectsOriginalFromCollectionProperties
        {
            get { return objectsOriginalFromCollectionsField ?? (objectsOriginalFromCollectionsField = new ObjectsOriginalFromCollectionProperties()); }
        }

        #region MethodsForChangeTrackingOnClient

        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            isDeserializingField = true;
        }

        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            isDeserializingField = false;
        }
        #endregion

        /// <summary>
        /// Captures the original value for a property that is changing.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public void RecordCurrentValue(string propertyName, object value)
        {
            if (objectTrackingEnabledField)
            {
                if (!PropertyValueStates.ContainsKey(propertyName))
                {
                    PropertyValueStates[propertyName] = new PropertyValueState() { PropertyName = propertyName, OriginalValue = value, CurrentValue = value, State = ObjectState.Unchanged };
                }
                // Compare original value new 
                else
                {
                    PropertyValueStates[propertyName].CurrentValue = value;
                    var originalValue = PropertyValueStates[propertyName].OriginalValue;
                    if (originalValue != null)
                    {
                        PropertyValueStates[propertyName].State = originalValue.Equals(value) ? ObjectState.Unchanged : ObjectState.Modified;
                    }
                    else
                    {
                        if (value == null)
                        {
                            PropertyValueStates[propertyName].State = ObjectState.Unchanged;
                        }
                        else
                        {
                            PropertyValueStates[propertyName].State = string.IsNullOrEmpty(value.ToString()) ? ObjectState.Unchanged : ObjectState.Modified;
                        }
                    }
                }
                UpdateChangeState();
            }
        }

        /// <summary>
        /// Records original items value of collection to collection valued properties on SelfTracking Entities.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        private void RecordOriginalToCollectionProperties(string propertyName, object value)
        {
            if (objectTrackingEnabledField)
            {
                if (!ObjectsOriginalFromCollectionProperties.ContainsKey(propertyName))
                {
                    ObjectsOriginalFromCollectionProperties[propertyName] = new ObjectList();
                    ObjectsOriginalFromCollectionProperties[propertyName].Add(value);
                }
                else
                {
                    ObjectsOriginalFromCollectionProperties[propertyName].Add(value);
                }
            }
        }


        /// <summary>
        /// Records an addition to collection valued properties on SelfTracking Entities.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        private void RecordAdditionToCollectionProperties(string propertyName, object value)
        {
            if (objectTrackingEnabledField)
            {
                // Add the entity back after deleting it, we should do nothing here then
                if (ObjectsRemovedFromCollectionProperties.ContainsKey(propertyName)
                    && ObjectsRemovedFromCollectionProperties[propertyName].Contains(value))
                {
                    ObjectsRemovedFromCollectionProperties[propertyName].Remove(value);
                    if (ObjectsRemovedFromCollectionProperties[propertyName].Count == 0)
                    {
                        ObjectsRemovedFromCollectionProperties.Remove(propertyName);
                    }
                    return;
                }

                if (!ObjectsAddedToCollectionProperties.ContainsKey(propertyName))
                {
                    ObjectsAddedToCollectionProperties[propertyName] = new ObjectList();
                    ObjectsAddedToCollectionProperties[propertyName].Add(value);
                }
                else
                {
                    ObjectsAddedToCollectionProperties[propertyName].Add(value);
                }
            }
        }

        /// <summary>
        /// Records a removal to collection valued properties on SelfTracking Entities.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The object value.</param>
        internal void RecordRemovalFromCollectionProperties(string propertyName, object value)
        {
            if (objectTrackingEnabledField)
            {
                // Delete the entity back after adding it, we should do nothing here then
                if (ObjectsAddedToCollectionProperties.ContainsKey(propertyName)
                    && ObjectsAddedToCollectionProperties[propertyName].Contains(value))
                {
                    ObjectsAddedToCollectionProperties[propertyName].Remove(value);
                    if (ObjectsAddedToCollectionProperties[propertyName].Count == 0)
                    {
                        ObjectsAddedToCollectionProperties.Remove(propertyName);
                    }
                    return;
                }

                if (!ObjectsRemovedFromCollectionProperties.ContainsKey(propertyName))
                {
                    ObjectsRemovedFromCollectionProperties[propertyName] = new ObjectList();
                    ObjectsRemovedFromCollectionProperties[propertyName].Add(value);
                }
                else
                {
                    if (!ObjectsRemovedFromCollectionProperties[propertyName].Contains(value))
                    {
                        ObjectsRemovedFromCollectionProperties[propertyName].Add(value);
                    }
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="info">The info.</param>
        public virtual void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }
    }
    #endregion

    #region TrackableCollection class
    public class TrackableCollection<T> : ObservableCollection<T>
    {

        /// <summary>
        /// Name of property
        /// </summary>
        private string propertyNameField;

        /// <summary>
        /// Occurs when [trackable collection changed].
        /// </summary>
        public virtual event NotifyTrackableCollectionChangedEventHandler TrackableCollectionChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackableCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public TrackableCollection(string propertyName)
        {
            propertyNameField = propertyName;
            base.CollectionChanged += TrackableCollection_CollectionChanged;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the TrackableCollection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        void TrackableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.TrackableCollectionChanged != null)
            {
                this.TrackableCollectionChanged(sender, e, this.propertyNameField);
            }
        }


    }
    #endregion

    #region PropertyValueStatesDictionary
    public class PropertyValueStatesDictionary : Dictionary<string, PropertyValueState>
    {
    }
    #endregion

    #region ObjectsRemovedFromCollectionProperties
    public class ObjectsRemovedFromCollectionProperties : Dictionary<string, ObjectList>
    {
    }
    #endregion

    #region ObjectsAddedToCollectionProperties
    public class ObjectsAddedToCollectionProperties : Dictionary<string, ObjectList>
    {
    }
    #endregion

    #region ObjectsOriginalFromCollectionProperties
    public class ObjectsOriginalFromCollectionProperties : Dictionary<string, ObjectList>
    {
    }
    #endregion

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRet")]
    public partial class SalesReceiptRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string txnIDField;

        private System.DateTime timeCreatedField;

        private System.DateTime timeModifiedField;

        private string associateField;

        private string cashierField;

        private string commentsField;

        private string customerListIDField;

        private decimal discountField;

        private decimal discountPercentField;

        private string historyDocStatusField;

        private string itemsCountField;

        private int priceLevelNumberField;

        private string promoCodeField;

        private string quickBooksFlagField;

        private string salesOrderTxnIDField;

        private string salesReceiptNumberField;

        private string salesReceiptTypeField;

        private System.DateTime shipDateField;

        private string storeExchangeStatusField;

        private string storeNumberField;

        private decimal subtotalField;

        private decimal taxAmountField;

        private string taxCategoryField;

        private decimal taxPercentageField;

        private string tenderTypeField;

        private string tipReceiverField;

        private decimal totalField;

        private string trackingNumberField;

        private System.DateTime txnDateField;

        private string txnStateField;

        private string workstationField;

        private SalesReceiptRetBillingInformation billingInformationField;

        private SalesReceiptRetShippingInformation shippingInformationField;

        private SalesReceiptRetSalesReceiptItemRet salesReceiptItemRetField;

        private SalesReceiptRetTenderAccountRet tenderAccountRetField;

        private SalesReceiptRetTenderCashRet tenderCashRetField;

        private SalesReceiptRetTenderCheckRet tenderCheckRetField;

        private SalesReceiptRetTenderCreditCardRet tenderCreditCardRetField;

        private SalesReceiptRetTenderDebitCardRet tenderDebitCardRetField;

        private SalesReceiptRetTenderDepositRet tenderDepositRetField;

        private SalesReceiptRetTenderGiftRet tenderGiftRetField;

        private SalesReceiptRetTenderGiftCardRet tenderGiftCardRetField;

        private SalesReceiptRetDataExtRet dataExtRetField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        public SalesReceiptRet()
        {
            this.dataExtRetField = new SalesReceiptRetDataExtRet();
            this.tenderGiftCardRetField = new SalesReceiptRetTenderGiftCardRet();
            this.tenderGiftRetField = new SalesReceiptRetTenderGiftRet();
            this.tenderDepositRetField = new SalesReceiptRetTenderDepositRet();
            this.tenderDebitCardRetField = new SalesReceiptRetTenderDebitCardRet();
            this.tenderCreditCardRetField = new SalesReceiptRetTenderCreditCardRet();
            this.tenderCheckRetField = new SalesReceiptRetTenderCheckRet();
            this.tenderCashRetField = new SalesReceiptRetTenderCashRet();
            this.tenderAccountRetField = new SalesReceiptRetTenderAccountRet();
            this.salesReceiptItemRetField = new SalesReceiptRetSalesReceiptItemRet();
            this.shippingInformationField = new SalesReceiptRetShippingInformation();
            this.billingInformationField = new SalesReceiptRetBillingInformation();
            this.SalesReceiptItems = new TrackableCollection<SalesReceiptRetSalesReceiptItemRet>(null);
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
        public string Cashier
        {
            get
            {
                return this.cashierField;
            }
            set
            {
                if (((this.cashierField == null)
                            || (cashierField.Equals(value) != true)))
                {
                    this.cashierField = value;
                    this.OnPropertyChanged("Cashier", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerListID
        {
            get
            {
                return this.customerListIDField;
            }
            set
            {
                if (((this.customerListIDField == null)
                            || (customerListIDField.Equals(value) != true)))
                {
                    this.customerListIDField = value;
                    this.OnPropertyChanged("CustomerListID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Discount
        {
            get
            {
                return this.discountField;
            }
            set
            {
                if ((discountField.Equals(value) != true))
                {
                    this.discountField = value;
                    this.OnPropertyChanged("Discount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal DiscountPercent
        {
            get
            {
                return this.discountPercentField;
            }
            set
            {
                if ((discountPercentField.Equals(value) != true))
                {
                    this.discountPercentField = value;
                    this.OnPropertyChanged("DiscountPercent", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ItemsCount
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PriceLevelNumber
        {
            get
            {
                return this.priceLevelNumberField;
            }
            set
            {
                if ((priceLevelNumberField.Equals(value) != true))
                {
                    this.priceLevelNumberField = value;
                    this.OnPropertyChanged("PriceLevelNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PromoCode
        {
            get
            {
                return this.promoCodeField;
            }
            set
            {
                if (((this.promoCodeField == null)
                            || (promoCodeField.Equals(value) != true)))
                {
                    this.promoCodeField = value;
                    this.OnPropertyChanged("PromoCode", value);
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
        public string SalesOrderTxnID
        {
            get
            {
                return this.salesOrderTxnIDField;
            }
            set
            {
                if (((this.salesOrderTxnIDField == null)
                            || (salesOrderTxnIDField.Equals(value) != true)))
                {
                    this.salesOrderTxnIDField = value;
                    this.OnPropertyChanged("SalesOrderTxnID", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SalesReceiptNumber
        {
            get
            {
                return this.salesReceiptNumberField;
            }
            set
            {
                if (((this.salesReceiptNumberField == null)
                            || (salesReceiptNumberField.Equals(value) != true)))
                {
                    this.salesReceiptNumberField = value;
                    this.OnPropertyChanged("SalesReceiptNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SalesReceiptType
        {
            get
            {
                return this.salesReceiptTypeField;
            }
            set
            {
                if (((this.salesReceiptTypeField == null)
                            || (salesReceiptTypeField.Equals(value) != true)))
                {
                    this.salesReceiptTypeField = value;
                    this.OnPropertyChanged("SalesReceiptType", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 17)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ShipDate
        {
            get
            {
                return this.shipDateField;
            }
            set
            {
                if ((shipDateField.Equals(value) != true))
                {
                    this.shipDateField = value;
                    this.OnPropertyChanged("ShipDate", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StoreNumber
        {
            get
            {
                return this.storeNumberField;
            }
            set
            {
                if (((this.storeNumberField == null)
                            || (storeNumberField.Equals(value) != true)))
                {
                    this.storeNumberField = value;
                    this.OnPropertyChanged("StoreNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Subtotal
        {
            get
            {
                return this.subtotalField;
            }
            set
            {
                if ((subtotalField.Equals(value) != true))
                {
                    this.subtotalField = value;
                    this.OnPropertyChanged("Subtotal", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                if ((taxAmountField.Equals(value) != true))
                {
                    this.taxAmountField = value;
                    this.OnPropertyChanged("TaxAmount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TaxCategory
        {
            get
            {
                return this.taxCategoryField;
            }
            set
            {
                if (((this.taxCategoryField == null)
                            || (taxCategoryField.Equals(value) != true)))
                {
                    this.taxCategoryField = value;
                    this.OnPropertyChanged("TaxCategory", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TaxPercentage
        {
            get
            {
                return this.taxPercentageField;
            }
            set
            {
                if ((taxPercentageField.Equals(value) != true))
                {
                    this.taxPercentageField = value;
                    this.OnPropertyChanged("TaxPercentage", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TenderType
        {
            get
            {
                return this.tenderTypeField;
            }
            set
            {
                if (((this.tenderTypeField == null)
                            || (tenderTypeField.Equals(value) != true)))
                {
                    this.tenderTypeField = value;
                    this.OnPropertyChanged("TenderType", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TipReceiver
        {
            get
            {
                return this.tipReceiverField;
            }
            set
            {
                if (((this.tipReceiverField == null)
                            || (tipReceiverField.Equals(value) != true)))
                {
                    this.tipReceiverField = value;
                    this.OnPropertyChanged("TipReceiver", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                if ((totalField.Equals(value) != true))
                {
                    this.totalField = value;
                    this.OnPropertyChanged("Total", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TrackingNumber
        {
            get
            {
                return this.trackingNumberField;
            }
            set
            {
                if (((this.trackingNumberField == null)
                            || (trackingNumberField.Equals(value) != true)))
                {
                    this.trackingNumberField = value;
                    this.OnPropertyChanged("TrackingNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 28)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Workstation
        {
            get
            {
                return this.workstationField;
            }
            set
            {
                if (((this.workstationField == null)
                            || (workstationField.Equals(value) != true)))
                {
                    this.workstationField = value;
                    this.OnPropertyChanged("Workstation", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetBillingInformation BillingInformation
        {
            get
            {
                return this.billingInformationField;
            }
            set
            {
                if (((this.billingInformationField == null)
                            || (billingInformationField.Equals(value) != true)))
                {
                    this.billingInformationField = value;
                    this.OnPropertyChanged("BillingInformation", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetShippingInformation ShippingInformation
        {
            get
            {
                return this.shippingInformationField;
            }
            set
            {
                if (((this.shippingInformationField == null)
                            || (shippingInformationField.Equals(value) != true)))
                {
                    this.shippingInformationField = value;
                    this.OnPropertyChanged("ShippingInformation", value);
                }
            }
        }

        public TrackableCollection<SalesReceiptRetSalesReceiptItemRet> SalesReceiptItems
        {
        get; set;
        }


        [System.Xml.Serialization.XmlElementAttribute(Order = 33)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetSalesReceiptItemRet SalesReceiptItemRet
        {
            get
            {
                return this.salesReceiptItemRetField;
            }
            set
            {
                if (((this.salesReceiptItemRetField == null)
                            || (salesReceiptItemRetField.Equals(value) != true)))
                {
                    this.salesReceiptItemRetField = value;
                    this.OnPropertyChanged("SalesReceiptItemRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 34)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderAccountRet TenderAccountRet
        {
            get
            {
                return this.tenderAccountRetField;
            }
            set
            {
                if (((this.tenderAccountRetField == null)
                            || (tenderAccountRetField.Equals(value) != true)))
                {
                    this.tenderAccountRetField = value;
                    this.OnPropertyChanged("TenderAccountRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 35)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderCashRet TenderCashRet
        {
            get
            {
                return this.tenderCashRetField;
            }
            set
            {
                if (((this.tenderCashRetField == null)
                            || (tenderCashRetField.Equals(value) != true)))
                {
                    this.tenderCashRetField = value;
                    this.OnPropertyChanged("TenderCashRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 36)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderCheckRet TenderCheckRet
        {
            get
            {
                return this.tenderCheckRetField;
            }
            set
            {
                if (((this.tenderCheckRetField == null)
                            || (tenderCheckRetField.Equals(value) != true)))
                {
                    this.tenderCheckRetField = value;
                    this.OnPropertyChanged("TenderCheckRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 37)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderCreditCardRet TenderCreditCardRet
        {
            get
            {
                return this.tenderCreditCardRetField;
            }
            set
            {
                if (((this.tenderCreditCardRetField == null)
                            || (tenderCreditCardRetField.Equals(value) != true)))
                {
                    this.tenderCreditCardRetField = value;
                    this.OnPropertyChanged("TenderCreditCardRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 38)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderDebitCardRet TenderDebitCardRet
        {
            get
            {
                return this.tenderDebitCardRetField;
            }
            set
            {
                if (((this.tenderDebitCardRetField == null)
                            || (tenderDebitCardRetField.Equals(value) != true)))
                {
                    this.tenderDebitCardRetField = value;
                    this.OnPropertyChanged("TenderDebitCardRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 39)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderDepositRet TenderDepositRet
        {
            get
            {
                return this.tenderDepositRetField;
            }
            set
            {
                if (((this.tenderDepositRetField == null)
                            || (tenderDepositRetField.Equals(value) != true)))
                {
                    this.tenderDepositRetField = value;
                    this.OnPropertyChanged("TenderDepositRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 40)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderGiftRet TenderGiftRet
        {
            get
            {
                return this.tenderGiftRetField;
            }
            set
            {
                if (((this.tenderGiftRetField == null)
                            || (tenderGiftRetField.Equals(value) != true)))
                {
                    this.tenderGiftRetField = value;
                    this.OnPropertyChanged("TenderGiftRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 41)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetTenderGiftCardRet TenderGiftCardRet
        {
            get
            {
                return this.tenderGiftCardRetField;
            }
            set
            {
                if (((this.tenderGiftCardRetField == null)
                            || (tenderGiftCardRetField.Equals(value) != true)))
                {
                    this.tenderGiftCardRetField = value;
                    this.OnPropertyChanged("TenderGiftCardRet", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 42)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SalesReceiptRetDataExtRet DataExtRet
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRet));
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
        /// Serializes current SalesReceiptRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRet object
        /// </summary>
        public virtual SalesReceiptRet Clone()
        {
            return ((SalesReceiptRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetBillingInformation")]
    public partial class SalesReceiptRetBillingInformation : System.ComponentModel.INotifyPropertyChanged
    {

        private string cityField;

        private string companyNameField;

        private string countryField;

        private string firstNameField;

        private string lastNameField;

        private string phoneField;

        private string phone2Field;

        private string phone3Field;

        private string phone4Field;

        private string postalCodeField;

        private string salutationField;

        private string stateField;

        private string streetField;

        private string street2Field;

        private string webNumberField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                if (((this.cityField == null)
                            || (cityField.Equals(value) != true)))
                {
                    this.cityField = value;
                    this.OnPropertyChanged("City", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                if (((this.companyNameField == null)
                            || (companyNameField.Equals(value) != true)))
                {
                    this.companyNameField = value;
                    this.OnPropertyChanged("CompanyName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                if (((this.countryField == null)
                            || (countryField.Equals(value) != true)))
                {
                    this.countryField = value;
                    this.OnPropertyChanged("Country", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                if (((this.firstNameField == null)
                            || (firstNameField.Equals(value) != true)))
                {
                    this.firstNameField = value;
                    this.OnPropertyChanged("FirstName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                if (((this.lastNameField == null)
                            || (lastNameField.Equals(value) != true)))
                {
                    this.lastNameField = value;
                    this.OnPropertyChanged("LastName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                if (((this.phoneField == null)
                            || (phoneField.Equals(value) != true)))
                {
                    this.phoneField = value;
                    this.OnPropertyChanged("Phone", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone2
        {
            get
            {
                return this.phone2Field;
            }
            set
            {
                if (((this.phone2Field == null)
                            || (phone2Field.Equals(value) != true)))
                {
                    this.phone2Field = value;
                    this.OnPropertyChanged("Phone2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone3
        {
            get
            {
                return this.phone3Field;
            }
            set
            {
                if (((this.phone3Field == null)
                            || (phone3Field.Equals(value) != true)))
                {
                    this.phone3Field = value;
                    this.OnPropertyChanged("Phone3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone4
        {
            get
            {
                return this.phone4Field;
            }
            set
            {
                if (((this.phone4Field == null)
                            || (phone4Field.Equals(value) != true)))
                {
                    this.phone4Field = value;
                    this.OnPropertyChanged("Phone4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                if (((this.postalCodeField == null)
                            || (postalCodeField.Equals(value) != true)))
                {
                    this.postalCodeField = value;
                    this.OnPropertyChanged("PostalCode", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Salutation
        {
            get
            {
                return this.salutationField;
            }
            set
            {
                if (((this.salutationField == null)
                            || (salutationField.Equals(value) != true)))
                {
                    this.salutationField = value;
                    this.OnPropertyChanged("Salutation", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                if (((this.stateField == null)
                            || (stateField.Equals(value) != true)))
                {
                    this.stateField = value;
                    this.OnPropertyChanged("State", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                if (((this.streetField == null)
                            || (streetField.Equals(value) != true)))
                {
                    this.streetField = value;
                    this.OnPropertyChanged("Street", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street2
        {
            get
            {
                return this.street2Field;
            }
            set
            {
                if (((this.street2Field == null)
                            || (street2Field.Equals(value) != true)))
                {
                    this.street2Field = value;
                    this.OnPropertyChanged("Street2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebNumber
        {
            get
            {
                return this.webNumberField;
            }
            set
            {
                if (((this.webNumberField == null)
                            || (webNumberField.Equals(value) != true)))
                {
                    this.webNumberField = value;
                    this.OnPropertyChanged("WebNumber", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetBillingInformation));
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
        /// Serializes current SalesReceiptRetBillingInformation object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetBillingInformation object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetBillingInformation object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetBillingInformation obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetBillingInformation);
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

        public static bool Deserialize(string xml, out SalesReceiptRetBillingInformation obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetBillingInformation Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetBillingInformation)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetBillingInformation Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetBillingInformation)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetBillingInformation object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetBillingInformation object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetBillingInformation object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetBillingInformation obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetBillingInformation);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetBillingInformation obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetBillingInformation obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetBillingInformation LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetBillingInformation LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetBillingInformation object
        /// </summary>
        public virtual SalesReceiptRetBillingInformation Clone()
        {
            return ((SalesReceiptRetBillingInformation)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetShippingInformation")]
    public partial class SalesReceiptRetShippingInformation : System.ComponentModel.INotifyPropertyChanged
    {

        private string addressNameField;

        private string cityField;

        private string companyNameField;

        private string countryField;

        private string fullNameField;

        private string phoneField;

        private string phone2Field;

        private string phone3Field;

        private string phone4Field;

        private string postalCodeField;

        private System.DateTime shipByField;

        private decimal shippingField;

        private string stateField;

        private string streetField;

        private string street2Field;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AddressName
        {
            get
            {
                return this.addressNameField;
            }
            set
            {
                if (((this.addressNameField == null)
                            || (addressNameField.Equals(value) != true)))
                {
                    this.addressNameField = value;
                    this.OnPropertyChanged("AddressName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                if (((this.cityField == null)
                            || (cityField.Equals(value) != true)))
                {
                    this.cityField = value;
                    this.OnPropertyChanged("City", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                if (((this.companyNameField == null)
                            || (companyNameField.Equals(value) != true)))
                {
                    this.companyNameField = value;
                    this.OnPropertyChanged("CompanyName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                if (((this.countryField == null)
                            || (countryField.Equals(value) != true)))
                {
                    this.countryField = value;
                    this.OnPropertyChanged("Country", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                if (((this.fullNameField == null)
                            || (fullNameField.Equals(value) != true)))
                {
                    this.fullNameField = value;
                    this.OnPropertyChanged("FullName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                if (((this.phoneField == null)
                            || (phoneField.Equals(value) != true)))
                {
                    this.phoneField = value;
                    this.OnPropertyChanged("Phone", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone2
        {
            get
            {
                return this.phone2Field;
            }
            set
            {
                if (((this.phone2Field == null)
                            || (phone2Field.Equals(value) != true)))
                {
                    this.phone2Field = value;
                    this.OnPropertyChanged("Phone2", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone3
        {
            get
            {
                return this.phone3Field;
            }
            set
            {
                if (((this.phone3Field == null)
                            || (phone3Field.Equals(value) != true)))
                {
                    this.phone3Field = value;
                    this.OnPropertyChanged("Phone3", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone4
        {
            get
            {
                return this.phone4Field;
            }
            set
            {
                if (((this.phone4Field == null)
                            || (phone4Field.Equals(value) != true)))
                {
                    this.phone4Field = value;
                    this.OnPropertyChanged("Phone4", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                if (((this.postalCodeField == null)
                            || (postalCodeField.Equals(value) != true)))
                {
                    this.postalCodeField = value;
                    this.OnPropertyChanged("PostalCode", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ShipBy
        {
            get
            {
                return this.shipByField;
            }
            set
            {
                if ((shipByField.Equals(value) != true))
                {
                    this.shipByField = value;
                    this.OnPropertyChanged("ShipBy", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Shipping
        {
            get
            {
                return this.shippingField;
            }
            set
            {
                if ((shippingField.Equals(value) != true))
                {
                    this.shippingField = value;
                    this.OnPropertyChanged("Shipping", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                if (((this.stateField == null)
                            || (stateField.Equals(value) != true)))
                {
                    this.stateField = value;
                    this.OnPropertyChanged("State", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                if (((this.streetField == null)
                            || (streetField.Equals(value) != true)))
                {
                    this.streetField = value;
                    this.OnPropertyChanged("Street", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street2
        {
            get
            {
                return this.street2Field;
            }
            set
            {
                if (((this.street2Field == null)
                            || (street2Field.Equals(value) != true)))
                {
                    this.street2Field = value;
                    this.OnPropertyChanged("Street2", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetShippingInformation));
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
        /// Serializes current SalesReceiptRetShippingInformation object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetShippingInformation object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetShippingInformation object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetShippingInformation obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetShippingInformation);
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

        public static bool Deserialize(string xml, out SalesReceiptRetShippingInformation obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetShippingInformation Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetShippingInformation)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetShippingInformation Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetShippingInformation)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetShippingInformation object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetShippingInformation object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetShippingInformation object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetShippingInformation obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetShippingInformation);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetShippingInformation obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetShippingInformation obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetShippingInformation LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetShippingInformation LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetShippingInformation object
        /// </summary>
        public virtual SalesReceiptRetShippingInformation Clone()
        {
            return ((SalesReceiptRetShippingInformation)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetSalesReceiptItemRet")]
    public partial class SalesReceiptRetSalesReceiptItemRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string listIDField;

        private string aLUField;

        private string associateField;

        private string attributeField;

        private decimal commissionField;

        private decimal costField;

        private string desc1Field;

        private string desc2Field;

        private decimal discountField;

        private decimal discountPercentField;

        private string discountTypeField;

        private string discountSourceField;

        private decimal extendedPriceField;

        private decimal extendedTaxField;

        private string itemNumberField;

        private string numberOfBaseUnitsField;

        private decimal priceField;

        private string priceLevelNumberField;

        private string qtyField;

        private string serialNumberField;

        private string sizeField;

        private decimal taxAmountField;

        private string taxCodeField;

        private decimal taxPercentageField;

        private string unitOfMeasureField;

        private string uPCField;

        private string webDescField;

        private string manufacturerField;

        private decimal weightField;

        private string webSKUField;

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

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Commission
        {
            get
            {
                return this.commissionField;
            }
            set
            {
                if ((commissionField.Equals(value) != true))
                {
                    this.commissionField = value;
                    this.OnPropertyChanged("Commission", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Discount
        {
            get
            {
                return this.discountField;
            }
            set
            {
                if ((discountField.Equals(value) != true))
                {
                    this.discountField = value;
                    this.OnPropertyChanged("Discount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal DiscountPercent
        {
            get
            {
                return this.discountPercentField;
            }
            set
            {
                if ((discountPercentField.Equals(value) != true))
                {
                    this.discountPercentField = value;
                    this.OnPropertyChanged("DiscountPercent", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DiscountType
        {
            get
            {
                return this.discountTypeField;
            }
            set
            {
                if (((this.discountTypeField == null)
                            || (discountTypeField.Equals(value) != true)))
                {
                    this.discountTypeField = value;
                    this.OnPropertyChanged("DiscountType", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DiscountSource
        {
            get
            {
                return this.discountSourceField;
            }
            set
            {
                if (((this.discountSourceField == null)
                            || (discountSourceField.Equals(value) != true)))
                {
                    this.discountSourceField = value;
                    this.OnPropertyChanged("DiscountSource", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ExtendedPrice
        {
            get
            {
                return this.extendedPriceField;
            }
            set
            {
                if ((extendedPriceField.Equals(value) != true))
                {
                    this.extendedPriceField = value;
                    this.OnPropertyChanged("ExtendedPrice", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ExtendedTax
        {
            get
            {
                return this.extendedTaxField;
            }
            set
            {
                if ((extendedTaxField.Equals(value) != true))
                {
                    this.extendedTaxField = value;
                    this.OnPropertyChanged("ExtendedTax", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ItemNumber
        {
            get
            {
                return this.itemNumberField;
            }
            set
            {
                if (((this.itemNumberField == null)
                            || (itemNumberField.Equals(value) != true)))
                {
                    this.itemNumberField = value;
                    this.OnPropertyChanged("ItemNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NumberOfBaseUnits
        {
            get
            {
                return this.numberOfBaseUnitsField;
            }
            set
            {
                if (((this.numberOfBaseUnitsField == null)
                            || (numberOfBaseUnitsField.Equals(value) != true)))
                {
                    this.numberOfBaseUnitsField = value;
                    this.OnPropertyChanged("NumberOfBaseUnits", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                if ((priceField.Equals(value) != true))
                {
                    this.priceField = value;
                    this.OnPropertyChanged("Price", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PriceLevelNumber
        {
            get
            {
                return this.priceLevelNumberField;
            }
            set
            {
                if (((this.priceLevelNumberField == null)
                            || (priceLevelNumberField.Equals(value) != true)))
                {
                    this.priceLevelNumberField = value;
                    this.OnPropertyChanged("PriceLevelNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Qty
        {
            get
            {
                return this.qtyField;
            }
            set
            {
                if (((this.qtyField == null)
                            || (qtyField.Equals(value) != true)))
                {
                    this.qtyField = value;
                    this.OnPropertyChanged("Qty", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                if ((taxAmountField.Equals(value) != true))
                {
                    this.taxAmountField = value;
                    this.OnPropertyChanged("TaxAmount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TaxPercentage
        {
            get
            {
                return this.taxPercentageField;
            }
            set
            {
                if ((taxPercentageField.Equals(value) != true))
                {
                    this.taxPercentageField = value;
                    this.OnPropertyChanged("TaxPercentage", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
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

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetSalesReceiptItemRet));
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
        /// Serializes current SalesReceiptRetSalesReceiptItemRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetSalesReceiptItemRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetSalesReceiptItemRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetSalesReceiptItemRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetSalesReceiptItemRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetSalesReceiptItemRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetSalesReceiptItemRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetSalesReceiptItemRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetSalesReceiptItemRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetSalesReceiptItemRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetSalesReceiptItemRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetSalesReceiptItemRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetSalesReceiptItemRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetSalesReceiptItemRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetSalesReceiptItemRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetSalesReceiptItemRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetSalesReceiptItemRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetSalesReceiptItemRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetSalesReceiptItemRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetSalesReceiptItemRet object
        /// </summary>
        public virtual SalesReceiptRetSalesReceiptItemRet Clone()
        {
            return ((SalesReceiptRetSalesReceiptItemRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderAccountRet")]
    public partial class SalesReceiptRetTenderAccountRet : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal tenderAmountField;

        private decimal tipAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TipAmount
        {
            get
            {
                return this.tipAmountField;
            }
            set
            {
                if ((tipAmountField.Equals(value) != true))
                {
                    this.tipAmountField = value;
                    this.OnPropertyChanged("TipAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderAccountRet));
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
        /// Serializes current SalesReceiptRetTenderAccountRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderAccountRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderAccountRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderAccountRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderAccountRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderAccountRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderAccountRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderAccountRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderAccountRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderAccountRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderAccountRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderAccountRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderAccountRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderAccountRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderAccountRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderAccountRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderAccountRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderAccountRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderAccountRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderAccountRet object
        /// </summary>
        public virtual SalesReceiptRetTenderAccountRet Clone()
        {
            return ((SalesReceiptRetTenderAccountRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderCashRet")]
    public partial class SalesReceiptRetTenderCashRet : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal tenderAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderCashRet));
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
        /// Serializes current SalesReceiptRetTenderCashRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderCashRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCashRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderCashRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCashRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderCashRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderCashRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderCashRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderCashRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderCashRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderCashRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderCashRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCashRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderCashRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCashRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCashRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCashRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderCashRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderCashRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderCashRet object
        /// </summary>
        public virtual SalesReceiptRetTenderCashRet Clone()
        {
            return ((SalesReceiptRetTenderCashRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderCheckRet")]
    public partial class SalesReceiptRetTenderCheckRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string checkNumberField;

        private decimal tenderAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CheckNumber
        {
            get
            {
                return this.checkNumberField;
            }
            set
            {
                if (((this.checkNumberField == null)
                            || (checkNumberField.Equals(value) != true)))
                {
                    this.checkNumberField = value;
                    this.OnPropertyChanged("CheckNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderCheckRet));
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
        /// Serializes current SalesReceiptRetTenderCheckRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderCheckRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCheckRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderCheckRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCheckRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderCheckRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderCheckRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderCheckRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderCheckRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderCheckRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderCheckRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderCheckRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCheckRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderCheckRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCheckRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCheckRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCheckRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderCheckRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderCheckRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderCheckRet object
        /// </summary>
        public virtual SalesReceiptRetTenderCheckRet Clone()
        {
            return ((SalesReceiptRetTenderCheckRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderCreditCardRet")]
    public partial class SalesReceiptRetTenderCreditCardRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string cardNameField;

        private decimal tenderAmountField;

        private decimal tipAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CardName
        {
            get
            {
                return this.cardNameField;
            }
            set
            {
                if (((this.cardNameField == null)
                            || (cardNameField.Equals(value) != true)))
                {
                    this.cardNameField = value;
                    this.OnPropertyChanged("CardName", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TipAmount
        {
            get
            {
                return this.tipAmountField;
            }
            set
            {
                if ((tipAmountField.Equals(value) != true))
                {
                    this.tipAmountField = value;
                    this.OnPropertyChanged("TipAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderCreditCardRet));
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
        /// Serializes current SalesReceiptRetTenderCreditCardRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderCreditCardRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCreditCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderCreditCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCreditCardRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderCreditCardRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderCreditCardRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderCreditCardRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderCreditCardRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderCreditCardRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderCreditCardRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderCreditCardRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderCreditCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderCreditCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderCreditCardRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCreditCardRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderCreditCardRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderCreditCardRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderCreditCardRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderCreditCardRet object
        /// </summary>
        public virtual SalesReceiptRetTenderCreditCardRet Clone()
        {
            return ((SalesReceiptRetTenderCreditCardRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderDebitCardRet")]
    public partial class SalesReceiptRetTenderDebitCardRet : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal cashbackField;

        private decimal tenderAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Cashback
        {
            get
            {
                return this.cashbackField;
            }
            set
            {
                if ((cashbackField.Equals(value) != true))
                {
                    this.cashbackField = value;
                    this.OnPropertyChanged("Cashback", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderDebitCardRet));
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
        /// Serializes current SalesReceiptRetTenderDebitCardRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderDebitCardRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderDebitCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderDebitCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderDebitCardRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderDebitCardRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderDebitCardRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderDebitCardRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderDebitCardRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderDebitCardRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderDebitCardRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderDebitCardRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderDebitCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderDebitCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderDebitCardRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderDebitCardRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderDebitCardRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderDebitCardRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderDebitCardRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderDebitCardRet object
        /// </summary>
        public virtual SalesReceiptRetTenderDebitCardRet Clone()
        {
            return ((SalesReceiptRetTenderDebitCardRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderDepositRet")]
    public partial class SalesReceiptRetTenderDepositRet : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal tenderAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderDepositRet));
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
        /// Serializes current SalesReceiptRetTenderDepositRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderDepositRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderDepositRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderDepositRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderDepositRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderDepositRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderDepositRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderDepositRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderDepositRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderDepositRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderDepositRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderDepositRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderDepositRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderDepositRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderDepositRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderDepositRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderDepositRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderDepositRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderDepositRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderDepositRet object
        /// </summary>
        public virtual SalesReceiptRetTenderDepositRet Clone()
        {
            return ((SalesReceiptRetTenderDepositRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderGiftRet")]
    public partial class SalesReceiptRetTenderGiftRet : System.ComponentModel.INotifyPropertyChanged
    {

        private string giftCertificateNumberField;

        private decimal tenderAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GiftCertificateNumber
        {
            get
            {
                return this.giftCertificateNumberField;
            }
            set
            {
                if (((this.giftCertificateNumberField == null)
                            || (giftCertificateNumberField.Equals(value) != true)))
                {
                    this.giftCertificateNumberField = value;
                    this.OnPropertyChanged("GiftCertificateNumber", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderGiftRet));
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
        /// Serializes current SalesReceiptRetTenderGiftRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderGiftRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderGiftRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderGiftRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderGiftRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderGiftRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderGiftRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderGiftRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderGiftRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderGiftRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderGiftRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderGiftRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderGiftRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderGiftRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderGiftRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderGiftRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderGiftRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderGiftRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderGiftRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderGiftRet object
        /// </summary>
        public virtual SalesReceiptRetTenderGiftRet Clone()
        {
            return ((SalesReceiptRetTenderGiftRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetTenderGiftCardRet")]
    public partial class SalesReceiptRetTenderGiftCardRet : System.ComponentModel.INotifyPropertyChanged
    {

        private decimal tenderAmountField;

        private decimal tipAmountField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        private ObjectChangeTracker changeTrackerField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TenderAmount
        {
            get
            {
                return this.tenderAmountField;
            }
            set
            {
                if ((tenderAmountField.Equals(value) != true))
                {
                    this.tenderAmountField = value;
                    this.OnPropertyChanged("TenderAmount", value);
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TipAmount
        {
            get
            {
                return this.tipAmountField;
            }
            set
            {
                if ((tipAmountField.Equals(value) != true))
                {
                    this.tipAmountField = value;
                    this.OnPropertyChanged("TipAmount", value);
                }
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetTenderGiftCardRet));
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
        /// Serializes current SalesReceiptRetTenderGiftCardRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetTenderGiftCardRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderGiftCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetTenderGiftCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderGiftCardRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetTenderGiftCardRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetTenderGiftCardRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetTenderGiftCardRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetTenderGiftCardRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetTenderGiftCardRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetTenderGiftCardRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetTenderGiftCardRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetTenderGiftCardRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetTenderGiftCardRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetTenderGiftCardRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderGiftCardRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetTenderGiftCardRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetTenderGiftCardRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetTenderGiftCardRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetTenderGiftCardRet object
        /// </summary>
        public virtual SalesReceiptRetTenderGiftCardRet Clone()
        {
            return ((SalesReceiptRetTenderGiftCardRet)(this.MemberwiseClone()));
        }
        #endregion
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SalesReceiptRetDataExtRet")]
    public partial class SalesReceiptRetDataExtRet : System.ComponentModel.INotifyPropertyChanged
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
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SalesReceiptRetDataExtRet));
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
        /// Serializes current SalesReceiptRetDataExtRet object into an XML document
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
        /// Deserializes workflow markup into an SalesReceiptRetDataExtRet object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output SalesReceiptRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out SalesReceiptRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetDataExtRet);
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

        public static bool Deserialize(string xml, out SalesReceiptRetDataExtRet obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static SalesReceiptRetDataExtRet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((SalesReceiptRetDataExtRet)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static SalesReceiptRetDataExtRet Deserialize(System.IO.Stream s)
        {
            return ((SalesReceiptRetDataExtRet)(Serializer.Deserialize(s)));
        }

        /// <summary>
        /// Serializes current SalesReceiptRetDataExtRet object into file
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
        /// Deserializes xml markup from file into an SalesReceiptRetDataExtRet object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output SalesReceiptRetDataExtRet object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out SalesReceiptRetDataExtRet obj, out System.Exception exception)
        {
            exception = null;
            obj = default(SalesReceiptRetDataExtRet);
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

        public static bool LoadFromFile(string fileName, out SalesReceiptRetDataExtRet obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out SalesReceiptRetDataExtRet obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static SalesReceiptRetDataExtRet LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static SalesReceiptRetDataExtRet LoadFromFile(string fileName, System.Text.Encoding encoding)
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
        /// Create a clone of this SalesReceiptRetDataExtRet object
        /// </summary>
        public virtual SalesReceiptRetDataExtRet Clone()
        {
            return ((SalesReceiptRetDataExtRet)(this.MemberwiseClone()));
        }
        #endregion
    }

}