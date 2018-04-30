SET QUOTED_IDENTIFIER OFF;
GO
USE [QuickSales];
GO

SET IDENTITY_INSERT [dbo].[Item] ON
INSERT INTO [dbo].[Item] ([Description], [ItemNotDiscountable], [ItemId], [ItemLookupCode], [Department], [Category], [Price], [Cost], [Quantity], [ExtendedDescription], [Inactive], [DateCreated],  [SalesTax]) VALUES (N'Ticket', 0, 1, N'Ticket', N'CarPark', N'Ticket', CAST(2.5000 AS Decimal(19, 4)), CAST(2.5000 AS Decimal(19, 4)), 0, N'Ticket', 0, N'2012-01-01 00:00:00', CAST(.15000 AS Decimal(19, 4)))
INSERT INTO [dbo].[Item] ([Description], [ItemNotDiscountable], [ItemId], [ItemLookupCode], [Department], [Category], [Price], [Cost], [Quantity], [ExtendedDescription], [Inactive], [DateCreated],  [SalesTax]) VALUES (N'Golly Mix Drink', 0, 2, N'123', N'Groceries', N'Snacks', CAST(1.0000 AS Decimal(19, 4)), CAST(0.5000 AS Decimal(19, 4)), 100, N'Golly Mix Drink Nestle', 0, N'2012-01-01 00:00:00',  CAST(.07000 AS Decimal(19, 4)))
INSERT INTO [dbo].[Item] ([Description], [ItemNotDiscountable], [ItemId], [ItemLookupCode], [Department], [Category], [Price], [Cost], [Quantity], [ExtendedDescription], [Inactive], [DateCreated],  [SalesTax]) VALUES (N'Asprin', 0, 3, N'0001', N'Pharmacuticals', N'Headache', CAST(5.0000 AS Decimal(19, 4)), CAST(1.0000 AS Decimal(19, 4)), 1000, N'Asprin Bayer', 0, N'2012-04-01 00:00:00',  CAST(0.150 AS Decimal(5, 3)))
INSERT INTO [dbo].[Item_Medicine] ([SuggestedDosage], [ItemId]) VALUES (N'Take one a day with meals', 3)
SET IDENTITY_INSERT [dbo].[Item] Off

INSERT INTO [dbo].[Item_StockItem] ([ItemId]) VALUES (2)
INSERT INTO [dbo].[TicketSetup] ([FreeMinutes], [ItemId]) VALUES (10, 1)
INSERT INTO [dbo].[Item_TicketItem] ([ItemId],[Price1],[Price2]) VALUES (1,3.45,2.30)




SET IDENTITY_INSERT [dbo].[Persons] ON
INSERT INTO [dbo].[Persons] ([Id], [FirstName], [LastName], [CompanyName], [Salutation]) VALUES (1, N'Kellon', N'Rubin', N'Port', N'Mr.')
INSERT INTO [dbo].[Persons] ([Id], [FirstName], [LastName], [CompanyName], [Salutation]) VALUES (2, N'BradShaw', N'Noel', N'Ocean', N'Dr.')
INSERT INTO [dbo].[Persons] ([Id], [FirstName], [LastName], [CompanyName], [Salutation]) VALUES (3, N'Joseph', N'Bartholomew', NULL, NULL)
INSERT INTO [dbo].[Persons] ([Id], [FirstName], [LastName], [CompanyName], [Salutation], [Address], [PhoneNumber], [InActive]) VALUES (4, N'Cavel', N'Antoine', N'Hills and Valleys', NULL, N'Tempe', N'456-8012', NULL)
SET IDENTITY_INSERT [dbo].[Persons] OFF

INSERT INTO [dbo].[Persons_Customers] ([Id]) VALUES (1)
INSERT INTO [dbo].[Persons_Doctor] ([Code], [Id]) VALUES (N'123', 2)
INSERT INTO [dbo].[Persons_Cashier] ([SPassword], [LoginName], [Id]) VALUES (N'test', N'Alpha', 3)
INSERT INTO [dbo].[Persons_Patient] ([Id]) VALUES (4)

SET IDENTITY_INSERT [dbo].[Company] ON
INSERT INTO [dbo].[Company] ([CompanyId], [CompanyName], [Address], [Address1], [SoftwareName], [PhoneNumber], [Motto]) VALUES (1, N'Hills and Valley Pharmacy', N'Halifax Street', N'St. George''s', N'QicTick', N'439-6904', N'Your Health Our Busines - We Care')
SET IDENTITY_INSERT [dbo].[Company] OFF



SET IDENTITY_INSERT [dbo].[Stores] ON
INSERT INTO [dbo].[Stores] ([StoreId], [StoreCode], [StoreAddress], [CompanyId], [SeedTransaction],[TransactionSeed]) VALUES (1, N'G2', N'Gate 2', 1,0,0)
SET IDENTITY_INSERT [dbo].[Stores] OFF

SET IDENTITY_INSERT [dbo].[Stations] ON
INSERT INTO [dbo].[Stations] ([StationId], [StationCode], [StoreId], [ReceiptPrinterName],[MachineName]) VALUES (1, N'S1', 1,N'Microsoft XPS Document Writer', N'Alphaquest-PC')
SET IDENTITY_INSERT [dbo].[Stations] OFF

SET IDENTITY_INSERT [dbo].[Pass] ON
INSERT INTO [dbo].[Pass] ([PassId], [StartDate], [EndDate], [CustomerId], [PassNumber], [FreePass]) VALUES (2, N'2012-01-01 00:00:00', N'2012-01-31 00:00:00', 1, N'00000000000062','False')
INSERT INTO [dbo].[Pass] ([PassId], [StartDate], [EndDate], [CustomerId], [PassNumber], [FreePass]) VALUES (3, N'2012-11-01 00:00:00', N'2012-11-30 00:00:00', 1, N'0000000000045','True')
SET IDENTITY_INSERT [dbo].[Pass] OFF


