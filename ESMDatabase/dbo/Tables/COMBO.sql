﻿CREATE TABLE [dbo].[COMBO]
(
	[Id] CHAR(9) NOT NULL, 
    [Discount] FLOAT NOT NULL, 
    [ProductIDList] NVARCHAR(200) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_COMBO] PRIMARY KEY ([Id])
)
