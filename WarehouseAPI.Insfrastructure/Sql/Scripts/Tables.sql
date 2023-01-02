﻿CREATE TABLE Product(
  Id INT IDENTITY(1, 1) NOT NULL,
  ProductName VARCHAR(100) NOT NULL,
  Description VARCHAR(200) NULL,
  Stock INT NOT NULL,
  Price MONEY NOT NULL

  CONSTRAINT PK_Product PRIMARY KEY(Id)
);

GO

CREATE TABLE Sale(
  Id INT IDENTITY(1, 1) NOT NULL,
  ProductId INT NOT NULL,
  Qty INT NOT NULL,
  Subtotal MONEY NOT NULL,
  Iva MONEY NOT NULL,
  Total MONEY NOT NULL,
  DateTime DATETIME NOT NULL

  CONSTRAINT PK_Sale PRIMARY KEY(Id)
  CONSTRAINT FK_Sale_Product FOREIGN KEY(ProductId)
    REFERENCES Product
);