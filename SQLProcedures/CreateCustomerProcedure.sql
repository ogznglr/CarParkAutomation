CREATE PROCEDURE [dbo].[CreateCustomer]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @PhoneNumber NVARCHAR(50),
	@Province NVARCHAR(50),
	@District NVARCHAR(50),
    @RegistrationDate DATETIME
AS
BEGIN
    INSERT INTO [dbo].[Customers] (FirstName, LastName, PhoneNumber, Province, District, RegistrationDate)
    VALUES (@FirstName, @LastName, @PhoneNumber, @Province, @District, @RegistrationDate);
END


CREATE PROCEDURE [dbo].[CreateCustomer]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @PhoneNumber NVARCHAR(50),
	@Province NVARCHAR(50),
	@District NVARCHAR(50),
    @RegistrationDate DATETIME
AS
BEGIN
    IF @FirstName IS NULL OR @LastName IS NULL OR @PhoneNumber IS NULL OR @Province IS NULL OR @District IS NULL OR @RegistrationDate IS NULL
    BEGIN
        RAISERROR('One or more input parameters are null. All input parameters must have non-null values.', 16, 1);
        RETURN;
    END
    
    INSERT INTO [dbo].[Customers] (FirstName, LastName, PhoneNumber, Province, District, RegistrationDate)
    VALUES (@FirstName, @LastName, @PhoneNumber, @Province, @District, @RegistrationDate);
END