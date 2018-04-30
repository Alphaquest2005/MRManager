
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2012 11:52:49
-- Generated from EDMX file: D:\Prism Projects\PrismApplication1\RMSDataAccessLayer\RxCare.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RxCare];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DoctorPhone_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoctorPhones] DROP CONSTRAINT [FK_DoctorPhone_Doctor];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryQuantities_InventoryItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryQuantities] DROP CONSTRAINT [FK_InventoryQuantities_InventoryItems];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineContents_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineContents] DROP CONSTRAINT [FK_MedicineContents_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineInventory_InventoryItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineInventories] DROP CONSTRAINT [FK_MedicineInventory_InventoryItems];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineInventory_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineInventories] DROP CONSTRAINT [FK_MedicineInventory_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineNegativeInteractions_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineNegativeInteractions] DROP CONSTRAINT [FK_MedicineNegativeInteractions_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineSideEffects_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineSideEffects] DROP CONSTRAINT [FK_MedicineSideEffects_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicineUnitPrice_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicineUnitPrices] DROP CONSTRAINT [FK_MedicineUnitPrice_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientAddress_Patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientAddresses] DROP CONSTRAINT [FK_PatientAddress_Patient];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientConditions_Conditions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientConditions] DROP CONSTRAINT [FK_PatientConditions_Conditions];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientConditions_Patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientConditions] DROP CONSTRAINT [FK_PatientConditions_Patient];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientPhone_Patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientPhones] DROP CONSTRAINT [FK_PatientPhone_Patient];
GO
IF OBJECT_ID(N'[dbo].[FK_PresciptionDetails_Medicine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresciptionDetails] DROP CONSTRAINT [FK_PresciptionDetails_Medicine];
GO
IF OBJECT_ID(N'[dbo].[FK_PresciptionDetails_MedicineUnitPrice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresciptionDetails] DROP CONSTRAINT [FK_PresciptionDetails_MedicineUnitPrice];
GO
IF OBJECT_ID(N'[dbo].[FK_PresciptionDetails_Prescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresciptionDetails] DROP CONSTRAINT [FK_PresciptionDetails_Prescription];
GO
IF OBJECT_ID(N'[dbo].[FK_Prescription_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prescriptions] DROP CONSTRAINT [FK_Prescription_Doctor];
GO
IF OBJECT_ID(N'[dbo].[FK_Prescription_Patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prescriptions] DROP CONSTRAINT [FK_Prescription_Patient];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Conditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conditions];
GO
IF OBJECT_ID(N'[dbo].[DoctorPhones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DoctorPhones];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[InventoryItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryItems];
GO
IF OBJECT_ID(N'[dbo].[InventoryQuantities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryQuantities];
GO
IF OBJECT_ID(N'[dbo].[MedicineContents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineContents];
GO
IF OBJECT_ID(N'[dbo].[MedicineInventories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineInventories];
GO
IF OBJECT_ID(N'[dbo].[MedicineNegativeInteractions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineNegativeInteractions];
GO
IF OBJECT_ID(N'[dbo].[Medicines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medicines];
GO
IF OBJECT_ID(N'[dbo].[MedicineSideEffects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineSideEffects];
GO
IF OBJECT_ID(N'[dbo].[MedicineUnitPrices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicineUnitPrices];
GO
IF OBJECT_ID(N'[dbo].[PatientAddresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientAddresses];
GO
IF OBJECT_ID(N'[dbo].[PatientConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientConditions];
GO
IF OBJECT_ID(N'[dbo].[PatientPhones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientPhones];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[PresciptionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PresciptionDetails];
GO
IF OBJECT_ID(N'[dbo].[Prescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prescriptions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Conditions'
CREATE TABLE [dbo].[Conditions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ConditionName] nvarchar(50)  NOT NULL,
    [Cause] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Office_Address] nvarchar(50)  NULL
);
GO

-- Creating table 'DoctorPhones'
CREATE TABLE [dbo].[DoctorPhones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(50)  NOT NULL,
    [Type] nvarchar(50)  NULL,
    [DoctorId] int  NOT NULL
);
GO

-- Creating table 'InventoryItems'
CREATE TABLE [dbo].[InventoryItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemName] nvarchar(50)  NULL,
    [SupplierName] nvarchar(50)  NULL,
    [Supplier] nvarchar(50)  NULL,
    [ReorderLevel] decimal(18,0)  NULL
);
GO

-- Creating table 'InventoryQuantities'
CREATE TABLE [dbo].[InventoryQuantities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(50)  NOT NULL,
    [Quantity] decimal(18,2)  NOT NULL,
    [DateEntered] datetime  NOT NULL,
    [Unit] nvarchar(50)  NOT NULL,
    [Cost] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [InventoryItemId] int  NOT NULL
);
GO

-- Creating table 'Medicines'
CREATE TABLE [dbo].[Medicines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Description] nvarchar(50)  NULL,
    [Brand] nvarchar(50)  NULL,
    [Generic] nvarchar(50)  NULL
);
GO

-- Creating table 'MedicineContents'
CREATE TABLE [dbo].[MedicineContents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(50)  NOT NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [Unit] nvarchar(50)  NULL,
    [Percentage] nvarchar(50)  NULL,
    [MedicineId] int  NOT NULL
);
GO

-- Creating table 'MedicineInventory'
CREATE TABLE [dbo].[MedicineInventory] (
    [MedicineId] int  NOT NULL,
    [InventoryItemId] int  NOT NULL
);
GO

-- Creating table 'MedicineNegativeInteractions'
CREATE TABLE [dbo].[MedicineNegativeInteractions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MedicineId2] int  NOT NULL,
    [MedicineId] int  NOT NULL
);
GO

-- Creating table 'MedicineSideEffects'
CREATE TABLE [dbo].[MedicineSideEffects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SideEffect] nvarchar(50)  NULL,
    [Action] nvarchar(50)  NULL,
    [MedicineId] int  NOT NULL
);
GO

-- Creating table 'MedicineUnitPrices'
CREATE TABLE [dbo].[MedicineUnitPrices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Unit] nvarchar(50)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [MedicineID] int  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [DateofBirth] datetime  NULL,
    [BloodType] nvarchar(50)  NULL,
    [Sex] nvarchar(50)  NULL,
    [Property1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PatientAddresses'
CREATE TABLE [dbo].[PatientAddresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Line1] nvarchar(50)  NULL,
    [Parish] nvarchar(50)  NULL,
    [City] nvarchar(50)  NULL,
    [PatientId] int  NOT NULL
);
GO

-- Creating table 'PatientConditions'
CREATE TABLE [dbo].[PatientConditions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateEntered] datetime  NOT NULL,
    [Notes] nvarchar(max)  NULL,
    [ConditionsId] int  NOT NULL,
    [PatientId] int  NOT NULL
);
GO

-- Creating table 'PatientPhones'
CREATE TABLE [dbo].[PatientPhones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(50)  NOT NULL,
    [Type] nvarchar(50)  NULL,
    [PatientId] int  NOT NULL
);
GO

-- Creating table 'PresciptionDetails'
CREATE TABLE [dbo].[PresciptionDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Days] int  NULL,
    [Times] int  NULL,
    [Dosage] decimal(18,2)  NULL,
    [Comments] nvarchar(max)  NULL,
    [Price] decimal(19,4)  NULL,
    [MedicineId] int  NOT NULL,
    [Unit] int  NOT NULL,
    [PrescriptionId] int  NOT NULL
);
GO

-- Creating table 'Prescriptions'
CREATE TABLE [dbo].[Prescriptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrescriptionNumber] nvarchar(50)  NOT NULL,
    [Instructions] nvarchar(max)  NULL,
    [DateEntered] datetime  NULL,
    [DoctorId] int  NOT NULL,
    [PatientId] int  NOT NULL
);
GO

-- Creating table 'MedicineInventories1'
CREATE TABLE [dbo].[MedicineInventories1] (
    [InventoryItems_Id] int  NOT NULL,
    [Medicines_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Conditions'
ALTER TABLE [dbo].[Conditions]
ADD CONSTRAINT [PK_Conditions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DoctorPhones'
ALTER TABLE [dbo].[DoctorPhones]
ADD CONSTRAINT [PK_DoctorPhones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryItems'
ALTER TABLE [dbo].[InventoryItems]
ADD CONSTRAINT [PK_InventoryItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryQuantities'
ALTER TABLE [dbo].[InventoryQuantities]
ADD CONSTRAINT [PK_InventoryQuantities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Medicines'
ALTER TABLE [dbo].[Medicines]
ADD CONSTRAINT [PK_Medicines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicineContents'
ALTER TABLE [dbo].[MedicineContents]
ADD CONSTRAINT [PK_MedicineContents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MedicineId], [InventoryItemId] in table 'MedicineInventory'
ALTER TABLE [dbo].[MedicineInventory]
ADD CONSTRAINT [PK_MedicineInventory]
    PRIMARY KEY NONCLUSTERED ([MedicineId], [InventoryItemId] ASC);
GO

-- Creating primary key on [Id] in table 'MedicineNegativeInteractions'
ALTER TABLE [dbo].[MedicineNegativeInteractions]
ADD CONSTRAINT [PK_MedicineNegativeInteractions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicineSideEffects'
ALTER TABLE [dbo].[MedicineSideEffects]
ADD CONSTRAINT [PK_MedicineSideEffects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicineUnitPrices'
ALTER TABLE [dbo].[MedicineUnitPrices]
ADD CONSTRAINT [PK_MedicineUnitPrices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientAddresses'
ALTER TABLE [dbo].[PatientAddresses]
ADD CONSTRAINT [PK_PatientAddresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientConditions'
ALTER TABLE [dbo].[PatientConditions]
ADD CONSTRAINT [PK_PatientConditions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientPhones'
ALTER TABLE [dbo].[PatientPhones]
ADD CONSTRAINT [PK_PatientPhones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PresciptionDetails'
ALTER TABLE [dbo].[PresciptionDetails]
ADD CONSTRAINT [PK_PresciptionDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [PK_Prescriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [InventoryItems_Id], [Medicines_Id] in table 'MedicineInventories1'
ALTER TABLE [dbo].[MedicineInventories1]
ADD CONSTRAINT [PK_MedicineInventories1]
    PRIMARY KEY NONCLUSTERED ([InventoryItems_Id], [Medicines_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DoctorId] in table 'DoctorPhones'
ALTER TABLE [dbo].[DoctorPhones]
ADD CONSTRAINT [FK_DoctorPhone_Doctor]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorPhone_Doctor'
CREATE INDEX [IX_FK_DoctorPhone_Doctor]
ON [dbo].[DoctorPhones]
    ([DoctorId]);
GO

-- Creating foreign key on [InventoryItemId] in table 'InventoryQuantities'
ALTER TABLE [dbo].[InventoryQuantities]
ADD CONSTRAINT [FK_InventoryQuantities_InventoryItems]
    FOREIGN KEY ([InventoryItemId])
    REFERENCES [dbo].[InventoryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryQuantities_InventoryItems'
CREATE INDEX [IX_FK_InventoryQuantities_InventoryItems]
ON [dbo].[InventoryQuantities]
    ([InventoryItemId]);
GO

-- Creating foreign key on [MedicineId] in table 'MedicineContents'
ALTER TABLE [dbo].[MedicineContents]
ADD CONSTRAINT [FK_MedicineContents_Medicine]
    FOREIGN KEY ([MedicineId])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineContents_Medicine'
CREATE INDEX [IX_FK_MedicineContents_Medicine]
ON [dbo].[MedicineContents]
    ([MedicineId]);
GO

-- Creating foreign key on [InventoryItemId] in table 'MedicineInventory'
ALTER TABLE [dbo].[MedicineInventory]
ADD CONSTRAINT [FK_MedicineInventory_InventoryItems]
    FOREIGN KEY ([InventoryItemId])
    REFERENCES [dbo].[InventoryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineInventory_InventoryItems'
CREATE INDEX [IX_FK_MedicineInventory_InventoryItems]
ON [dbo].[MedicineInventory]
    ([InventoryItemId]);
GO

-- Creating foreign key on [MedicineId] in table 'MedicineInventory'
ALTER TABLE [dbo].[MedicineInventory]
ADD CONSTRAINT [FK_MedicineInventory_Medicine]
    FOREIGN KEY ([MedicineId])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MedicineId] in table 'MedicineNegativeInteractions'
ALTER TABLE [dbo].[MedicineNegativeInteractions]
ADD CONSTRAINT [FK_MedicineNegativeInteractions_Medicine]
    FOREIGN KEY ([MedicineId])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineNegativeInteractions_Medicine'
CREATE INDEX [IX_FK_MedicineNegativeInteractions_Medicine]
ON [dbo].[MedicineNegativeInteractions]
    ([MedicineId]);
GO

-- Creating foreign key on [MedicineId] in table 'MedicineSideEffects'
ALTER TABLE [dbo].[MedicineSideEffects]
ADD CONSTRAINT [FK_MedicineSideEffects_Medicine]
    FOREIGN KEY ([MedicineId])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineSideEffects_Medicine'
CREATE INDEX [IX_FK_MedicineSideEffects_Medicine]
ON [dbo].[MedicineSideEffects]
    ([MedicineId]);
GO

-- Creating foreign key on [MedicineID] in table 'MedicineUnitPrices'
ALTER TABLE [dbo].[MedicineUnitPrices]
ADD CONSTRAINT [FK_MedicineUnitPrice_Medicine]
    FOREIGN KEY ([MedicineID])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineUnitPrice_Medicine'
CREATE INDEX [IX_FK_MedicineUnitPrice_Medicine]
ON [dbo].[MedicineUnitPrices]
    ([MedicineID]);
GO

-- Creating foreign key on [PatientId] in table 'PatientAddresses'
ALTER TABLE [dbo].[PatientAddresses]
ADD CONSTRAINT [FK_PatientAddress_Patient]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientAddress_Patient'
CREATE INDEX [IX_FK_PatientAddress_Patient]
ON [dbo].[PatientAddresses]
    ([PatientId]);
GO

-- Creating foreign key on [ConditionsId] in table 'PatientConditions'
ALTER TABLE [dbo].[PatientConditions]
ADD CONSTRAINT [FK_PatientConditions_Conditions]
    FOREIGN KEY ([ConditionsId])
    REFERENCES [dbo].[Conditions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientConditions_Conditions'
CREATE INDEX [IX_FK_PatientConditions_Conditions]
ON [dbo].[PatientConditions]
    ([ConditionsId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientConditions'
ALTER TABLE [dbo].[PatientConditions]
ADD CONSTRAINT [FK_PatientConditions_Patient]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientConditions_Patient'
CREATE INDEX [IX_FK_PatientConditions_Patient]
ON [dbo].[PatientConditions]
    ([PatientId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientPhones'
ALTER TABLE [dbo].[PatientPhones]
ADD CONSTRAINT [FK_PatientPhone_Patient]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientPhone_Patient'
CREATE INDEX [IX_FK_PatientPhone_Patient]
ON [dbo].[PatientPhones]
    ([PatientId]);
GO

-- Creating foreign key on [MedicineId] in table 'PresciptionDetails'
ALTER TABLE [dbo].[PresciptionDetails]
ADD CONSTRAINT [FK_PresciptionDetails_Medicine]
    FOREIGN KEY ([MedicineId])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresciptionDetails_Medicine'
CREATE INDEX [IX_FK_PresciptionDetails_Medicine]
ON [dbo].[PresciptionDetails]
    ([MedicineId]);
GO

-- Creating foreign key on [Unit] in table 'PresciptionDetails'
ALTER TABLE [dbo].[PresciptionDetails]
ADD CONSTRAINT [FK_PresciptionDetails_MedicineUnitPrice]
    FOREIGN KEY ([Unit])
    REFERENCES [dbo].[MedicineUnitPrices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresciptionDetails_MedicineUnitPrice'
CREATE INDEX [IX_FK_PresciptionDetails_MedicineUnitPrice]
ON [dbo].[PresciptionDetails]
    ([Unit]);
GO

-- Creating foreign key on [PrescriptionId] in table 'PresciptionDetails'
ALTER TABLE [dbo].[PresciptionDetails]
ADD CONSTRAINT [FK_PresciptionDetails_Prescription]
    FOREIGN KEY ([PrescriptionId])
    REFERENCES [dbo].[Prescriptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresciptionDetails_Prescription'
CREATE INDEX [IX_FK_PresciptionDetails_Prescription]
ON [dbo].[PresciptionDetails]
    ([PrescriptionId]);
GO

-- Creating foreign key on [DoctorId] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [FK_Prescription_Doctor1]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Prescription_Doctor1'
CREATE INDEX [IX_FK_Prescription_Doctor1]
ON [dbo].[Prescriptions]
    ([DoctorId]);
GO

-- Creating foreign key on [InventoryItemId] in table 'InventoryQuantities'
ALTER TABLE [dbo].[InventoryQuantities]
ADD CONSTRAINT [FK_InventoryQuantities_InventoryItems1]
    FOREIGN KEY ([InventoryItemId])
    REFERENCES [dbo].[InventoryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryQuantities_InventoryItems1'
CREATE INDEX [IX_FK_InventoryQuantities_InventoryItems1]
ON [dbo].[InventoryQuantities]
    ([InventoryItemId]);
GO

-- Creating foreign key on [PatientId] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [FK_Prescription_Patient1]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Prescription_Patient1'
CREATE INDEX [IX_FK_Prescription_Patient1]
ON [dbo].[Prescriptions]
    ([PatientId]);
GO

-- Creating foreign key on [InventoryItems_Id] in table 'MedicineInventories1'
ALTER TABLE [dbo].[MedicineInventories1]
ADD CONSTRAINT [FK_MedicineInventories_InventoryItem]
    FOREIGN KEY ([InventoryItems_Id])
    REFERENCES [dbo].[InventoryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Medicines_Id] in table 'MedicineInventories1'
ALTER TABLE [dbo].[MedicineInventories1]
ADD CONSTRAINT [FK_MedicineInventories_Medicine]
    FOREIGN KEY ([Medicines_Id])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicineInventories_Medicine'
CREATE INDEX [IX_FK_MedicineInventories_Medicine]
ON [dbo].[MedicineInventories1]
    ([Medicines_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------