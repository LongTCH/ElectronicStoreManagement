﻿CREATE TABLE [dbo].[MONITOR]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Size] VARCHAR(50) NOT NULL, 
    [Panel] VARCHAR(50) NOT NULL, 
    [RefreshRate] INT NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Discount] MONEY NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(50), 
    CONSTRAINT [PK_MONITOR] PRIMARY KEY ([Id])
)
