CREATE OR ALTER PROCEDURE GetPayNetContacts
(
    @CustomerId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        c.Id       AS CustomerId,
        c.FullName AS Name,
        c.Phone
    FROM Customer c
    INNER JOIN Accounts a ON a.CustomerId = c.Id
    WHERE c.IsActive = 1
      AND a.IsActive = 1
      AND c.Id <> @CustomerId;
END
