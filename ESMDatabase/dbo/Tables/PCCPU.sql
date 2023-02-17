CREATE TABLE [dbo].[PCCPU]
(
	[Id] CHAR(9) NOT NULL ,
    [Name] NVARCHAR(100) NOT NULL, 
    [Socket] VARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Discount] FLOAT NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(50), 
    CONSTRAINT [PK_PCCPU] PRIMARY KEY ([Id])
)
