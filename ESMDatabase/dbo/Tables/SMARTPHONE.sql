﻿CREATE TABLE [dbo].[SMARTPHONE]
(
	[Id] INT NOT NULL ,
    [Name] NVARCHAR(100) NOT NULL, 
    [CPU] NVARCHAR(50) NOT NULL, 
    [RAM] VARCHAR(50) NOT NULL, 
    [Storage] VARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Discount] MONEY NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_SMARTPHONE] PRIMARY KEY ([Id]), 
)
