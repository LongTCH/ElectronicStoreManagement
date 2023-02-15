CREATE TABLE [dbo].[PC]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(100) NOT NULL, 
    [CPU] NVARCHAR(50) NOT NULL, 
    [RAM] VARCHAR(50) NULL, 
    [Storage] VARCHAR(50) NULL, 
    [Graphic] VARCHAR(50) NULL, 
    [Price] MONEY NOT NULL, 
    [Discount] MONEY NULL, 
    [Remain] SMALLINT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(50)
)
