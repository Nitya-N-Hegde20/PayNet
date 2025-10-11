

CREATE  OR ALTER PROCEDURE DeleteAccount
    @AccountNumber NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    Delete from Account where AccountNumber = @AccountNumber;
END


