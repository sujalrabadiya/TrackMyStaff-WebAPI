--1. Users Table
CREATE TABLE Users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    email VARCHAR(100),
    password VARCHAR(255) NOT NULL,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(15) UNIQUE NOT NULL,
	img_url VARCHAR(MAX) NULL DEFAULT 'https://tableconvert.com/images/avatar.png',
    role NVARCHAR(50) NOT NULL CHECK (role IN ('Admin', 'Supervisor', 'Staff')),
    company_id INT FOREIGN KEY REFERENCES Companies(id) ON DELETE CASCADE,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);


--2. Companies Table
CREATE TABLE Companies (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone VARCHAR(15) UNIQUE NOT NULL,
    company_code NVARCHAR(6) UNIQUE NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);


--3. Locations Table
CREATE TABLE Locations (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(id)  ON DELETE CASCADE,
    latitude DECIMAL(10, 8) NOT NULL,
    longitude DECIMAL(11, 8) NOT NULL,
    timestamp DATETIME DEFAULT GETDATE()
);


--4. Messages Table
CREATE TABLE Messages (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sender_id INT,
    receiver_id INT,
    message TEXT NULL,
    image_url VARCHAR(255) NULL,
    sent_at DATETIME DEFAULT GETDATE()
);


--5. Notifications Table
CREATE TABLE Notifications (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(id)  ON DELETE CASCADE,
    title VARCHAR(100) NOT NULL,
    body TEXT NULL,
    created_at DATETIME DEFAULT GETDATE()
);


--6. LocationsArchive Table
CREATE TABLE LocationsArchive (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(id)  ON DELETE CASCADE,
    latitude DECIMAL(10, 8) NOT NULL,
    longitude DECIMAL(11, 8) NOT NULL,
    timestamp DATETIME DEFAULT GETDATE()
);


--7. NotificationsArchive Table
CREATE TABLE NotificationsArchive (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES Users(id)  ON DELETE CASCADE,
    title VARCHAR(100) NOT NULL,
    body TEXT NULL,
    created_at DATETIME DEFAULT GETDATE()
);