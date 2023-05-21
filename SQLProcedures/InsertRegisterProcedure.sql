CREATE PROCEDURE InsertRegister
    @CustomerId INT,
    @Plaka VARCHAR(50),
    @EntryDate DATETIME
AS
BEGIN
    INSERT INTO IORegistrations (CustomerId, Plaka, EntryDate)
    VALUES (@CustomerId, @Plaka, @EntryDate);
END;