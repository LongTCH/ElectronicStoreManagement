﻿CREATE TABLE [dbo].[DISCOUNT]
(
	[Id] INT NOT NULL, 
    [ProductIDList] NVARCHAR(200) NULL, 
    [Discount] FLOAT NULL, 
    [StartDate] DATETIME2(3) NULL, 
    [EndDate] DATETIME2(3) NULL, 
    [Name] NVARCHAR(200) NULL, 
    CONSTRAINT [PK_DISCOUNT] PRIMARY KEY ([Id]) 
)
