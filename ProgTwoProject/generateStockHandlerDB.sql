USE [master]
GO
/****** Object:  Database [OrderManagementDb]    Script Date: 4/07/2021 1:11:20 PM ******/
CREATE DATABASE [OrderManagementDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrderManagementDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\OrderManagementDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrderManagementDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\OrderManagementDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OrderManagementDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrderManagementDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrderManagementDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrderManagementDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrderManagementDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrderManagementDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrderManagementDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrderManagementDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrderManagementDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrderManagementDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrderManagementDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrderManagementDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrderManagementDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrderManagementDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrderManagementDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrderManagementDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrderManagementDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrderManagementDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrderManagementDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrderManagementDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrderManagementDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrderManagementDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrderManagementDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrderManagementDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrderManagementDb] SET RECOVERY FULL 
GO
ALTER DATABASE [OrderManagementDb] SET  MULTI_USER 
GO
ALTER DATABASE [OrderManagementDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrderManagementDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrderManagementDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrderManagementDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OrderManagementDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrderManagementDb] SET QUERY_STORE = OFF
GO
USE [OrderManagementDb]
GO
/****** Object:  Table [dbo].[OrderHeaders]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderStateId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderHeaderId] [int] NOT NULL,
	[StockItemId] [int] NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[OrderHeaderId] ASC,
	[StockItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStates]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockItems]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[InStock] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OrderHeaders] ON 

INSERT [dbo].[OrderHeaders] ([Id], [OrderStateId], [DateTime]) VALUES (19, 3, CAST(N'2020-12-15T06:29:24.007' AS DateTime))
INSERT [dbo].[OrderHeaders] ([Id], [OrderStateId], [DateTime]) VALUES (20, 3, CAST(N'2020-12-15T07:22:04.890' AS DateTime))
INSERT [dbo].[OrderHeaders] ([Id], [OrderStateId], [DateTime]) VALUES (1020, 1, CAST(N'2021-03-01T20:13:40.240' AS DateTime))
SET IDENTITY_INSERT [dbo].[OrderHeaders] OFF
GO
INSERT [dbo].[OrderItems] ([OrderHeaderId], [StockItemId], [Description], [Price], [Quantity]) VALUES (19, 2, N'Chair', CAST(25 AS Decimal(18, 0)), 1)
INSERT [dbo].[OrderItems] ([OrderHeaderId], [StockItemId], [Description], [Price], [Quantity]) VALUES (20, 3, N'Sofa', CAST(250 AS Decimal(18, 0)), 2)
INSERT [dbo].[OrderItems] ([OrderHeaderId], [StockItemId], [Description], [Price], [Quantity]) VALUES (20, 4, N'Wardrobe', CAST(180 AS Decimal(18, 0)), 1)
GO
SET IDENTITY_INSERT [dbo].[OrderStates] ON 

INSERT [dbo].[OrderStates] ([Id], [Name]) VALUES (1, N'New')
INSERT [dbo].[OrderStates] ([Id], [Name]) VALUES (2, N'Pending')
INSERT [dbo].[OrderStates] ([Id], [Name]) VALUES (3, N'Rejected')
INSERT [dbo].[OrderStates] ([Id], [Name]) VALUES (4, N'Complete')
SET IDENTITY_INSERT [dbo].[OrderStates] OFF
GO
SET IDENTITY_INSERT [dbo].[StockItems] ON 

INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (1, N'Table', CAST(100 AS Decimal(18, 0)), 1)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (2, N'Chair', CAST(25 AS Decimal(18, 0)), 7)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (3, N'Sofa', CAST(250 AS Decimal(18, 0)), 6)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (4, N'Wardrobe', CAST(180 AS Decimal(18, 0)), 6)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (5, N'Cupboard', CAST(65 AS Decimal(18, 0)), 9)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (6, N'Single Bed', CAST(120 AS Decimal(18, 0)), 10)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (7, N'Double Bed', CAST(180 AS Decimal(18, 0)), 10)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (8, N'Queen Bed', CAST(220 AS Decimal(18, 0)), 8)
INSERT [dbo].[StockItems] ([Id], [Name], [Price], [InStock]) VALUES (9, N'King Bed', CAST(320 AS Decimal(18, 0)), 9)
SET IDENTITY_INSERT [dbo].[StockItems] OFF
GO
ALTER TABLE [dbo].[StockItems] ADD  CONSTRAINT [DF_Product_InStock]  DEFAULT ((10)) FOR [InStock]
GO
ALTER TABLE [dbo].[OrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_OrderState] FOREIGN KEY([OrderStateId])
REFERENCES [dbo].[OrderStates] ([Id])
GO
ALTER TABLE [dbo].[OrderHeaders] CHECK CONSTRAINT [FK_OrderHeader_OrderState]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_OrderHeader] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeaders] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItem_OrderHeader]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([StockItemId])
REFERENCES [dbo].[StockItems] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
ALTER TABLE [dbo].[StockItems]  WITH CHECK ADD  CONSTRAINT [CK_StockItems] CHECK  (([InStock]>=(0)))
GO
ALTER TABLE [dbo].[StockItems] CHECK CONSTRAINT [CK_StockItems]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteOrderHeaderAndOrderItems]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteOrderHeaderAndOrderItems]
@orderHeaderId int
AS
BEGIN
DELETE FROM OrderItems WHERE [OrderHeaderId]  = @orderHeaderId;
    DELETE FROM OrderHeaders WHERE [Id]  = @orderHeaderId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteOrderItem]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  TAFE NSW
-- Create date: 20191002
-- =============================================
create PROCEDURE [dbo].[sp_DeleteOrderItem]
@orderHeaderId int,
@stockItemId int
AS
BEGIN
    DELETE FROM OrderItems WHERE [OrderHeaderId]  = @orderHeaderId AND [StockItemId] = @stockItemId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertOrderHeader]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertOrderHeader]
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO OrderHeaders (OrderStateId,DateTime) VALUES (1,GETDATE()); 
	SELECT SCOPE_IDENTITY() Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertOrderItem]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertOrderItem]
	@orderHeaderId int,
	@stockItemId int,
	@description varchar(150),
	@price decimal(18,0),
	@quantity int
AS
BEGIN
    INSERT INTO OrderItems([OrderHeaderId],[StockItemId],[Description],[Price],[Quantity]) VALUES (@orderHeaderId,@stockItemId,@description,@price,@quantity);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectOrderHeaderById]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectOrderHeaderById]
	@id int
AS
BEGIN
SET NOCOUNT ON;
   SELECT 
   OrderHeaders.Id,
    OrderHeaders.OrderStateId,
   OrderHeaders.[DateTime], 
   OrderItems.StockItemId,
   OrderItems.[Description],
   OrderItems.Price,
   OrderItems.Quantity
   FROM OrderHeaders
   LEFT OUTER JOIN OrderItems ON OrderItems.OrderHeaderId = OrderHeaders.Id
   WHERE OrderHeaders.Id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectOrderHeaders]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectOrderHeaders]
AS
BEGIN
SET NOCOUNT ON;
   SELECT 
   OrderHeaders.Id,
   OrderHeaders.OrderStateId,
   OrderHeaders.[DateTime],  
   OrderItems.StockItemId,
   OrderItems.[Description],
   OrderItems.Price,
   OrderItems.Quantity
   FROM OrderHeaders
   INNER JOIN OrderItems ON OrderItems.OrderHeaderId = OrderHeaders.Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectStockItemById]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectStockItemById]
	@id int
AS
BEGIN
SET NOCOUNT ON;
   SELECT * FROM StockItems WHERE Id = @id; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SelectStockItems]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectStockItems]
AS
BEGIN
SET NOCOUNT ON;
   SELECT * FROM StockItems; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateOrderState]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateOrderState]
	@orderHeaderId int,
	@stateId int
AS
BEGIN
	SET NOCOUNT ON;
    UPDATE OrderHeaders set OrderStateId = @stateId WHERE Id = @orderHeaderId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateStockItemAmount]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TAFE NSW
-- Create date: 20191002
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateStockItemAmount]
	@id int,
	@amount int
AS
BEGIN
SET NOCOUNT ON;
   UPDATE StockItems SET InStock = InStock + @amount WHERE Id = @id;
   SELECT InStock from StockItems WHERE Id = @id; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpsertOrderItem]    Script Date: 4/07/2021 1:11:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpsertOrderItem] (@orderHeaderId int, @stockItemId int, @description varchar(150), @price decimal(18,0), @quantity int)
AS 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN 
		IF EXISTS ( SELECT * FROM OrderItems WITH (UPDLOCK) WHERE OrderHeaderId = @orderHeaderId AND StockItemId = @stockItemId)
			UPDATE [OrderItems] SET [Quantity] = @quantity WHERE OrderHeaderId = @orderHeaderId AND StockItemId = @stockItemId;
		ELSE 
			INSERT INTO OrderItems([OrderHeaderId],[StockItemId],[Description],[Price],[Quantity]) VALUES (@orderHeaderId,@stockItemId,@description,@price,@quantity);
	COMMIT
GO
USE [master]
GO
ALTER DATABASE [OrderManagementDb] SET  READ_WRITE 
GO
