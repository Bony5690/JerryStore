CREATE TABLE [dbo].[CustomerPackages] (
    [CustomerID]   INT NOT NULL,
    [PackageID]    INT NOT NULL,
    [PurchaseDate] DATETIME NULL, 
    CONSTRAINT PK_CustomerPackages PRIMARY KEY CLUSTERED ([CustomerID] ASC, [PackageID] ASC),
    CONSTRAINT FK_CustomerPackages_Customers FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]),
    CONSTRAINT FK_CustomerPackages_Packages FOREIGN KEY ([PackageID]) REFERENCES [dbo].[Packages] ([ID]), 
    
);

