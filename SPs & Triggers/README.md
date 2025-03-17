# **TrackMyStaff Web API - Database Scripts**
This directory contains the **SQL scripts** used in the **TrackMyStaff Web API** for table creation, stored procedures, and triggers.

## **Files**
- 📜 **TableCreation.sql** - Contains SQL scripts to create necessary tables.
- 📜 **StoredProcedures.sql** - Includes stored procedures for data handling and business logic.
- 📜 **Triggers.sql** - Defines database triggers to enforce constraints and automate tasks.

## **How to Use**
### **1️⃣ Running the Table Creation Script**
Before running any other script, ensure that all required tables are created:
```sql
-- Open SQL Server Management Studio (SSMS)
-- Select the target database
-- Execute the TableCreation.sql script
```

### **2️⃣ Running the Stored Procedures**
To execute stored procedures:
```sql
EXEC ProcedureName @Parameter1 = Value, @Parameter2 = Value;
```
Replace `ProcedureName` with the actual stored procedure name and adjust parameters as needed.

### **3️⃣ Running the Triggers**
Triggers are executed automatically when certain actions (INSERT, UPDATE, DELETE) occur on the associated tables.

## **Best Practices**
✔️ Always back up the database before applying schema changes.  
✔️ Review stored procedures and triggers to understand their effects before executing them.  
✔️ Ensure correct user permissions when running SQL scripts.
