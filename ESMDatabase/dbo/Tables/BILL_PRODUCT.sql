﻿CREATE TABLE [dbo].[BILL_PRODUCT]
(
    [BillID] INT NOT NULL, 
    [ProductID] CHAR(9) NOT NULL, 
    [Number] INT NOT NULL, 
    [Warranty]VARCHAR(50) NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    [SellPrice] MONEY NOT NULL, 
    [Amount] MONEY NOT NULL, 
    CONSTRAINT [FK_BILL_PRODUCT_BILL] FOREIGN KEY ([BillID]) REFERENCES [BILL]([Id]), 
    CONSTRAINT [PK_BILL_PRODUCT] PRIMARY KEY ([BillID], [ProductID]) 
)