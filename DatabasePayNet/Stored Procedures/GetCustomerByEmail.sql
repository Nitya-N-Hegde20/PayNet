

CREATE  OR ALTER PROCEDURE GetCustomerByEmail
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM Customer WHERE Email = @Email AND IsActive = 1;
END


