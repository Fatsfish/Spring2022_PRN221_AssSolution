USE [master]
GO

CREATE DATABASE [SignalRAssignmentDB03]
USE [SignalRAssignmentDB03]

CREATE TABLE [dbo].[AppUser](
	[UserId] int PRIMARY KEY NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Address] [nvarchar](255) NULL,
	[Password] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL
)
GO
CREATE TABLE [dbo].[PostCategories](
	[CategoryID] [int]  PRIMARY KEY NOT NULL,
	[CategoryName] [nvarchar](160) NOT NULL,
	[Description] [nvarchar](250) NULL
)
GO

CREATE TABLE [dbo].[Post](
	[PostId] [int] PRIMARY KEY NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[PublishStatus] [bit] NOT NULL,
	[AuthorId] int NOT NULL FOREIGN KEY REFERENCES [AppUser](UserId) ON DELETE CASCADE,
	[CategoryID] int NOT NULL FOREIGN KEY REFERENCES [PostCategories](CategoryID) ON DELETE CASCADE
)
GO

USE [SignalRAssignmentDB03]
GO
INSERT [dbo].[AppUser] ([UserId], [FullName], [Address], [Password], [Email]) VALUES (12, N'Saint Michael ', N'United State', N'1234567890', N'SaintMichael@company.com')
GO
INSERT [dbo].[AppUser] ([UserId], [FullName], [Address], [Password], [Email]) VALUES (23, N'David Luiz', N'United Kingdon', N'1234567890', N'DavidLuiz@company.com')
GO
INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (1, N'Sport', N'Sport available for everyone')
GO
INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (2, N'Special Features', N'Special Features')
GO
INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (3, N'Culture', N'World Culture')
GO
INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (4, N'World News', N'Arround the World News')
GO
INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (5, N'News and Features', N'World News and Features')
GO
INSERT [dbo].[Post] ([PostId], [CreatedDate], [UpdatedDate], [Title], [Content], [PublishStatus], [AuthorId], [CategoryID]) VALUES (1, CAST(N'2022-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2022-02-03T00:00:00.0000000' AS DateTime2), N'Stokes striving for atonement with England and focusing on Test career', N'Just ask Ben Stokes, who after weighing up another unforgiving schedule this year has shouldered arms to the Indian Premier League – and a likely seven-figure pay day, going by the all-rounder’s previous value – to focus on his Test career and the vice-captaincy of an ailing England team.', 1, 12, 5)
GO
INSERT [dbo].[Post] ([PostId], [CreatedDate], [UpdatedDate], [Title], [Content], [PublishStatus], [AuthorId], [CategoryID]) VALUES (2, CAST(N'2022-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-02T00:00:00.0000000' AS DateTime2), N'Exeter move back into Premiership play-off picture with victory over Sale', N'Chiefs will be frustrated, though, at their failure to increase a 19-0 lead, missing out on a bonus point, with the only second-half scoring being Sale tries from Ewan Ashman and Curtis Langdon, plus one AJ MacGinty conversion.', 1, 12, 1)
GO


