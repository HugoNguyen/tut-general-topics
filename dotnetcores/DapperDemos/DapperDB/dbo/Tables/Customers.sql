CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [DateOfBirth] DATETIME NULL, 
    [Email] VARCHAR(50) NULL
)
