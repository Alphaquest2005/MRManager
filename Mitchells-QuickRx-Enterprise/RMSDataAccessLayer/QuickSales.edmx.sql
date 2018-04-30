
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/07/2016 20:54:20
-- Generated from EDMX file: C:\prism1\HillsAndValley\QuickRx-Enterprise\RMSDataAccessLayer\QuickSales.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuickSales-Enterprise];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BatchCashier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Batches] DROP CONSTRAINT [FK_BatchCashier];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchCashier1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Batches] DROP CONSTRAINT [FK_BatchCashier1];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchTransactionBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase] DROP CONSTRAINT [FK_BatchTransactionBase];
GO
IF OBJECT_ID(N'[dbo].[FK_BatchTransactionBase1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase] DROP CONSTRAINT [FK_BatchTransactionBase1];
GO
IF OBJECT_ID(N'[dbo].[FK_Cashier_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Cashier] DROP CONSTRAINT [FK_Cashier_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_CashierCashierLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashierLogs] DROP CONSTRAINT [FK_CashierCashierLog];
GO
IF OBJECT_ID(N'[dbo].[FK_CashierTransactionBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase] DROP CONSTRAINT [FK_CashierTransactionBase];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyStore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stores] DROP CONSTRAINT [FK_CompanyStore];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase] DROP CONSTRAINT [FK_CustomerTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorPrescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase_Prescription] DROP CONSTRAINT [FK_DoctorPrescription];
GO
IF OBJECT_ID(N'[dbo].[FK_Medicine_inherits_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Item_Medicine] DROP CONSTRAINT [FK_Medicine_inherits_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Patient] DROP CONSTRAINT [FK_Patient_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientPrescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase_Prescription] DROP CONSTRAINT [FK_PatientPrescription];
GO
IF OBJECT_ID(N'[dbo].[FK_Prescription_inherits_TransactionBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase_Prescription] DROP CONSTRAINT [FK_Prescription_inherits_TransactionBase];
GO
IF OBJECT_ID(N'[dbo].[FK_PrescriptionEntry_inherits_TransactionEntryBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEntryBase_PrescriptionEntry] DROP CONSTRAINT [FK_PrescriptionEntry_inherits_TransactionEntryBase];
GO
IF OBJECT_ID(N'[dbo].[FK_QuickPrescription_inherits_TransactionBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase_QuickPrescription] DROP CONSTRAINT [FK_QuickPrescription_inherits_TransactionBase];
GO
IF OBJECT_ID(N'[dbo].[FK_StationBatch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Batches] DROP CONSTRAINT [FK_StationBatch];
GO
IF OBJECT_ID(N'[dbo].[FK_StationTransactionBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionBase] DROP CONSTRAINT [FK_StationTransactionBase];
GO
IF OBJECT_ID(N'[dbo].[FK_StockItem_inherits_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Item_StockItem] DROP CONSTRAINT [FK_StockItem_inherits_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreStation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stations] DROP CONSTRAINT [FK_StoreStation];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEntryItem_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEntryItem] DROP CONSTRAINT [FK_TransactionEntryItem_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEntryItem_TransactionEntryBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEntryItem] DROP CONSTRAINT [FK_TransactionEntryItem_TransactionEntryBase];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionTransactionEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEntryBase] DROP CONSTRAINT [FK_TransactionTransactionEntry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Batches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Batches];
GO
IF OBJECT_ID(N'[dbo].[CashierLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashierLogs];
GO
IF OBJECT_ID(N'[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
IF OBJECT_ID(N'[dbo].[Item]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Item];
GO
IF OBJECT_ID(N'[dbo].[Item_Medicine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Item_Medicine];
GO
IF OBJECT_ID(N'[dbo].[Item_StockItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Item_StockItem];
GO
IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Persons_Cashier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Cashier];
GO
IF OBJECT_ID(N'[dbo].[Persons_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Doctor];
GO
IF OBJECT_ID(N'[dbo].[Persons_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Patient];
GO
IF OBJECT_ID(N'[dbo].[QBInventoryItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QBInventoryItems];
GO
IF OBJECT_ID(N'[dbo].[Stations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stations];
GO
IF OBJECT_ID(N'[dbo].[Stores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stores];
GO
IF OBJECT_ID(N'[dbo].[TransactionBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionBase];
GO
IF OBJECT_ID(N'[dbo].[TransactionBase_Prescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionBase_Prescription];
GO
IF OBJECT_ID(N'[dbo].[TransactionBase_QuickPrescription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionBase_QuickPrescription];
GO
IF OBJECT_ID(N'[dbo].[TransactionEntryBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionEntryBase];
GO
IF OBJECT_ID(N'[dbo].[TransactionEntryBase_PrescriptionEntry]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionEntryBase_PrescriptionEntry];
GO
IF OBJECT_ID(N'[dbo].[TransactionEntryItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionEntryItem];
GO
IF OBJECT_ID(N'[RMSEntitiesStoreContainer].[SearchView]', 'U') IS NOT NULL
    DROP TABLE [RMSEntitiesStoreContainer].[SearchView];
GO
IF OBJECT_ID(N'[RMSEntitiesStoreContainer].[TransactionsView]', 'U') IS NOT NULL
    DROP TABLE [RMSEntitiesStoreContainer].[TransactionsView];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Item'
CREATE TABLE [dbo].[Item] (
    [Description] nvarchar(1000)  NULL,
    [ItemNotDiscountable] bit  NULL,
    [ItemId] int IDENTITY(1,1) NOT NULL,
    [ItemLookupCode] nvarchar(1000)  NULL,
    [Department] nvarchar(255)  NULL,
    [Category] nvarchar(255)  NULL,
    [Price] float  NOT NULL,
    [Cost] float  NULL,
    [Quantity] float  NOT NULL,
    [ExtendedDescription] nvarchar(max)  NULL,
    [Inactive] bit  NULL,
    [DateCreated] datetime  NULL,
    [SalesTax] float  NULL,
    [QBItemListID] nvarchar(50)  NULL,
    [UnitOfMeasure] nvarchar(50)  NULL,
    [ItemName] nvarchar(50)  NULL,
    [ItemNumber] nvarchar(50)  NULL,
    [Size] nvarchar(50)  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL,
    [QBActive] bit  NULL
);
GO

-- Creating table 'TransactionBase'
CREATE TABLE [dbo].[TransactionBase] (
    [StationId] int  NOT NULL,
    [BatchId] int  NOT NULL,
    [CloseBatchId] int  NULL,
    [Time] datetime  NOT NULL,
    [CustomerId] int  NULL,
    [CashierId] int  NOT NULL,
    [Comment] nvarchar(255)  NULL,
    [ReferenceNumber] nvarchar(50)  NULL,
    [StoreCode] varchar(30)  NULL,
    [TransactionId] int IDENTITY(1,1) NOT NULL,
    [OpenClose] bit  NOT NULL,
    [PharmacistId] int  NULL,
    [Status] varchar(50)  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'TransactionEntryBase'
CREATE TABLE [dbo].[TransactionEntryBase] (
    [StoreID] int  NOT NULL,
    [TransactionEntryId] int IDENTITY(1,1) NOT NULL,
    [TransactionId] int  NOT NULL,
    [Quantity] float  NOT NULL,
    [Taxable] bit  NOT NULL,
    [Comment] nvarchar(255)  NULL,
    [TransactionTime] datetime  NULL,
    [SalesTaxPercent] float  NOT NULL,
    [Discount] float  NULL,
    [EntryNumber] smallint  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Company'
CREATE TABLE [dbo].[Company] (
    [CompanyId] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Address1] nvarchar(max)  NULL,
    [SoftwareName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Motto] nvarchar(max)  NOT NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [CompanyName] nvarchar(max)  NULL,
    [Salutation] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [InActive] bit  NULL,
    [Sex] bit  NULL,
    [DOB] datetime  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'Batches'
CREATE TABLE [dbo].[Batches] (
    [BatchId] int IDENTITY(1,1) NOT NULL,
    [OpeningCash] float  NOT NULL,
    [EndingCash] float  NULL,
    [OpeningTime] datetime  NOT NULL,
    [ClosingTime] datetime  NULL,
    [TotalTender] float  NULL,
    [TotalChange] float  NULL,
    [Status] nvarchar(max)  NOT NULL,
    [StationId] int  NOT NULL,
    [OpeningCashier] int  NOT NULL,
    [ClosingCashier] int  NULL,
    [Sales] float  NOT NULL,
    [OpenTransactions] int  NOT NULL,
    [CloseTransactions] int  NOT NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'Stations'
CREATE TABLE [dbo].[Stations] (
    [StationId] int IDENTITY(1,1) NOT NULL,
    [StationCode] nvarchar(max)  NOT NULL,
    [StoreId] int  NOT NULL,
    [ReceiptPrinterName] nvarchar(max)  NOT NULL,
    [MachineName] nvarchar(max)  NOT NULL,
    [PrintServer] nvarchar(max)  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'Stores'
CREATE TABLE [dbo].[Stores] (
    [StoreId] int IDENTITY(1,1) NOT NULL,
    [StoreCode] nvarchar(max)  NOT NULL,
    [StoreAddress] nvarchar(max)  NOT NULL,
    [CompanyId] int  NOT NULL,
    [TransactionSeed] int  NOT NULL,
    [SeedTransaction] int  NOT NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'CashierLogs'
CREATE TABLE [dbo].[CashierLogs] (
    [CashierLogId] int IDENTITY(1,1) NOT NULL,
    [MachineName] nvarchar(max)  NOT NULL,
    [LoginTime] datetime  NOT NULL,
    [LogoutTime] datetime  NULL,
    [Status] nvarchar(max)  NOT NULL,
    [PersonId] int  NOT NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'QBInventoryItems'
CREATE TABLE [dbo].[QBInventoryItems] (
    [ListID] nvarchar(50)  NOT NULL,
    [ItemDesc2] nvarchar(max)  NULL,
    [ItemName] nvarchar(max)  NULL,
    [Size] nvarchar(max)  NULL,
    [DepartmentCode] nvarchar(max)  NULL,
    [ItemNumber] int  NOT NULL,
    [TaxCode] nvarchar(max)  NULL,
    [Price] float  NULL,
    [Quantity] float  NULL,
    [UnitOfMeasure] nvarchar(50)  NULL,
    [EntryTimeStamp] binary(8)  NOT NULL
);
GO

-- Creating table 'TransactionsViews'
CREATE TABLE [dbo].[TransactionsViews] (
    [TransactionId] int  NOT NULL,
    [Time] datetime  NOT NULL,
    [ReferenceNumber] nvarchar(50)  NULL,
    [TotalSales] decimal(28,7)  NULL,
    [CustomerId] int  NULL
);
GO

-- Creating table 'SearchViews'
CREATE TABLE [dbo].[SearchViews] (
    [Time] datetime  NOT NULL,
    [TransactionId] int  NOT NULL,
    [ItemId] int  NOT NULL,
    [ItemInfo] nvarchar(1053)  NOT NULL,
    [PatientId] int  NOT NULL,
    [PatientInfo] nvarchar(max)  NOT NULL,
    [DoctorId] int  NOT NULL,
    [DoctorInfo] nvarchar(max)  NOT NULL,
    [SearchInfo] nvarchar(max)  NULL
);
GO

-- Creating table 'TransactionEntryItems'
CREATE TABLE [dbo].[TransactionEntryItems] (
    [TransactionEntryId] int  NOT NULL,
    [QBItemListID] varchar(50)  NOT NULL,
    [ItemNumber] varchar(50)  NOT NULL,
    [ItemName] varchar(50)  NOT NULL,
    [ItemId] int  NULL
);
GO

-- Creating table 'Persons_Cashier'
CREATE TABLE [dbo].[Persons_Cashier] (
    [SPassword] nvarchar(max)  NULL,
    [LoginName] nvarchar(max)  NULL,
    [Role] nvarchar(50)  NULL,
    [Initials] nvarchar(3)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Persons_Doctor'
CREATE TABLE [dbo].[Persons_Doctor] (
    [Code] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'TransactionBase_Prescription'
CREATE TABLE [dbo].[TransactionBase_Prescription] (
    [DoctorId] int  NOT NULL,
    [PatientId] int  NOT NULL,
    [TransactionId] int  NOT NULL
);
GO

-- Creating table 'Persons_Patient'
CREATE TABLE [dbo].[Persons_Patient] (
    [Allergies] nvarchar(max)  NULL,
    [Guardian] nvarchar(max)  NULL,
    [Discount] float  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'TransactionEntryBase_PrescriptionEntry'
CREATE TABLE [dbo].[TransactionEntryBase_PrescriptionEntry] (
    [Dosage] nvarchar(max)  NULL,
    [ExpiryDate] datetime  NULL,
    [Repeat] int  NULL,
    [TransactionEntryId] int  NOT NULL
);
GO

-- Creating table 'Item_Medicine'
CREATE TABLE [dbo].[Item_Medicine] (
    [SuggestedDosage] nvarchar(max)  NULL,
    [ExpiryDate] datetime  NULL,
    [ItemId] int  NOT NULL
);
GO

-- Creating table 'Item_StockItem'
CREATE TABLE [dbo].[Item_StockItem] (
    [ItemId] int  NOT NULL
);
GO

-- Creating table 'TransactionBase_QuickPrescription'
CREATE TABLE [dbo].[TransactionBase_QuickPrescription] (
    [TransactionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ItemId] in table 'Item'
ALTER TABLE [dbo].[Item]
ADD CONSTRAINT [PK_Item]
    PRIMARY KEY CLUSTERED ([ItemId] ASC);
GO

-- Creating primary key on [TransactionId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [PK_TransactionBase]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [TransactionEntryId] in table 'TransactionEntryBase'
ALTER TABLE [dbo].[TransactionEntryBase]
ADD CONSTRAINT [PK_TransactionEntryBase]
    PRIMARY KEY CLUSTERED ([TransactionEntryId] ASC);
GO

-- Creating primary key on [CompanyId] in table 'Company'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [PK_Company]
    PRIMARY KEY CLUSTERED ([CompanyId] ASC);
GO

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [BatchId] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [PK_Batches]
    PRIMARY KEY CLUSTERED ([BatchId] ASC);
GO

-- Creating primary key on [StationId] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [PK_Stations]
    PRIMARY KEY CLUSTERED ([StationId] ASC);
GO

-- Creating primary key on [StoreId] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [PK_Stores]
    PRIMARY KEY CLUSTERED ([StoreId] ASC);
GO

-- Creating primary key on [CashierLogId] in table 'CashierLogs'
ALTER TABLE [dbo].[CashierLogs]
ADD CONSTRAINT [PK_CashierLogs]
    PRIMARY KEY CLUSTERED ([CashierLogId] ASC);
GO

-- Creating primary key on [ListID] in table 'QBInventoryItems'
ALTER TABLE [dbo].[QBInventoryItems]
ADD CONSTRAINT [PK_QBInventoryItems]
    PRIMARY KEY CLUSTERED ([ListID] ASC);
GO

-- Creating primary key on [TransactionId] in table 'TransactionsViews'
ALTER TABLE [dbo].[TransactionsViews]
ADD CONSTRAINT [PK_TransactionsViews]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [TransactionId] in table 'SearchViews'
ALTER TABLE [dbo].[SearchViews]
ADD CONSTRAINT [PK_SearchViews]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [TransactionEntryId] in table 'TransactionEntryItems'
ALTER TABLE [dbo].[TransactionEntryItems]
ADD CONSTRAINT [PK_TransactionEntryItems]
    PRIMARY KEY CLUSTERED ([TransactionEntryId] ASC);
GO

-- Creating primary key on [Id] in table 'Persons_Cashier'
ALTER TABLE [dbo].[Persons_Cashier]
ADD CONSTRAINT [PK_Persons_Cashier]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Persons_Doctor'
ALTER TABLE [dbo].[Persons_Doctor]
ADD CONSTRAINT [PK_Persons_Doctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TransactionId] in table 'TransactionBase_Prescription'
ALTER TABLE [dbo].[TransactionBase_Prescription]
ADD CONSTRAINT [PK_TransactionBase_Prescription]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [Id] in table 'Persons_Patient'
ALTER TABLE [dbo].[Persons_Patient]
ADD CONSTRAINT [PK_Persons_Patient]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TransactionEntryId] in table 'TransactionEntryBase_PrescriptionEntry'
ALTER TABLE [dbo].[TransactionEntryBase_PrescriptionEntry]
ADD CONSTRAINT [PK_TransactionEntryBase_PrescriptionEntry]
    PRIMARY KEY CLUSTERED ([TransactionEntryId] ASC);
GO

-- Creating primary key on [ItemId] in table 'Item_Medicine'
ALTER TABLE [dbo].[Item_Medicine]
ADD CONSTRAINT [PK_Item_Medicine]
    PRIMARY KEY CLUSTERED ([ItemId] ASC);
GO

-- Creating primary key on [ItemId] in table 'Item_StockItem'
ALTER TABLE [dbo].[Item_StockItem]
ADD CONSTRAINT [PK_Item_StockItem]
    PRIMARY KEY CLUSTERED ([ItemId] ASC);
GO

-- Creating primary key on [TransactionId] in table 'TransactionBase_QuickPrescription'
ALTER TABLE [dbo].[TransactionBase_QuickPrescription]
ADD CONSTRAINT [PK_TransactionBase_QuickPrescription]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TransactionId] in table 'TransactionEntryBase'
ALTER TABLE [dbo].[TransactionEntryBase]
ADD CONSTRAINT [FK_TransactionTransactionEntry]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionBase]
        ([TransactionId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionTransactionEntry'
CREATE INDEX [IX_FK_TransactionTransactionEntry]
ON [dbo].[TransactionEntryBase]
    ([TransactionId]);
GO

-- Creating foreign key on [CustomerId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_CustomerTransaction]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerTransaction'
CREATE INDEX [IX_FK_CustomerTransaction]
ON [dbo].[TransactionBase]
    ([CustomerId]);
GO

-- Creating foreign key on [CashierId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_CashierTransactionBase]
    FOREIGN KEY ([CashierId])
    REFERENCES [dbo].[Persons_Cashier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CashierTransactionBase'
CREATE INDEX [IX_FK_CashierTransactionBase]
ON [dbo].[TransactionBase]
    ([CashierId]);
GO

-- Creating foreign key on [BatchId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_BatchTransactionBase]
    FOREIGN KEY ([BatchId])
    REFERENCES [dbo].[Batches]
        ([BatchId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchTransactionBase'
CREATE INDEX [IX_FK_BatchTransactionBase]
ON [dbo].[TransactionBase]
    ([BatchId]);
GO

-- Creating foreign key on [StoreId] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [FK_StoreStation]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Stores]
        ([StoreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreStation'
CREATE INDEX [IX_FK_StoreStation]
ON [dbo].[Stations]
    ([StoreId]);
GO

-- Creating foreign key on [StationId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_StationTransactionBase]
    FOREIGN KEY ([StationId])
    REFERENCES [dbo].[Stations]
        ([StationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StationTransactionBase'
CREATE INDEX [IX_FK_StationTransactionBase]
ON [dbo].[TransactionBase]
    ([StationId]);
GO

-- Creating foreign key on [DoctorId] in table 'TransactionBase_Prescription'
ALTER TABLE [dbo].[TransactionBase_Prescription]
ADD CONSTRAINT [FK_DoctorPrescription]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Persons_Doctor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorPrescription'
CREATE INDEX [IX_FK_DoctorPrescription]
ON [dbo].[TransactionBase_Prescription]
    ([DoctorId]);
GO

-- Creating foreign key on [PatientId] in table 'TransactionBase_Prescription'
ALTER TABLE [dbo].[TransactionBase_Prescription]
ADD CONSTRAINT [FK_PatientPrescription]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Persons_Patient]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientPrescription'
CREATE INDEX [IX_FK_PatientPrescription]
ON [dbo].[TransactionBase_Prescription]
    ([PatientId]);
GO

-- Creating foreign key on [CompanyId] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [FK_CompanyStore]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Company]
        ([CompanyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyStore'
CREATE INDEX [IX_FK_CompanyStore]
ON [dbo].[Stores]
    ([CompanyId]);
GO

-- Creating foreign key on [StationId] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [FK_StationBatch]
    FOREIGN KEY ([StationId])
    REFERENCES [dbo].[Stations]
        ([StationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StationBatch'
CREATE INDEX [IX_FK_StationBatch]
ON [dbo].[Batches]
    ([StationId]);
GO

-- Creating foreign key on [PersonId] in table 'CashierLogs'
ALTER TABLE [dbo].[CashierLogs]
ADD CONSTRAINT [FK_CashierCashierLog]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons_Cashier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CashierCashierLog'
CREATE INDEX [IX_FK_CashierCashierLog]
ON [dbo].[CashierLogs]
    ([PersonId]);
GO

-- Creating foreign key on [OpeningCashier] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [FK_BatchCashier]
    FOREIGN KEY ([OpeningCashier])
    REFERENCES [dbo].[Persons_Cashier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchCashier'
CREATE INDEX [IX_FK_BatchCashier]
ON [dbo].[Batches]
    ([OpeningCashier]);
GO

-- Creating foreign key on [ClosingCashier] in table 'Batches'
ALTER TABLE [dbo].[Batches]
ADD CONSTRAINT [FK_BatchCashier1]
    FOREIGN KEY ([ClosingCashier])
    REFERENCES [dbo].[Persons_Cashier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchCashier1'
CREATE INDEX [IX_FK_BatchCashier1]
ON [dbo].[Batches]
    ([ClosingCashier]);
GO

-- Creating foreign key on [CloseBatchId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_BatchTransactionBase1]
    FOREIGN KEY ([CloseBatchId])
    REFERENCES [dbo].[Batches]
        ([BatchId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BatchTransactionBase1'
CREATE INDEX [IX_FK_BatchTransactionBase1]
ON [dbo].[TransactionBase]
    ([CloseBatchId]);
GO

-- Creating foreign key on [QBItemListID] in table 'Item'
ALTER TABLE [dbo].[Item]
ADD CONSTRAINT [FK_Item_QBInventoryItems]
    FOREIGN KEY ([QBItemListID])
    REFERENCES [dbo].[QBInventoryItems]
        ([ListID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_QBInventoryItems'
CREATE INDEX [IX_FK_Item_QBInventoryItems]
ON [dbo].[Item]
    ([QBItemListID]);
GO

-- Creating foreign key on [PharmacistId] in table 'TransactionBase'
ALTER TABLE [dbo].[TransactionBase]
ADD CONSTRAINT [FK_TransactionBase_Persons_Pharmacist]
    FOREIGN KEY ([PharmacistId])
    REFERENCES [dbo].[Persons_Cashier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionBase_Persons_Pharmacist'
CREATE INDEX [IX_FK_TransactionBase_Persons_Pharmacist]
ON [dbo].[TransactionBase]
    ([PharmacistId]);
GO

-- Creating foreign key on [TransactionId] in table 'SearchViews'
ALTER TABLE [dbo].[SearchViews]
ADD CONSTRAINT [FK_TransactionsearchView]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionBase_Prescription]
        ([TransactionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemId] in table 'TransactionEntryItems'
ALTER TABLE [dbo].[TransactionEntryItems]
ADD CONSTRAINT [FK_TransactionEntryItem_Item]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Item]
        ([ItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionEntryItem_Item'
CREATE INDEX [IX_FK_TransactionEntryItem_Item]
ON [dbo].[TransactionEntryItems]
    ([ItemId]);
GO

-- Creating foreign key on [TransactionEntryId] in table 'TransactionEntryItems'
ALTER TABLE [dbo].[TransactionEntryItems]
ADD CONSTRAINT [FK_TransactionEntryItem_TransactionEntryBase]
    FOREIGN KEY ([TransactionEntryId])
    REFERENCES [dbo].[TransactionEntryBase]
        ([TransactionEntryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Persons_Cashier'
ALTER TABLE [dbo].[Persons_Cashier]
ADD CONSTRAINT [FK_Cashier_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Persons_Doctor'
ALTER TABLE [dbo].[Persons_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TransactionId] in table 'TransactionBase_Prescription'
ALTER TABLE [dbo].[TransactionBase_Prescription]
ADD CONSTRAINT [FK_Prescription_inherits_TransactionBase]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionBase]
        ([TransactionId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Persons_Patient'
ALTER TABLE [dbo].[Persons_Patient]
ADD CONSTRAINT [FK_Patient_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TransactionEntryId] in table 'TransactionEntryBase_PrescriptionEntry'
ALTER TABLE [dbo].[TransactionEntryBase_PrescriptionEntry]
ADD CONSTRAINT [FK_PrescriptionEntry_inherits_TransactionEntryBase]
    FOREIGN KEY ([TransactionEntryId])
    REFERENCES [dbo].[TransactionEntryBase]
        ([TransactionEntryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemId] in table 'Item_Medicine'
ALTER TABLE [dbo].[Item_Medicine]
ADD CONSTRAINT [FK_Medicine_inherits_Item]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Item]
        ([ItemId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemId] in table 'Item_StockItem'
ALTER TABLE [dbo].[Item_StockItem]
ADD CONSTRAINT [FK_StockItem_inherits_Item]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Item]
        ([ItemId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TransactionId] in table 'TransactionBase_QuickPrescription'
ALTER TABLE [dbo].[TransactionBase_QuickPrescription]
ADD CONSTRAINT [FK_QuickPrescription_inherits_TransactionBase]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionBase]
        ([TransactionId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------