CREATE TABLE [dbo].[ACCOUNT]
(
    [Id] CHAR(9) NOT NULL,
    [PasswordHash] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Sex] BIT NOT NULL , 
    [EmailAddress] NVARCHAR(256) NOT NULL, 
    [Birthday] DATE NOT NULL , 
    [City] NVARCHAR(50) NOT NULL, 
    [District] NVARCHAR(50) NOT NULL, 
    [Sub-district] NVARCHAR(50) NOT NULL, 
    [Street] NVARCHAR(100) NOT NULL, 
    [Phone] NVARCHAR(30) NOT NULL, 
    [Avatar_Path] NVARCHAR(200) NULL, 
    CONSTRAINT [PK_ACCOUNT] PRIMARY KEY ([Id])   
)

GO


CREATE INDEX [IX_ACCOUNT_Id] ON [dbo].[ACCOUNT] ([Id])
