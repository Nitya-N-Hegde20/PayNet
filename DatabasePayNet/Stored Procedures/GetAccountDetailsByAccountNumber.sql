CREATE OR ALTER PROCEDURE GetAccountDetailsByAccountNumber
(
    @AccountNumber NVARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        AccountNumber,
        BankName,
        IFSC,
        Balance,
        CustomerId,
        IsActive
    FROM Accounts
    WHERE AccountNumber = @AccountNumber;
END
GO
