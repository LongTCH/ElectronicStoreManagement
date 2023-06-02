USE [master]
GO
/****** Object:  Database [ESMDatabase]    Script Date: 02/06/2023 9:16:59 SA ******/
CREATE DATABASE [ESMDatabase]
GO
ALTER DATABASE [ESMDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ESMDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ESMDatabase] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [ESMDatabase] SET ANSI_NULLS ON 
GO
ALTER DATABASE [ESMDatabase] SET ANSI_PADDING ON 
GO
ALTER DATABASE [ESMDatabase] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [ESMDatabase] SET ARITHABORT ON 
GO
ALTER DATABASE [ESMDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ESMDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ESMDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ESMDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ESMDatabase] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [ESMDatabase] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [ESMDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ESMDatabase] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [ESMDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ESMDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ESMDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ESMDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ESMDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ESMDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ESMDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ESMDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ESMDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ESMDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [ESMDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [ESMDatabase] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [ESMDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ESMDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ESMDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ESMDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ESMDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ESMDatabase] SET QUERY_STORE = OFF
GO
USE [ESMDatabase]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[Id] [char](9) NOT NULL,
	[PasswordHash] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[Birthday] [date] NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[District] [nvarchar](50) NOT NULL,
	[Sub-district] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[Avatar_Path] [nvarchar](200) NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[Id] [int] NOT NULL,
	[StaffID] [char](9) NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Phone] [nvarchar](30) NULL,
	[City] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[Sub_district] [nvarchar](50) NULL,
	[Street] [nvarchar](100) NULL,
	[PurchasedTime] [datetime2](3) NOT NULL,
	[TotalAmount] [money] NOT NULL,
 CONSTRAINT [PK_BILL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL_COMBO]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL_COMBO](
	[Id] [int] NOT NULL,
	[StaffID] [char](9) NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Phone] [nvarchar](30) NULL,
	[City] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[Sub_district] [nvarchar](50) NULL,
	[Street] [nvarchar](100) NULL,
	[PurchasedTime] [datetime2](3) NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[ComboID] [char](9) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_BILL_COMBO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL_PRODUCT]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL_PRODUCT](
	[BillID] [int] NOT NULL,
	[ProductID] [char](9) NOT NULL,
	[Number] [int] NOT NULL,
	[Warranty] [varchar](50) NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[SellPrice] [money] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_BILL_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMBO]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBO](
	[Id] [char](9) NOT NULL,
	[Discount] [float] NOT NULL,
	[ProductIDList] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_COMBO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DISCOUNT]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DISCOUNT](
	[Id] [int] NOT NULL,
	[ProductIDList] [nvarchar](200) NULL,
	[Discount] [float] NULL,
	[StartDate] [datetime2](3) NULL,
	[EndDate] [datetime2](3) NULL,
	[Name] [nvarchar](200) NULL,
 CONSTRAINT [PK_DISCOUNT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMPORT]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMPORT](
	[Id] [int] NOT NULL,
	[StaffID] [char](9) NOT NULL,
	[Provider_Bill_ID] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[District] [nvarchar](50) NOT NULL,
	[Sub_district] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[ImportDate] [date] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[ProviderId] [int] NOT NULL,
 CONSTRAINT [PK_IMPORT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMPORT_PRODUCT]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMPORT_PRODUCT](
	[ImportID] [int] NOT NULL,
	[ProductID] [char](9) NOT NULL,
	[Number] [int] NOT NULL,
	[Warranty] [varchar](50) NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[SellPrice] [money] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_IMPORT_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ImportID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LAPTOP]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LAPTOP](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[CPU] [nvarchar](50) NOT NULL,
	[RAM] [varchar](50) NOT NULL,
	[Storage] [varchar](50) NOT NULL,
	[Graphic] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Need] [nvarchar](200) NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nchar](10) NOT NULL,
 CONSTRAINT [PK_LAPTOP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MONITOR]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONITOR](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Size] [varchar](50) NOT NULL,
	[Panel] [varchar](50) NOT NULL,
	[RefreshRate] [nvarchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Need] [nvarchar](200) NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MONITOR] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PC]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PC](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[CPU] [nvarchar](50) NOT NULL,
	[RAM] [varchar](50) NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Need] [nvarchar](200) NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PCCPU]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PCCPU](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Socket] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Need] [nvarchar](200) NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PCCPU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PCHARDDISK]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PCHARDDISK](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Storage] [varchar](50) NOT NULL,
	[Connect] [varchar](20) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Type] [varchar](20) NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PCHARDDISK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROVIDER]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVIDER](
	[Id] [int] NOT NULL,
	[ProviderName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[website] [nvarchar](200) NULL,
	[note] [nvarchar](200) NULL,
 CONSTRAINT [PK_PROVIDER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMARTPHONE]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMARTPHONE](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[CPU] [nvarchar](50) NOT NULL,
	[RAM] [varchar](50) NOT NULL,
	[Storage] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SMARTPHONE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VGA]    Script Date: 02/06/2023 9:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VGA](
	[Id] [char](9) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Chip] [varchar](50) NOT NULL,
	[Chipset] [varchar](10) NOT NULL,
	[VRAM] [varchar](50) NOT NULL,
	[Gen] [varchar](20) NOT NULL,
	[Price] [money] NOT NULL,
	[Remain] [int] NOT NULL,
	[Detail_Path] [nvarchar](200) NULL,
	[Image_Path] [nvarchar](200) NULL,
	[Series] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Avatar_Path] [nvarchar](200) NULL,
	[Unit] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VGA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'020230000', N'$s2$16384$8$1$lbXgVLgLdiqX+t2ZmPrMclAguAaiDE/SeRui+VakvMw=$lJHuBTMuYWJj1raV/dvS+70HLwJgO6tmii/NwnpQhPU=', N'Long', N'Trương', 1, N'inbaicualong@gmail.com', CAST(N'2003-01-01' AS Date), N'Thành phố Đà Nẵng', N'Huyện Hòa Vang', N'Xã Hòa Phong', N'Thôn An Tân 3', N'0762668222', N'C:\Users\DELL\Downloads\NIKE.png')
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'020230001', N'$s2$16384$8$1$Y0ElGUh6GjCLc++4F292MU+WHHXC6pbEyoprnK51mr4=$UK4Jgrevlh5le55LF52PTsxUIhH/hT7iOZnpO1/WILY=', N'Long1', N'Truong', 1, N'inbaicualong@gmail.com', CAST(N'2005-05-29' AS Date), N'Thành phố Đà Nẵng', N'Huyện Hòa Vang', N'Xã Hòa Phong', N'An Tan', N'0', N'C:\Users\DELL\Downloads\pngegg.png')
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'120200000', N'$s2$16384$8$1$o1aDonfulp5Gh3nu4DYHQs2pQy1IEerfdIfFKcs6vu0=$iTN2L6u+GNyiaJCJDY06+R0Puvk/GSFFuedEpEz9sCI=', N'Long', N'Truong', 1, N'fg@gmail.com', CAST(N'2005-04-08' AS Date), N'Thành phố Đà Nẵng', N'Quận Sơn Trà', N'Phường Nại Hiên Đông', N'123', N'123123123', NULL)
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'120230000', N'00000000000000000', N'asdfasd', N'fwef', 1, N'2345@gmai.com', CAST(N'2005-05-30' AS Date), N'Thành phố Đà Nẵng', N'Quận Liên Chiểu', N'Phường Hòa Hiệp Nam', N'asdfds', N'0', NULL)
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'220200000', N'$s2$16384$8$1$lbXgVLgLdiqX+t2ZmPrMclAguAaiDE/SeRui+VakvMw=$lJHuBTMuYWJj1raV/dvS+70HLwJgO6tmii/NwnpQhPU=', N'Long', N'123', 1, N'fg@gmail.com', CAST(N'2005-04-08' AS Date), N'Thành phố Đà Nẵng', N'Quận Sơn Trà', N'Phường Nại Hiên Đông', N'123', N'123123123', NULL)
INSERT [dbo].[ACCOUNT] ([Id], [PasswordHash], [FirstName], [LastName], [Gender], [EmailAddress], [Birthday], [City], [District], [Sub-district], [Street], [Phone], [Avatar_Path]) VALUES (N'220230000', N'00000000000000000', N'Thanh', N'123', 1, N'1234@gmail.com', CAST(N'2005-05-30' AS Date), N'Thành phố Đà Nẵng', N'Quận Liên Chiểu', N'Phường Hòa Hiệp Bắc', N'fsda', N'4234234', NULL)
GO
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (0, N'020230000', N'Thu Ha', N'0328261609', NULL, NULL, NULL, NULL, CAST(N'2021-01-19T11:38:53.3310000' AS DateTime2), 14810692.7000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (1, N'020230000', N'Khac Duoc', N'0412824204', NULL, NULL, NULL, NULL, CAST(N'2021-01-20T13:32:26.8530000' AS DateTime2), 40979763.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (2, N'020230000', N'Tran Van A', N'0927523752', NULL, NULL, NULL, NULL, CAST(N'2021-02-20T13:33:30.3590000' AS DateTime2), 12689935.5000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (3, N'020230000', N'Tran Van C', N'0345235523', NULL, NULL, NULL, NULL, CAST(N'2022-03-20T13:34:17.5390000' AS DateTime2), 50759742.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (4, N'020230000', N'Thu Ha', N'0349234334', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T13:34:53.2470000' AS DateTime2), 31990125.3000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (5, N'020230000', N'Doan Thi Trang', N'0243284235', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T13:35:25.6020000' AS DateTime2), 46179890.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (6, N'020230000', N'Le Thi Tam', N'0534252353', NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:36:26.5000000' AS DateTime2), 11989041.9000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (7, N'020230000', N'Dinh manh', N'0235235325', NULL, NULL, NULL, NULL, CAST(N'2021-09-20T13:36:57.8280000' AS DateTime2), 11989041.9000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (8, N'020230000', N'Trung Hieu', N'0218423536', NULL, NULL, NULL, NULL, CAST(N'2021-12-20T13:37:36.6840000' AS DateTime2), 26222939.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (9, N'020230000', N'Kim Hiếu', N'0353464612', NULL, NULL, NULL, NULL, CAST(N'2022-01-20T13:39:18.3960000' AS DateTime2), 27015420.3000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (10, N'020230000', N'Anh Dũng', N'0232353257', NULL, NULL, NULL, NULL, CAST(N'2022-01-20T13:40:24.0410000' AS DateTime2), 3389999.5700)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (11, N'020230000', N'Le A', N'0242343256', NULL, NULL, NULL, NULL, CAST(N'2022-02-20T13:40:53.8490000' AS DateTime2), 3389999.5700)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (12, N'020230000', N'Thanh Vinh', N'0323535678', NULL, NULL, NULL, NULL, CAST(N'2023-02-20T13:41:42.9280000' AS DateTime2), 93570000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (13, N'020230000', N'Anh Minh', N'0923573253', NULL, NULL, NULL, NULL, CAST(N'2022-05-20T13:43:19.2940000' AS DateTime2), 4789980.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (14, N'020230000', N'Hoai An', N'0364645757', NULL, NULL, NULL, NULL, CAST(N'2022-09-20T13:43:51.9490000' AS DateTime2), 508996.9500)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (15, N'020230000', N'Ai My', N'0352353456', NULL, NULL, NULL, NULL, CAST(N'2023-02-20T13:44:22.7320000' AS DateTime2), 10191500.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (16, N'020230000', N'Nguyen Hien', N'0923535346', NULL, NULL, NULL, NULL, CAST(N'2023-02-20T13:44:49.9770000' AS DateTime2), 10191500.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (17, N'020230000', N'Khanh Hoa', N'0353454654', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T13:45:30.3430000' AS DateTime2), 18990052.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (18, N'020230000', N'tran tam', N'0347237852', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T13:49:15.3500000' AS DateTime2), 29579163.9000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (19, N'020230000', N'Le Tu', N'0234723746', NULL, NULL, NULL, NULL, CAST(N'2022-04-20T13:49:42.0170000' AS DateTime2), 29579163.9000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (20, N'020230000', N'Do ha', N'0235346587', NULL, NULL, NULL, NULL, CAST(N'2023-04-20T13:50:46.3600000' AS DateTime2), 270725537.1900)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (21, N'020230000', N'Ho Thi Nhu Quynh', N'0624214251', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T19:35:44.0150000' AS DateTime2), 2021041.2600)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (22, N'020230000', N'Tran Anh', N'0232423235', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T19:42:05.4970000' AS DateTime2), 11546346.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (23, N'020230000', N'Xuan Anh', N'0988274803', NULL, NULL, NULL, NULL, CAST(N'2023-04-20T19:42:29.8070000' AS DateTime2), 16678114.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (24, N'020230000', N'Lê Oi', N'0353877453', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T19:43:06.7680000' AS DateTime2), 22980000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (25, N'020230000', N'sfdsf', N'0678565765', NULL, NULL, NULL, NULL, CAST(N'2021-03-20T19:45:19.8850000' AS DateTime2), 10191500.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (26, N'020230000', N'sdfsdf', N'0676575545', NULL, NULL, NULL, NULL, CAST(N'2022-03-20T19:45:56.7190000' AS DateTime2), 6489970.9000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (27, N'020230000', N'gfdg', N'0897856645', NULL, NULL, NULL, NULL, CAST(N'2023-04-20T19:46:22.0640000' AS DateTime2), 37980104.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (28, N'020230000', N'fstergr', N'0445756867', NULL, NULL, NULL, NULL, CAST(N'2023-04-20T19:46:49.4950000' AS DateTime2), 20383000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (29, N'020230000', N'dgdg', N'0567575456', NULL, NULL, NULL, NULL, CAST(N'2021-03-20T19:49:40.5680000' AS DateTime2), 7889960.9100)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (30, N'020230000', N'fhfd', N'0234327429', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T19:50:46.9880000' AS DateTime2), 1489999.5000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (31, N'020230000', N'kf', N'0453422346', NULL, NULL, NULL, NULL, CAST(N'2022-03-20T19:54:56.8460000' AS DateTime2), 167990516.1000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (32, N'020230000', N'sfsaf', N'0253534676', NULL, NULL, NULL, NULL, CAST(N'2023-03-29T19:56:13.8710000' AS DateTime2), 12289948.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (33, N'020230000', N'dgrd', N'0235345347', NULL, NULL, NULL, NULL, CAST(N'2020-01-20T20:08:43.6310000' AS DateTime2), 148106927.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (34, N'020230000', N'sdfs', N'0454353456', NULL, NULL, NULL, NULL, CAST(N'2020-02-20T20:09:35.3100000' AS DateTime2), 204898816.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (35, N'020230000', N'sdfdsf', N'0234823426', NULL, NULL, NULL, NULL, CAST(N'2020-03-20T20:10:36.8380000' AS DateTime2), 126899355.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (36, N'020230000', N'sdgf', N'0824351232', NULL, NULL, NULL, NULL, CAST(N'2020-04-20T20:11:39.3060000' AS DateTime2), 176898985.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (37, N'020230000', N'ythfg', N'0563543224', NULL, NULL, NULL, NULL, CAST(N'2020-05-20T20:12:40.9290000' AS DateTime2), 588000000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (38, N'020230000', N'asd', N'0353453465', NULL, NULL, NULL, NULL, CAST(N'2020-06-20T20:13:19.3330000' AS DateTime2), 300215000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (39, N'020230000', N'sdfsd', N'0453453493', NULL, NULL, NULL, NULL, CAST(N'2020-07-20T20:14:43.0610000' AS DateTime2), 92359780.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (40, N'020230000', N'hgert', N'0238354553', NULL, NULL, NULL, NULL, CAST(N'2020-08-20T20:15:22.1960000' AS DateTime2), 67140248.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (41, N'020230000', N'sfsfsd', N'0243253583', NULL, NULL, NULL, NULL, CAST(N'2020-09-20T20:15:57.7900000' AS DateTime2), 78330289.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (42, N'020230000', N'etrdt', N'041242353', NULL, NULL, NULL, NULL, CAST(N'2020-10-20T20:17:41.1510000' AS DateTime2), 235200000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (43, N'020230000', N'egdfg', N'0646457547', NULL, NULL, NULL, NULL, CAST(N'2020-11-20T20:18:49.4330000' AS DateTime2), 141519083.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (44, N'020230000', N'sdsd', N'0676574556', NULL, NULL, NULL, NULL, CAST(N'2020-12-20T20:19:22.3670000' AS DateTime2), 215190554.7000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (45, N'020230000', N'stger', N'0234353473', NULL, NULL, NULL, NULL, CAST(N'2021-01-20T20:27:24.9720000' AS DateTime2), 159950626.5000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (46, N'020230000', N'sfdsg', N'0435344646', NULL, NULL, NULL, NULL, CAST(N'2021-02-20T20:27:57.4570000' AS DateTime2), 383881503.6000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (47, N'020230000', N'tyha', N'0842323553', NULL, NULL, NULL, NULL, CAST(N'2021-03-20T20:29:14.8830000' AS DateTime2), 92939578.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (48, N'020230000', N'dgdfg', N'0242353557', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T20:30:35.8270000' AS DateTime2), 53069656.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (49, N'020230000', N'kfd', N'024354301', NULL, NULL, NULL, NULL, CAST(N'2021-05-20T20:31:16.3740000' AS DateTime2), 420301000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (50, N'020230000', N'rgdh', N'0243254306', NULL, NULL, NULL, NULL, CAST(N'2021-06-20T20:31:44.5910000' AS DateTime2), 117600000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (51, N'020230000', N'khanh', N'0433535381', NULL, NULL, NULL, NULL, CAST(N'2021-07-20T20:32:19.0000000' AS DateTime2), 127960501.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (52, N'020230000', N'thuhe', N'0214252352', NULL, NULL, NULL, NULL, CAST(N'2021-08-20T20:32:59.0790000' AS DateTime2), 123090455.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (53, N'020230000', N'hai', N'0353534614', NULL, NULL, NULL, NULL, CAST(N'2021-09-20T20:36:06.0230000' AS DateTime2), 11190041.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (54, N'020230000', N'sgsdg', N'0824242361', NULL, NULL, NULL, NULL, CAST(N'2021-10-20T20:38:47.8680000' AS DateTime2), 60043000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (55, N'020230000', N'ett', N'0242351673', NULL, NULL, NULL, NULL, CAST(N'2021-11-20T20:39:13.6700000' AS DateTime2), 14810692.7000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (56, N'020230000', N'hieu', N'0243634671', NULL, NULL, NULL, NULL, CAST(N'2021-12-20T20:41:35.1030000' AS DateTime2), 61959718.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (57, N'020230000', N'sdt', N'0232528916', NULL, NULL, NULL, NULL, CAST(N'2022-01-20T20:50:08.5750000' AS DateTime2), 300215000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (58, N'020230000', N'gsg', N'0924235923', NULL, NULL, NULL, NULL, CAST(N'2022-02-20T20:50:54.8080000' AS DateTime2), 63980250.6000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (59, N'020230000', N'sgs', N'0243259142', NULL, NULL, NULL, NULL, CAST(N'2022-03-20T20:51:33.0480000' AS DateTime2), 352800000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (60, N'020230000', N'fdhf', N'0724253281', NULL, NULL, NULL, NULL, CAST(N'2023-03-25T20:55:53.1500000' AS DateTime2), 141519188.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (61, N'020230000', N'gdfh', N'0234325534', NULL, NULL, NULL, NULL, CAST(N'2023-04-02T20:56:24.2050000' AS DateTime2), 40979763.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (62, N'020230000', N'gsdg', N'0243243253', NULL, NULL, NULL, NULL, CAST(N'2021-01-20T21:00:31.4960000' AS DateTime2), 40766000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (63, N'020230000', N'ewt', N'0242352353', NULL, NULL, NULL, NULL, CAST(N'2021-02-28T21:00:51.0680000' AS DateTime2), 58409738.1000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (64, N'020230000', N'tseh', N'0923532592', NULL, NULL, NULL, NULL, CAST(N'2021-03-03T21:03:45.8380000' AS DateTime2), 151920417.6000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (65, N'020230000', N'hl', N'028261609', NULL, NULL, NULL, NULL, CAST(N'2021-04-19T21:02:17.5950000' AS DateTime2), 10191500.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (66, N'020230000', N'ai tran', N'0242353252', NULL, NULL, NULL, NULL, CAST(N'2021-05-09T21:03:07.4360000' AS DateTime2), 12979941.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (67, N'020230000', N'ksdf', N'0242353543', NULL, NULL, NULL, NULL, CAST(N'2021-06-20T21:06:45.0300000' AS DateTime2), 101915000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (68, N'020230000', N'thu', N'0242353553', NULL, NULL, NULL, NULL, CAST(N'2021-07-20T21:07:14.9030000' AS DateTime2), 45429796.3000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (69, N'020230000', N'gh', N'0712423432', NULL, NULL, NULL, NULL, CAST(N'2021-08-20T21:07:51.0050000' AS DateTime2), 94950261.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (70, N'020230000', N'deg', N'0234646472', NULL, NULL, NULL, NULL, CAST(N'2021-09-01T21:09:40.2670000' AS DateTime2), 56970156.6000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (71, N'020230000', N'sfds', N'0824235561', NULL, NULL, NULL, NULL, CAST(N'2021-10-05T21:14:04.5440000' AS DateTime2), 71340500.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (72, N'020230000', N'thsd', N'0235353462', NULL, NULL, NULL, NULL, CAST(N'2022-02-20T21:15:28.2720000' AS DateTime2), 227880626.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (73, N'020230000', N'fsdg', N'0235345436', NULL, NULL, NULL, NULL, CAST(N'2022-05-08T21:15:57.0740000' AS DateTime2), 38939825.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (74, N'020230000', N'th', N'0234353464', NULL, NULL, NULL, NULL, CAST(N'2022-09-20T21:16:33.0960000' AS DateTime2), 19469912.7000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (75, N'020230000', N'k', N'0122353537', NULL, NULL, NULL, NULL, CAST(N'2022-12-20T21:16:58.4610000' AS DateTime2), 37980104.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (76, N'020230000', N'hai', N'024235235', NULL, NULL, NULL, NULL, CAST(N'2021-05-20T21:20:56.5360000' AS DateTime2), 58679637.2400)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (77, N'020230000', N'h', N'0353464646', NULL, NULL, NULL, NULL, CAST(N'2021-07-28T21:21:19.3100000' AS DateTime2), 81950375.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (78, N'020230000', N'hdrh', N'0243549243', NULL, NULL, NULL, NULL, CAST(N'2021-09-20T21:21:45.2850000' AS DateTime2), 110609533.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (79, N'020230000', N'oqq', N'0243534646', NULL, NULL, NULL, NULL, CAST(N'2021-12-20T21:22:04.2470000' AS DateTime2), 12289948.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (80, N'020230000', N'dgdg', N'0243432325', NULL, NULL, NULL, NULL, CAST(N'2022-02-20T21:22:33.7690000' AS DateTime2), 131120600.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (81, N'020230000', N'hao', N'0125343463', NULL, NULL, NULL, NULL, CAST(N'2022-04-23T21:23:00.0090000' AS DateTime2), 49159792.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (82, N'020230000', N'dươc', N'0248253257', NULL, NULL, NULL, NULL, CAST(N'2021-04-10T21:49:42.0020000' AS DateTime2), 95912335.2000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (83, N'020230000', N'Tka', N'02343646775', NULL, NULL, NULL, NULL, CAST(N'2021-02-18T21:51:49.1130000' AS DateTime2), 135547826.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (84, N'020230000', N'fgfd', N'0236456571', NULL, NULL, NULL, NULL, CAST(N'2021-01-20T22:34:04.9050000' AS DateTime2), 113519757.6000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (85, N'020230000', N'hdh', N'0354646234', NULL, NULL, NULL, NULL, CAST(N'2021-03-20T22:35:20.2460000' AS DateTime2), 167409104.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (86, N'020230000', N'sfds', N'04353444654', NULL, NULL, NULL, NULL, CAST(N'2021-01-10T22:45:16.0670000' AS DateTime2), 29339818.6200)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (87, N'020230000', N'g', N'0325435346', NULL, NULL, NULL, NULL, CAST(N'2021-02-20T22:45:36.3610000' AS DateTime2), 50391853.3600)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (88, N'020230000', N'hdh', N'0343534512', NULL, NULL, NULL, NULL, CAST(N'2021-03-20T22:46:03.5410000' AS DateTime2), 49159792.8000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (89, N'020230000', N'jsf', N'0242353564', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T22:46:29.5780000' AS DateTime2), 65560300.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (90, N'020230000', N'dgdg', N'0345436547', NULL, NULL, NULL, NULL, CAST(N'2021-01-05T22:48:32.7130000' AS DateTime2), 22231453.8600)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (91, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-01-27T22:48:47.0070000' AS DateTime2), 147895819.5000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (92, N'020230000', N'd', N'0353623546', NULL, NULL, NULL, NULL, CAST(N'2021-02-08T22:49:09.5060000' AS DateTime2), 44029783.5600)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (93, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-03-17T22:49:23.9380000' AS DateTime2), 50319752.6400)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (94, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-18T22:49:42.5600000' AS DateTime2), 10579920.5200)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (95, N'020230000', N'df', N'054657657', NULL, NULL, NULL, NULL, CAST(N'2021-01-25T22:54:28.1180000' AS DateTime2), 20339997.4200)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (96, N'020230000', N'g', N'02534546757', NULL, NULL, NULL, NULL, CAST(N'2021-02-03T22:54:55.3920000' AS DateTime2), 62700000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (97, N'020230000', N'dfdg', N'0233534691', NULL, NULL, NULL, NULL, CAST(N'2021-03-14T22:55:31.7460000' AS DateTime2), 765900000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (98, N'020230000', N't', N'0235464576', NULL, NULL, NULL, NULL, CAST(N'2021-04-07T22:55:55.3080000' AS DateTime2), 9690000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (99, N'020230000', N'hang', N'0923546677', NULL, NULL, NULL, NULL, CAST(N'2021-01-24T23:07:10.8030000' AS DateTime2), 957996.0000)
GO
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (100, N'020230000', N'ksf', N'0346547568', NULL, NULL, NULL, NULL, CAST(N'2021-02-07T23:09:03.4140000' AS DateTime2), 4989963.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (101, N'020230000', N'linh', N'024365475', NULL, NULL, NULL, NULL, CAST(N'2021-03-16T23:09:51.4800000' AS DateTime2), 2544984.7500)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (102, N'020230000', N'khanh', N'0254365476', NULL, NULL, NULL, NULL, CAST(N'2021-04-12T23:10:22.5080000' AS DateTime2), 3492974.1000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (103, N'020230000', N'hoang', N'03254657658', NULL, NULL, NULL, NULL, CAST(N'2021-04-19T23:10:46.1010000' AS DateTime2), 689000.3100)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (104, N'020230000', N'jfdh', N'0436547658', NULL, NULL, NULL, NULL, CAST(N'2022-01-24T23:25:27.8250000' AS DateTime2), 266212475.1000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (105, N'020230000', N'kghk', N'0343645645', NULL, NULL, NULL, NULL, CAST(N'2022-02-05T23:26:05.2130000' AS DateTime2), 26969976.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (106, N'020230000', N'ksdg', N'0235353463', NULL, NULL, NULL, NULL, CAST(N'2022-03-18T23:26:45.9060000' AS DateTime2), 26449801.3000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (107, N'020230000', N'hdf', N'0346457572', NULL, NULL, NULL, NULL, CAST(N'2021-08-12T23:30:42.9390000' AS DateTime2), 48450000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (108, N'020230000', N'kht', N'0465475756', NULL, NULL, NULL, NULL, CAST(N'2021-09-12T23:34:13.4420000' AS DateTime2), 9729957.3000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (109, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-01-07T23:34:39.0290000' AS DateTime2), 1378000.6200)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (110, N'020230000', N'hdfh', N'0345465467', NULL, NULL, NULL, NULL, CAST(N'2023-03-20T23:38:18.9180000' AS DateTime2), 204898816.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (111, N'020230000', N'Thu', N'0242523512', NULL, NULL, NULL, NULL, CAST(N'2023-04-21T15:36:57.6360000' AS DateTime2), 29621385.4000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (112, N'020230000', N'Th', N'035932535', NULL, NULL, NULL, NULL, CAST(N'2023-04-21T15:40:39.8780000' AS DateTime2), 60521998.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (113, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-04-24T09:51:55.2000000' AS DateTime2), 316587172.7000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (114, N'020230000', N'Long', N'0762668222', NULL, NULL, NULL, NULL, CAST(N'2023-05-03T17:03:08.4460000' AS DateTime2), 14831000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (115, N'020230000', N'2354', N'24352', NULL, NULL, NULL, NULL, CAST(N'2023-05-03T19:24:40.3070000' AS DateTime2), 41630400.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (116, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-05-03T19:25:21.7700000' AS DateTime2), 33300400.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (117, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-05-04T13:33:25.8090000' AS DateTime2), 60000000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (118, N'020230001', N'sdf', N'234', NULL, NULL, NULL, NULL, CAST(N'2023-05-30T15:03:14.2670000' AS DateTime2), 3090000.0000)
INSERT [dbo].[BILL] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount]) VALUES (119, N'020230000', N'haha', N'123123', NULL, NULL, NULL, NULL, CAST(N'2023-05-30T18:27:38.4960000' AS DateTime2), 30990000.0000)
GO
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (0, N'020230000', N'Thu Ha', N'0328261609', NULL, NULL, NULL, NULL, CAST(N'2021-04-20T13:46:11.9230000' AS DateTime2), 28812602.2595, N'990000002', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (1, N'020230000', N'Thu Thao', N'0325293523', NULL, NULL, NULL, NULL, CAST(N'2023-04-20T13:46:39.4330000' AS DateTime2), 22522037.5050, N'990000004', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (2, N'020230000', N'hsfaf', N'0988274803', NULL, NULL, NULL, NULL, CAST(N'2021-02-20T23:14:09.4830000' AS DateTime2), 18991469.9114, N'990000000', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (3, N'020230000', N'ai my', N'0892235526', NULL, NULL, NULL, NULL, CAST(N'2021-02-20T23:15:35.2490000' AS DateTime2), 18991469.9114, N'990000000', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (4, N'020230000', N'tran', N'02437575686', NULL, NULL, NULL, NULL, CAST(N'2022-03-04T23:17:12.0410000' AS DateTime2), 18991469.9114, N'990000000', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (5, N'020230000', N'hanh', N'045657567', NULL, NULL, NULL, NULL, CAST(N'2023-01-05T23:40:48.4200000' AS DateTime2), 28812602.2595, N'990000002', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (6, N'020230000', N'hL', N'024235325', NULL, NULL, NULL, NULL, CAST(N'2023-04-21T15:00:27.0760000' AS DateTime2), 22522037.5050, N'990000004', 1)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (7, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-05-03T19:13:47.2420000' AS DateTime2), 67966920.0000, N'990000000', 3)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (8, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-05-03T19:21:58.5910000' AS DateTime2), 67966920.0000, N'990000000', 3)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (9, N'020230000', N'Long', N'12345', NULL, NULL, NULL, NULL, CAST(N'2023-05-05T13:21:06.4710000' AS DateTime2), 396278400.0000, N'990000003', 7)
INSERT [dbo].[BILL_COMBO] ([Id], [StaffID], [CustomerName], [Phone], [City], [District], [Sub_district], [Street], [PurchasedTime], [TotalAmount], [ComboID], [Number]) VALUES (10, N'020230000', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-05-05T13:26:33.0930000' AS DateTime2), 32290330.0000, N'990000001', 1)
GO
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (1, N'010000001', 2, NULL, N'Cái       ', 20489881.6000, 40979763.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (2, N'010000003', 1, NULL, N'Cái       ', 12689935.5000, 12689935.5000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (3, N'010000003', 4, NULL, N'Cái       ', 12689935.5000, 50759742.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (4, N'010000010', 1, NULL, N'Cái       ', 31990125.3000, 31990125.3000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (5, N'010000007', 2, NULL, N'Cái       ', 23089945.0000, 46179890.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (6, N'020000000', 1, NULL, N'Bộ', 11989041.9000, 11989041.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (7, N'020000000', 1, NULL, N'Bộ', 11989041.9000, 11989041.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (8, N'020000001', 3, NULL, N'Bộ', 8740979.8000, 26222939.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (9, N'020000002', 1, NULL, N'Bộ', 11546346.4000, 11546346.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (9, N'020000008', 1, NULL, N'Bộ', 15469073.9000, 15469073.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (10, N'030000000', 1, NULL, N'Cái', 3389999.5700, 3389999.5700)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (11, N'030000000', 1, NULL, N'Cái', 3389999.5700, 3389999.5700)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (12, N'030000003', 2, NULL, N'Cái', 9690000.0000, 19380000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (12, N'030000004', 3, NULL, N'Cái', 20900000.0000, 62700000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (12, N'030000005', 1, NULL, N'Cái', 11490000.0000, 11490000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (13, N'040000000', 10, NULL, N'cái', 478998.0000, 4789980.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (14, N'040000003', 1, NULL, N'cái', 508996.9500, 508996.9500)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'070000000', 1, NULL, N'Cái', 10191500.0000, 10191500.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'070000000', 1, NULL, N'Cái', 10191500.0000, 10191500.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'070000002', 1, NULL, N'Cái', 18990052.2000, 18990052.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'060000005', 1, NULL, N'Cái', 29579163.9000, 29579163.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'060000005', 1, NULL, N'Cái', 29579163.9000, 29579163.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'050000000', 2, NULL, N'Cái', 4889969.7700, 9779939.5400)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'050000002', 1, NULL, N'Cái', 167990516.1000, 167990516.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'050000003', 3, NULL, N'Cái', 16390075.0000, 49170225.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'050000004', 1, NULL, N'Cái', 12289948.2000, 12289948.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'050000005', 5, NULL, N'Cái', 6298981.6700, 31494908.3500)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (21, N'060000001', 1, NULL, N'Cái', 2021041.2600, 2021041.2600)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (22, N'020000002', 1, NULL, N'Bộ', 11546346.4000, 11546346.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (23, N'020000007', 1, NULL, N'Bộ', 16678114.2000, 16678114.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (24, N'020000006', 2, NULL, N'Bộ', 11490000.0000, 22980000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (25, N'070000000', 1, NULL, N'Cái', 10191500.0000, 10191500.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (26, N'070000001', 1, NULL, N'Cái', 6489970.9000, 6489970.9000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (27, N'070000002', 2, NULL, N'Cái', 18990052.2000, 37980104.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (28, N'070000000', 2, NULL, N'Cái', 10191500.0000, 20383000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (29, N'050000001', 1, NULL, N'Cái', 7889960.9100, 7889960.9100)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (30, N'040000001', 1, NULL, N'cái', 1489999.5000, 1489999.5000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (31, N'050000002', 1, NULL, N'Cái', 167990516.1000, 167990516.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (32, N'050000004', 1, NULL, N'Cái', 12289948.2000, 12289948.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (33, N'010000000', 10, NULL, N'Cái       ', 14810692.7000, 148106927.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (34, N'010000001', 10, NULL, N'Cái       ', 20489881.6000, 204898816.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (35, N'010000003', 10, NULL, N'Cái       ', 12689935.5000, 126899355.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (36, N'010000004', 10, NULL, N'Cái       ', 17689898.5000, 176898985.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (37, N'010000005', 10, NULL, N'Cái       ', 58800000.0000, 588000000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (38, N'010000006', 5, NULL, N'Cái       ', 60043000.0000, 300215000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (39, N'010000007', 4, NULL, N'Cái       ', 23089945.0000, 92359780.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (40, N'010000009', 6, NULL, N'Cái       ', 11190041.4000, 67140248.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (41, N'010000009', 7, NULL, N'Cái       ', 11190041.4000, 78330289.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (42, N'010000005', 4, NULL, N'Cái       ', 58800000.0000, 235200000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (43, N'010000012', 8, NULL, N'Cái       ', 17689885.4000, 141519083.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (44, N'020000010', 11, NULL, N'Bộ', 19562777.7000, 215190554.7000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (45, N'010000010', 5, NULL, N'Cái       ', 31990125.3000, 159950626.5000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (46, N'010000010', 12, NULL, N'Cái       ', 31990125.3000, 383881503.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (47, N'010000011', 6, NULL, N'Cái       ', 15489929.7000, 92939578.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (48, N'010000012', 3, NULL, N'Cái       ', 17689885.4000, 53069656.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (49, N'010000006', 7, NULL, N'Cái       ', 60043000.0000, 420301000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (50, N'010000005', 2, NULL, N'Cái       ', 58800000.0000, 117600000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (51, N'010000010', 4, NULL, N'Cái       ', 31990125.3000, 127960501.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (52, N'010000009', 11, NULL, N'Cái       ', 11190041.4000, 123090455.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (53, N'010000009', 1, NULL, N'Cái       ', 11190041.4000, 11190041.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (54, N'010000006', 1, NULL, N'Cái       ', 60043000.0000, 60043000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (55, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (56, N'010000011', 4, NULL, N'Cái       ', 15489929.7000, 61959718.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (57, N'010000006', 5, NULL, N'Cái       ', 60043000.0000, 300215000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (58, N'010000010', 2, NULL, N'Cái       ', 31990125.3000, 63980250.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (59, N'010000005', 6, NULL, N'Cái       ', 58800000.0000, 352800000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (60, N'010000004', 8, NULL, N'Cái       ', 17689898.5000, 141519188.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (61, N'010000001', 2, NULL, N'Cái       ', 20489881.6000, 40979763.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (62, N'070000000', 4, NULL, N'Cái', 10191500.0000, 40766000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (63, N'070000001', 9, NULL, N'Cái', 6489970.9000, 58409738.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (64, N'070000002', 8, NULL, N'Cái', 18990052.2000, 151920417.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (65, N'070000000', 1, NULL, N'Cái', 10191500.0000, 10191500.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (66, N'070000001', 2, NULL, N'Cái', 6489970.9000, 12979941.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (67, N'070000000', 10, NULL, N'Cái', 10191500.0000, 101915000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (68, N'070000001', 7, NULL, N'Cái', 6489970.9000, 45429796.3000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (69, N'070000002', 5, NULL, N'Cái', 18990052.2000, 94950261.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (70, N'070000002', 3, NULL, N'Cái', 18990052.2000, 56970156.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (71, N'070000000', 7, NULL, N'Cái', 10191500.0000, 71340500.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (72, N'070000002', 12, NULL, N'Cái', 18990052.2000, 227880626.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (73, N'070000001', 6, NULL, N'Cái', 6489970.9000, 38939825.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (74, N'070000001', 3, NULL, N'Cái', 6489970.9000, 19469912.7000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (75, N'070000002', 2, NULL, N'Cái', 18990052.2000, 37980104.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (76, N'050000000', 12, NULL, N'Cái', 4889969.7700, 58679637.2400)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (77, N'050000003', 5, NULL, N'Cái', 16390075.0000, 81950375.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (78, N'050000004', 9, NULL, N'Cái', 12289948.2000, 110609533.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (79, N'050000004', 1, NULL, N'Cái', 12289948.2000, 12289948.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (80, N'050000003', 8, NULL, N'Cái', 16390075.0000, 131120600.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (81, N'050000004', 4, NULL, N'Cái', 12289948.2000, 49159792.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (82, N'020000000', 8, NULL, N'Bộ', 11989041.9000, 95912335.2000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (83, N'020000005', 7, NULL, N'Bộ', 19363975.2000, 135547826.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (84, N'020000003', 8, NULL, N'Bộ', 14189969.7000, 113519757.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (85, N'020000004', 12, NULL, N'Bộ', 13950758.7000, 167409104.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (86, N'050000000', 6, NULL, N'Cái', 4889969.7700, 29339818.6200)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (87, N'050000005', 8, NULL, N'Cái', 6298981.6700, 50391853.3600)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (88, N'050000004', 4, NULL, N'Cái', 12289948.2000, 49159792.8000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (89, N'050000003', 4, NULL, N'Cái', 16390075.0000, 65560300.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (90, N'060000001', 11, NULL, N'Cái', 2021041.2600, 22231453.8600)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (91, N'060000005', 5, NULL, N'Cái', 29579163.9000, 147895819.5000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (92, N'060000003', 7, NULL, N'Cái', 6289969.0800, 44029783.5600)
GO
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (93, N'060000003', 8, NULL, N'Cái', 6289969.0800, 50319752.6400)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (94, N'060000009', 2, NULL, N'Cái', 5289960.2600, 10579920.5200)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (95, N'030000000', 6, NULL, N'Cái', 3389999.5700, 20339997.4200)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (96, N'030000004', 3, NULL, N'Cái', 20900000.0000, 62700000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (97, N'030000002', 10, NULL, N'Cái', 76590000.0000, 765900000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (98, N'030000003', 1, NULL, N'Cái', 9690000.0000, 9690000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (99, N'040000000', 2, NULL, N'cái', 478998.0000, 957996.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (100, N'040000008', 10, NULL, N'cái', 498996.3000, 4989963.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (101, N'040000003', 5, NULL, N'cái', 508996.9500, 2544984.7500)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (102, N'040000008', 7, NULL, N'cái', 498996.3000, 3492974.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (103, N'040000002', 1, NULL, N'cái', 689000.3100, 689000.3100)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (104, N'060000005', 9, NULL, N'Cái', 29579163.9000, 266212475.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (105, N'060000007', 3, NULL, N'Cái', 8989992.0000, 26969976.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (106, N'060000009', 5, NULL, N'Cái', 5289960.2600, 26449801.3000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (107, N'030000003', 5, NULL, N'Cái', 9690000.0000, 48450000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (108, N'040000005', 7, NULL, N'cái', 1389993.9000, 9729957.3000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (109, N'040000002', 2, NULL, N'cái', 689000.3100, 1378000.6200)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (110, N'010000001', 10, NULL, N'Cái       ', 20489881.6000, 204898816.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (111, N'010000000', 2, NULL, N'Cái       ', 14810692.7000, 29621385.4000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (112, N'010000006', 1, NULL, N'Cái       ', 60043000.0000, 60043000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (112, N'040000000', 1, NULL, N'cái', 478998.0000, 478998.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (113, N'020000001', 17, NULL, N'Bộ', 8740979.8000, 148596656.6000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (113, N'050000002', 1, NULL, N'Cái', 167990516.1000, 167990516.1000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (114, N'040000001', 3, NULL, N'cái', 1617000.0000, 4851000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (114, N'060000002', 2, NULL, N'Cái', 4990000.0000, 9980000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (115, N'010000000', 1, NULL, N'Cái       ', 16650200.0000, 16650200.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (115, N'010000001', 1, NULL, N'Cái       ', 24980200.0000, 24980200.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (116, N'010000000', 2, NULL, N'Cái       ', 16650200.0000, 33300400.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (117, N'010000005', 1, NULL, N'Cái       ', 60000000.0000, 60000000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (118, N'030000001', 1, NULL, N'Cái', 3090000.0000, 3090000.0000)
INSERT [dbo].[BILL_PRODUCT] ([BillID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (119, N'070000002', 1, NULL, N'Cái', 30990000.0000, 30990000.0000)
GO
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000000', 2, N'020000000 050000000 040000000 060000001', N'Combo Tết Nguyên Đán 2021', N'combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000001', 3, N'060000009 030000005 020000006', N'Combo Hè 2022', N'combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000002', 5, N'040000006 050000001 020000011', N'Combo Kỉ Niệm Thành Lập', N'combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000003', 4, N'020000004 040000007 030000004 010000004', N'Combo Tết Nguyên Đán 2022', N'combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000004', 5, N'020000010 060000000', N'Combo Tri Ân Khách Hàng', N'combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000006', 2, N'010000000 030000002', N'Combo Khuyến mãi 8/3', N'Combo', 1)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000008', 3, N'010000000 010000001', N'test', N'combo', 0)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000010', 10, N'040000007 040000008', N'test', N'combo', 0)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000012', 0, N'010000000 010000001', N'fffff', N'fffff', 0)
INSERT [dbo].[COMBO] ([Id], [Discount], [ProductIDList], [Name], [Unit], [Status]) VALUES (N'990000013', 2, N'040000000 040000001 060000003 060000004', N'ga', N'combo', 1)
GO
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (1, N'010000001 010000003', 20, CAST(N'2023-05-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-05-02T00:00:00.0000000' AS DateTime2), N'ga')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (2, N'010000003 010000004 020000000', 23, CAST(N'2023-05-02T15:25:37.4690000' AS DateTime2), CAST(N'2023-07-02T00:00:00.0000000' AS DateTime2), N'haha')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (3, N'040000000 040000001', 2, CAST(N'2023-05-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-17T00:00:00.0000000' AS DateTime2), N'hehe')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (4, N'050000000 050000001', 2, CAST(N'2023-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-16T00:00:00.0000000' AS DateTime2), N'1234')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (5, N'020000000 020000001 020000002', 5, CAST(N'2023-05-03T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T00:00:00.0000000' AS DateTime2), N'fads')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (6, N'010000000 010000001 010000003', 3, CAST(N'2023-06-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), N'haha')
INSERT [dbo].[DISCOUNT] ([Id], [ProductIDList], [Discount], [StartDate], [EndDate], [Name]) VALUES (7, N'050000000 050000001 050000002', 43, CAST(N'2023-06-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-09T00:00:00.0000000' AS DateTime2), N'234')
GO
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (0, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 1504410120.8000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (1, N'020230000', N'1', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 44432078.1000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (2, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 29621385.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (3, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (4, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 29621385.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (5, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 29621385.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (6, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 44432078.1000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (7, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (8, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (9, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (10, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 40979763.2000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (11, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (12, N'020230000', N'001', N'', N'', N'', N'', CAST(N'2023-04-19' AS Date), 29621385.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (13, N'020230000', N'43', N'', N'', N'', N'', CAST(N'2023-04-03' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (14, N'020230000', N'213', N'', N'', N'', N'', CAST(N'2023-04-03' AS Date), 14810692.7000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (15, N'220200000', N'001', N'', N'', N'', N'', CAST(N'2020-04-15' AS Date), 5679466702.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (16, N'220200000', N'002', N'', N'', N'', N'', CAST(N'2020-04-05' AS Date), 3385424510.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (17, N'220200000', N'003', N'', N'', N'', N'', CAST(N'2020-04-01' AS Date), 2494999981.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (18, N'220200000', N'004', N'', N'', N'', N'', CAST(N'2020-04-08' AS Date), 189879725.4000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (19, N'220200000', N'005', N'', N'', N'', N'', CAST(N'2022-06-14' AS Date), 4314989033.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (20, N'220200000', N'006', N'', N'', N'', N'', CAST(N'2020-02-04' AS Date), 1533022992.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (21, N'220200000', N'007', N'', N'', N'', N'', CAST(N'2020-03-28' AS Date), 713430462.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (22, N'220200000', N'008', N'', N'', N'', N'', CAST(N'2020-04-08' AS Date), 85800056.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (23, N'020230000', N'007', N'', N'', N'', N'', CAST(N'2020-01-01' AS Date), 1783576155.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (24, N'020230000', N'002', N'', N'', N'', N'', CAST(N'2023-04-03' AS Date), 92359780.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (25, N'020230000', N'002', N'', N'', N'', N'', CAST(N'2023-04-02' AS Date), 20000000.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (26, N'220200000', N'a', N'', N'', N'', N'', CAST(N'2020-01-05' AS Date), 0.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (27, N'220200000', N'abc', N'', N'', N'', N'', CAST(N'2020-01-06' AS Date), 40000000.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (28, N'220200000', N'a', N'', N'', N'', N'', CAST(N'2020-04-03' AS Date), 400000000.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (29, N'020230000', N'12312312', N'', N'', N'', N'', CAST(N'2023-05-02' AS Date), 10000000.0000, 1)
INSERT [dbo].[IMPORT] ([Id], [StaffID], [Provider_Bill_ID], [City], [District], [Sub_district], [Street], [ImportDate], [TotalAmount], [ProviderId]) VALUES (30, N'220200000', N'Gs234534', N'', N'', N'', N'', CAST(N'2023-05-18' AS Date), 5000000.0000, 3)
GO
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000000', 10, NULL, N'Cái       ', 14810692.7000, 148106927.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000001', 6, NULL, N'Cái       ', 20489881.6000, 122939289.6000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000003', 7, NULL, N'Cái       ', 12689935.5000, 88829548.5000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000004', 5, NULL, N'Cái       ', 17689898.5000, 88449492.5000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000005', 5, NULL, N'Cái       ', 58800000.0000, 294000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000006', 5, NULL, N'Cái       ', 60043000.0000, 300215000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000007', 5, NULL, N'Cái       ', 23089945.0000, 115449725.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000009', 5, NULL, N'Cái       ', 11190041.4000, 55950207.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000010', 5, NULL, N'Cái       ', 31990125.3000, 159950626.5000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000011', 5, NULL, N'Cái       ', 15489929.7000, 77449648.5000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (0, N'010000012', 3, NULL, N'Cái       ', 17689885.4000, 53069656.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (1, N'010000000', 3, NULL, N'Cái       ', 14810692.7000, 44432078.1000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (2, N'010000000', 2, NULL, N'Cái       ', 14810692.7000, 29621385.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (3, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (4, N'010000000', 2, NULL, N'Cái       ', 14810692.7000, 29621385.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (5, N'010000000', 2, NULL, N'Cái       ', 14810692.7000, 29621385.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (6, N'010000000', 3, NULL, N'Cái       ', 14810692.7000, 44432078.1000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (7, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (8, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (9, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (10, N'010000001', 2, NULL, N'Cái       ', 20489881.6000, 40979763.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (11, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (12, N'010000000', 2, NULL, N'Cái       ', 14810692.7000, 29621385.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (13, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (14, N'010000000', 1, NULL, N'Cái       ', 14810692.7000, 14810692.7000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000000', 20, NULL, N'Cái       ', 14810692.7000, 296213854.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000001', 20, NULL, N'Cái       ', 20489881.6000, 409797632.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000003', 20, NULL, N'Cái       ', 12689935.5000, 253798710.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000004', 20, NULL, N'Cái       ', 17689898.5000, 353797970.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000005', 20, NULL, N'Cái       ', 58800000.0000, 1176000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000006', 20, NULL, N'Cái       ', 60043000.0000, 1200860000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000007', 20, NULL, N'Cái       ', 23089945.0000, 461798900.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000009', 20, NULL, N'Cái       ', 11190041.4000, 223800828.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000010', 20, NULL, N'Cái       ', 31990125.3000, 639802506.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000011', 20, NULL, N'Cái       ', 15489929.7000, 309798594.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (15, N'010000012', 20, NULL, N'Cái       ', 17689885.4000, 353797708.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000000', 20, NULL, N'Bộ', 11989041.9000, 239780838.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000001', 20, NULL, N'Bộ', 8740979.8000, 174819596.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000002', 20, NULL, N'Bộ', 11546346.4000, 230926928.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000003', 20, NULL, N'Bộ', 14189969.7000, 283799394.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000004', 20, NULL, N'Bộ', 13950758.7000, 279015174.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000005', 20, NULL, N'Bộ', 19363975.2000, 387279504.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000006', 20, NULL, N'Bộ', 11490000.0000, 229800000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000007', 20, NULL, N'Bộ', 16678114.2000, 333562284.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000008', 20, NULL, N'Bộ', 15469073.9000, 309381478.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000009', 20, NULL, N'Bộ', 4800095.1000, 96001902.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000010', 20, NULL, N'Bộ', 19562777.7000, 391255554.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (16, N'020000011', 20, NULL, N'Bộ', 21490092.9000, 429801858.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000000', 20, NULL, N'Cái', 3389999.5700, 67799991.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000001', 20, NULL, N'Cái', 2689999.5000, 53799990.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000002', 20, NULL, N'Cái', 76590000.0000, 1531800000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000003', 20, NULL, N'Cái', 9690000.0000, 193800000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000004', 20, NULL, N'Cái', 20900000.0000, 418000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (17, N'030000005', 20, NULL, N'Cái', 11490000.0000, 229800000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000000', 20, NULL, N'cái', 478998.0000, 9579960.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000001', 20, NULL, N'cái', 1489999.5000, 29799990.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000002', 20, NULL, N'cái', 689000.3100, 13780006.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000003', 20, NULL, N'cái', 508996.9500, 10179939.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000004', 20, NULL, N'cái', 649001.9100, 12980038.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000005', 20, NULL, N'cái', 1389993.9000, 27799878.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000006', 20, NULL, N'cái', 949001.2000, 18980024.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000007', 20, NULL, N'cái', 1389995.1000, 27799902.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000008', 20, NULL, N'cái', 498996.3000, 9979926.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (18, N'040000009', 20, NULL, N'cái', 1450003.1000, 29000062.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000000', 20, NULL, N'Cái', 4889969.7700, 97799395.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000001', 20, NULL, N'Cái', 7889960.9100, 157799218.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000002', 20, NULL, N'Cái', 167990516.1000, 3359810322.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000003', 20, NULL, N'Cái', 16390075.0000, 327801500.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000004', 20, NULL, N'Cái', 12289948.2000, 245798964.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (19, N'050000005', 20, NULL, N'Cái', 6298981.6700, 125979633.4000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000000', 20, NULL, N'Cái', 4144630.2000, 82892604.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000001', 20, NULL, N'Cái', 2021041.2600, 40420825.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000003', 20, NULL, N'Cái', 6289969.0800, 125799381.6000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000004', 20, NULL, N'Cái', 4289972.7000, 85799454.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000005', 20, NULL, N'Cái', 29579163.9000, 591583278.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000006', 20, NULL, N'Cái', 4289978.1000, 85799562.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000007', 20, NULL, N'Cái', 8989992.0000, 179799840.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000008', 20, NULL, N'Cái', 7766438.1000, 155328762.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000009', 20, NULL, N'Cái', 5289960.2600, 105799205.2000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (20, N'060000010', 20, NULL, N'Cái', 3990004.0000, 79800080.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (21, N'070000000', 20, NULL, N'Cái', 10191500.0000, 203830000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (21, N'070000001', 20, NULL, N'Cái', 6489970.9000, 129799418.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (21, N'070000002', 20, NULL, N'Cái', 18990052.2000, 379801044.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (22, N'060000002', 20, NULL, N'Cái', 4290002.8000, 85800056.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (23, N'070000000', 50, NULL, N'Cái', 10191500.0000, 509575000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (23, N'070000001', 50, NULL, N'Cái', 6489970.9000, 324498545.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (23, N'070000002', 50, NULL, N'Cái', 18990052.2000, 949502610.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (24, N'010000007', 4, NULL, N'Cái       ', 23089945.0000, 92359780.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (25, N'020000000', 1, NULL, N'Bộ', 20000000.0000, 20000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (26, N'010000001', 5, NULL, N'Cái       ', 0.0000, 0.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (27, N'010000001', 2, NULL, N'Cái       ', 20000000.0000, 40000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (28, N'010000000', 20, NULL, N'Cái       ', 20000000.0000, 400000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (29, N'010000000', 1, NULL, N'Cái       ', 10000000.0000, 10000000.0000)
INSERT [dbo].[IMPORT_PRODUCT] ([ImportID], [ProductID], [Number], [Warranty], [Unit], [SellPrice], [Amount]) VALUES (30, N'030000003', 1, NULL, N'Cái', 5000000.0000, 5000000.0000)
GO
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000000', N'Laptop MSI Gaming GF63 Thin 11SC', N'Core i5', N'8GB', N'512GB', N'GTX 1650', 16990000.0000, 55, N'D:\ESMData\Laptop\010000000\Detail.xlsx', N'D:\ESMData\Laptop\010000000\Images', N'GF63 Thin', N'MSI', N'Gaming', N'D:\ESMData\Laptop\010000000\Images\unnamed (1).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000001', N'Laptop HP Victus 16-e1107AX (Ryzen 5 6600H/RAM 8GB/512GB SSD/ Windows 11)', N'Ryzen 5', N'8GB', N'512GB', N'RTX 3050', 25490000.0000, 10, N'D:\ESMData\Laptop\010000001\Detail.xlsx', N'D:\ESMData\Laptop\010000001\Images', N'VICTUS', N'HP', N'Văn phòng, Doanh nghiệp, Học sinh - Sinh viên, Gaming', N'D:\ESMData\Laptop\010000001\Images\unnamed (2).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000002', N'PC HP 200 Pro G4 AIO 74S22PA', N'Core i3', N'8GB', N'256GB ', N' Intel UHD Graphics', 14790000.0000, -1, N'D:\ESMData\PC\020000003\PC4.xlsx', N'D:\ESMData\PC\020000003\ảnh', N'Pro', N'Hp', N'Văn phòng', N'D:\ESMData\PC\020000003\ảnh\unnamed (6).webp', N'Bộ        ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000003', N'Laptop Lenovo IdeaPad 3 15IAU7 (i3-1215U/RAM 8GB/512GB SSD/ Windows 11)', N'Core i3', N'8GB', N'512GB', N'Onboard', 15290000.0000, 12, N'D:\ESMData\Laptop\010000002\Detail.xlsx', N'D:\ESMData\Laptop\010000002\Images', N'IdeaPad', N'Lenovo', N'Văn phòng', N'D:\ESMData\Laptop\010000002\Images\unnamed.webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000004', N'Laptop HP Pavilion 15-eg2035TX (i5-1235U/RAM 8GB/512GB SSD/ Windows 11)', N'Core i5', N'8GB', N'512GB', N'GeForce MX550', 20590000.0000, 0, N'D:\ESMData\Laptop\010000003\Detail.xlsx', N'D:\ESMData\Laptop\010000003\Images', N'Pavilion', N'HP', N'Văn phòng, Học sinh - Sinh viên', N'D:\ESMData\Laptop\010000003\Images\unnamed (3).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000005', N'Laptop Dell XPS 13 Plus 9320 (i7-1260P/RAM 16GB/512GB SSD/ Windows 11 + Office)', N'Core i7', N'16GB', N'512GB', N'Onboard', 60000000.0000, 2, N'D:\ESMData\Laptop\010000004\Detail.xlsx', N'D:\ESMData\Laptop\010000004\Images', N'XPS', N'DELL', N'Doanh nhân', N'D:\ESMData\Laptop\010000004\Images\unnamed (6).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000006', N'Laptop HP OMEN 16-b0123TX (i7-11800H/RAM 32GB/1TB SSD/ Windows 11)', N'Core i7', N'32GB', N'1TB', N'RTX 3070', 40000000.0000, 0, N'D:\ESMData\Laptop\010000005\Detail.xlsx', N'D:\ESMData\Laptop\010000005\Images', N'OMEN', N'HP', N'Văn phòng, Gaming', N'D:\ESMData\Laptop\010000005\Images\unnamed (7).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000007', N'Laptop ASUS Gaming ROG Strix G513IE-HN246W (Ryzen 7 4800H/RAM 8GB/512GB SSD/ Windows 11)', N'Ryzen 7', N'8GB', N'512GB', N'RTX 3050Ti', 26990000.0000, 23, N'D:\ESMData\Laptop\010000006\Detail.xlsx', N'D:\ESMData\Laptop\010000006\Images', N'ROG', N'ASUS', N'Gaming', N'D:\ESMData\Laptop\010000006\Images\unnamed (5).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000008', N'Laptop MSI Modern 14 C11M (i3-1115G4/RAM 8GB/512GB SSD/ Win 11)', N'Core i3', N'8GB', N'512GB ', N'Onboard ', 13990000.0000, -1, N'D:\ESMData\Laptop\010000007\Detail.xlsx', N'D:\ESMData\Laptop\010000007\Images', NULL, N'MSI', N'Văn phòng, Học sinh - Sinh viên', N'D:\ESMData\Laptop\010000007\Images\unnamed.webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000009', N'Laptop MSI Modern 14 C11M (i3-1115G4/RAM 8GB/512GB SSD/ Win 11)', N'Core i3', N'8GB', N'512GB', N'Onboard', 13990000.0000, 0, N'D:\ESMData\Laptop\010000007\Detail.xlsx', N'D:\ESMData\Laptop\010000007\Images', N'Modern Series', N'MSI', N'Văn phòng, Học sinh - Sinh viên', N'D:\ESMData\Laptop\010000007\Images\unnamed.webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000010', N'Laptop Lenovo Legion 5 15IAH7-82RC0036VN', N'Core i5', N'8GB ', N'512GB ', N'RTX 3050Ti', 38990000.0000, 1, N'D:\ESMData\Laptop\010000008\Detail.xlsx', N'D:\ESMData\Laptop\010000008', N'Legion', N'Lenovo', N'Gaming', N'D:\ESMData\Laptop\010000008\Images\unnamed.webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000011', N'Laptop ASUS Vivobook M513UA-EJ710W (Ryzen 7 5700U/RAM 16GB/512GB SSD/ Windows 11)', N'Ryzen 7', N'16GB ', N'512GB ', N'Onboard', 17990000.0000, 15, N'D:\ESMData\Laptop\010000009\Detail.xlsx', N'D:\ESMData\Laptop\010000009\Images', N'VivoBook', N'ASUS', N'Văn phòng', N'D:\ESMData\Laptop\010000009\Images\unnamed (7).webp', N'Cái       ')
INSERT [dbo].[LAPTOP] ([Id], [Name], [CPU], [RAM], [Storage], [Graphic], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'010000012', N'Laptop Dell Vostro 3510 (i5-1135G7/RAM 8GB/512GB SSD/ Windows 11 + Office)', N'Core i5', N'8GB', N'512GB ', N'Onboard', 22990000.0000, 12, N'D:\ESMData\Laptop\010000009\Detail.xlsx', N'D:\ESMData\Laptop\010000009\Images', N'Vostro ', N'DELL', N'Văn phòng', N'D:\ESMData\Laptop\010000009\Images\unnamed (7).webp', N'Cái       ')
GO
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000000', N'Màn hình LCD MSI Modern MD272QPW (2560 x 1440/IPS/75Hz/4 ms)', N'27"', N'IPS', N'75Hz', 4699000.0000, 12, N'D:\ESMData\Monitor\030000000\Detail.xlsx', N'D:\ESMData\Monitor\030000000\Images', N'', N'ACER', N'Gaming, Văn phòng', N'D:\ESMData\Monitor\030000000\Images\unnamed (1).webp', N'Cái')
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000001', N'Màn hình LCD MSI PRO MP242C (1920 x 1080/VA/75Hz/1 ms)', N'24"', N'VA', N'75Hz', 3090000.0000, 19, N'D:\ESMData\Monitor\030000001\Detail.xlsx', N'D:\ESMData\Monitor\030000001\Images', NULL, N'MSI', N'Văn phòng, Học sinh - Sinh viên', N'D:\ESMData\Monitor\030000001\Images\unnamed.webp', N'Cái')
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000002', N'Màn hình LCD LG 27EP950 ; 32EP950 (3840 x 2160/IPS/60Hz/1 ms)', N'31.5"', N'OLED', N'60Hz', 76590000.0000, 10, N'D:\ESMData\Monitor\030000002\Detail.xlsx', N'D:\ESMData\Monitor\030000002', NULL, N'LG', N'Đồ họa - Kỹ thuật', N'D:\ESMData\Monitor\030000002\Images\unnamed (3).webp', N'Cái')
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000003', N'Màn hình LCD MSI Modern MD272QPW (2560 x 1440/IPS/75Hz/4 ms)', N'27"', N'IPS', N'75Hz', 9690000.0000, 13, N'D:\ESMData\Monitor\030000003\Detail.xlsx', N'D:\ESMData\Monitor\030000003\Images', N'Modern ', N'MSI', N'Văn phòng, Đồ họa - Kỹ thuật', N'D:\ESMData\Monitor\030000003\Images\unnamed (6).webp', N'Cái')
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000004', N'Màn hình LCD LG Ergonomic 32UN880 (3840 x 2160/IPS/60Hz/5 ms/FreeSync)', N'31.5"', N'IPS', N'60Hz', 20900000.0000, 7, N'D:\ESMData\Monitor\030000004\Detail.xlsx', N'D:\ESMData\Monitor\030000004\Images', NULL, N'LG', N'Đồ họa - Kỹ thuật', N'D:\ESMData\Monitor\030000004\Images\unnamed (7).webp', N'Cái')
INSERT [dbo].[MONITOR] ([Id], [Name], [Size], [Panel], [RefreshRate], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'030000005', N'Màn hình LCD Dell 27 inch S2722DGM (2560 x 1440/ VA/ 165Hz/ 6ms)', N'27"', N'VA', N'165Hz', 11490000.0000, 18, N'D:\ESMData\Monitor\030000005\Detail.xlsx', N'D:\ESMData\Monitor\030000005\Images', NULL, N'DELL', NULL, N'D:\ESMData\Monitor\030000005\Images\unnamed (11).webp', N'Cái')
GO
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000000', N'PC Dell Vostro 3910 71000335', N'Core i3', N'8GB', 13290000.0000, 5, N'D:\ESMData\PC\020000000\PC1.xlsx', N'D:\ESMData\PC\020000000\Images', N'Vostro', N'DELL', N'Văn phòng, Đồ họa , Doanh nghiệp, Học sinh, Doanh nhân, Gia đình', N'D:\ESMData\PC\020000000\Images\pc-dong-bo-dell-vostro-3910mt-42vt390001-1-500x500.jpg', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000001', N'PC HP 280 Pro G5 SFF 60G67PA', N'Core i3', N'8GB', 9890000.0000, 0, N'D:\ESMData\PC\020000001\PC2.xlsx', N'D:\ESMData\PC\020000001\ảnh', N'Pro', N'HP', N'Văn phòng,  Doanh nghiệp, Học sinh - Sinh viên,  Gia đình', N'D:\ESMData\PC\020000001\ảnh\unnamed (1).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000002', N'PC HP Pavilion TP01-3009d 6K7A8PA', N'Core i5', N'4GB', 12890000.0000, 18, N'D:\ESMData\PC\020000002\PC0.xlsx', N'D:\ESMData\PC\020000002\ảnh', N'Pavilion', N'HP', N'Kỹ thuật, Doanh nghiệp, Học sinh ,  Gia đình', N'D:\ESMData\PC\020000002\ảnh\unnamed (1).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000003', N'PC HP 200 Pro G4 AIO 74S22PA', N'Core i3', N'8GB', 14790000.0000, 12, N'D:\ESMData\PC\020000003\PC4.xlsx', N'D:\ESMData\PC\020000003\ảnh', N'Pro', N'HP', N'Văn phòng, Kỹ thuật, Doanh nghiệp, Gia đình', N'D:\ESMData\PC\020000003\ảnh\unnamed (6).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000004', N'PC ASUS S501MD S501MD-512400079W', N'Core i5', N'8GB', 15490000.0000, 1, N'D:\ESMData\PC\020000004\PC5.xlsx', N'D:\ESMData\PC\020000004\ảnh', N'Workstation', N'ASUS', N'Văn phòng, Kỹ thuật, Doanh nghiệp,  Gia đình', N'D:\ESMData\PC\020000004\ảnh\unnamed (2).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000005', N'PC ASUS V241E V241EAK-BA128W', N'Core i5', N'8GB', 20390000.0000, 13, N'D:\ESMData\PC\020000007\PC8.xlsx', N'D:\ESMData\PC\020000007\ảnh', N'AIO', N'ASUS', N'Văn phòng, Doanh nghiệp, Gia đình', N'D:\ESMData\PC\020000007\ảnh\unnamed (8).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000006', N'PC Lenovo V530-15ICR', N'Core i5', N'4GB', 11490000.0000, 17, N'D:\ESMData\PC\020000005\PC6.xlsx', N'D:\ESMData\PC\020000005\ảnh', N'Workstation', N'Lenovo', N'Văn phòng,Sinh viên, Gia đình', N'D:\ESMData\PC\020000005\ảnh\unnamed.webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000007', N'Máy tính để bàn - PC Lenovo Ideacentre 520 22ICB F0DT0058VN', N'Core i3', N'4GB', 17490000.0000, 19, N'D:\ESMData\PC\020000006\PC7.xlsx', N'D:\ESMData\PC\020000006\ảnh', N'AIO', N'Lenovo', N'Văn phòng, Kỹ thuật, Học sinh, Gia đình', N'D:\ESMData\PC\020000006\ảnh\unnamed (5).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000008', N'Máy tính để bàn - PC Acer AS XC-885 DT.BAQSV.004', N'Core i7', N'4GB', 15890000.0000, 19, N'D:\ESMData\PC\020000008\PC9.xlsx', N'D:\ESMData\PC\020000008\ảnh', N'Workstation', N'Acer', N'Văn phòng', N'D:\ESMData\PC\020000008\ảnh\unnamed (7).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000009', N'Máy tính để bàn - PC Acer Aspire M230 UX.VQVSI.144', N'Core i3', N'4GB', 9990000.0000, 20, N'D:\ESMData\PC\020000009\PC10.xlsx', N'D:\ESMData\PC\020000009\ảnh', N'Workstation', N'Acer', N'Văn phòng, Gia đình', N'D:\ESMData\PC\020000009\ảnh\unnamed (2).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000010', N'Máy tính để bàn - PC HP Pavilion 590-p0079d 4LY18AA', N'Core i7', N'8GB', 20390000.0000, 7, N'D:\ESMData\PC\020000010\P11.xlsx', N'D:\ESMData\PC\020000010\ảnh', N'Pavilion', N'HP', N'Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Học sinh - Sinh viên, Doanh nhân, Gia đình', N'D:\ESMData\PC\020000010\ảnh\unnamed (6).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000011', N'PC Dell Vostro 3910 71000336', N'Core i7', N'8GB', 22590000.0000, 18, N'D:\ESMData\PC\020000011\PC12.xlsx', N'D:\ESMData\PC\020000011\ảnh', N'Vostro', N'DELL', N'Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Doanh nhân, Gia đình', N'D:\ESMData\PC\020000011\ảnh\unnamed (1).webp', N'Bộ')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000012', N'PcDell', N'Core i5', N'32GB', 12000000.0000, -1, N'D:\ESMData\PC\020000000\PC1.xlsx', N'D:\ESMData\PC\020000000\Images', N'ffsa', N'Dell', N'Hoc Sinh', N'D:\ESMData\PC\020000000\Images\pc-dong-bo-dell-vostro-3910mt-42vt390001-1-500x500.jpg', N'Cái')
INSERT [dbo].[PC] ([Id], [Name], [CPU], [RAM], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'020000013', N'LaptopDell', N'Core', N'64GB', 20000000.0000, -1, N'D:\ESMData\PC\020000000\PC1.xlsx', N'D:\ESMData\PC\020000000\Images', N'df', N'Dell', N'Hoc', N'D:\ESMData\PC\020000000\Images\pc-dong-bo-dell-vostro-3910mt-42vt390001-1-500x500.jpg', N'Cái')
GO
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000000', N'CPU INTEL Core i5-12400 (6C/12T, 2.50 GHz - 4.40 GHz, 18MB) - 1700', N'1700', 6439000.0000, 0, N'D:\ESMData\Cpu\050000000\Detail.xlsx', N'D:\ESMData\Cpu\050000000\Images', N'Core i5', N'INTEL', N'Gaming, Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Học sinh - Sinh viên', N'D:\ESMData\Cpu\050000000\Images\unnamed (6).webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000001', N'CPU INTEL Core i7-12700F (12C/20T, 4.90 GHz, 25MB) - 1700', N'1700', 10299000.0000, 17, N'D:\ESMData\Cpu\050000001\Detail.xlsx', N'D:\ESMData\Cpu\050000001\Images', N'Core i7', N'INTEL', N'Gaming, Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Học sinh - Sinh viên', N'D:\ESMData\Cpu\050000001\Images\unnamed (1).webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000002', N'CPU AMD Ryzen Threadripper pro 5995WX (64C/128T, 2.7 GHz - 4.5 GHz, 256MB) - sWRX8', N'sWRX8', 178090000.0000, 17, N'D:\ESMData\Cpu\050000002\Detail.xlsx', N'D:\ESMData\Cpu\050000002\Images', N'Ryzen Threadripper', N'AMD', N'Gaming, Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Học sinh - Sinh viên', N'D:\ESMData\Cpu\050000002\Images\unnamed (2).webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000003', N'CPU AMD Ryzen 9 7950X (16C/32T, 4.5GHz - 5.7GHz, 64MB) - AM5', N'AM5', 17390000.0000, 0, N'D:\ESMData\Cpu\050000003\Detail.xlsx', N'D:\ESMData\Cpu\050000003\Images', N'Ryzen 9', N'AMD', N'Gaming, Văn phòng, Đồ họa - Kỹ thuật, Doanh nghiệp, Học sinh - Sinh viên', N'D:\ESMData\Cpu\050000003\Images\unnamed (1).webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000004', N'CPU INTEL i9-10900X (10C/20T, 3.70 GHz - 4.50 GHz, 19.25MB) - 2066', N'2066', 18990000.0000, 0, N'D:\ESMData\Cpu\050000004\Detail.xlsx', N'D:\ESMData\Cpu\050000004\Images', N'Core i9', N'INTEL', NULL, N'D:\ESMData\Cpu\050000004\Images\unnamed.webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000005', N'CPU Intel Core i7-9700K (8C/8T, 3.6 GHz - 4.9 GHz, 12MB) - LGA 1151-v2', N'1151-v2', 10979000.0000, 0, N'D:\ESMData\Cpu\050000005\Detail.xlsx', N'D:\ESMData\Cpu\050000005\Images', N'Core i7', N'INTEL', NULL, N'D:\ESMData\Cpu\050000005\Images\unnamed (1).webp', N'Cái')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000006', N'1234', N'234', 4234.0000, -1, NULL, N'C:\Users\DELL\Desktop\102210038-TRƯƠNG CÔNG HOÀNG LONG', NULL, N'234', NULL, N'C:\Users\DELL\Desktop\102210038-TRƯƠNG CÔNG HOÀNG LONG\BK TECHSHOW.jpg', N'4')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000008', N'TTTTTTTTTTTTTTTTTTT', N'345', 345.0000, 0, NULL, NULL, NULL, N'345', NULL, NULL, N'345')
INSERT [dbo].[PCCPU] ([Id], [Name], [Socket], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Need], [Avatar_Path], [Unit]) VALUES (N'050000010', N'Hoa Da', N'12', 10.0000, -1, NULL, NULL, NULL, N'23', NULL, NULL, N'cai')
GO
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000000', N'Ổ cứng SSD WD Green 2.5" 240GB SATA III', N'240GB', N'SATA 3', 890000.0000, 3, N'D:\ESMData\HardDisk\040000000\Detail.xlsx', N'D:\ESMData\HardDisk\040000000\Images', N'Green', N'WD', N'SSD', N'D:\ESMData\HardDisk\040000000\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000001', N'ổ cứng HDD PC Seagate Barracuda 2TB 3.5" SATA', N'2TB', N'SATA 3', 1650000.0000, 19, N'D:\ESMData\HardDisk\040000001\Detail.xlsx', N'D:\ESMData\HardDisk\040000001\Images', N'BarraCuda', N'SEAGATE', N'HDD', N'D:\ESMData\HardDisk\040000001\Images\unnamed (3).webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000002', N'Ổ cứng gắn trong/ SSD Kingston NV2 250GB M.2 2280 PCIe Gen 4.0 NVMe', N'250GB', N'M.2 NVMe', 999000.0000, 17, N'D:\ESMData\HardDisk\040000002\Detail.xlsx', N'D:\ESMData\HardDisk\040000002\Images', NULL, N'KINGSTON', N'SSD', N'D:\ESMData\HardDisk\040000002\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000003', N'Ổ cứng SSD Kingston A400 240GB Sata 3', N'240GB', N'240GB', 889000.0000, 14, N'D:\ESMData\HardDisk\040000003\Detail.xlsx', N'D:\ESMData\HardDisk\040000003\Images', NULL, N'KINGSTON', N'SSD', N'D:\ESMData\HardDisk\040000003\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000004', N'Ổ cứng SSD Gigabyte 256GB M.2 2280 NVMe Gen3 x4 ', N'256GB', N'M.2 NVMe', 1259000.0000, 20, N'D:\ESMData\HardDisk\040000004\Detail.xlsx', N'D:\ESMData\HardDisk\040000004\Images', NULL, N'GIGABYTE', N'SSD', N'D:\ESMData\HardDisk\040000004\Images\unnamed (1).webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000005', N'Ổ cứng di động HDD Western Digital Elements Portable 1TB 2.5" USB 3.0', N'1TB', N'USB 3.0', 1590000.0000, 13, N'D:\ESMData\HardDisk\040000005\Images\unnamed.webp', N'D:\ESMData\HardDisk\040000005\Images', N'Element', N'WD', N'HDD', N'D:\ESMData\HardDisk\040000005\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000006', N'Ổ cứng HDD Western Digital Blue 1TB 3.5" SATA 3', N'1TB', N'SATA 3', 1190000.0000, 18, N'D:\ESMData\HardDisk\040000006\Detail.xlsx', N'D:\ESMData\HardDisk\040000006\Images', N'Blue', N'WD', N'HDD', N'D:\ESMData\HardDisk\040000006\Images\unnamed (5).webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000007', N'Ổ cứng SSD Samsung 870 EVO 500GB SATA III 2.5 inch', N'500GB', N'SATA 3', 1990000.0000, 13, N'D:\ESMData\HardDisk\040000007\Detail.xlsx', N'D:\ESMData\HardDisk\040000007\Images', N'Evo', N'SAMSUNG', N'SSD', N'D:\ESMData\HardDisk\040000007\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000008', N'Ổ cứng SSD WD Green 240GB M.2-2280 SATA III', N'240GB', N'M.2 SATA', 890000.0000, 3, N'D:\ESMData\HardDisk\040000008\Detail.xlsx', N'D:\ESMData\HardDisk\040000008\Images', N'Green', N'WD', N'SSD', N'D:\ESMData\HardDisk\040000008\Images\unnamed (4).webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000009', N'Ổ cứng di động HDD WD My Passport 1TB 2.5" USB 3.2', N'1TB', N'USB 3.2', 1690000.0000, 20, N'D:\ESMData\HardDisk\040000009\Detail.xlsx', N'D:\ESMData\HardDisk\040000009\Images', N'My Passport', N'WD', N'HDD', N'D:\ESMData\HardDisk\040000009\Images\unnamed.webp', N'cái')
INSERT [dbo].[PCHARDDISK] ([Id], [Name], [Storage], [Connect], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Type], [Avatar_Path], [Unit]) VALUES (N'040000010', N'1344134', N'12', N'13', 54352435.0000, -1, NULL, NULL, N'244526425', N'245', N'14', NULL, N'4')
GO
INSERT [dbo].[PROVIDER] ([Id], [ProviderName], [Phone], [website], [note]) VALUES (1, N'FPT', N'13423452345', NULL, NULL)
INSERT [dbo].[PROVIDER] ([Id], [ProviderName], [Phone], [website], [note]) VALUES (2, N'563', N'0', NULL, NULL)
INSERT [dbo].[PROVIDER] ([Id], [ProviderName], [Phone], [website], [note]) VALUES (3, N'Thế giới di động', N'1234567', NULL, NULL)
INSERT [dbo].[PROVIDER] ([Id], [ProviderName], [Phone], [website], [note]) VALUES (4, N'Điện máy xanh', N'1234345234', NULL, NULL)
GO
INSERT [dbo].[SMARTPHONE] ([Id], [Name], [CPU], [RAM], [Storage], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'070000000', N'Điện thoại Samsung Galaxy A73 5G', N'Snapdragon 778G 5G', N'8GB', N'128GB', 11990000.0000, 43, N'D:\ESMData\Smart Phone\070000000\Detail.xlsx', N'D:\ESMData\Smart Phone\070000000\Images', N'Galaxy A', N'Samsung', N'D:\ESMData\Smart Phone\070000000\Images\unnamed (8).webp', N'Cái')
INSERT [dbo].[SMARTPHONE] ([Id], [Name], [CPU], [RAM], [Storage], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'070000001', N'Xiaomi Redmi Note 11 Pro 5G', N'MediaTek Helio G96', N'8GB', N'128GB', 8990000.0000, 42, N'D:\ESMData\Smart Phone\070000001\Detail.xlsx', N'D:\ESMData\Smart Phone\070000001\Images', N'Redmi', N'Xiaomi', N'D:\ESMData\Smart Phone\070000001\Images\unnamed (5).webp', N'Cái')
INSERT [dbo].[SMARTPHONE] ([Id], [Name], [CPU], [RAM], [Storage], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'070000002', N'Điện thoại di động Samsung Galaxy S21 Ultra 5G (12+128GB) SM-G998BZSDXXV (Silver)', N'Mediatek', N'12GB', N'128GB', 30990000.0000, 36, N'D:\ESMData\Smart Phone\070000002\Detail.xlsx', N'D:\ESMData\Smart Phone\070000002\Images', N'Galaxy S', N'Samsung', N'D:\ESMData\Smart Phone\070000002\Images\unnamed (8).webp', N'Cái')
GO
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000000', N'Card màn hình ASUS TUF Gaming GeForce GTX 1660 SUPER OC Edition 6GB GDDR6 TUF-GTX1660S-O6G-GAMING', N'GeForce GTX 1660 Super', N'NVIDIA', N'6GB', N'GDDR6', 5190000.0000, 18, N'D:\ESMData\VGA\060000000\Detail.xlsx', N'D:\ESMData\VGA\060000000\Images', N'TUF', N'ASUS', N'D:\ESMData\VGA\060000000\Images\unnamed.webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000001', N'Card màn hình GIGABYTE GeForce GT 1030 2GB GDDR5 OC (GV-N1030OC-2GI)', N'GT 1030 OC 2G', N'NVIDIA', N'2GB', N'GDDR5', 2499000.0000, 13, N'D:\ESMData\VGA\060000001\Detail.xlsx', N'D:\ESMData\VGA\060000001\Images', NULL, N'GIGABYTE', N'D:\ESMData\VGA\060000001\Images\unnamed (6).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000002', N'Card màn hình MSI GeForce GTX 1650 D6 VENTUS XS OCV1 4GB GDDR6 GeForce-GTX-1650-D6-VENTUS-XS-OCV1', N'GeForce GTX 1650', N'NVIDIA', N'4GB', N'GDDR6', 4990000.0000, 20, N'D:\ESMData\VGA\060000002\Detail.xlsx', N'D:\ESMData\VGA\060000002\Images', N'VENTUS', N'MSI', N'D:\ESMData\VGA\060000002\Images\unnamed (3).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000003', N'Card màn hình GIGABYTE GeForce RTX 2060 D6 6G 6GB GDDR6 GV-N2060D6-6GD', N'GeForce RTX 2060', N'NVIDIA', N'6GB', N'GDDR6', 10209000.0000, 5, N'D:\ESMData\VGA\060000003\Detail.xlsx', N'D:\ESMData\VGA\060000003\Images', NULL, N'GIGABYTE', N'D:\ESMData\VGA\060000003\Images\unnamed (5).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000004', N'Card màn hình ASUS TUF Gaming GeForce GTX 1650 OC Edition 4GB 4GB GDDR6', N'GeForce GTX 1650', N'NVIDIA', N'4GB', N'GDDR6', 6990000.0000, 20, N'D:\ESMData\VGA\060000004\Detail.xlsx', N'D:\ESMData\VGA\060000004\Images', N'TUF', N'ASUS', N'D:\ESMData\VGA\060000004\Images\unnamed (4).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000005', N'Card màn hình ASUS ROG STRIX Radeon RX 6800 16GB GDDR6 ROG-STRIX-RX6800-O16G-GAMING', N'Radeon RX 6800', N'AMD', N'16GB', N'GDDR6', 32990000.0000, 4, N'D:\ESMData\VGA\060000005\Detail.xlsx', N'D:\ESMData\VGA\060000005\Images', N'ROG', N'ASUS', N'D:\ESMData\VGA\060000005\Images\unnamed (5).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000006', N'Card màn hình Colorful GeForce GTX 1650 NB 4GD6-V 4GB GDDR6', N'GeForce GTX 1650', N'NVIDIA', N'4GB', N'GDDR6', 5990000.0000, 20, N'D:\ESMData\VGA\060000006\Detail.xlsx', N'D:\ESMData\VGA\060000006\Images', NULL, N'Colorful', N'D:\ESMData\VGA\060000006\Images\unnamed (7).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000007', N'Card màn hình Colorful GeForce RTX 3060 NB DUO 12G V2 L-V 12GB GDDR6 6970417596073', N'GeForce RTX 3060', N'NVIDIA', N'12GB', N'GDDR6', 9900000.0000, 17, N'D:\ESMData\VGA\060000007\Detail.xlsx', N'D:\ESMData\VGA\060000007\Images', NULL, N'Colorful', N'D:\ESMData\VGA\060000007\Images\unnamed (3).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000008', N'Card màn hình MSI GeForce RTX™ 3060 VENTUS 2X 12G OC 12GB GDDR6 GeForce-RTX-3060-VENTUS-2X-12G-OC', N'GeForce RTX 3060', N'NVIDIA', N'12GB', N'GDDR6', 9690000.0000, 20, N'D:\ESMData\VGA\060000008\Detail.xlsx', N'D:\ESMData\VGA\060000008\Images', N'VENTUS', N'MSI', N'D:\ESMData\VGA\060000008\Images\unnamed (6).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000009', N'Card màn hình GIGABYTE GeForce GTX 1660 SUPER™ OC 6G 6GB GDDR6 GV-N166SOC-6GD', N'GeForce GTX 1660 Super', N'NVIDIA', N'6GB', N'GDDR6', 10309000.0000, 12, N'D:\ESMData\VGA\060000009\Detail.xlsx', N'D:\ESMData\VGA\060000009\Images', NULL, N'GIGABYTE', N'D:\ESMData\VGA\060000009\Images\unnamed (4).webp', N'Cái')
INSERT [dbo].[VGA] ([Id], [Name], [Chip], [Chipset], [VRAM], [Gen], [Price], [Remain], [Detail_Path], [Image_Path], [Series], [Company], [Avatar_Path], [Unit]) VALUES (N'060000010', N'Card màn hình ASROCK Arc A380 Challenger ITX 6GB GDDR6 90-GA3KZZ-00UANF', N'Arc A', N'INTEL', N'6GB', N'GDDR6', 4990000.0000, 20, N'D:\ESMData\VGA\060000010\Detail.xlsx', N'D:\ESMData\VGA\060000010\Images', N'Arc A', N'ASROCK', N'D:\ESMData\VGA\060000010\Images\unnamed (2).webp', N'Cái')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ACCOUNT_Id]    Script Date: 02/06/2023 9:17:00 SA ******/
CREATE NONCLUSTERED INDEX [IX_ACCOUNT_Id] ON [dbo].[ACCOUNT]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT (getutcdate()) FOR [PurchasedTime]
GO
ALTER TABLE [dbo].[BILL_COMBO] ADD  DEFAULT (getutcdate()) FOR [PurchasedTime]
GO
ALTER TABLE [dbo].[BILL_COMBO] ADD  DEFAULT ((1)) FOR [Number]
GO
ALTER TABLE [dbo].[COMBO] ADD  CONSTRAINT [DF_Combo_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD  CONSTRAINT [FK_BILL_ACCOUNT] FOREIGN KEY([StaffID])
REFERENCES [dbo].[ACCOUNT] ([Id])
GO
ALTER TABLE [dbo].[BILL] CHECK CONSTRAINT [FK_BILL_ACCOUNT]
GO
ALTER TABLE [dbo].[BILL_COMBO]  WITH CHECK ADD  CONSTRAINT [FK_BILL_COMBO_ACCOUNT] FOREIGN KEY([StaffID])
REFERENCES [dbo].[ACCOUNT] ([Id])
GO
ALTER TABLE [dbo].[BILL_COMBO] CHECK CONSTRAINT [FK_BILL_COMBO_ACCOUNT]
GO
ALTER TABLE [dbo].[BILL_COMBO]  WITH CHECK ADD  CONSTRAINT [FK_BILL_COMBO_COMBO] FOREIGN KEY([ComboID])
REFERENCES [dbo].[COMBO] ([Id])
GO
ALTER TABLE [dbo].[BILL_COMBO] CHECK CONSTRAINT [FK_BILL_COMBO_COMBO]
GO
ALTER TABLE [dbo].[BILL_PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_BILL_PRODUCT_BILL] FOREIGN KEY([BillID])
REFERENCES [dbo].[BILL] ([Id])
GO
ALTER TABLE [dbo].[BILL_PRODUCT] CHECK CONSTRAINT [FK_BILL_PRODUCT_BILL]
GO
ALTER TABLE [dbo].[IMPORT]  WITH CHECK ADD  CONSTRAINT [FK_IMPORT_ACCOUNT] FOREIGN KEY([StaffID])
REFERENCES [dbo].[ACCOUNT] ([Id])
GO
ALTER TABLE [dbo].[IMPORT] CHECK CONSTRAINT [FK_IMPORT_ACCOUNT]
GO
ALTER TABLE [dbo].[IMPORT]  WITH CHECK ADD  CONSTRAINT [FK_IMPORT_PROVIDER] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[PROVIDER] ([Id])
GO
ALTER TABLE [dbo].[IMPORT] CHECK CONSTRAINT [FK_IMPORT_PROVIDER]
GO
ALTER TABLE [dbo].[IMPORT_PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_IMPORT_PRODUCT_IMPORT] FOREIGN KEY([ImportID])
REFERENCES [dbo].[IMPORT] ([Id])
GO
ALTER TABLE [dbo].[IMPORT_PRODUCT] CHECK CONSTRAINT [FK_IMPORT_PRODUCT_IMPORT]
GO
USE [master]
GO
ALTER DATABASE [ESMDatabase] SET  READ_WRITE 
GO
