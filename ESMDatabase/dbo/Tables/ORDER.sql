CREATE TABLE [dbo].[ORDER]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(-2147483648, 1), 
    [StaffID] CHAR(9) NOT NULL,
    [CustomerName] NVARCHAR(50) NULL, 
    [Phone nvarchar(30)] NCHAR(10) NULL, 
    [City] NVARCHAR(50) NULL, 
    [District] NVARCHAR(50) NULL, 
    [Sub_district] NVARCHAR(50) NULL, 
    [Street] NVARCHAR(100) NULL, 
    [PurchasedTime] DATETIME2(3) NOT NULL DEFAULT getutcdate()
)
