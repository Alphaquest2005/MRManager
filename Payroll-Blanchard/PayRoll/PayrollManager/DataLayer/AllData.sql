
USE [PayrollDB];

SET IDENTITY_INSERT [dbo].[Branches] ON
INSERT INTO [dbo].[Branches] ([BranchId], [Name], [Address], [PhoneNumber]) VALUES (1, N'Main Branch', N'Town', N'435 1000')
INSERT INTO [dbo].[Branches] ([BranchId], [Name], [Address], [PhoneNumber]) VALUES (2, N'Gouyave', N'Gouyave', N'435 1001')
SET IDENTITY_INSERT [dbo].[Branches] OFF

SET IDENTITY_INSERT [dbo].[ChargeTypes] ON
INSERT INTO [dbo].[ChargeTypes] ([ChargeTypeId], [ChargeTypeName]) VALUES (1, N'Rate')
INSERT INTO [dbo].[ChargeTypes] ([ChargeTypeId], [ChargeTypeName]) VALUES (2, N'Amount')
SET IDENTITY_INSERT [dbo].[ChargeTypes] OFF


SET IDENTITY_INSERT [dbo].[AccountTypes] ON
INSERT INTO [dbo].[AccountTypes] ([AccountTypeId], [AccountTypeName]) VALUES (1, N'Bank Account')
--INSERT INTO [dbo].[AccountTypes] ([AccountTypeId], [AccountTypeName]) VALUES (2, N'NIS Account')
INSERT INTO [dbo].[AccountTypes] ([AccountTypeId], [AccountTypeName]) VALUES (3, N'Salary Account')
SET IDENTITY_INSERT [dbo].[AccountTypes] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [MiddleName], [Title], [BranchId], [SupervisorId], [EmploymentStartDate]) VALUES (1, N'joseph', N'bartholomew', NULL, N'programer', 1, NULL, '3/1/2013')
SET IDENTITY_INSERT [dbo].[Employees] OFF

INSERT INTO [dbo].[Employees_User] ([Username], [Password], [EmployeeId]) VALUES (N'alpha', N'test', 1)

SET IDENTITY_INSERT [dbo].[Institutions] ON
INSERT INTO [dbo].[Institutions] ([InstitutionId], [Name] , [ShortName]) VALUES (1,'National Insurance Scheme', N'NIS')
INSERT INTO [dbo].[Institutions] ([InstitutionId], [Name], [ShortName]) VALUES (2, 'Communial Cooperative Credit Union',N'CCCU')
INSERT INTO [dbo].[Institutions] ([InstitutionId], [Name], [ShortName]) VALUES (3,'Republic Bank', N'REP BK')
INSERT INTO [dbo].[Institutions] ([InstitutionId], [Name], [ShortName]) VALUES (4,'Government of Grenada', N'Inland Revenue')
INSERT INTO [dbo].[Institutions] ([InstitutionId], [Name], [ShortName]) VALUES (5,'National Cooperative Bank', N'Coop Bank')
SET IDENTITY_INSERT [dbo].[Institutions] OFF

--SET IDENTITY_INSERT [dbo].[Accounts] ON
--INSERT INTO [dbo].[Accounts] ([AccountId], [Accountnumber], [InstitutionId], [AccountName], [AccountType]) VALUES (1, N'122', 1, N'NIS Account',N'NIS Account')
--INSERT INTO [dbo].[Accounts] ([AccountId], [Accountnumber], [InstitutionId], [AccountName], [AccountType]) VALUES (2, N'123', 1, N'Salary Account',N'Salary Account')
--SET IDENTITY_INSERT [dbo].[Accounts] OFF

SET IDENTITY_INSERT [dbo].[PayrollJobTypes] ON
INSERT INTO [dbo].[PayrollJobTypes] ([PayrollJobTypeId], [Name]) VALUES (1, N'Month')
INSERT INTO [dbo].[PayrollJobTypes] ([PayrollJobTypeId], [Name]) VALUES (2, N'Mid Month')
SET IDENTITY_INSERT [dbo].[PayrollJobTypes] OFF