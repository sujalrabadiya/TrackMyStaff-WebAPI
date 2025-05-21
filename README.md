# 📍 TrackMyStaff – Web API

**TrackMyStaff-WebAPI** is a backend solution built using **ASP.NET Core Web API**, designed for learning and practicing enterprise-grade development with real-world concepts like **role management**, **FluentValidation**, **SQL Server integration**, **stored procedures**, and **triggers**.

This project simulates a staff tracking system where businesses can manage their employees, track their locations, and enable communication through messaging endpoints – all powered by clean API architecture.

> ⚠️ **Note**: This project is not production-ready and was built as a personal learning project.

---

## 🧠 Core Objectives

* Practice and implement key **ASP.NET Core Web API** concepts
* Understand real-world **API architecture**
* Explore **FluentValidation** and **SQL Server integration**
* Create and use **Stored Procedures** and **Triggers**
* Implement clean coding practices and modular organization

---

## 🧰 Tech Stack

| Category        | Technology                             |
| --------------- | -------------------------------------- |
| Framework       | ASP.NET Core Web API                   |
| Language        | C#                                     |
| Database        | SQL Server                             |
| ORM             | ADO.NET (manual SQL execution)         |
| Validation      | FluentValidation                       |
| DB Operations   | Stored Procedures & Triggers           |
| Tools           | Visual Studio, SSMS                    |

---

## 📁 Project Structure

```
TrackMyStaffWebAPI/
│
├── Controllers/              // API endpoints for authentication, staff, location, etc.
├── Data/                     // Database connection & repository implementations
├── Models/                   // DTOs and entity models
├── Validators/               // FluentValidation classes
├── SPs & Triggers/           // SQL stored procedures and trigger scripts
├── Properties/               // Launch settings and project metadata
├── Program.cs                // Application startup
├── appsettings.json          // Configuration and DB connection string
├── appsettings.Development.json
├── TrackMyStaffWebApplication.csproj
├── TrackMyStaffWebApplication.http
└── TrackMyStaffWebApplication.sln
```

---

## 🔐 Roles
  * `Admin` (Business Owner)
  * `Supervisor`
  * `Staff`

---

## 📦 Key Features

* ✅ **Authentication & Authorization**
* ✅ Role-based API access control
* ✅ **FluentValidation** for request validation
* ✅ **Stored Procedures** and **Triggers** used for all DB operations
* ✅ Organized folder structure with separation of concerns
* ✅ Robust **error handling** and response messaging
* ✅ RESTful API design for maintainability
* ✅ Ready for frontend integration (e.g., Flutter or React)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/sujalrabadiya/TrackMyStaff-WebAPI.git
```

### 2. Configure the Database

* Open `appsettings.json`
* Set your SQL Server connection string:

  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TrackMyStaffDB;Trusted_Connection=True;"
  }
  ```

### 3. Set Up Database

* Execute all stored procedures and triggers in the `SPs & Triggers` folder using **SQL Server Management Studio**.

### 4. Run the API

* Open the solution in **Visual Studio**
* Set `TrackMyStaffWebApplication` as the startup project
* Run the API (`F5` or `Ctrl+F5`)

---
#### You can checkout [TrackMyStaff-Flutter](https://github.com/sujalrabadiya/TrackMyStaff-Flutter) for frontend.

## 👨‍💻 Author

**Sujal Rabadiya**
.NET Backend Developer & App Enthusiast
[LinkedIn](https://linkedin.com/in/sujal-rabadiya) · [GitHub](https://github.com/sujalrabadiya)

