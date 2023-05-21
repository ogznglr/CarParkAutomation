CREATE PROCEDURE dbo.GetCustomerById
    @customerId INT
AS
BEGIN
    SELECT * FROM Customers WHERE Id = @customerId;
END;