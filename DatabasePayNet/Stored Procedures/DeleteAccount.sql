CREATE OR ALTER PROCEDURE DeleteAccount
    @AccountNumber NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Accounts WHERE AccountNumber = @AccountNumber)
    BEGIN
        DELETE FROM Accounts WHERE AccountNumber = @AccountNumber;
        SELECT CAST(1 AS BIT) AS Success;
    END
    ELSE
    BEGIN
        SELECT CAST(0 AS BIT) AS Success; 
    END
END
