# Eventage API

Eventage API is an event management backend built with ASP.NET Core. It provides authentication with JWT and CRUD endpoints for events, backed by SQL Server and Entity Framework Core.

**Key Features**
1. User registration and login with JWT issuance.
1. Event CRUD (create, read, update, delete).
1. Swagger/OpenAPI in development for interactive testing.
1. SQL Server persistence with EF Core migrations.

**Tech Stack**
1. ASP.NET Core (minimal hosting model)
1. Entity Framework Core
1. ASP.NET Core Identity
1. SQL Server
1. Swagger / OpenAPI

**Requirements**
1. .NET SDK 10.0
1. SQL Server instance (local or remote)

**Configuration**
The API uses `appsettings.json` for defaults. In production, set secrets via environment variables.

Required settings:
1. `ConnectionStrings:DefaultConnection`
1. `JWT:ValidAudience`
1. `JWT:ValidIssuer`
1. `JWT:Secret`
1. `JWT:ExpiresMinutes`

Environment variable equivalents:
1. `ConnectionStrings__DefaultConnection`
1. `JWT__ValidAudience`
1. `JWT__ValidIssuer`
1. `JWT__Secret`
1. `JWT__ExpiresMinutes`

**Local Development**
1. Restore and run:

```bash
dotnet restore
dotnet run
```

2. Apply migrations (if database is empty):

```bash
dotnet ef database update
```

The default development URL is `http://localhost:5162` (see `Properties/launchSettings.json`).

**Docker**
Build and run:

```bash
docker build -t eventageapi .
docker run -p 8080:8080 ^
  -e ASPNETCORE_ENVIRONMENT=Development ^
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=EventageDB;User ID=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True" ^
  -e JWT__ValidAudience="http://localhost:3000" ^
  -e JWT__ValidIssuer="http://localhost:8080" ^
  -e JWT__Secret="REPLACE_WITH_SECRET" ^
  -e JWT__ExpiresMinutes="60" ^
  eventageapi
```

**API Endpoints**
Auth:
1. `POST /Auth/register`
1. `POST /Auth/login`

Events:
1. `GET /Event`
1. `GET /Event/{id}`
1. `POST /Event` (requires JWT)
1. `PUT /Event/{id}` (requires JWT)
1. `DELETE /Event/{id}` (requires JWT)

Other:
1. `GET /WeatherForecast` (requires JWT, demo endpoint)

**Example Requests**
Register:

```http
POST /Auth/register
Content-Type: application/json

{
  "name": "Test User",
  "email": "test@example.com",
  "password": "Passw0rd!",
  "isOrganizer": true
}
```

Login:

```http
POST /Auth/login
Content-Type: application/json

{
  "email": "test@example.com",
  "password": "Passw0rd!"
}
```

Create event:

```http
POST /Event
Authorization: Bearer <JWT>
Content-Type: application/json

{
  "title": "Launch Party",
  "description": "Product launch event",
  "date": "2026-03-11T18:00:00Z",
  "location": "Johannesburg"
}
```

**Project Structure**
1. `Controllers/` HTTP endpoints
1. `Models/` Entity models
1. `Services/` Business logic and DTOs
1. `Data/` EF Core DbContext
1. `Migrations/` EF Core migrations

**Notes**
1. Swagger is enabled in Development.
1. JWT tokens include user ID and roles; the user ID is read from the `sub` claim.
