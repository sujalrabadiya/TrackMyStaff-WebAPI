---1. Stored Procedures---

--1.1 Authentication and Role Management
--Business Registration
--Login
--List All Business
--Select Business By id
--Update Business By id
--Delete Business By id
--Add Staff Using Company Id
--Login

--1.2 Location Tracking
--Add Location Update
--Get Live Locations By Company Id-------
--DROP-- Get Live Locations By Staff Id-------
--DROP-- Get Location History By Staff Id-------

--1.3 Messaging
--Send Message
--Get Conversation History
--Get Conversation List

--1.4 User Management
--List All Users
--List Users By Role
--Select User By id
--Update User By id
--Delete User By id
--Soft Delete User (for later)

--1.5 Notifications
--Send Notification
--Get Notifications
--Archive Removed Notifications.

---2. Scheduled Tasks---
--2.1 Archive Old Location Data
--Archive location data older than a specific date to maintain performance.

---1. Stored Procedures---

--1.1 Authentication and Role Management
--Business Registration
CREATE PROCEDURE [dbo].[PR_Business_Insert]
    @CName VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
	@UName VARCHAR(100),
    @Password VARCHAR(255),
    @ImgUrl NVARCHAR(50) = 'https://tableconvert.com/images/avatar.png'
AS
BEGIN
    INSERT INTO Companies (name, email, phone )
    VALUES (@CName, @Email, @Phone);
	DECLARE @CompanyId INT = (Select id FROM Companies WHERE email = @Email);
	INSERT INTO Users (name, email, phone, password, role, img_url, company_id)
    VALUES (@UName, @Email, @Phone, @Password, 'Admin', @ImgUrl, @CompanyId);
END;

--Login
CREATE PROCEDURE [dbo].[PR_User_Login]
    @Phone VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
    SELECT 
        u.id, u.name, u.email, u.img_url, u.role, u.company_id, c.name AS company_name
    FROM Users u
    INNER JOIN Companies c ON u.company_id = c.id
    WHERE u.phone = @Phone AND u.password = @Password;
END;

--List All Business
CREATE PROCEDURE [dbo].[PR_Business_SelectAll]
AS
BEGIN
    SELECT 
        id, name, email, phone, created_at, updated_at
    FROM Companies
    ORDER BY name;
END;

--Select Business By ID
CREATE PROCEDURE [dbo].[PR_Business_SelectByPK]
    @ID INT
AS
BEGIN
    SELECT 
        id, name, email, phone, created_at, updated_at
    FROM Companies
    WHERE id = @ID;
END;

--Update Business By ID
CREATE PROCEDURE [dbo].[PR_Business_UpdateByPK]
    @ID INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15)
AS
BEGIN
    UPDATE Companies
    SET 
        name = @Name,
        email = @Email,
        phone = @Phone
    WHERE id = @ID;
END;

--Delete Business By ID
CREATE PROCEDURE [dbo].[PR_Business_DeleteByPK]
    @ID INT
AS
BEGIN
    DELETE FROM Companies
    WHERE id = @ID;
END;

--Add Staff Using Company Id
CREATE PROCEDURE [dbo].[PR_Staff_AddByCompanyId] 
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Password VARCHAR(255),
    @Role NVARCHAR(50) = 'Staff',
	@ImgUrl VARCHAR(MAX) = 'https://tableconvert.com/images/avatar.png',
    @CompanyId INT
AS
BEGIN
    IF @CompanyId IS NOT NULL
    BEGIN
        INSERT INTO Users (name, email, phone, password, role, img_url, company_id)
        VALUES (@Name, @Email, @Phone, @Password, @Role, @ImgUrl, @CompanyId);
    END
    ELSE
    BEGIN
        THROW 50001, 'Invalid Company Id.', 1;
    END
END;
Select * from Messages

--1.2 Location Tracking
--Add Location Update
CREATE PROCEDURE [dbo].[PR_Location_AddUpdate]
    @UserId INT,
    @Latitude DECIMAL(10, 8),
    @Longitude DECIMAL(11, 8)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Locations WHERE user_id = @UserId)
    BEGIN
        UPDATE Locations
        SET latitude = @Latitude, longitude = @Longitude, timestamp = GETDATE()
        WHERE user_id = @UserId;
    END
    ELSE
    BEGIN
        INSERT INTO Locations (user_id, latitude, longitude)
        VALUES (@UserId, @Latitude, @Longitude);
    END
END;

--Get Live Locations By Staff ID
CREATE PROCEDURE [dbo].[PR_Location_GetLiveByStaff]
    @UserId INT
AS
BEGIN
    SELECT TOP 1
        u.name, l.latitude, l.longitude, l.timestamp
    FROM Locations l
    INNER JOIN Users u ON l.user_id = u.id
    WHERE u.id = @UserId AND u.role = 'Staff'
    ORDER BY l.timestamp DESC;
END;

--Get Live Locations By Company ID
CREATE PROCEDURE [dbo].[PR_Location_GetLiveByCompany] 
    @CompanyId INT
AS
BEGIN
    SELECT 
        u.name, l.latitude, l.longitude, l.timestamp
    FROM Locations l
    INNER JOIN Users u ON l.user_id = u.id
    WHERE u.company_id = @CompanyId AND u.role = 'Staff'
    ORDER BY l.timestamp DESC;
END;

--Get Location History By Staff ID
DROP PROCEDURE [dbo].[PR_Location_GetHistoryByStaff]
    @UserId INT,
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SELECT 
        latitude, longitude, timestamp
    FROM Locations
    WHERE user_id = @UserId AND timestamp BETWEEN @StartDate AND @EndDate;
END;

--1.3 Messaging
--Send Message
CREATE PROCEDURE [dbo].[PR_Message_Send]
    @SenderId INT,
    @ReceiverId INT,
    @Message TEXT = NULL,
    @ImageURL VARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO Messages (sender_id, receiver_id, message, image_url)
    VALUES (@SenderId, @ReceiverId, @Message, @ImageURL);
END;

--Get Conversation History
CREATE PROCEDURE [dbo].[PR_Message_GetConversation]
    @UserId1 INT,
    @UserId2 INT
AS
BEGIN
    SELECT 
        sender_id, receiver_id, image_url, message, sent_at
    FROM Messages
    WHERE 
        (sender_id = @UserId1 AND receiver_id = @UserId2)
        OR (sender_id = @UserId2 AND receiver_id = @UserId1)
    ORDER BY sent_at;
END;

--Delete Conversation History
CREATE PROCEDURE [dbo].[PR_Message_ClearChat]
    @UserId1 INT,
    @UserId2 INT
AS
BEGIN
    DELETE FROM Messages
    WHERE 
        (sender_id = @UserId1 AND receiver_id = @UserId2)
        OR (sender_id = @UserId2 AND receiver_id = @UserId1);
END;

--Get Conversation User List
CREATE PROCEDURE [dbo].[PR_Message_GetConversationUsers] 
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get the company of the current user
    DECLARE @CompanyId INT;
    SELECT @CompanyId = company_id FROM Users WHERE id = @UserId;

    -- Get the latest message for each user who has conversed with @UserId
    WITH LastMessages AS (
        SELECT 
            CASE 
                WHEN m.sender_id = @UserId THEN m.receiver_id
                ELSE m.sender_id
            END AS other_user_id,
            m.message,
			m.image_url,
            m.sent_at,
            ROW_NUMBER() OVER (PARTITION BY 
                CASE 
                    WHEN m.sender_id = @UserId THEN m.receiver_id
                    ELSE m.sender_id
                END 
                ORDER BY m.sent_at DESC) AS rn
        FROM Messages m
        WHERE m.sender_id = @UserId OR m.receiver_id = @UserId
    )

    -- Select all users from the same company with their latest message (if any)
    SELECT 
        u.id AS other_user_id,
        u.name AS other_user_name,
        u.phone AS other_user_phone,
        u.img_url AS other_user_img_url,
        lm.message,
		lm.image_url,
        lm.sent_at
    FROM Users u
    LEFT JOIN LastMessages lm 
        ON u.id = lm.other_user_id AND lm.rn = 1 -- Only latest message
    WHERE u.company_id = @CompanyId 
        AND u.id <> @UserId  -- Exclude the current user
    ORDER BY ISNULL(lm.sent_at, '1900-01-01') DESC; -- Sort by latest message, nulls last
END;
Select * from Users
--1.4 User Management
--List All Users
CREATE PROCEDURE [dbo].[PR_User_SelectAll]
    @CompanyId INT
AS
BEGIN
    SELECT 
        id, name, email, phone, img_url, role, created_at, updated_at
    FROM Users
    WHERE company_id = @CompanyId
    ORDER BY name;
END;

--List Users By Role
CREATE PROCEDURE [dbo].[PR_User_SelectByRole]
    @CompanyId INT,
    @Role NVARCHAR(50)
AS
BEGIN
    SELECT 
        id, name, email, phone, role, created_at, updated_at
    FROM Users
    WHERE company_id = @CompanyId AND role = @Role
    ORDER BY name;
END;

--Select User By ID
CREATE PROCEDURE [dbo].[PR_User_SelectByPK]
    @UserId INT
AS
BEGIN
    SELECT 
        id, name, email, phone, role, created_at, updated_at
    FROM Users
    WHERE id = @UserId;
END;

--Update User By ID
CREATE PROCEDURE [dbo].[PR_User_UpdateByPK]
    @UserId INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Role NVARCHAR(50)
AS
BEGIN
    UPDATE Users
    SET 
        name = @Name,
        email = @Email,
        phone = @Phone,
        role = @Role
    WHERE id = @UserId;
END;

--Delete User By ID
CREATE PROCEDURE [dbo].[PR_User_DeleteByPK]
    @UserId INT
AS
BEGIN
    DELETE FROM Users
    WHERE id = @UserId;
END;

--Soft Delete User
CREATE PROCEDURE [dbo].[PR_User_SoftDelete]
    @UserId INT
AS
BEGIN
    UPDATE Users
    SET is_active = 0
    WHERE id = @UserId;
END;

--1.5 Notifications
--Send Notification
CREATE PROCEDURE [dbo].[PR_Notification_Send]
    @UserId INT,
    @Title VARCHAR(100),
    @Body TEXT = NULL
AS
BEGIN
    INSERT INTO Notifications (user_id, title, body)
    VALUES (@UserId, @Title, @Body);
END;

--Get Notifications
CREATE PROCEDURE [dbo].[PR_Notification_Get]
    @UserId INT
AS
BEGIN
    SELECT 
        title, body, created_at
    FROM Notifications
    WHERE user_id = @UserId
    ORDER BY created_at DESC;
END;

--Archive Removed Notifications
CREATE PROCEDURE [dbo].[PR_Notification_Archive]
    @ID INT
AS
BEGIN
    INSERT INTO NotificationsArchive (user_id, title, body, created_at)
    SELECT 
        user_id, title, body, created_at
    FROM Notifications
    WHERE id = @ID;

    DELETE FROM Notifications
    WHERE id = @ID;
END;

---2. Scheduled Tasks---

--Archive Old Location Data
CREATE PROCEDURE [dbo].[PR_Location_ArchiveOld]
AS
BEGIN
    INSERT INTO LocationsArchive (user_id, latitude, longitude, timestamp)
    SELECT 
        user_id, latitude, longitude, timestamp
    FROM Locations
    WHERE timestamp < DATEADD(DAY, -30, GETDATE());

    DELETE FROM Locations
    WHERE timestamp < DATEADD(DAY, -30, GETDATE());
END;



---------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------

