# 🛠️ **InventorySystem API**

---

## 📋 **Table of Contents**
- [📖 Overview](#-overview)
- [✨ Key Features](#-key-features)
  - [1️⃣ Authentication and Authorization](#1️⃣-authentication-and-authorization)
  - [2️⃣ Role and Permission Management](#2️⃣-role-and-permission-management)
  - [3️⃣ Data Mapping](#3️⃣-data-mapping)
  - [4️⃣ Transaction Management](#4️⃣-transaction-management)
  - [5️⃣ Custom Validation](#5️⃣-custom-validation)
- [🧩 Design Patterns](#-design-patterns)
  - [📦 Dependency Injection](#-dependency-injection)
  - [🔗 Unit of Work Pattern](#-unit-of-work-pattern)
  - [🗂️ Repository Pattern](#-repository-pattern)
  - [🔀 Mapper Pattern](#-mapper-pattern)
  - [🛠️ Custom Middleware](#-custom-middleware)
  - [🔍 Aspect-Oriented Programming](#-aspect-oriented-programming)
  - [🌀 Factory Pattern](#-factory-pattern)
  - [🔐 Caching Pattern](#-caching-pattern)
  - [♾️ Singleton Pattern](#-singleton-pattern)
- [🔒 Authentication and Security](#-authentication-and-security)
  - [🔑 JWT-Based Authentication](#-jwt-based-authentication)
  - [🔒 Two-Factor Authentication (2FA)](#-two-factor-authentication-2fa)
  - [🛡️ Role-Based Authorization](#-role-based-authorization)
  - [🔏 Claim-Based Security](#-claim-based-security)
- [🏗️ Project Structure](#-project-structure)
- [🚀 Getting Started](#-getting-started)
  - [1️⃣ Clone the Repository](#1️⃣-clone-the-repository)
  - [2️⃣ Update Configuration](#2️⃣-update-configuration)
  - [3️⃣ Build the Application](#3️⃣-build-the-application)
  - [4️⃣ Run the Application](#4️⃣-run-the-application)
- [⚙️ Configuration](#️-configuration)
- [📜 Logging](#-logging)

---

## 📖 **Overview**
The **InventorySystem API** is a **Basic Inventory System** built with **.NET Core 8**, designed for scalability, maintainability, and clean architecture. **This project demonstrates what can be done with a structured .NET Core application**, including best practices for authentication, data management, and more. By following modern design patterns, it aims to provide a seamless developer experience and illustrate the capabilities of a robust .NET Core solution.

---

## ✨ **Key Features**

### 1️⃣ **Authentication and Authorization**
- **Description:** Implements JWT-based authentication with role and claim-based authorization.
- **Highlights:**
  - Supports 2FA for enhanced security.
  - Issues encrypted JWT tokens containing user claims.
- **Sample Code:**
  ```csharp
  var tokenDescriptor = new SecurityTokenDescriptor
  {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(sessionLifeTime),
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
  };
  ```

### 2️⃣ **Role and Permission Management**
- **Description:** Dynamically assigns roles, permissions, and features to users and groups.
- **Highlights:** 
  - Includes feature assignment and group management.
- **Sample Code:**
  ```csharp
  await _unitOfWork.Context.UserRoles.AddAsync(new UserRole() { RoleId = role.Id, UserId = user.Id });
  await _unitOfWork.CompleteAsync();
  ```

### 3️⃣ **Data Mapping**
- **Description:** Simplifies data exchange between layers using `MapperService`.
- **Highlights:**
  - Centralized mappers for transforming entities to DTOs and vice versa.
- **Sample Code:**
  ```csharp
  SecurityGroup? newItem = _mapperService.SecurityGroupMapper.MapDataFormToEntity(form);
  ```

### 4️⃣ **Transaction Management**
- **Description:** Ensures atomicity in database operations using the Unit of Work pattern.
- **Sample Code:**
  ```csharp
  using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
  try
  {
      await _unitOfWork.SecurityGroupRepository.InsertAsync(newItem, true);
      await transaction.CommitAsync();
  }
  catch
  {
      await transaction.RollbackAsync();
  }
  ```

### 5️⃣ **Custom Validation**
- **Description:** Validates user inputs such as email formats and password complexity.
- **Sample Code:**
  ```csharp
  if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) 
  {
      throw new ArgumentException("Invalid email format.");
  }
  ```

---

## 🧩 **Design Patterns**

### 📦 **Dependency Injection**
- **Description:** Ensures loose coupling by injecting dependencies into controllers and services.
- **Sample Code:**
  ```csharp
  public AccountService(IUnitOfWork unitOfWork, IHelperService helperService)
  {
      _unitOfWork = unitOfWork;
      _helperService = helperService;
  }
  ```

### 🔗 **Unit of Work Pattern**
- **Description:** Manages transactions and repository interactions in a single unit.
- **Sample Code:**
  ```csharp
  using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
  await _unitOfWork.CompleteAsync();
  ```

### 🗂️ **Repository Pattern**
- **Description:** Abstracts data access logic for individual entities.
- **Sample Code:**
  ```csharp
  var users = await _unitOfWork.UserRepository.GetAll();
  ```

### 🔀 **Mapper Pattern**
- **Description:** Centralizes entity-to-DTO conversions and vice versa.
- **Sample Code:**
  ```csharp
  User? newItem = _mapperService.UserMapper.MapDataFormToEntity(form);
  ```

### 🛠️ **Custom Middleware**
- **Description:** Handles cross-cutting concerns like logging, validation, and error handling.
- **Highlights:**
  - Middleware pipeline includes custom logging and maintenance checks.
- **Sample Code:**
  ```csharp
  app.UseMiddleware<ErrorHandlerMiddleware>();
  ```

### 🔍 **Aspect-Oriented Programming**
- **Description:** Handles cross-cutting concerns such as logging and validation.
- **Sample Code:**
  ```csharp
  _logService.LogError(exp, "Error occurred: {Message}", exp.Message);
  ```

### 🌀 **Factory Pattern**
- **Description:** Creates complex objects like `DatabaseContext` using a factory class.
- **Sample Code:**
  ```csharp
  public DatabaseContext CreateDbContext(string[] args)
  {
      return new DatabaseContext(...);
  }
  ```

### 🔐 **Caching Pattern**
- **Description:** Implements caching to optimize repetitive queries.
- **Sample Code:**
  ```csharp
  public CachingInterceptor(IMemoryCache memoryCache)
  {
      _memoryCache = memoryCache;
  }
  ```

### ♾️ **Singleton Pattern**
- **Description:** Ensures that a class has only one instance throughout the application's lifetime. Implemented using dependency injection for services like logging and email handling.
- **Sample Code:**
  ```csharp
  builder.Services.AddSingleton<ISmtpService, SmtpService>();
  builder.Services.AddSingleton<ITranslationService, TranslationService>();
  ```

---

## 🔒 **Authentication and Security**

### 🔑 **JWT-Based Authentication**
- **Description:** Stateless authentication using JSON Web Tokens.
- **Highlights:**
  - Tokens are signed using HMAC-SHA512 and encrypted for added security.
- **Sample Code:**
  ```csharp
  var tokenDescriptor = new SecurityTokenDescriptor
  {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(sessionLifeTime),
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
  };
  ```

### 🔒 **Two-Factor Authentication (2FA)**
- **Description:** Provides enhanced security by requiring OTPs for login.
- **Sample Code:**
  ```csharp
  Twofactor twofactor = new() 
  { 
      Code = GenerateRandomPassword(8), 
      ExpirationDate = DateTime.UtcNow.AddMinutes(5) 
  };
  ```

### 🛡️ **Role-Based Authorization**
- **Description:** Controls access to resources using roles and policies.
- **Sample Code:**
  ```csharp
  options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
  ```

### 🔏 **Claim-Based Security**
- **Description:** Grants granular permissions using claims.
- **Sample Code:**
  ```csharp
  claims.Add(new Claim("FeaturePermission", permission));
  ```

---

## 🏗️ **Project Structure**
```plaintext
InventorySystem/
├── Api/                     # API entry point and controllers
│   ├── Controllers/         # Handles API endpoints
│   ├── Middlewares/         # Custom middleware components
│   ├── Extensions/          # Extensions for controllers and services
├── Data/                    # Data access layer
│   ├── Context/             # Database context
│   ├── Interceptors/        # Database interceptors
│   ├── GenericRepository/   # Implementation of Repository pattern
├── ServiceImplementation/   # Business logic and services
│   ├── UnitOfWork/          # Unit of Work pattern implementation
├── Mappers/                 # Entity-to-DTO mapping logic
├── Shared/                  # Shared entities, DTOs, and utilities
│   ├── Entities/            # Domain models
│   ├── DTOs/                # Data Transfer Objects
│   ├── Enumerations/        # Shared enums
│   ├── Interfaces/          # Interface definitions
```

---

## 🚀 **Getting Started**

### 1️⃣ **Clone the Repository**
```bash
git clone https://github.com/OdaiHasanAbuHaroon/InventorySystem.git
cd InventorySystem
```

### 2️⃣ **Update Configuration**
Edit `appsettings.json` with your environment-specific configurations.

### 3️⃣ **Build the Application**
```bash
dotnet build
```

### 4️⃣ **Run the Application**
```bash
dotnet run --project InventorySystem.Api
```

---

## ⚙️ **Configuration**
- The application uses `appsettings.json` for environment-specific settings.
- Environment variables override default settings during runtime.

---

## 📜 **Logging**
- **Implementation:** Logging is centralized using `ILogger`.
- **Purpose:** Provides detailed logs for error, warning, and informational messages.
- **Sample Code:**
  ```csharp
  _logService.LogError(exp, "An error occurred: {ErrorMessage}", exp.Message);
  ```

