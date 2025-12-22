CREATE OR ALTER PROCEDURE SendMoney
(
    @FromAccountId NVARCHAR(50),
    @ToAccountId NVARCHAR(50),
    @Amount DECIMAL(18,2)
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    -- Check balance
    IF (SELECT Balance FROM Accounts WHERE AccountNumber = @FromAccountId) < @Amount
    BEGIN
        ROLLBACK;
        THROW 50001, 'Insufficient balance', 1;
    END

    -- Debit sender
    UPDATE Accounts
    SET Balance = Balance - @Amount
    WHERE AccountNumber = @FromAccountId;

    -- Credit receiver
    UPDATE Accounts
    SET Balance = Balance + @Amount
    WHERE AccountNumber = @ToAccountId;

    -- Record transaction
    INSERT INTO Transactions (FromAccountId, ToAccountId, Amount, TransactionType, Status)
    VALUES (@FromAccountId, @ToAccountId, @Amount, 'SEND', 'SUCCESS');

    COMMIT;

    SELECT 'SUCCESS' AS Result;
END;
