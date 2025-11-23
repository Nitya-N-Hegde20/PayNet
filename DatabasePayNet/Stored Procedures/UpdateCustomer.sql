CREATE or ALter PROCEDURE UpdateCustomer
(
    @Id INT,
    @FullName NVARCHAR(100),
    @Address NVARCHAR(250),
    @Phone NVARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Update customer
    UPDATE Customer
    SET 
        FullName = @FullName,
        Address = @Address,
        Phone = @Phone
    WHERE Id = @Id;

    -- Return updated record
    SELECT 
        Id,
        FullName,
        Email,
        Phone,
        Address
    FROM Customer
    WHERE Id = @Id;
END
