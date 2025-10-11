CREATE OR ALTER PROCEDURE dbo.CreateAccount
    @CustomerId INT,
    @InitialBalance DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AccountNumber NVARCHAR(20);
    -- Simple logic: ACC + current timestamp to make unique number
    SET @AccountNumber = 'ACC' + RIGHT(CONVERT(VARCHAR(20), GETDATE(), 112) + 
                         CONVERT(VARCHAR(10), DATEPART(SECOND, GETDATE())), 8) +
                         CAST(@CustomerId AS NVARCHAR(10));

    INSERT INTO Accounts (CustomerId, AccountNumber, Balance)
    VALUES (@CustomerId, @AccountNumber, @InitialBalance);

    SELECT * FROM Accounts WHERE AccountNumber = @AccountNumber;
END
GO
