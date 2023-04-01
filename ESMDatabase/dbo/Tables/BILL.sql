CREATE TABLE [dbo].[BILL]
(
	[Id] INT NOT NULL,
    [StaffID] CHAR(9) NOT NULL,
    [CustomerName] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(30) NULL, 
    [City] NVARCHAR(50) NULL, 
    [District] NVARCHAR(50) NULL, 
    [Sub_district] NVARCHAR(50) NULL, 
    [Street] NVARCHAR(100) NULL, 
    [PurchasedTime] DATETIME2(3) NOT NULL DEFAULT getutcdate(), 
    [TotalAmount] MONEY NOT NULL, 
    CONSTRAINT [PK_BILL] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_BILL_ACCOUNT] FOREIGN KEY ([StaffID]) REFERENCES [ACCOUNT]([Id]) 
)
