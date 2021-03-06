USE master
GO

CREATE DATABASE FStoreDBAssignment;
GO

USE FStoreDBAssignment;
GO

CREATE TABLE Member (
	MemberId int NOT NULL PRIMARY KEY,
	Email varchar(100) NOT NULL,
	CompanyName varchar(40) NOT NULL,
	City varchar(15) NOT NULL,
	Country varchar(15) NOT NULL,
	Password varchar(30) NOT NULL
);
GO

CREATE TABLE [Order] (
	OrderId int NOT NULL PRIMARY KEY,
	MemberId int FOREIGN KEY REFERENCES Member(MemberId) ON DELETE CASCADE,
	OrderDate datetime NOT NULL,
	RequiredDate datetime,
	ShippedDate datetime,
	Freight money
);
GO

CREATE TABLE Product (
	ProductId int NOT NULL PRIMARY KEY,
	CategoryId int NOT NULL,
	ProductName varchar(40) NOT NULL,
	Weight varchar(20) NOT NULL,
	UnitPrice money NOT NULL,
	UnitslnStock int NOT NULL
);
GO

CREATE TABLE OrderDetail (
	OrderId int REFERENCES [Order](OrderId) ON DELETE CASCADE,
	ProductId int REFERENCES Product(ProductId) ON DELETE CASCADE,
	UnitPrice money NOT NULL,
	Quantity int NOT NULL,
	Discount float NOT NULL,
	PRIMARY KEY (OrderId, ProductId)
);
GO

INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (1, N'member1@fstore.com', N'KMS', N'HCM', N'Viet nam', N'1')
GO
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (2, N'member2@fstore.com', N'CyberSoft', N'HCM', N'Viet nam', N'1')
GO
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (4665, 1, CAST(N'2021-11-05 12:05:07.677' AS DateTime), CAST(N'2021-11-04 00:00:00.000' AS DateTime), CAST(N'2021-11-05 00:00:00.000' AS DateTime), 10000.0000)
GO
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (6113, 2, CAST(N'2021-11-05 14:04:07.950' AS DateTime), CAST(N'2021-11-04 00:00:00.000' AS DateTime), CAST(N'2021-11-05 00:00:00.000' AS DateTime), 20000.0000)
GO
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (6259, 1, CAST(N'2021-11-05 14:02:50.557' AS DateTime), CAST(N'2021-11-06 00:00:00.000' AS DateTime), CAST(N'2021-11-07 00:00:00.000' AS DateTime), 15000.0000)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (1, 1, N'Candy', N'500g', 20000.0000, 19)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (2, 1, N'Mixed Candy', N'300g', 300000.0000, 18)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (3, 2, N'Cake', N'200g', 15000.0000, 40)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (4, 3, N'Pepsi', N'250ml', 10000.0000, 45)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (5, 4, N'Snack Oshi''s', N'100g', 15000.0000, 31)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (4665, 1, 20000.0000, 1, 5)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6113, 4, 10000.0000, 3, 10)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6113, 5, 15000.0000, 4, 15)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6259, 2, 300000.0000, 2, 5)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6259, 4, 10000.0000, 2, 5)
GO
