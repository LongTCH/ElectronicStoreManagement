CREATE TABLE [dbo].[VGA]
(
	[Id] CHAR(9) NOT NULL ,
    [Name] NVARCHAR(100) NOT NULL, 
    [Chip] VARCHAR(50) NOT NULL, 
    [Chipset] VARCHAR(10) NOT NULL, 
    [VRAM] VARCHAR(50) NOT NULL, 
    [Gen] VARCHAR(20) NOT NULL,  
    [Price] MONEY NOT NULL, 
    [Discount] FLOAT NULL, 
    [Remain] INT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Avatar_Path] NVARCHAR(200) NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_VGA] PRIMARY KEY ([Id])
)
