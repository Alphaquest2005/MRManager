﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MNIB_Distribution_Manager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MNIBDistributionManager")]
	public partial class MNIBDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBox(Box instance);
    partial void UpdateBox(Box instance);
    partial void DeleteBox(Box instance);
    partial void InsertHarvester(Harvester instance);
    partial void UpdateHarvester(Harvester instance);
    partial void DeleteHarvester(Harvester instance);
    partial void InsertExportDetail(ExportDetail instance);
    partial void UpdateExportDetail(ExportDetail instance);
    partial void DeleteExportDetail(ExportDetail instance);
    partial void InsertExport(Export instance);
    partial void UpdateExport(Export instance);
    partial void DeleteExport(Export instance);
    #endregion
		
		public MNIBDBDataContext() : 
				base(global::MNIB_Distribution_Manager.Properties.Settings.Default.MNIBDistributionManagerConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Item> Items
		{
			get
			{
				return this.GetTable<Item>();
			}
		}
		
		public System.Data.Linq.Table<Box> Boxes
		{
			get
			{
				return this.GetTable<Box>();
			}
		}
		
		public System.Data.Linq.Table<Harvester> Harvesters
		{
			get
			{
				return this.GetTable<Harvester>();
			}
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<ExportDetail> ExportDetails
		{
			get
			{
				return this.GetTable<ExportDetail>();
			}
		}
		
		public System.Data.Linq.Table<PurchaseOrderDetail> PurchaseOrderDetails
		{
			get
			{
				return this.GetTable<PurchaseOrderDetail>();
			}
		}
		
		public System.Data.Linq.Table<Export> Exports
		{
			get
			{
				return this.GetTable<Export>();
			}
		}
		
		public System.Data.Linq.Table<ExportReportLine> ExportReportLines
		{
			get
			{
				return this.GetTable<ExportReportLine>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Items")]
	public partial class Item
	{
		
		private string _ProductDescription;
		
		private string _ProductId;
		
		public Item()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductDescription", DbType="VarChar(30)")]
		public string ProductDescription
		{
			get
			{
				return this._ProductDescription;
			}
			set
			{
				if ((this._ProductDescription != value))
				{
					this._ProductDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductId", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				if ((this._ProductId != value))
				{
					this._ProductId = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Boxes")]
	public partial class Box : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _BoxId;
		
		private string _Description;
		
		private double _Weight;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBoxIdChanging(int value);
    partial void OnBoxIdChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnWeightChanging(double value);
    partial void OnWeightChanged();
    #endregion
		
		public Box()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BoxId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int BoxId
		{
			get
			{
				return this._BoxId;
			}
			set
			{
				if ((this._BoxId != value))
				{
					this.OnBoxIdChanging(value);
					this.SendPropertyChanging();
					this._BoxId = value;
					this.SendPropertyChanged("BoxId");
					this.OnBoxIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="Float NOT NULL")]
		public double Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._Weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Harvesters")]
	public partial class Harvester : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _HarvesterId;
		
		private string _Name;
		
		private string _Intials;
		
		private System.Nullable<bool> _CanDelete;
		
		private string _Password;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnHarvesterIdChanging(int value);
    partial void OnHarvesterIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnIntialsChanging(string value);
    partial void OnIntialsChanged();
    partial void OnCanDeleteChanging(System.Nullable<bool> value);
    partial void OnCanDeleteChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public Harvester()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HarvesterId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int HarvesterId
		{
			get
			{
				return this._HarvesterId;
			}
			set
			{
				if ((this._HarvesterId != value))
				{
					this.OnHarvesterIdChanging(value);
					this.SendPropertyChanging();
					this._HarvesterId = value;
					this.SendPropertyChanged("HarvesterId");
					this.OnHarvesterIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Intials", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Intials
		{
			get
			{
				return this._Intials;
			}
			set
			{
				if ((this._Intials != value))
				{
					this.OnIntialsChanging(value);
					this.SendPropertyChanging();
					this._Intials = value;
					this.SendPropertyChanged("Intials");
					this.OnIntialsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanDelete", DbType="Bit")]
		public System.Nullable<bool> CanDelete
		{
			get
			{
				return this._CanDelete;
			}
			set
			{
				if ((this._CanDelete != value))
				{
					this.OnCanDeleteChanging(value);
					this.SendPropertyChanging();
					this._CanDelete = value;
					this.SendPropertyChanged("CanDelete");
					this.OnCanDeleteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(50)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customers")]
	public partial class Customer
	{
		
		private string _CustomerName;
		
		private string _CustomerAddress;
		
		private string _TicketNo;
		
		private string _CustomerNumber;
		
		private System.Nullable<System.DateTime> _TicketDate;
		
		private string _OrderNo;
		
		public Customer()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerName", DbType="VarChar(40)")]
		public string CustomerName
		{
			get
			{
				return this._CustomerName;
			}
			set
			{
				if ((this._CustomerName != value))
				{
					this._CustomerName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerAddress", DbType="VarChar(76) NOT NULL", CanBeNull=false)]
		public string CustomerAddress
		{
			get
			{
				return this._CustomerAddress;
			}
			set
			{
				if ((this._CustomerAddress != value))
				{
					this._CustomerAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TicketNo", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string TicketNo
		{
			get
			{
				return this._TicketNo;
			}
			set
			{
				if ((this._TicketNo != value))
				{
					this._TicketNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerNumber", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string CustomerNumber
		{
			get
			{
				return this._CustomerNumber;
			}
			set
			{
				if ((this._CustomerNumber != value))
				{
					this._CustomerNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TicketDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> TicketDate
		{
			get
			{
				return this._TicketDate;
			}
			set
			{
				if ((this._TicketDate != value))
				{
					this._TicketDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderNo", DbType="VarChar(15)")]
		public string OrderNo
		{
			get
			{
				return this._OrderNo;
			}
			set
			{
				if ((this._OrderNo != value))
				{
					this._OrderNo = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ExportDetails")]
	public partial class ExportDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ExportDetailId;
		
		private int _ExportId;
		
		private double _Weight;
		
		private string _Barcode;
		
		private int _LineNumber;
		
		private string _ReceiptNumber;
		
		private int _BoxId;
		
		private string _TicketNo;
		
		private double _BoxWeight;
		
		private string _CustomerInfo;
		
		private string _ProductDescription;
		
		private string _OrderNo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnExportDetailIdChanging(int value);
    partial void OnExportDetailIdChanged();
    partial void OnExportIdChanging(int value);
    partial void OnExportIdChanged();
    partial void OnWeightChanging(double value);
    partial void OnWeightChanged();
    partial void OnBarcodeChanging(string value);
    partial void OnBarcodeChanged();
    partial void OnLineNumberChanging(int value);
    partial void OnLineNumberChanged();
    partial void OnReceiptNumberChanging(string value);
    partial void OnReceiptNumberChanged();
    partial void OnBoxIdChanging(int value);
    partial void OnBoxIdChanged();
    partial void OnTicketNoChanging(string value);
    partial void OnTicketNoChanged();
    partial void OnBoxWeightChanging(double value);
    partial void OnBoxWeightChanged();
    partial void OnCustomerInfoChanging(string value);
    partial void OnCustomerInfoChanged();
    partial void OnProductDescriptionChanging(string value);
    partial void OnProductDescriptionChanged();
    partial void OnOrderNoChanging(string value);
    partial void OnOrderNoChanged();
    #endregion
		
		public ExportDetail()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportDetailId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ExportDetailId
		{
			get
			{
				return this._ExportDetailId;
			}
			set
			{
				if ((this._ExportDetailId != value))
				{
					this.OnExportDetailIdChanging(value);
					this.SendPropertyChanging();
					this._ExportDetailId = value;
					this.SendPropertyChanged("ExportDetailId");
					this.OnExportDetailIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportId", DbType="Int NOT NULL")]
		public int ExportId
		{
			get
			{
				return this._ExportId;
			}
			set
			{
				if ((this._ExportId != value))
				{
					this.OnExportIdChanging(value);
					this.SendPropertyChanging();
					this._ExportId = value;
					this.SendPropertyChanged("ExportId");
					this.OnExportIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="Float NOT NULL")]
		public double Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._Weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Barcode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Barcode
		{
			get
			{
				return this._Barcode;
			}
			set
			{
				if ((this._Barcode != value))
				{
					this.OnBarcodeChanging(value);
					this.SendPropertyChanging();
					this._Barcode = value;
					this.SendPropertyChanged("Barcode");
					this.OnBarcodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LineNumber", DbType="Int NOT NULL")]
		public int LineNumber
		{
			get
			{
				return this._LineNumber;
			}
			set
			{
				if ((this._LineNumber != value))
				{
					this.OnLineNumberChanging(value);
					this.SendPropertyChanging();
					this._LineNumber = value;
					this.SendPropertyChanged("LineNumber");
					this.OnLineNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceiptNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ReceiptNumber
		{
			get
			{
				return this._ReceiptNumber;
			}
			set
			{
				if ((this._ReceiptNumber != value))
				{
					this.OnReceiptNumberChanging(value);
					this.SendPropertyChanging();
					this._ReceiptNumber = value;
					this.SendPropertyChanged("ReceiptNumber");
					this.OnReceiptNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BoxId", DbType="Int NOT NULL")]
		public int BoxId
		{
			get
			{
				return this._BoxId;
			}
			set
			{
				if ((this._BoxId != value))
				{
					this.OnBoxIdChanging(value);
					this.SendPropertyChanging();
					this._BoxId = value;
					this.SendPropertyChanged("BoxId");
					this.OnBoxIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TicketNo", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string TicketNo
		{
			get
			{
				return this._TicketNo;
			}
			set
			{
				if ((this._TicketNo != value))
				{
					this.OnTicketNoChanging(value);
					this.SendPropertyChanging();
					this._TicketNo = value;
					this.SendPropertyChanged("TicketNo");
					this.OnTicketNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BoxWeight", DbType="Float NOT NULL")]
		public double BoxWeight
		{
			get
			{
				return this._BoxWeight;
			}
			set
			{
				if ((this._BoxWeight != value))
				{
					this.OnBoxWeightChanging(value);
					this.SendPropertyChanging();
					this._BoxWeight = value;
					this.SendPropertyChanged("BoxWeight");
					this.OnBoxWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerInfo", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string CustomerInfo
		{
			get
			{
				return this._CustomerInfo;
			}
			set
			{
				if ((this._CustomerInfo != value))
				{
					this.OnCustomerInfoChanging(value);
					this.SendPropertyChanging();
					this._CustomerInfo = value;
					this.SendPropertyChanged("CustomerInfo");
					this.OnCustomerInfoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductDescription", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProductDescription
		{
			get
			{
				return this._ProductDescription;
			}
			set
			{
				if ((this._ProductDescription != value))
				{
					this.OnProductDescriptionChanging(value);
					this.SendPropertyChanging();
					this._ProductDescription = value;
					this.SendPropertyChanged("ProductDescription");
					this.OnProductDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderNo", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string OrderNo
		{
			get
			{
				return this._OrderNo;
			}
			set
			{
				if ((this._OrderNo != value))
				{
					this.OnOrderNoChanging(value);
					this.SendPropertyChanging();
					this._OrderNo = value;
					this.SendPropertyChanged("OrderNo");
					this.OnOrderNoChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PurchaseOrderDetails")]
	public partial class PurchaseOrderDetail
	{
		
		private string _LotNumber;
		
		private string _PurchaseOrderNo;
		
		private int _LineNumber;
		
		private string _ItemNumber;
		
		private string _ItemDescription;
		
		public PurchaseOrderDetail()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LotNumber", DbType="VarChar(26)")]
		public string LotNumber
		{
			get
			{
				return this._LotNumber;
			}
			set
			{
				if ((this._LotNumber != value))
				{
					this._LotNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PurchaseOrderNo", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string PurchaseOrderNo
		{
			get
			{
				return this._PurchaseOrderNo;
			}
			set
			{
				if ((this._PurchaseOrderNo != value))
				{
					this._PurchaseOrderNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LineNumber", DbType="Int NOT NULL")]
		public int LineNumber
		{
			get
			{
				return this._LineNumber;
			}
			set
			{
				if ((this._LineNumber != value))
				{
					this._LineNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ItemNumber", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string ItemNumber
		{
			get
			{
				return this._ItemNumber;
			}
			set
			{
				if ((this._ItemNumber != value))
				{
					this._ItemNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ItemDescription", DbType="VarChar(50)")]
		public string ItemDescription
		{
			get
			{
				return this._ItemDescription;
			}
			set
			{
				if ((this._ItemDescription != value))
				{
					this._ItemDescription = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Exports")]
	public partial class Export : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ExportId;
		
		private System.DateTime _ExportDate;
		
		private string _ProductNumber;
		
		private string _ProductDescription;
		
		private double _TotalWeight;
		
		private string _SourceTransaction;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnExportIdChanging(int value);
    partial void OnExportIdChanged();
    partial void OnExportDateChanging(System.DateTime value);
    partial void OnExportDateChanged();
    partial void OnProductNumberChanging(string value);
    partial void OnProductNumberChanged();
    partial void OnProductDescriptionChanging(string value);
    partial void OnProductDescriptionChanged();
    partial void OnTotalWeightChanging(double value);
    partial void OnTotalWeightChanged();
    partial void OnSourceTransactionChanging(string value);
    partial void OnSourceTransactionChanged();
    #endregion
		
		public Export()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ExportId
		{
			get
			{
				return this._ExportId;
			}
			set
			{
				if ((this._ExportId != value))
				{
					this.OnExportIdChanging(value);
					this.SendPropertyChanging();
					this._ExportId = value;
					this.SendPropertyChanged("ExportId");
					this.OnExportIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportDate", DbType="Date NOT NULL")]
		public System.DateTime ExportDate
		{
			get
			{
				return this._ExportDate;
			}
			set
			{
				if ((this._ExportDate != value))
				{
					this.OnExportDateChanging(value);
					this.SendPropertyChanging();
					this._ExportDate = value;
					this.SendPropertyChanged("ExportDate");
					this.OnExportDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProductNumber
		{
			get
			{
				return this._ProductNumber;
			}
			set
			{
				if ((this._ProductNumber != value))
				{
					this.OnProductNumberChanging(value);
					this.SendPropertyChanging();
					this._ProductNumber = value;
					this.SendPropertyChanged("ProductNumber");
					this.OnProductNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductDescription", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProductDescription
		{
			get
			{
				return this._ProductDescription;
			}
			set
			{
				if ((this._ProductDescription != value))
				{
					this.OnProductDescriptionChanging(value);
					this.SendPropertyChanging();
					this._ProductDescription = value;
					this.SendPropertyChanged("ProductDescription");
					this.OnProductDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalWeight", DbType="Float NOT NULL")]
		public double TotalWeight
		{
			get
			{
				return this._TotalWeight;
			}
			set
			{
				if ((this._TotalWeight != value))
				{
					this.OnTotalWeightChanging(value);
					this.SendPropertyChanging();
					this._TotalWeight = value;
					this.SendPropertyChanged("TotalWeight");
					this.OnTotalWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceTransaction", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SourceTransaction
		{
			get
			{
				return this._SourceTransaction;
			}
			set
			{
				if ((this._SourceTransaction != value))
				{
					this.OnSourceTransactionChanging(value);
					this.SendPropertyChanging();
					this._SourceTransaction = value;
					this.SendPropertyChanged("SourceTransaction");
					this.OnSourceTransactionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ExportReportLines")]
	public partial class ExportReportLine
	{
		
		private System.DateTime _ExportDate;
		
		private string _SourceTransaction;
		
		private string _ReceiptNumber;
		
		private string _CustomerName;
		
		private string _ProductNumber;
		
		private string _ProductDescription;
		
		private int _LineNumber;
		
		private double _Weight;
		
		private string _ExportNumber;
		
		private string _TicketNo;
		
		private string _Harvester;
		
		public ExportReportLine()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportDate", DbType="Date NOT NULL")]
		public System.DateTime ExportDate
		{
			get
			{
				return this._ExportDate;
			}
			set
			{
				if ((this._ExportDate != value))
				{
					this._ExportDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceTransaction", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SourceTransaction
		{
			get
			{
				return this._SourceTransaction;
			}
			set
			{
				if ((this._SourceTransaction != value))
				{
					this._SourceTransaction = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceiptNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ReceiptNumber
		{
			get
			{
				return this._ReceiptNumber;
			}
			set
			{
				if ((this._ReceiptNumber != value))
				{
					this._ReceiptNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerName", DbType="VarChar(40)")]
		public string CustomerName
		{
			get
			{
				return this._CustomerName;
			}
			set
			{
				if ((this._CustomerName != value))
				{
					this._CustomerName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProductNumber
		{
			get
			{
				return this._ProductNumber;
			}
			set
			{
				if ((this._ProductNumber != value))
				{
					this._ProductNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductDescription", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProductDescription
		{
			get
			{
				return this._ProductDescription;
			}
			set
			{
				if ((this._ProductDescription != value))
				{
					this._ProductDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LineNumber", DbType="Int NOT NULL")]
		public int LineNumber
		{
			get
			{
				return this._LineNumber;
			}
			set
			{
				if ((this._LineNumber != value))
				{
					this._LineNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="Float NOT NULL")]
		public double Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this._Weight = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExportNumber", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ExportNumber
		{
			get
			{
				return this._ExportNumber;
			}
			set
			{
				if ((this._ExportNumber != value))
				{
					this._ExportNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TicketNo", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string TicketNo
		{
			get
			{
				return this._TicketNo;
			}
			set
			{
				if ((this._TicketNo != value))
				{
					this._TicketNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Harvester", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Harvester
		{
			get
			{
				return this._Harvester;
			}
			set
			{
				if ((this._Harvester != value))
				{
					this._Harvester = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
