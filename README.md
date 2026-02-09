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

## ™️ Development Team & Task Distribution

🔵 1. Hossein Hosseini (hossein2081)

🟥Focused on the overall structure, security, and merging the team’s code.

🔸 Architecture & Solution Structure:

Defined the Clean Architecture layers (Domain, Application, Infrastructure, API).

Managed Merge Requests and integrated code from other branches (Merge branch, Update Solution Layers).

Implemented IUnitOfWork pattern for database transactions.

🔸 Authentication & Security (Auth Module):

Developed the Login & Registration flow (LoginCommand, RegisterCommand).

Created the AuthController to handle JWT tokens.

Implemented ChangePassword and UpdateProfile logic.

🔸 Medical Files (Download Side):

Implemented the logic for Downloading medical files securely (DownloadFileQuery).

Refactored and finalized the MedicalFilesController.

🔵 2. Parsa Nasiri (Parsanb)

🟥Focused on the administrative side, error handling, and making the system robust (Logging/Validation).

🔸 Data Transfer Objects (DTOs):

Standardized data flow by creating core DTOs: UserProfileDto, ScheduleDto, AppointmentDetailDto.

🔸 CQRS Pattern:

All operations are separated into Commands and Queries, managed efficiently using the MediatR library.

🔸 Admin Panel & Management:

Built the AdminController for system administrators.

Implemented Doctor Verification logic (VerifyDoctorCommand).

Managed user roles and permissions (UpdateUserRoleCommand).

🔸 Infrastructure & Logging:

Integrated Serilog for advanced logging (Add Serilog, Log to File).

Created the global Exception Middleware to handle runtime errors gracefully without crashing the app.

🔸 Reporting System:

Developed system-wide reporting queries (GetSystemReportsQuery).

Created DTOs for reports (SystemReportDto).

🔸 Validation (Specific Rules):

Wrote specific validators to protect data integrity (e.g., CreateAppointmentValidator, RegisterValidator).

Ensured inputs are checked before reaching the database.

🔵 3. Milad Fazlollah Hamedani (miladdrag)

🟥Focused on the “Brain” of the application, handling the complex rules and external integrations.

   🔸 Domain Core & Entities:

Designed the database entities: Doctor, Appointment, MedicalFile, Schedule.

Configured Entity Framework relationships (EntityConfiguration).

🔸 Telegram Integration (External Service):

Built the full Telegram Bot Service (TelegramBotService).

Added TelegramChatId to the database for notifications.

Handled the Telegram.Bot package integration.

🔸 Advanced Appointment Logic:

Implemented the core handler for booking (CreateAppointmentHandler).

Wrote logic for Canceling appointments and Updating Status (CancelAppointmentCommand).

Implemented ValidationBehavior (Pipeline) to trigger validations automatically.

🔸 Medical Files (Upload Side)

Implemented the logic for Uploading files to the server (UploadFileCommand).

Created the IdentityService interface for user identification.

🔵Collaborative Modules (Shared Work)

🟥Use this section to prove to your professor that you worked as a Team, not just individuals.

In true Agile fashion, critical parts of the system were developed jointly. The Git logs show significant overlap in these areas:

1. 🔶 The Appointment Module (The Core Feature):

Hossein: Created the initial structure and Controller endpoints.
Milad: Added the complex business logic (Creating, Canceling, Status updates) inside the Handlers.
Parsa: Added the Validator layer to ensure no bad data enters the system and fixed bugs in the Controller.

Result: A robust, secure, and validated booking system.

2. 🔶 Medical Files System:

    Milad: Built the Upload functionality and the storage logic.
    Hossein: Built the Download functionality and secured the Controller.
    Parsa: Reviewed and optimized the Controller code during the “Refactor” phase.

   Result: Full cycle (Upload/Download) file management.

3. 🔶 Authentication & User Management

    Hossein: Built the Command logic (Login/Register).
    Parsa: Added validation rules (checking password strength, email format).
    Milad: Connected it to the IdentityService and Domain Entities.

**🔰Git Workflow & Collaboration**

To maintain code quality and ensure a smooth development process, our team follows a structured GitHub Flow model. This ensures that the main branch always contains stable, deployable code.

⭕ 1. Branching Strategy

We use a feature-branching strategy to isolate different tasks:

main Branch: Contains the production-ready code. Direct commits to main are strictly prohibited.

feature/ Branches: Used for developing new features (e.g., feature/jwt-auth, feature/telegram-integration).

fix/ Branches: Used for urgent bug fixes (e.g., fix/appointment-conflict).

refactor/ Branches: Used for code optimization without changing functionality.

⭕ 2. Development Process
   
Synchronize: Always start by pulling the latest changes from main:

Bash
git checkout main
git pull origin main
Create a Branch: Create a descriptive branch for your task:

Bash
git checkout -b feature/your-feature-name
Commit Changes: Follow the Conventional Commits standard (see below).

Push & Pull Request: Push your branch to GitHub and open a Pull Request (PR):

Bash
git push origin feature/your-feature-name
Review: At least one other team member must review the code for logic errors and architectural consistency (Clean Architecture principles).

Merge: Once approved and all tests pass, the branch is merged into main and deleted.

⭕ 3. Commit Message Convention
We use clear and concise commit messages to keep the history readable:

feat: A new feature (e.g., feat: add telegram notification service).

fix: A bug fix (e.g., fix: resolve jwt token expiration issue).

docs: Documentation changes (e.g., docs: update readme with git workflow).

style: Formatting, missing semi-colons, etc. (no production code change).

refactor: Refactoring production code (e.g., refactor: optimize generic repository logic).

⭕ 4. Database Migrations in Team

When working on features that change the database schema:

Create the migration in the Infrastructure project.

Include the migration files in your commit.

Other team members must run dotnet ef database update after pulling your changes.


**🌐 Automated External Service: Telegram Integration**
MedReserve is integrated with a dedicated Telegram Bot Service, providing patients with a seamless and real-time mobile experience to track their appointments and receive instant updates.

**🔹 Key Features Demonstrated**
Real-time Notifications: As seen in the system's successful output, the bot sends an automated "🔔 New Appointment Confirmed!" alert immediately after a booking is registered in the database.

On-Demand Appointment Retrieval: Users can interact with the bot using the "My Appointments" keyboard button to fetch a live list of their upcoming visits, including dates and current statuses (e.g., Pending, Confirmed).

Informative Interaction: The bot includes a "Description" command that introduces the MedReserve clinic management system to new users, ensuring a user-friendly onboarding process.

Secure Account Linking: The system uses a pairing mechanism (via /linkbyusername or /linkbyid) to securely associate a user's web account with their Telegram ChatId.

**🔹 Technical Implementation Details**
Background Worker: The bot operates as a BackgroundService in the Infrastructure layer, allowing it to listen for messages and process notifications without interrupting the main Web API performance.

MediatR Integration: The notification logic is embedded within the CreateAppointmentHandler, triggering the Telegram API call as soon as the UnitOfWork successfully saves a new appointment.

Persistent Storage: User-specific Telegram identifiers are stored in the Users table, enabling persistent communication even after the application restarts.

**➡️External Service Showcase: Telegram Bot Appointment Reservation Demo**

☑️Link: https://filebin.net/l2l37ghfifa35j4u

**🔎Contributors**

Project Supervisor: Dr. Ali Rahimi Hossein Abadi 

Developers: Parsa Nasiri Boyoni, Hossein Hosseini, Milad Fazlollah Hamedani 

Institution: Shahid Shamsipour Technical Faculty
