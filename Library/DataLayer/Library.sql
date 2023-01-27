USE [Library]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 27/01/2023 6:12:40 pm ******/
DROP TABLE [dbo].[Book]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 27/01/2023 6:12:40 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookName] [nvarchar](50) NULL,
	[Author] [nvarchar](50) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/******************************************************************************************/
USE [Library]
GO

/****** Object:  Table [dbo].[Borrower]    Script Date: 27/01/2023 6:12:51 pm ******/
DROP TABLE [dbo].[Borrower]
GO

/****** Object:  Table [dbo].[Borrower]    Script Date: 27/01/2023 6:12:51 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Borrower](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_Borrower] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/********************************************************************************************/
USE [Library]
GO

ALTER TABLE [dbo].[BorrowedBook] DROP CONSTRAINT [FK_BorrowedBook_Borrower]
GO

ALTER TABLE [dbo].[BorrowedBook] DROP CONSTRAINT [FK_BorrowedBook_Book]
GO

/****** Object:  Table [dbo].[BorrowedBook]    Script Date: 27/01/2023 6:13:19 pm ******/
DROP TABLE [dbo].[BorrowedBook]
GO

/****** Object:  Table [dbo].[BorrowedBook]    Script Date: 27/01/2023 6:13:19 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BorrowedBook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[BorrowerId] [int] NOT NULL,
	[DateBorrowed] [datetime] NOT NULL,
	[DateReturned] [datetime] NULL,
 CONSTRAINT [PK_BorrowedBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BorrowedBook]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBook_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
GO

ALTER TABLE [dbo].[BorrowedBook] CHECK CONSTRAINT [FK_BorrowedBook_Book]
GO

ALTER TABLE [dbo].[BorrowedBook]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBook_Borrower] FOREIGN KEY([BorrowerId])
REFERENCES [dbo].[Borrower] ([Id])
GO

ALTER TABLE [dbo].[BorrowedBook] CHECK CONSTRAINT [FK_BorrowedBook_Borrower]
GO

