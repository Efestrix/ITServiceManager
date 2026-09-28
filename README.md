# ITServiceManager

A full-stack service management system for managing customers, devices, repair orders, technicians, repair history and related data.

The project is being developed as a larger C#/.NET portfolio and school project with a focus on clean architecture, REST APIs, authentication, database design and practical software development.

## 🚀 Tech Stack

### Backend

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* REST API
* JWT Authentication
* BCrypt password hashing
* Swagger / OpenAPI
* Postman

### Frontend

* Angular *(planned)*

### DevOps

* Docker *(planned)*
* Docker Compose *(planned)*
* Kubernetes *(planned)*
* CI/CD *(planned)*

## 📋 Features

* Customer management
* Device management
* Device types
* Repair orders
* Repair statuses
* Repair history
* Photo management
* User management
* Role-based authorization
* JWT authentication
* Password hashing with BCrypt
* DTO-based API communication
* Service and Repository architecture
* Input validation
* Centralized exception handling
* API logging
* Query filtering using query parameters
* `/api/auth/me` endpoint for retrieving the currently authenticated user

## 🏗️ Architecture

The backend follows a layered structure to separate responsibilities:

```text
ITServiceManager.API
│
├── Controllers
│   └── HTTP / API endpoints
│
├── Services
│   └── Business logic
│
├── Repositories
│   └── Data access
│
├── Entities
│   └── Database entities
│
├── DTOs
│   └── API request / response models
│
├── Validators
│   └── Input validation
│
├── Mappings
│   └── Entity ↔ DTO mapping
│
├── Data
│   └── Entity Framework Core DbContext
│
├── Middleware
│   └── Global exception handling
│
└── Program.cs
    └── Dependency injection and application configuration
```

A typical request flows through the application like this:

```text
HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository / DbContext
     ↓
MySQL Database
```

## 🔐 Authentication & Authorization

The API uses JWT Bearer Authentication.

Users authenticate through the authentication endpoints and receive a JWT token. The token is then used to access protected endpoints.

Example:

```text
POST /api/auth/login
        ↓
      JWT
        ↓
Authorization: Bearer <token>
        ↓
Protected API endpoint
```

Role-based authorization is also used to restrict specific operations to appropriate users.

Available roles include:

* Admin
* Technician

The `/api/auth/me` endpoint uses the authenticated user's JWT claims to retrieve their own account information.

## 🔎 Query Parameters

Selected resources support filtering through query parameters.

Example:

```http
GET /api/customer?firstName=Jakub&lastName=Novak
```

```http
GET /api/device?deviceTypeId=2&customerId=15
```

```http
GET /api/user?username=admin&role=Admin
```

## 🗄️ Database

The application uses MySQL with Entity Framework Core.

Main entities include:

* Customer
* Device
* DeviceType
* User
* RepairOrder
* RepairHistory
* RepairStatus
* Photo

The database is accessed through the application's `DatabaseContext`.

## 🧪 Testing

Unit testing is planned for the service and validation layers.

The testing strategy will focus on:

* Business logic
* Validation
* Successful operations
* Error handling
* Not-found scenarios
* Authentication logic
* Authorization-related behavior

Integration testing will be considered after the main unit test suite is implemented.

## 🛠️ Development Status

The project is currently under active development.

### Completed / implemented

* ASP.NET Core Web API
* Entity Framework Core
* MySQL database integration
* Entity models
* DTOs
* Services
* Repositories
* Validation
* Exception middleware
* Swagger / OpenAPI
* JWT authentication
* BCrypt password hashing
* Role-based authorization
* `/api/auth/me`
* Query parameter filtering
* API logging

### Planned

* Complete unit test suite
* Angular frontend
* Docker
* Docker Compose
* Kubernetes
* CI/CD
* Deployment to cloud infrastructure

## ▶️ Running the Project

### Requirements

* .NET 8 SDK
* MySQL
* Visual Studio 2022 or another .NET-compatible IDE

### Setup

1. Clone the repository.

```bash
git clone <repository-url>
```

2. Configure the MySQL connection string.

3. Create/update the database using Entity Framework Core migrations.

4. Start the API:

```bash
dotnet run
```

5. Open Swagger to explore and test the API.

Postman can also be used for manual API testing.

## 🎯 Project Goals

The main goals of the project are:

* Improve C# and .NET development skills
* Build a realistic REST API
* Practice professional backend architecture
* Learn authentication and authorization
* Work with relational databases and Entity Framework Core
* Learn automated testing
* Gain practical experience with Docker and Kubernetes
* Build a portfolio project suitable for real-world development

## 👨‍💻 Author

**Jakub Strakoš**

C# / .NET Developer

GitHub: [Efestrix](https://github.com/Efestrix)

---

> This project is continuously evolving as new features, tests and infrastructure are added.
