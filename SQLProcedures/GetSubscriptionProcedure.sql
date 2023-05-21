CREATE PROCEDURE GetSubscription
    @customerId INT
AS
BEGIN
    SELECT * FROM Subscriptions WHERE CustomerId = @customerId;
END;