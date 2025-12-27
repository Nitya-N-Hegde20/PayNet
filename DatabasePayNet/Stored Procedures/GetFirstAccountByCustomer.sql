CREATE OR ALTER PROCEDURE GetFirstAccountByCustomer
(
    @CustomerId INT
)
AS
BEGIN
    SELECT TOP 1
        Id,
        AccountNumber
    FROM Accounts
    WHERE CustomerId = @CustomerId
      AND IsActive = 1
    ORDER BY Id;
END
