CREATE PROCEDURE UpdateRegister
    @Id INT,
    @OutDate DATETIME,
    @Price DECIMAL(10, 2),
    @Time INT
AS
BEGIN
    UPDATE IORegistrations SET OutDate = @OutDate, Price = @Price, Time = @Time WHERE Id = @Id;
END;