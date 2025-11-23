CREATE or Alter PROCEDURE UpdateCustomer
(
    @Id INT,
    @FullName NVARCHAR(200),
    @Address NVARCHAR(200),
    @Phone NVARCHAR(15),
    @Email NVARCHAR(200)
)
AS
BEGIN
    UPDATE Customer
    SET FullName = @FullName,
        Address  = @Address,
        Phone    = @Phone
    WHERE Id = @Id;  

    SELECT * FROM Customer WHERE Id = @Id;
END
