CREATE TABLE Transactions (
    Id INT IDENTITY PRIMARY KEY,
    FromAccountId  NVARCHAR(50),
    ToAccountId  NVARCHAR(50),
    Amount DECIMAL(18,2),
    TransactionType NVARCHAR(50), 
    Status NVARCHAR(20),          
    CreatedAt DATETIME DEFAULT GETDATE()
);
