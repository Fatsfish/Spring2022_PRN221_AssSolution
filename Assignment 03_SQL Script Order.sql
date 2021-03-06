USE master
GO

CREATE DATABASE PostDBAssignment;
GO

USE PostDBAssignment
GO

CREATE TABLE [AppUsers] (
	UserId int NOT NULL PRIMARY KEY,
	Password varchar(30) NOT NULL,
	Address varchar(40) NOT NULL,
	FullName varchar(50) NOT NULL,
	Email varchar(30) NOT NULL
);
GO

CREATE TABLE Categories(
	CategoryId int NOT NULL PRIMARY KEY,
	CategoryName varchar(40) NOT NULL,
	Description varchar(50) NOT NULL
);
GO

CREATE TABLE Posts(
	PostId int NOT NULL PRIMARY KEY,
	AuthorId int REFERENCES AppUsers(UserId) ON DELETE CASCADE,
	CreatedDate datetime NOT NULL,
	UpdatedDate datetime,
	Title varchar(30) NOT NULL,
	[Content] varchar(100) NOT NULL,
	PublishStatus bit,
	CategoryId int REFERENCES Categories(CategoryId) ON DELETE CASCADE
);
GO