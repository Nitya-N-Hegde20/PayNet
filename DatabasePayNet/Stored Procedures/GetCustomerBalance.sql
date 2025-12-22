CREATE OR ALTER PROCEDURE GetCustomerBalance
(
    @CustomerId INT
)
AS
BEGIN
    SELECT ISNULL(Balance, 0) AS Balance
    FROM Accounts 
    WHERE CustomerId = @CustomerId
END
