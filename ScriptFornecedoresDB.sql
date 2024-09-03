CREATE DATABASE FornecedoresDB;
GO

USE FornecedoresDB;
GO

CREATE TABLE Fornecedores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);