
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/18/2013 01:24:23
-- Generated from EDMX file: C:\Prism Projects\Payroll\PayRoll\PayrollManager\DataLayer\PayrollData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PayrollDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PayrollJobUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollJobs] DROP CONSTRAINT [FK_PayrollJobUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPayrollJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollJobs] DROP CONSTRAINT [FK_UserPayrollJob];
GO
IF OBJECT_ID(N'[dbo].[FK_BranchEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_BranchEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionAccountASN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_InstitutionAccountASN];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeEmployeeAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_EmployeeAccount] DROP CONSTRAINT [FK_EmployeeEmployeeAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_EmployeeEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePayrollItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_EmployeePayrollItem];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollJobPayrollItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollJobPayrollItem];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountPayrollItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_AccountPayrollItem];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountPayrollItem1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_AccountPayrollItem1];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollSetupItemPayrollEmployeeSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollEmployeeSetup] DROP CONSTRAINT [FK_PayrollSetupItemPayrollEmployeeSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePayrollEmployeeSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollEmployeeSetup] DROP CONSTRAINT [FK_EmployeePayrollEmployeeSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollJobTypePayrollJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollJobs] DROP CONSTRAINT [FK_PayrollJobTypePayrollJob];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountAccountEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountEntries] DROP CONSTRAINT [FK_AccountAccountEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollItemAccountEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountEntries] DROP CONSTRAINT [FK_PayrollItemAccountEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAccountPayrollEmployeeSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollEmployeeSetup] DROP CONSTRAINT [FK_EmployeeAccountPayrollEmployeeSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountPayrollSetupItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollSetupItems] DROP CONSTRAINT [FK_AccountPayrollSetupItem];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionInstitutionAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_InstitutionAccount] DROP CONSTRAINT [FK_InstitutionInstitutionAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollSetupItemPayrollItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollSetupItemPayrollItem];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollItemPayrollItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollItemPayrollItem];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollJobTypePayrollEmployeeSetup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollEmployeeSetup] DROP CONSTRAINT [FK_PayrollJobTypePayrollEmployeeSetup];
GO
IF OBJECT_ID(N'[dbo].[FK_BranchPayrollJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollJobs] DROP CONSTRAINT [FK_BranchPayrollJob];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollJobBranch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Branches] DROP CONSTRAINT [FK_PayrollJobBranch];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees_User] DROP CONSTRAINT [FK_User_inherits_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAccount_inherits_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_EmployeeAccount] DROP CONSTRAINT [FK_EmployeeAccount_inherits_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionAccount_inherits_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_InstitutionAccount] DROP CONSTRAINT [FK_InstitutionAccount_inherits_Account];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PayrollJobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollJobs];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Branches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branches];
GO
IF OBJECT_ID(N'[dbo].[Institutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Institutions];
GO
IF OBJECT_ID(N'[dbo].[PayrollItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollItems];
GO
IF OBJECT_ID(N'[dbo].[PayrollSetupItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollSetupItems];
GO
IF OBJECT_ID(N'[dbo].[PayrollEmployeeSetup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollEmployeeSetup];
GO
IF OBJECT_ID(N'[dbo].[PayrollJobTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollJobTypes];
GO
IF OBJECT_ID(N'[dbo].[AccountEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountEntries];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[AccountTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountTypes];
GO
IF OBJECT_ID(N'[dbo].[ChargeTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChargeTypes];
GO
IF OBJECT_ID(N'[dbo].[Employees_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees_User];
GO
IF OBJECT_ID(N'[dbo].[Accounts_EmployeeAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts_EmployeeAccount];
GO
IF OBJECT_ID(N'[dbo].[Accounts_InstitutionAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts_InstitutionAccount];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PayrollJobs'
CREATE TABLE [dbo].[PayrollJobs] (
    [PayrollJobId] int IDENTITY(1,1) NOT NULL,
    [CheckedBy] int  NULL,
    [PreparedBy] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [PayrollJobTypeId] int  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [Title] nvarchar(max)  NULL,
    [CompanyId] int  NOT NULL,
    [SupervisorId] int  NULL,
    [EmploymentStartDate] datetime  NOT NULL,
    [UnionMember] bit  NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [CompanyId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [CurrentPayrollJobId] int  NULL
);
GO

-- Creating table 'Institutions'
CREATE TABLE [dbo].[Institutions] (
    [InstitutionId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PayrollItems'
CREATE TABLE [dbo].[PayrollItems] (
    [PayrollItemId] int IDENTITY(1,1) NOT NULL,
    [EmployeeId] int  NOT NULL,
    [CreditAccountId] int  NOT NULL,
    [DebitAccountId] int  NOT NULL,
    [Priority] int  NOT NULL,
    [PayrollJobId] int  NOT NULL,
    [Amount] float  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Rate] real  NOT NULL,
    [BaseAmount] float  NOT NULL,
    [IncomeDeduction] bit  NOT NULL,
    [Status] nvarchar(max)  NULL,
    [PayrollSetupItemId] int  NULL,
    [ParentPayrollItemId] int  NULL,
    [RateRounding] nvarchar(max)  NULL
);
GO

-- Creating table 'PayrollSetupItems'
CREATE TABLE [dbo].[PayrollSetupItems] (
    [PayrollSetupItemId] int IDENTITY(1,1) NOT NULL,
    [Priority] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CompanyLineItemDescription] nvarchar(max)  NULL,
    [DebitCredit] bit  NOT NULL,
    [RecuranceDays] nvarchar(max)  NULL,
    [EmployeeAccountType] nvarchar(max)  NOT NULL,
    [IncomeDeduction] bit  NOT NULL,
    [PayrollItemAccountId] int  NOT NULL,
    [CompanyAccountId] int  NULL,
    [RateRounding] nvarchar(max)  NULL
);
GO

-- Creating table 'PayrollEmployeeSetup'
CREATE TABLE [dbo].[PayrollEmployeeSetup] (
    [PayrollEmployeeSetupId] int IDENTITY(1,1) NOT NULL,
    [PayrollSetupItemId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EndDate] datetime  NULL,
    [StartDate] datetime  NULL,
    [EmployeeAccountId] int  NOT NULL,
    [Amount] float  NULL,
    [CompanyAmount] float  NULL,
    [Rate] real  NULL,
    [CompanyRate] real  NULL,
    [ChargeType] nvarchar(max)  NOT NULL,
    [PayrollJobTypeId] int  NOT NULL,
    [RateRounding] nvarchar(max)  NULL
);
GO

-- Creating table 'PayrollJobTypes'
CREATE TABLE [dbo].[PayrollJobTypes] (
    [PayrollJobTypeId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AccountEntries'
CREATE TABLE [dbo].[AccountEntries] (
    [AccountEntryId] int IDENTITY(1,1) NOT NULL,
    [EntryTime] datetime  NOT NULL,
    [TradeDate] datetime  NOT NULL,
    [AccountId] int  NOT NULL,
    [DebitAmount] float  NOT NULL,
    [CreditAmount] float  NOT NULL,
    [Memo] nvarchar(max)  NULL,
    [PayrollItemId] int  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccountId] int IDENTITY(1,1) NOT NULL,
    [AccountNumber] nvarchar(max)  NOT NULL,
    [InstitutionId] int  NOT NULL,
    [AccountName] nvarchar(max)  NOT NULL,
    [AccountType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AccountTypes'
CREATE TABLE [dbo].[AccountTypes] (
    [AccountTypeId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ChargeTypes'
CREATE TABLE [dbo].[ChargeTypes] (
    [ChargeTypeId] int IDENTITY(1,1) NOT NULL,
    [ChargeTypeName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees_User'
CREATE TABLE [dbo].[Employees_User] (
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'Accounts_EmployeeAccount'
CREATE TABLE [dbo].[Accounts_EmployeeAccount] (
    [EmployeeId] int  NOT NULL,
    [SalaryAssignment] float  NULL,
    [AccountId] int  NOT NULL
);
GO

-- Creating table 'Accounts_InstitutionAccount'
CREATE TABLE [dbo].[Accounts_InstitutionAccount] (
    [PayeeInstitutionId] int  NOT NULL,
    [AccountId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PayrollJobId] in table 'PayrollJobs'
ALTER TABLE [dbo].[PayrollJobs]
ADD CONSTRAINT [PK_PayrollJobs]
    PRIMARY KEY CLUSTERED ([PayrollJobId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [CompanyId] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([CompanyId] ASC);
GO

-- Creating primary key on [InstitutionId] in table 'Institutions'
ALTER TABLE [dbo].[Institutions]
ADD CONSTRAINT [PK_Institutions]
    PRIMARY KEY CLUSTERED ([InstitutionId] ASC);
GO

-- Creating primary key on [PayrollItemId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [PK_PayrollItems]
    PRIMARY KEY CLUSTERED ([PayrollItemId] ASC);
GO

-- Creating primary key on [PayrollSetupItemId] in table 'PayrollSetupItems'
ALTER TABLE [dbo].[PayrollSetupItems]
ADD CONSTRAINT [PK_PayrollSetupItems]
    PRIMARY KEY CLUSTERED ([PayrollSetupItemId] ASC);
GO

-- Creating primary key on [PayrollEmployeeSetupId] in table 'PayrollEmployeeSetup'
ALTER TABLE [dbo].[PayrollEmployeeSetup]
ADD CONSTRAINT [PK_PayrollEmployeeSetup]
    PRIMARY KEY CLUSTERED ([PayrollEmployeeSetupId] ASC);
GO

-- Creating primary key on [PayrollJobTypeId] in table 'PayrollJobTypes'
ALTER TABLE [dbo].[PayrollJobTypes]
ADD CONSTRAINT [PK_PayrollJobTypes]
    PRIMARY KEY CLUSTERED ([PayrollJobTypeId] ASC);
GO

-- Creating primary key on [AccountEntryId] in table 'AccountEntries'
ALTER TABLE [dbo].[AccountEntries]
ADD CONSTRAINT [PK_AccountEntries]
    PRIMARY KEY CLUSTERED ([AccountEntryId] ASC);
GO

-- Creating primary key on [AccountId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [AccountTypeId] in table 'AccountTypes'
ALTER TABLE [dbo].[AccountTypes]
ADD CONSTRAINT [PK_AccountTypes]
    PRIMARY KEY CLUSTERED ([AccountTypeId] ASC);
GO

-- Creating primary key on [ChargeTypeId] in table 'ChargeTypes'
ALTER TABLE [dbo].[ChargeTypes]
ADD CONSTRAINT [PK_ChargeTypes]
    PRIMARY KEY CLUSTERED ([ChargeTypeId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees_User'
ALTER TABLE [dbo].[Employees_User]
ADD CONSTRAINT [PK_Employees_User]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [AccountId] in table 'Accounts_EmployeeAccount'
ALTER TABLE [dbo].[Accounts_EmployeeAccount]
ADD CONSTRAINT [PK_Accounts_EmployeeAccount]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [AccountId] in table 'Accounts_InstitutionAccount'
ALTER TABLE [dbo].[Accounts_InstitutionAccount]
ADD CONSTRAINT [PK_Accounts_InstitutionAccount]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CheckedBy] in table 'PayrollJobs'
ALTER TABLE [dbo].[PayrollJobs]
ADD CONSTRAINT [FK_PayrollJobUser]
    FOREIGN KEY ([CheckedBy])
    REFERENCES [dbo].[Employees_User]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollJobUser'
CREATE INDEX [IX_FK_PayrollJobUser]
ON [dbo].[PayrollJobs]
    ([CheckedBy]);
GO

-- Creating foreign key on [PreparedBy] in table 'PayrollJobs'
ALTER TABLE [dbo].[PayrollJobs]
ADD CONSTRAINT [FK_UserPayrollJob]
    FOREIGN KEY ([PreparedBy])
    REFERENCES [dbo].[Employees_User]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPayrollJob'
CREATE INDEX [IX_FK_UserPayrollJob]
ON [dbo].[PayrollJobs]
    ([PreparedBy]);
GO

-- Creating foreign key on [CompanyId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_BranchEmployee]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Branches]
        ([CompanyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BranchEmployee'
CREATE INDEX [IX_FK_BranchEmployee]
ON [dbo].[Employees]
    ([CompanyId]);
GO

-- Creating foreign key on [InstitutionId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_InstitutionAccountASN]
    FOREIGN KEY ([InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionAccountASN'
CREATE INDEX [IX_FK_InstitutionAccountASN]
ON [dbo].[Accounts]
    ([InstitutionId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Accounts_EmployeeAccount'
ALTER TABLE [dbo].[Accounts_EmployeeAccount]
ADD CONSTRAINT [FK_EmployeeEmployeeAccount]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeEmployeeAccount'
CREATE INDEX [IX_FK_EmployeeEmployeeAccount]
ON [dbo].[Accounts_EmployeeAccount]
    ([EmployeeId]);
GO

-- Creating foreign key on [SupervisorId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeeEmployee]
    FOREIGN KEY ([SupervisorId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeEmployee'
CREATE INDEX [IX_FK_EmployeeEmployee]
ON [dbo].[Employees]
    ([SupervisorId]);
GO

-- Creating foreign key on [EmployeeId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_EmployeePayrollItem]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePayrollItem'
CREATE INDEX [IX_FK_EmployeePayrollItem]
ON [dbo].[PayrollItems]
    ([EmployeeId]);
GO

-- Creating foreign key on [PayrollJobId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_PayrollJobPayrollItem]
    FOREIGN KEY ([PayrollJobId])
    REFERENCES [dbo].[PayrollJobs]
        ([PayrollJobId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollJobPayrollItem'
CREATE INDEX [IX_FK_PayrollJobPayrollItem]
ON [dbo].[PayrollItems]
    ([PayrollJobId]);
GO

-- Creating foreign key on [CreditAccountId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_AccountPayrollItem]
    FOREIGN KEY ([CreditAccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountPayrollItem'
CREATE INDEX [IX_FK_AccountPayrollItem]
ON [dbo].[PayrollItems]
    ([CreditAccountId]);
GO

-- Creating foreign key on [DebitAccountId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_AccountPayrollItem1]
    FOREIGN KEY ([DebitAccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountPayrollItem1'
CREATE INDEX [IX_FK_AccountPayrollItem1]
ON [dbo].[PayrollItems]
    ([DebitAccountId]);
GO

-- Creating foreign key on [PayrollSetupItemId] in table 'PayrollEmployeeSetup'
ALTER TABLE [dbo].[PayrollEmployeeSetup]
ADD CONSTRAINT [FK_PayrollSetupItemPayrollEmployeeSetup]
    FOREIGN KEY ([PayrollSetupItemId])
    REFERENCES [dbo].[PayrollSetupItems]
        ([PayrollSetupItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollSetupItemPayrollEmployeeSetup'
CREATE INDEX [IX_FK_PayrollSetupItemPayrollEmployeeSetup]
ON [dbo].[PayrollEmployeeSetup]
    ([PayrollSetupItemId]);
GO

-- Creating foreign key on [EmployeeId] in table 'PayrollEmployeeSetup'
ALTER TABLE [dbo].[PayrollEmployeeSetup]
ADD CONSTRAINT [FK_EmployeePayrollEmployeeSetup]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePayrollEmployeeSetup'
CREATE INDEX [IX_FK_EmployeePayrollEmployeeSetup]
ON [dbo].[PayrollEmployeeSetup]
    ([EmployeeId]);
GO

-- Creating foreign key on [PayrollJobTypeId] in table 'PayrollJobs'
ALTER TABLE [dbo].[PayrollJobs]
ADD CONSTRAINT [FK_PayrollJobTypePayrollJob]
    FOREIGN KEY ([PayrollJobTypeId])
    REFERENCES [dbo].[PayrollJobTypes]
        ([PayrollJobTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollJobTypePayrollJob'
CREATE INDEX [IX_FK_PayrollJobTypePayrollJob]
ON [dbo].[PayrollJobs]
    ([PayrollJobTypeId]);
GO

-- Creating foreign key on [AccountId] in table 'AccountEntries'
ALTER TABLE [dbo].[AccountEntries]
ADD CONSTRAINT [FK_AccountAccountEntry]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountAccountEntry'
CREATE INDEX [IX_FK_AccountAccountEntry]
ON [dbo].[AccountEntries]
    ([AccountId]);
GO

-- Creating foreign key on [PayrollItemId] in table 'AccountEntries'
ALTER TABLE [dbo].[AccountEntries]
ADD CONSTRAINT [FK_PayrollItemAccountEntry]
    FOREIGN KEY ([PayrollItemId])
    REFERENCES [dbo].[PayrollItems]
        ([PayrollItemId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollItemAccountEntry'
CREATE INDEX [IX_FK_PayrollItemAccountEntry]
ON [dbo].[AccountEntries]
    ([PayrollItemId]);
GO

-- Creating foreign key on [EmployeeAccountId] in table 'PayrollEmployeeSetup'
ALTER TABLE [dbo].[PayrollEmployeeSetup]
ADD CONSTRAINT [FK_EmployeeAccountPayrollEmployeeSetup]
    FOREIGN KEY ([EmployeeAccountId])
    REFERENCES [dbo].[Accounts_EmployeeAccount]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAccountPayrollEmployeeSetup'
CREATE INDEX [IX_FK_EmployeeAccountPayrollEmployeeSetup]
ON [dbo].[PayrollEmployeeSetup]
    ([EmployeeAccountId]);
GO

-- Creating foreign key on [PayrollItemAccountId] in table 'PayrollSetupItems'
ALTER TABLE [dbo].[PayrollSetupItems]
ADD CONSTRAINT [FK_AccountPayrollSetupItem]
    FOREIGN KEY ([PayrollItemAccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountPayrollSetupItem'
CREATE INDEX [IX_FK_AccountPayrollSetupItem]
ON [dbo].[PayrollSetupItems]
    ([PayrollItemAccountId]);
GO

-- Creating foreign key on [PayeeInstitutionId] in table 'Accounts_InstitutionAccount'
ALTER TABLE [dbo].[Accounts_InstitutionAccount]
ADD CONSTRAINT [FK_InstitutionInstitutionAccount]
    FOREIGN KEY ([PayeeInstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionInstitutionAccount'
CREATE INDEX [IX_FK_InstitutionInstitutionAccount]
ON [dbo].[Accounts_InstitutionAccount]
    ([PayeeInstitutionId]);
GO

-- Creating foreign key on [PayrollSetupItemId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_PayrollSetupItemPayrollItem]
    FOREIGN KEY ([PayrollSetupItemId])
    REFERENCES [dbo].[PayrollSetupItems]
        ([PayrollSetupItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollSetupItemPayrollItem'
CREATE INDEX [IX_FK_PayrollSetupItemPayrollItem]
ON [dbo].[PayrollItems]
    ([PayrollSetupItemId]);
GO

-- Creating foreign key on [ParentPayrollItemId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_PayrollItemPayrollItem]
    FOREIGN KEY ([ParentPayrollItemId])
    REFERENCES [dbo].[PayrollItems]
        ([PayrollItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollItemPayrollItem'
CREATE INDEX [IX_FK_PayrollItemPayrollItem]
ON [dbo].[PayrollItems]
    ([ParentPayrollItemId]);
GO

-- Creating foreign key on [PayrollJobTypeId] in table 'PayrollEmployeeSetup'
ALTER TABLE [dbo].[PayrollEmployeeSetup]
ADD CONSTRAINT [FK_PayrollJobTypePayrollEmployeeSetup]
    FOREIGN KEY ([PayrollJobTypeId])
    REFERENCES [dbo].[PayrollJobTypes]
        ([PayrollJobTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollJobTypePayrollEmployeeSetup'
CREATE INDEX [IX_FK_PayrollJobTypePayrollEmployeeSetup]
ON [dbo].[PayrollEmployeeSetup]
    ([PayrollJobTypeId]);
GO

-- Creating foreign key on [CompanyId] in table 'PayrollJobs'
ALTER TABLE [dbo].[PayrollJobs]
ADD CONSTRAINT [FK_BranchPayrollJob]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Branches]
        ([CompanyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BranchPayrollJob'
CREATE INDEX [IX_FK_BranchPayrollJob]
ON [dbo].[PayrollJobs]
    ([CompanyId]);
GO

-- Creating foreign key on [CurrentPayrollJobId] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [FK_PayrollJobBranch]
    FOREIGN KEY ([CurrentPayrollJobId])
    REFERENCES [dbo].[PayrollJobs]
        ([PayrollJobId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollJobBranch'
CREATE INDEX [IX_FK_PayrollJobBranch]
ON [dbo].[Branches]
    ([CurrentPayrollJobId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Employees_User'
ALTER TABLE [dbo].[Employees_User]
ADD CONSTRAINT [FK_User_inherits_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccountId] in table 'Accounts_EmployeeAccount'
ALTER TABLE [dbo].[Accounts_EmployeeAccount]
ADD CONSTRAINT [FK_EmployeeAccount_inherits_Account]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccountId] in table 'Accounts_InstitutionAccount'
ALTER TABLE [dbo].[Accounts_InstitutionAccount]
ADD CONSTRAINT [FK_InstitutionAccount_inherits_Account]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------