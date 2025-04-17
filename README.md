# 🚗 Vehicle Rental System 🚀

## 📌 Overview
The Vehicle Rental System is a web application built using **ASP.NET Core MVC with Razor Pages** that allows users to rent vehicles efficiently. It follows the **Code-First approach** with Entity Framework Core and implements the **Repository Pattern** along with a **Service Layer** for better separation of concerns.

## 🔹 Features
- ✅ **User Authentication & Authorization** (JWT-based)
- ✅ **Vehicle Management** (Add, Update, Delete, List)
- ✅ **Rental Booking System** (Ensures a vehicle can only be booked by one user at a time)
- ✅ **Booking Validation** (Prevents double booking, throws an exception)
- ✅ **Global Exception Handling Middleware**
- ✅ **Search & Filtering for Vehicles**
- ✅ **Admin Dashboard for Vehicle Management**

## 🛠️ Tech Stack
- **Frontend:** Razor Pages (ASP.NET Core MVC)
- **Backend:** ASP.NET Core MVC
- **Database:** SQL Server (EF Core – Code-First Approach)
- **ORM:** Entity Framework Core
- **Architecture:** Repository Pattern + Service Layer
- **Authentication:** JWT Authentication
- **Exception Handling:** Global Exception Handling Middleware

## 🚀 How to Run Locally

### 1️⃣ Clone the repository
```sh
git clone https://github.com/prasadm11/VehicleRentalSystem.git
cd VehicleRentalSystem
```

### 2️⃣ Configure the `appsettings.json` file for database connection

### 3️⃣ Apply migrations and update database
```sh
dotnet ef database update
```

### 4️⃣ Run the application
```sh
dotnet run
```

### 5️⃣ Open in browser
```
https://localhost:5001/
```

## 📜 License
This project is open-source under the **MIT License**.
