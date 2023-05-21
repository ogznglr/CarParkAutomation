CREATE PROCEDURE dbo.MigrateDatabase
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[Customers] (
            [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
            [FirstName] NVARCHAR(50) NOT NULL,
            [LastName] NVARCHAR(50) NOT NULL,
            [PhoneNumber] NVARCHAR(50) NOT NULL UNIQUE,
			[Province] NVARCHAR(50) NOT NULL,
			[District] NVARCHAR(50) NOT NULL,
            [RegistrationDate] DATETIME NOT NULL,
			[Subscribed] BIT NOT NULL DEFAULT 0
        )
    END

    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IORegistrations]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[IORegistrations] (
            [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
            [CustomerId] INT NOT NULL,
			[Plaka] NVARCHAR(50) NOT NULL,
            [EntryDate] DATETIME NOT NULL,
            [OutDate] DATETIME,
            [Time] INT,
            [Price] INT,
            CONSTRAINT FK_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
        )
    END

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subscriptions]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[Subscriptions] (
            [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
            [CustomerId] INT NOT NULL UNIQUE,
            [RegisterDate] DATETIME NOT NULL,
            [FinishDate] DATETIME NOT NULL,
            [Price] INT NOT NULL,
            CONSTRAINT FK_CustomerId_Subscription FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
        )
    END

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tariff]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[Tariff] (
            [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
            [FirstPrice] INT NOT NULL,
            [SecondPrice] INT NOT NULL,
			[ThirthPrice] INT NOT NULL,
			[DefaultPrice] INT NOT NULL,
        )
    END
END