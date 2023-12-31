USE [News]
GO
/****** Object:  Table [dbo].[Accouts]    Script Date: 2022-01-03 7:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accouts](
	[UserName] [varchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [nvarchar](100) NULL,
	[Permission] [tinyint] NULL,
	[Email] [varchar](50) NULL,
	[Lock] [bit] NULL,
	[Active] [bit] NULL,
	[Info] [nvarchar](500) NULL,
	[Avata] [varchar](100) NULL,
 CONSTRAINT [PK_Accouts] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu_Footer]    Script Date: 2022-01-03 7:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu_Footer](
	[ID_Footer] [bigint] NOT NULL,
	[ParentID] [bigint] NULL,
	[Link] [nvarchar](200) NULL,
	[TitleFooter] [nvarchar](200) NULL,
 CONSTRAINT [PK_Menu_Footer] PRIMARY KEY CLUSTERED 
(
	[ID_Footer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 2022-01-03 7:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[ID_MN] [bigint] NOT NULL,
	[Parent] [bigint] NULL,
	[Pos] [tinyint] NULL,
	[Lablel] [nvarchar](200) NULL,
	[UrlLink] [varchar](200) NULL,
	[OrderKey] [bigint] NULL,
	[UserAdd] [varchar](50) NULL,
	[UserText] [varchar](50) NULL,
	[Hide] [bit] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[ID_MN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageFooter]    Script Date: 2022-01-03 7:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageFooter](
	[ID_F] [bigint] NOT NULL,
	[ID_Footer] [bigint] NULL,
	[ContentF] [ntext] NULL,
	[TitleF] [nvarchar](500) NULL,
 CONSTRAINT [PK_PageFooter] PRIMARY KEY CLUSTERED 
(
	[ID_F] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageItems]    Script Date: 2022-01-03 7:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageItems](
	[ID_P] [bigint] NOT NULL,
	[ID_MN] [bigint] NULL,
	[Title] [nvarchar](500) NULL,
	[Sumary] [nvarchar](500) NULL,
	[Contents] [ntext] NULL,
	[CreaDate] [nvarchar](50) NULL,
	[ModiDate] [smalldatetime] NULL,
	[OrderKey] [bigint] NULL,
	[UserAdd] [varchar](50) NULL,
	[UserText] [varchar](50) NULL,
	[Hide] [bit] NULL,
	[Image] [varchar](100) NULL,
 CONSTRAINT [PK_PageItems] PRIMARY KEY CLUSTERED 
(
	[ID_P] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_Accouts] FOREIGN KEY([UserAdd])
REFERENCES [dbo].[Accouts] ([UserName])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_Accouts]
GO
ALTER TABLE [dbo].[PageFooter]  WITH CHECK ADD  CONSTRAINT [FK_PageFooter_Menu_Footer] FOREIGN KEY([ID_Footer])
REFERENCES [dbo].[Menu_Footer] ([ID_Footer])
GO
ALTER TABLE [dbo].[PageFooter] CHECK CONSTRAINT [FK_PageFooter_Menu_Footer]
GO
ALTER TABLE [dbo].[PageItems]  WITH CHECK ADD  CONSTRAINT [FK_PageItems_Menus1] FOREIGN KEY([ID_MN])
REFERENCES [dbo].[Menus] ([ID_MN])
GO
ALTER TABLE [dbo].[PageItems] CHECK CONSTRAINT [FK_PageItems_Menus1]
GO
