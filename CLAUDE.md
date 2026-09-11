# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

CareHomeApi is an ASP.NET Core 9 Web API (C#) for managing a care home: care recipients, care providers, utility bills, meals, notes, and a duty rotation schedule. It uses PostgreSQL via EF Core and JWT for authentication.

## Commands

- Build: `dotnet build`
- Run (dev): `dotnet run` (serves on `http://localhost:5119`, plus `https://localhost:7117` for the `https` profile) — Swagger UI is available at `/swagger` in the `Development` environment.
- Watch/hot reload: `dotnet watch run`
- Apply EF Core migrations: `dotnet ef database update`
- Add a new migration after changing a model: `dotnet ef migrations add <Name>`
- There is no test project in this repo currently.

The app requires a running PostgreSQL instance matching the `ConnectionStrings:DefaultConnection` value in `appsettings.json`/`appsettings.Development.json` (defaults to `Host=localhost;Port=5432;Database=carehome;Username=postgres;Password=admin`).

## Architecture

Each resource follows the same four-layer pattern; when adding a new resource, replicate all four pieces:

1. **Model** (`Models/`) — EF Core entity, plain POCO.
2. **DTOs** (`DTOs/<Resource>/`) — request/response shapes (e.g. `CreateXDto`, `UpdateXDto`, `XDto`). Controllers and services never expose entities directly; services map entities to DTOs manually (no AutoMapper).
3. **Service** (`Services/`, interface in `Services/Interfaces/`) — holds all business logic and EF Core queries against `AppDbContext`. Registered as scoped in `Program.cs`. Controllers depend only on the interface.
4. **Controller** (`Controllers/`) — thin, `[ApiController]` + `[Route("api/[controller]")]`. Delegates to the service, translates service results/exceptions into HTTP responses (`Ok`, `NotFound`, `Conflict`, `NoContent`, etc.). No business logic here.

Other conventions:

- **Database naming**: `AppDbContext` (`Data/AppDbContext.cs`) uses `UseSnakeCaseNamingConvention()`, so C# `PascalCase` properties map to `snake_case` columns automatically — don't hand-roll column names.
- **JSON casing**: API request/response bodies use `camelCase` (configured globally in `Program.cs` via `JsonSerializerOptions.PropertyNamingPolicy`).
- **Entity relationships and delete behavior** are configured in `AppDbContext.OnModelCreating` (e.g. `Bill -> UtilityBillType` is `Restrict`, `Note -> Recipient` is `SetNull`, `DutyRotationEntry -> CareProvider` is `Cascade` with a unique index on `CareProviderId`). Check this method before adding new relationships.
- **Seed data**: `UtilityBillType` has fixed seed rows (`Su`, `Elektrik`, `Doğalgaz`) via `HasData` in `OnModelCreating`.
- **Domain exceptions**: custom exceptions live in `Exceptions/` (e.g. `DuplicateBillException`). The pattern is: service catches a `DbUpdateException` wrapping a specific Postgres error (e.g. unique violation, `SqlState 23505`), throws a domain exception with a user-facing message, and the controller catches that exception to return the right HTTP status (e.g. `409 Conflict`). Follow this pattern instead of pre-checking for duplicates before insert.
- **Auth**: JWT bearer auth is fully wired up (`Program.cs`, `Services/AuthService.cs`, config under `Jwt` in `appsettings.json`) and `AuthController` issues tokens via `/api/auth/register` and `/api/auth/login`, but no controller currently has an `[Authorize]` attribute — all endpoints are effectively open. Keep this in mind when adding new controllers; don't assume protection is already enforced.
- **CORS**: a single policy named `Frontend` allows only `http://localhost:3000`, configured in `Program.cs`.
- **Nullable reference types** and **implicit usings** are enabled project-wide (`CareHomeApi.csproj`).
