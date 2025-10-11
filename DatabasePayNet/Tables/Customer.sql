USE [PayNet]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 11-10-2025 14:58:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Password] [nvarchar](255) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


