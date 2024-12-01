# 📚 PRN231-DotNet-CleanArchitecture 🚀
Just a simple .Net API with CRUD features 🥲! Powered by `.NET 8` and `C hashtag version 12`.
## 🛠️ Built With
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Redis](https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
## 🚧 Development Setup
### Prerequisites
Before starting, ensure you have the following tools and environments set up on your machine:
- .NET 8.0 SDK
- PostgreSQL
- Redis
- Docker (optional)
### ⭐ Local Development Setup
#### 1. Clone the Repository
```bash
git clone <repository_url>
cd <project_directory>
```
#### 2. Configure Application Settings
Update the configuration files located at `PRN231.API/appsettings.json` and `PRN231.API/appsettings.Development.json` with appropriate database connection strings, Redis configurations, and other settings.
#### 3. Restore Dependencies
At the root directory, restore the required NuGet packages by running:
```bash
dotnet restore
```
#### 4. Run the Application
Launch the application locally using the following command:
```bash
dotnet run --project PRN231.API
```

__🚀 The application will listen on:__
- HTTP: **http://localhost:5184**
- HTTPS: **https://localhost:7100**

(You can change these ports in the launchSettings.json file located at PRN231.API/Properties/launchSettings.json)
### 🐳 Dockerized Development Setup (Optional)
#### 1. Configure Application Settings
Update the same configuration files (`PRN231.API/appsettings.json` and `PRN231.API/appsettings.Development.json`) with appropriate database connection strings, Redis configurations, and other settings.
#### 2. Build and Run Docker Containers
At the project root directory, use Docker Compose to build and run the containers:
```bash
docker compose up
```
## 🎯 To-Dos
- [x] Develop CRUD operations.
- [x] Develop "hand-made" authentication operations.
- [x] Integrate soft delete functionality.
- [x] Implement DTOs and AutoMapper for object mapping.
- [x] Implement DbFactory, Unit of Work, and Repository patterns.
- [x] Implement exception handling middleware / .NET 8 exception handler.
- [x] Implement Result Monad.
- [x] Enable authentication using JWT tokens.
- [x] Automatically log AuditLogs to the database.
- [x] Automatically track Entities' CreatedAt and ModifiedAt timestamps.
- [x] Integrate SignalR for real-time notifications.
- [x] Implement Redis Cache for efficient data caching.
- [x] Implement SMTP Email sender.
- [ ] Refactor: Use email templates instead of hard-coding.
- [x] Set up Hangfire for background jobs.
- [ ] Implement ASP.NET Core Identity.
- [ ] Implement RabbitMQ for messaging services.
- [ ] Apply CQRS Pattern (Command and Query Responsibility Segregation).
- [x] Dockerize the application for easy deployment 🐳.
