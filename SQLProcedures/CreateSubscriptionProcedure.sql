CREATE PROCEDURE CreateSubscription
    @customerIdParam INT,
    @registerDateParam DATETIME,
    @finishDateParam DATETIME,
    @priceParam INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    DECLARE @customerId INT = @customerIdParam, 
            @registerDate DATETIME = @registerDateParam, 
            @finishDate DATETIME = @finishDateParam, 
            @price INT = @priceParam;

    INSERT INTO Subscriptions (CustomerId, RegisterDate, FinishDate, Price) 
    VALUES (@customerId, @registerDate, @finishDate, @price);

    UPDATE Customers SET subscribed = 1 WHERE Id = @customerId;

    COMMIT;
END;