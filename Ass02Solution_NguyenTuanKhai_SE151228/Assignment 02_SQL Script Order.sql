USE master
GO

CREATE DATABASE PizzaStoreDBAssignment;
GO

USE PizzaStoreDBAssignment
GO

CREATE TABLE Customers (
	CustomerId int NOT NULL PRIMARY KEY,
	Password varchar(30) NOT NULL,
	ContactName varchar(40) NOT NULL,
	Address varchar(50) NOT NULL,
	Phone varchar(20) NOT NULL
);
GO


CREATE TABLE [Accounts] (
	AccountId int NOT NULL PRIMARY KEY,
	Password varchar(30) NOT NULL,
	UserName varchar(40) NOT NULL unique,
	FullName varchar(50) NOT NULL,
	Type varchar(20) NOT NULL
);
GO

CREATE TABLE [Orders] (
	OrderId int NOT NULL PRIMARY KEY,
	CustomerId int FOREIGN KEY REFERENCES Customers(CustomerId) ON DELETE CASCADE,
	OrderDate datetime NOT NULL,
	RequiredDate datetime,
	ShippedDate datetime,
	Freight money,
	ShipAddress varchar(50)
);
GO


CREATE TABLE Catergories(
	CategoryId int NOT NULL PRIMARY KEY,
	CategoryName varchar(40) NOT NULL,
	Description varchar(50) NOT NULL
);
GO


CREATE TABLE Suppliers(
	SupplierId int NOT NULL PRIMARY KEY,
	CompanyName varchar(40) NOT NULL,
	Address varchar(50) NOT NULL,
	Phone varchar(20) NOT NULL
);
GO


CREATE TABLE Products(
	ProductId int NOT NULL PRIMARY KEY,
	CategoryId int REFERENCES Catergories(CategoryId) ON DELETE CASCADE,
	SupplierId int REFERENCES Suppliers(SupplierId) ON DELETE CASCADE,
	ProductName varchar(40) NOT NULL,
	UnitPrice money NOT NULL,
	QuantityPerUnit int NOT NULL,
	ProductImage varchar(50) not null
);
GO

CREATE TABLE OrderDetails (
	OrderId int REFERENCES [Orders](OrderId) ON DELETE CASCADE,
	ProductId int REFERENCES Products(ProductId) ON DELETE CASCADE,
	UnitPrice money NOT NULL,
	Quantity int NOT NULL,
	PRIMARY KEY (OrderId, ProductId)
);


INSERT [dbo].[Customers] ([CustomerId], [ContactName], [Address], [Phone], [Password]) VALUES (1, N'Customer number 1', N'District 5, HCM city', N'0789123123', N'1')
GO
INSERT [dbo].[Customers] ([CustomerId], [ContactName], [Address], [Phone], [Password]) VALUES (2, N'Customer number 2', N'District 9, HCM city', N'0909686868', N'1')
GO
INSERT [dbo].[Accounts] ([AccountId], [FullName], [UserName], [Type], [Password]) VALUES (1, N'Staff number 1', N'Staff1', N'2', N'1')
GO
INSERT [dbo].[Accounts] ([AccountId], [FullName], [UserName], [Type], [Password]) VALUES (2, N'Staff number 2', N'Staff2', N'2', N'1')
GO
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipAddress]) VALUES (4665, 1, CAST(N'2021-11-05 12:05:07.677' AS DateTime), CAST(N'2021-11-04 00:00:00.000' AS DateTime), CAST(N'2021-11-05 00:00:00.000' AS DateTime), 10000.0000, N'District 1, HCM city')
GO
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipAddress]) VALUES (6113, 2, CAST(N'2021-11-05 14:04:07.950' AS DateTime), CAST(N'2021-11-04 00:00:00.000' AS DateTime), CAST(N'2021-11-05 00:00:00.000' AS DateTime), 20000.0000, N'District 2, HCM city')
GO
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipAddress]) VALUES (6259, 1, CAST(N'2021-11-05 14:02:50.557' AS DateTime), CAST(N'2021-11-06 00:00:00.000' AS DateTime), CAST(N'2021-11-07 00:00:00.000' AS DateTime), 15000.0000, N'District 3, HCM city')
GO
INSERT [dbo].[Catergories] ([CategoryId], [CategoryName], [Description]) VALUES (1, N'Type 1', N'Other foods type 1')
GO
INSERT [dbo].[Catergories] ([CategoryId], [CategoryName], [Description]) VALUES (2, N'Type 2', N'Other foods type 2')
GO
INSERT [dbo].[Catergories] ([CategoryId], [CategoryName], [Description]) VALUES (3, N'Type 3', N'Other foods type 3')
GO
INSERT [dbo].[Catergories] ([CategoryId], [CategoryName], [Description]) VALUES (4, N'Type 4', N'Other foods type 4')
GO
INSERT [dbo].[Suppliers] ([SupplierId], [CompanyName], [Address], [Phone]) VALUES (1, N'Pepsi', N'District 5, HCM city', N'0909868686')
GO
INSERT [dbo].[Suppliers] ([SupplierId], [CompanyName], [Address], [Phone]) VALUES (2, N'Coca Cola', N'District 9, Thu Duc city', N'0999786523')
GO
INSERT [dbo].[Suppliers] ([SupplierId], [CompanyName], [Address], [Phone]) VALUES (3, N'Chuong Duong', N'District 1, HCM city', N'0921988788')
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SupplierId], [ProductName], [ProductImage], [UnitPrice], [QuantityPerUnit]) VALUES (1, 1, 1, N'Candy', N'500g', 20000.0000, 19)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SupplierId], [ProductName], [ProductImage], [UnitPrice], [QuantityPerUnit]) VALUES (2, 1, 1, N'Mixed Candy', N'300g', 300000.0000, 18)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SupplierId], [ProductName], [ProductImage], [UnitPrice], [QuantityPerUnit]) VALUES (3, 2, 1, N'Cake', N'200g', 15000.0000, 40)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SupplierId], [ProductName], [ProductImage], [UnitPrice], [QuantityPerUnit]) VALUES (4, 3, 1, N'Pepsi', N'250ml', 10000.0000, 45)
GO
INSERT [dbo].[Products] ([ProductId], [CategoryId], [SupplierId], [ProductName], [ProductImage], [UnitPrice], [QuantityPerUnit]) VALUES (5, 4, 1, N'Snack Oshi''s', N'100g', 15000.0000, 31)
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [UnitPrice], [Quantity]) VALUES (4665, 1, 20000.0000, 1)
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [UnitPrice], [Quantity]) VALUES (6113, 4, 10000.0000, 3)
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [UnitPrice], [Quantity]) VALUES (6113, 5, 15000.0000, 4)
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [UnitPrice], [Quantity]) VALUES (6259, 2, 300000.0000, 2)
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [UnitPrice], [Quantity]) VALUES (6259, 4, 10000.0000, 2)
GO
