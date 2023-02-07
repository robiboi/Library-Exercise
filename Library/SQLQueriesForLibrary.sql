USE [Library]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 30/01/2023 9:20:22 am ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type in (N'U'))
DROP TABLE [dbo].[Book]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 30/01/2023 9:20:22 am ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BookName] [varchar](50) NULL,
	[Author] [varchar](50) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [Library]
GO

/****** Object:  Table [dbo].[Borrower]    Script Date: 30/01/2023 9:20:44 am ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Borrower]') AND type in (N'U'))
DROP TABLE [dbo].[Borrower]
GO

/****** Object:  Table [dbo].[Borrower]    Script Date: 30/01/2023 9:20:44 am ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Borrower](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BorrowerName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Borrower] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Library]
GO

ALTER TABLE [dbo].[BorrowedBook] DROP CONSTRAINT [FK_BorrowedBook_Borrower]
GO

ALTER TABLE [dbo].[BorrowedBook] DROP CONSTRAINT [FK_BorrowedBook_Book]
GO

/****** Object:  Table [dbo].[BorrowedBook]    Script Date: 30/01/2023 9:20:33 am ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BorrowedBook]') AND type in (N'U'))
DROP TABLE [dbo].[BorrowedBook]
GO

/****** Object:  Table [dbo].[BorrowedBook]    Script Date: 30/01/2023 9:20:33 am ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BorrowedBook](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[BorrowerId] [int] NOT NULL,
	[DateBorrowed] [datetime] NULL,
	[DateReturned] [datetime] NULL,
 CONSTRAINT [PK_BorrowedBook] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BorrowedBook]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBook_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([id])
GO

ALTER TABLE [dbo].[BorrowedBook] CHECK CONSTRAINT [FK_BorrowedBook_Book]
GO

ALTER TABLE [dbo].[BorrowedBook]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBook_Borrower] FOREIGN KEY([BorrowerId])
REFERENCES [dbo].[Borrower] ([id])
GO

ALTER TABLE [dbo].[BorrowedBook] CHECK CONSTRAINT [FK_BorrowedBook_Borrower]
GO



