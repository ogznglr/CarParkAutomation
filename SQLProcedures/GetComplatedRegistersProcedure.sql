CREATE PROCEDURE dbo.GetCompletedRegisters
AS
BEGIN
    SELECT * FROM IORegistrations WHERE OutDate IS NOT NULL;
END;