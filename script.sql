USE [master]
GO
/****** Object:  Database [ESMDatabase]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[BILL]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[BILL_COMBO]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[BILL_PRODUCT]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[COMBO]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[DISCOUNT]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[IMPORT]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[IMPORT_PRODUCT]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[LAPTOP]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[MONITOR]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[PC]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[PCCPU]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[PCHARDDISK]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[PROVIDER]    Script Date: 19/05/2023 3:29:39 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVIDER](
	[Id] [int] NOT NULL,
	[ProviderName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_PROVIDER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMARTPHONE]    Script Date: 19/05/2023 3:29:39 CH ******/
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
/****** Object:  Table [dbo].[VGA]    Script Date: 19/05/2023 3:29:39 CH ******/
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
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ACCOUNT_Id]    Script Date: 19/05/2023 3:29:39 CH ******/
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
