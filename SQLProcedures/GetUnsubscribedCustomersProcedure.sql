CREATE PROCEDURE dbo.GetUnsubscribedCustomers
AS
BEGIN
    SELECT * FROM Customers WHERE Subscribed = 0;
END;