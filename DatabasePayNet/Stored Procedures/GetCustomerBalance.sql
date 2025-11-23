CREATE OR ALTER PROCEDURE GetCustomerBalance
(
    @CustomerId INT
)
AS
BEGIN
    SELECT SUM(Balance) AS Balance
    FROM Accounts
    WHERE CustomerId = @CustomerId;
END
