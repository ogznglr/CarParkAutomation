CREATE PROCEDURE InsertTariff
    @FirstPrice DECIMAL(10, 2),
    @SecondPrice DECIMAL(10, 2),
    @ThirthPrice DECIMAL(10, 2),
    @DefaultPrice DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Tariff (FirstPrice, SecondPrice, ThirthPrice, DefaultPrice)
    VALUES (@FirstPrice, @SecondPrice, @ThirthPrice, @DefaultPrice);
END;