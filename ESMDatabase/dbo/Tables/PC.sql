CREATE TABLE [dbo].[PC]
(
	[Id] CHAR(9) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [CPU] NVARCHAR(50) NOT NULL, 
    [RAM] VARCHAR(50) NULL, 
    [Price] MONEY NOT NULL, 
    [Discount] FLOAT NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(50), 
    [Avatar_Path] NVARCHAR(200) NULL, 
    CONSTRAINT [PK_PC] PRIMARY KEY ([Id])
)
