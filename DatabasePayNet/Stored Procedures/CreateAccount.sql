CREATE or ALTER PROCEDURE CreateAccount
    @CustomerId INT,
    @BankName NVARCHAR(100),
    @BankCode NVARCHAR(20),
    @BranchName NVARCHAR(100),
    @IFSC NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NextId INT;
    SELECT @NextId = ISNULL(MAX(Id), 0) + 1 FROM Accounts;

    DECLARE @AccountNumber NVARCHAR(50);
    SET @AccountNumber = 'PN-' + @BankCode + '-' + FORMAT(GETDATE(), 'yyyy') + '-' + FORMAT(@NextId, '000000');

    INSERT INTO Accounts (CustomerId, BankName, BankCode, BranchName, IFSC, AccountNumber)
    VALUES (@CustomerId, @BankName, @BankCode, @BranchName, @IFSC, @AccountNumber);

    SELECT @AccountNumber AS AccountNumber;
END
