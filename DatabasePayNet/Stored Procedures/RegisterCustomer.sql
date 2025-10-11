
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE RegisterCustomer
    @FullName NVARCHAR(50),
    @Address NVARCHAR(max),
    @Email NVARCHAR(50),
    @Phone NVARCHAR(20),
    @PasswordHash NVARCHAR(255),
    @IsActive BIT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Customer WHERE Email = @Email)
    BEGIN
        SELECT -1 AS Result; -- Email already exists
        RETURN;
    END
    INSERT INTO Customer (FullName, Address, Email, Phone, Password, IsActive)
    VALUES (@FullName, @Address, @Email, @Phone, @PasswordHash, @IsActive);

    SELECT SCOPE_IDENTITY() AS Result;
END
GO
