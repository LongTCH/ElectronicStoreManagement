CREATE TABLE [dbo].[PCHARDDISK]
(
	[Id] CHAR(9) NOT NULL,
    [Name] NVARCHAR(300) NOT NULL, 
    [Storage] VARCHAR(50) NOT NULL, 
    [Connect] VARCHAR(20) NOT NULL,  
    [Price] MONEY NOT NULL, 
    [Remain] INT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Type] VARCHAR(20), 
    [Avatar_Path] NVARCHAR(200) NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_PCHARDDISK] PRIMARY KEY ([Id])
)
