
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/02/2013 15:04:55
-- Generated from EDMX file: C:\Prism Projects\live\PrismApplication1\QuickBooks\QuickBooks\QBPos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QBPOS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_InventoryAdjustmentInventoryAdjustmentItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryAdjustmentItems] DROP CONSTRAINT [FK_InventoryAdjustmentInventoryAdjustmentItem];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesReceiptSalesReceiptDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReceiptDetails] DROP CONSTRAINT [FK_SalesReceiptSalesReceiptDetails];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[InventoryAdjustmentItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryAdjustmentItems];
GO
IF OBJECT_ID(N'[dbo].[InventoryAdjustments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryAdjustments];
GO
IF OBJECT_ID(N'[dbo].[InventoryItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryItems];
GO
IF OBJECT_ID(N'[dbo].[SalesReceiptDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesReceiptDetails];
GO
IF OBJECT_ID(N'[dbo].[SalesReceipts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesReceipts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'InventoryAdjustmentItems'
CREATE TABLE [dbo].[InventoryAdjustmentItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemListID] nvarchar(max)  NOT NULL,
    [NewQuantity] decimal(18,0)  NOT NULL,
    [OldQuantity] decimal(18,0)  NOT NULL,
    [QuantityDifference] decimal(18,0)  NOT NULL,
    [InventoryAdjustmentId] int  NOT NULL
);
GO

-- Creating table 'InventoryAdjustments'
CREATE TABLE [dbo].[InventoryAdjustments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TxnDate] datetime  NOT NULL,
    [AdjustmentNumber] nvarchar(max)  NOT NULL,
    [AdjustmentSource] nvarchar(max)  NOT NULL,
    [TxnID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'InventoryItems'
CREATE TABLE [dbo].[InventoryItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ListID] nvarchar(max)  NOT NULL,
    [Attribute] nvarchar(max)  NULL,
    [ItemDesc1] nvarchar(max)  NULL,
    [ItemDesc2] nvarchar(max)  NULL,
    [ALU] nvarchar(max)  NULL,
    [ItemName] nvarchar(max)  NULL,
    [Size] nvarchar(max)  NULL,
    [DepartmentCode] nvarchar(max)  NULL,
    [ItemNumber] int  NOT NULL,
    [ItemType] nvarchar(max)  NULL,
    [TaxCode] nvarchar(max)  NULL,
    [Price] float  NULL
);
GO

-- Creating table 'SalesReceiptDetails'
CREATE TABLE [dbo].[SalesReceiptDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SalesReceiptId] int  NOT NULL,
    [ItemListID] nvarchar(max)  NOT NULL,
    [QtySold] decimal(15,8)  NOT NULL,
    [ItemKey] nvarchar(max)  NOT NULL,
    [Tax] decimal(15,8)  NOT NULL,
    [QtyAllocated] decimal(18,0)  NULL,
    [ItemDesc1] nvarchar(max)  NULL,
    [ItemDesc2] nvarchar(max)  NULL,
    [ItemALU] nvarchar(max)  NULL,
    [Price] float  NULL,
    [TaxCode] nvarchar(max)  NULL
);
GO

-- Creating table 'SalesReceipts'
CREATE TABLE [dbo].[SalesReceipts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SalesReceiptNumber] nvarchar(max)  NOT NULL,
    [TxnDate] datetime  NOT NULL,
    [Associate] nvarchar(max)  NOT NULL,
    [Cashier] nvarchar(max)  NOT NULL,
    [Comments] nvarchar(max)  NOT NULL,
    [StoreNumber] nvarchar(max)  NOT NULL,
    [Workstation] nvarchar(max)  NOT NULL,
    [TxnState] nvarchar(max)  NOT NULL,
    [TxnID] nvarchar(max)  NULL,
    [SalesReceiptType] nvarchar(max)  NOT NULL,
    [TrackingNumber] nvarchar(max)  NULL,
    [Discount] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'InventoryAdjustmentItems'
ALTER TABLE [dbo].[InventoryAdjustmentItems]
ADD CONSTRAINT [PK_InventoryAdjustmentItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryAdjustments'
ALTER TABLE [dbo].[InventoryAdjustments]
ADD CONSTRAINT [PK_InventoryAdjustments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryItems'
ALTER TABLE [dbo].[InventoryItems]
ADD CONSTRAINT [PK_InventoryItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SalesReceiptDetails'
ALTER TABLE [dbo].[SalesReceiptDetails]
ADD CONSTRAINT [PK_SalesReceiptDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SalesReceipts'
ALTER TABLE [dbo].[SalesReceipts]
ADD CONSTRAINT [PK_SalesReceipts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InventoryAdjustmentId] in table 'InventoryAdjustmentItems'
ALTER TABLE [dbo].[InventoryAdjustmentItems]
ADD CONSTRAINT [FK_InventoryAdjustmentInventoryAdjustmentItem]
    FOREIGN KEY ([InventoryAdjustmentId])
    REFERENCES [dbo].[InventoryAdjustments]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryAdjustmentInventoryAdjustmentItem'
CREATE INDEX [IX_FK_InventoryAdjustmentInventoryAdjustmentItem]
ON [dbo].[InventoryAdjustmentItems]
    ([InventoryAdjustmentId]);
GO

-- Creating foreign key on [SalesReceiptId] in table 'SalesReceiptDetails'
ALTER TABLE [dbo].[SalesReceiptDetails]
ADD CONSTRAINT [FK_SalesReceiptSalesReceiptDetails]
    FOREIGN KEY ([SalesReceiptId])
    REFERENCES [dbo].[SalesReceipts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesReceiptSalesReceiptDetails'
CREATE INDEX [IX_FK_SalesReceiptSalesReceiptDetails]
ON [dbo].[SalesReceiptDetails]
    ([SalesReceiptId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------