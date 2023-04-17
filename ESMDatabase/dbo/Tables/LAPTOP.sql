CREATE TABLE [dbo].[LAPTOP]
(
	[Id] CHAR(9) NOT NULL, 
    [Name] NVARCHAR(200) NOT NULL, 
    [CPU] NVARCHAR(50) NOT NULL, 
    [RAM] VARCHAR(50) NOT NULL, 
    [Storage] VARCHAR(50) NOT NULL, 
    [Graphic] VARCHAR(50) NOT NULL,  
    [Price] MONEY NOT NULL, 
    [Discount] FLOAT NULL, 
    [Remain] INT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(100), 
    [Avatar_Path] NVARCHAR(200) NULL, 
    [Unit] NCHAR(10) NOT NULL, 
    CONSTRAINT [PK_LAPTOP] PRIMARY KEY ([Id])
)
