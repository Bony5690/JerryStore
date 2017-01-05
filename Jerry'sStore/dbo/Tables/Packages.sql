CREATE TABLE [dbo].[Packages] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Price]     MONEY          NOT NULL,
    [Assistant] NVARCHAR (100) NOT NULL,
    [Task]      INT            NOT NULL,
    CONSTRAINT PK_Packages PRIMARY KEY CLUSTERED ([ID] ASC)
);

