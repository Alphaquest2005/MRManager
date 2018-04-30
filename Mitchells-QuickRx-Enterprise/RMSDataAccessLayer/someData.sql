SET QUOTED_IDENTIFIER OFF;
GO
USE [QuickSales];
GO







SET IDENTITY_INSERT [dbo].[Company] ON
INSERT INTO [dbo].[Company] ([CompanyId], [CompanyName], [Address], [Address1], [SoftwareName], [PhoneNumber], [Motto]) VALUES (1, N'Hills and Valley Pharmacy', N'Halifax Street', N'St. George''s', N'QicTick', N'439-6904', N'Your Health Our Busines - We Care')
SET IDENTITY_INSERT [dbo].[Company] OFF



SET IDENTITY_INSERT [dbo].[Stores] ON
INSERT INTO [dbo].[Stores] ([StoreId], [StoreCode], [StoreAddress], [CompanyId], [SeedTransaction],[TransactionSeed]) VALUES (1, N'G2', N'Gate 2', 1,0,0)
SET IDENTITY_INSERT [dbo].[Stores] OFF

SET IDENTITY_INSERT [dbo].[Stations] ON
INSERT INTO [dbo].[Stations] ([StationId], [StationCode], [StoreId], [ReceiptPrinterName],[MachineName]) VALUES (1, N'S1', 1,N'Microsoft XPS Document Writer', N'Alphaquest-PC')
SET IDENTITY_INSERT [dbo].[Stations] OFF



