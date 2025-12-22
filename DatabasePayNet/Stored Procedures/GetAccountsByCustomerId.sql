CREATE OR ALTER PROCEDURE GetAccountsByCustomerId
(
    @CustomerId INT
)
AS
BEGIN
    SELECT 
        Id,
        AccountNumber,
        BankName,
        IFSC,
        Balance
    FROM Accounts
    WHERE CustomerId = @CustomerId
      AND IsActive = 1;
END
