--Trigger for Updating updated_at
--1. Users Table
CREATE TRIGGER trg_UpdateUsers
ON Users
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET updated_at = GETDATE()
	FROM Inserted
    WHERE Users.id = Inserted.id
END;


--2. Companies Table
CREATE TRIGGER trg_UpdateCompanies
ON Companies
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Companies
    SET updated_at = GETDATE()
    FROM Inserted
    WHERE Companies.id = Inserted.id
END;


--Trigger for Inserting created_at
--1. Users Table
CREATE TRIGGER trg_InsertUsers
ON Users
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET created_at = GETDATE()
    FROM Inserted
    WHERE Users.id = Inserted.id
END;


--2. Companies Table
CREATE TRIGGER trg_InsertCompanies
ON Companies
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Companies
    SET created_at = GETDATE()
    FROM Inserted
    WHERE Companies.id = Inserted.id
END;


--3. Locations Table
CREATE TRIGGER trg_InsertLocations
ON Locations
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Locations
    SET timestamp = GETDATE()
    FROM Inserted
    WHERE Locations.id = Inserted.id
END;


--4. Messages Table
CREATE TRIGGER trg_InsertMessages
ON Messages
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Messages
    SET sent_at = GETDATE()
    FROM Inserted
    WHERE Messages.id = Inserted.id
END;


--5. Notifications Table
CREATE TRIGGER trg_InsertNotifications
ON Notifications
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Notifications
    SET created_at = GETDATE()
	FROM Inserted
    WHERE Notifications.id = Inserted.id
END;
