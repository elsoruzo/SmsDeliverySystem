# SmsDeliverySystem
-- migrate to db
USE [SmsDb]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 1/18/2022 6:10:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TokenHash] [nvarchar](1000) NOT NULL,
	[TokenSalt] [nvarchar](50) NOT NULL,
	[TS] [smalldatetime] NOT NULL,
	[ExpiryDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sms]    Script Date: 1/18/2022 6:10:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[TS] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Sms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/18/2022 6:10:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordSalt] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[TS] [smalldatetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sms] ON 
GO
INSERT [dbo].[Sms] ([Id], [UserId], [Name], [IsCompleted], [TS]) VALUES (1, 1, N'Blog about Access Token and Refresh Token Authentication', 1, CAST(N'2022-01-14T00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[Sms] ([Id], [UserId], [Name], [IsCompleted], [TS]) VALUES (3, 1, N'Vaccum the House', 0, CAST(N'2022-01-14T00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[Sms] ([Id], [UserId], [Name], [IsCompleted], [TS]) VALUES (4, 1, N'Farmers Market Shopping', 0, CAST(N'2022-01-14T00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[Sms] ([Id], [UserId], [Name], [IsCompleted], [TS]) VALUES (5, 1, N'Practice Juggling', 0, CAST(N'2022-01-15T00:00:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[Sms] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_User]
GO
ALTER TABLE [dbo].[Sms]  WITH CHECK ADD  CONSTRAINT [FK_Sms_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Sms] CHECK CONSTRAINT [FK_Sms_User]
GO

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsMessage](
	[Id] [UniqueIdentifier] NOT NULL  default newid(),
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SmsId] [UniqueIdentifier] NOT NULL,
	[To] [nvarchar](100) NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_Sms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[SmsMessage]  WITH CHECK ADD  CONSTRAINT [FK_SmsMessage_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SmsMessage] CHECK CONSTRAINT [FK_SmsMessage_User]
GO

ALTER TABLE [dbo].[SmsStatus]  WITH CHECK ADD  CONSTRAINT [FK_SmsStatus_SmsMessage] FOREIGN KEY([SmsId])
REFERENCES [dbo].[SmsMessage] ([Id])
GO
ALTER TABLE [dbo].[SmsStatus] CHECK CONSTRAINT [FK_SmsStatus_SmsMessage]
GO

SET IDENTITY_INSERT [dbo].[SmsMessage] ON

USE [SmsDb]
GO