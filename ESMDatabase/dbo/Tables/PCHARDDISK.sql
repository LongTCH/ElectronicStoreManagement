CREATE TABLE [dbo].[PCHARDDISK]
(
	[Id] CHAR(9) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Storage] VARCHAR(50) NOT NULL, 
    [Connect] VARCHAR(20) NOT NULL,  
    [Price] MONEY NOT NULL, 
    [Discount] MONEY NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Type] VARCHAR(20), 
    CONSTRAINT [PK_PCHARDDISK] PRIMARY KEY ([Id])
)
