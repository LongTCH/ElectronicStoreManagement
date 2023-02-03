CREATE TABLE [dbo].[ACCOUNT]
(
    [Id] INT NOT NULL IDENTITY(-2147483648, 1),
	[Username] nvarchar(128) NOT NULL , 
    [PasswordHash] NVARCHAR(128) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Sex] NVARCHAR(10) NULL , 
    [EmailAddress] NVARCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [City] NVARCHAR(50) NULL, 
    [District] NVARCHAR(50) NULL, 
    [Sub-district] NVARCHAR(50) NULL, 
    [Street] NVARCHAR(100) NULL, 
    [Phone] NVARCHAR(30) NULL, 
    [Role] NVARCHAR(50) NOT NULL DEFAULT 'Customer', 
    [Avatar_Path] NVARCHAR(200) NULL,  
    CONSTRAINT [PK_ACCOUNT] PRIMARY KEY ([Id]) 
    
)
