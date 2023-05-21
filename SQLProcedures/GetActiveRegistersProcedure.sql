CREATE PROCEDURE GetActiveRegister
    @Plaka VARCHAR(50)
AS
BEGIN
    SELECT * FROM IORegistrations WHERE Plaka = @Plaka AND OutDate IS NULL
END;