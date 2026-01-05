**⭕MedReserve (Clinic Appointment System)**

MedReserve is a comprehensive clinic management and online booking system built with .NET 8 and ASP.NET Core Web API. The project is designed with a focus on Clean Architecture principles and modern design patterns to ensure scalability, maintainability, and high testability

**🔶Architecture & Design**

**This project follows Clean Architecture, decoupling the core business logic from external concerns:**

Domain: Contains enterprise logic, including Entities, Enums, and common types

Application: Implements the CQRS pattern using MediatR. This layer handles commands, queries, and validation logic via Fluent Validation

Infrastructure: Manages data persistence with Entity Framework Core, SQL Server, and handles identity services like JWT authentication

WebAPI: The entry point of the application, managing HTTP requests, middleware, and API documentation via Swagger

**🚩Tech Stack:**

Framework: .NET 8 / ASP.NET Core Web API 

Database: SQL Server 

ORM: Entity Framework Core (Code First) 

Messaging/CQRS: MediatR 

Security: JWT Bearer Authentication & Role-Based Access Control (RBAC) 

Logging: Structured logging with Serilog 

Patterns: Repository & Unit of Work

**✅Installation**

Clone the repository:

git clone https://github.com/mph-shams/medreserve.git

Configure Database: 

Update the DefaultConnection in appsettings.json within the WebAPI project.

Apply Migrations:

dotnet ef database update --project Infrastructure --startup-project WebAPI

Run the application:

dotnet run --project WebAPI

**🔎Contributors**

Project Supervisor: Dr. Ali Rahimi Hossein Abadi 

Developers: Parsa Nasiri Boyoni, Hossein Hosseini, Milad Fazlollah Hamedani 

Institution: Shahid Shamsipour Technical Faculty
