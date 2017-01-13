CREATE TABLE [dbo].[Customers] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NULL,
    [LastName]  NVARCHAR (100) NULL,
    [EmailAddress] NCHAR(100) NULL, 
    CONSTRAINT PK_Customers PRIMARY KEY CLUSTERED ([ID] ASC)
);

