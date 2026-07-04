## Title
A small **Roles & Permissions** web application built in **ASP.NET Core with Razor** and **Clean Architecture**


## Requirements

- .NET 10 SDK
- SQL Server **or** Docker and Docker compose

## Instructions

if using local sql server, make sure to update the connection string at `vmnova.Web/appsetings.json` accordingly.

for example: `Server=localhost,1433;Database=vmnova;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;`

if using docker compose, at the project root run:

```bash
docker compose up -d
```

The application creates the database and seeds data when it starts.

## Run

after database is set up, run:

```bash
dotnet run --project src/vmnova.Web
```

Open browser on `http://localhost:5000`


## Code of Honor

Code of Honor: I confirm the submitted work is my own and was completed without AI code-generation tools.  — Yousef Alaa, 2026-07-04
