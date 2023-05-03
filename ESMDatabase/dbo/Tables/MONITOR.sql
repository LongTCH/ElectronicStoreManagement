CREATE TABLE [dbo].[MONITOR]
(
	[Id] CHAR(9) NOT NULL, 
    [Name] NVARCHAR(300) NOT NULL, 
    [Size] VARCHAR(50) NOT NULL, 
    [Panel] VARCHAR(50) NOT NULL, 
    [RefreshRate] NVARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Remain] INT NOT NULL, 
    [Detail_Path] NVARCHAR(200) NULL, 
    [Image_Path] NVARCHAR(200) NULL, 
    [Series] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Need] NVARCHAR(200), 
    [Avatar_Path] NVARCHAR(200) NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_MONITOR] PRIMARY KEY ([Id])
)
