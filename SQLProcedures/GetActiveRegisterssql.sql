CREATE PROCEDURE GetActiveRegisters
AS
BEGIN
    SELECT * FROM IORegistrations WHERE OutDate IS NULL;
END;