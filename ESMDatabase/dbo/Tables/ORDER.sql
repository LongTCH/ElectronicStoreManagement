CREATE TABLE [dbo].[ORDER]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(-2147483648, 1), 
    [CustomerName] NVARCHAR(50) NOT NULL, 
    [Phone nvarchar(30)] NCHAR(10) NOT NULL, 
    [ReceivedCity] NVARCHAR(50) NOT NULL, 
    [ReceivedDistrict] NVARCHAR(50) NOT NULL, 
    [ReceivedSub_district] NVARCHAR(50) NOT NULL, 
    [ReceivedDate] DATETIME2 NOT NULL, 
    [Cash] MONEY NOT NULL, 
    [Status] BIT NOT NULL DEFAULT 0
)
