CREATE TABLE CustomerPackages(
CustomerID int NOT NULL,
PackageID int NOT NULL,
NumberOfTask int NOT NULL,
PRIMARY KEY(CustomerID, PackageID),
FOREIGN KEY (CustomerID)REFERENCES Customers(ID),
FOREIGN KEY (PackageID) REFERENCES Packages(ID),
FOREIGN KEY (NumberOfTask)REFERENCES Packages(ID)
)