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
