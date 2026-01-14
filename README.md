# ChipSoft Assessment

Welcome and thank you for your interest in ChipSoft!

This repository contains a sample Blazor (Server + WebAssembly) application used for assessment purposes.

## Projects
- `ChipSoft.Assessment.Presentation` - Host app, server for API and static hosting of client.
- `ChipSoft.Assessment.Presentation.Client` - Blazor WebAssembly client.
- `ChipSoft.Assessment.Application` - Application services, DTOs, validators.
- `ChipSoft.Assessment.Domain` - Domain entities and enums.
- `ChipSoft.Assessment.Infrastructure` - EF Core persistence (SQLite).
- `ChipSoft.Assessment.Tests` - Unit tests.

## Prerequisites
- .NET 10 SDK
- Optional: Visual Studio 2022/2023, or VS Code with C# extensions

## Local configuration
- `ApiSettings:BaseAddress` must be configured for the Blazor client to communicate with the server. For local development you can set this to `https://localhost:7149/` (adjust port to what the server uses).
- Use `appsettings.Development.json` for local overrides. Do not commit secrets.
- Use `dotnet user-secrets` or environment variables for sensitive values.

## Running locally
1. Build the solution:
   ```bash
   dotnet build
   ```

2. Run the server (Presentation project). From solution root:
   ```bash
   dotnet run --project ChipSoft.Assessment.Presentation/ChipSoft.Assessment.Presentation.csproj
   ```

3. In another terminal, run the WASM client if running standalone (optional):
   ```bash
   dotnet run --project ChipSoft.Assessment.Presentation/ChipSoft.Assessment.Presentation.Client/ChipSoft.Assessment.Presentation.Client.csproj
   ```

4. Open the browser and navigate to the server address. The client is hosted as static assets by the server when using the integrated hosting model.

## Testing
- Run unit tests:
  ```bash
  dotnet test
  ```

## Repository hygiene and recommended files
- `.editorconfig` added to enforce consistent formatting and C# conventions.
- `.gitattributes` added to normalize line endings and treat binary files correctly.
- `.gitignore` updated to exclude build artifacts, IDE files, local SQLite files (`temp.db`), and other local-only files.
- Do not commit local database files or secrets. Add any local-only test data to `.gitignore` if necessary.

## CI / GitHub
- Add a GitHub Actions workflow that runs `dotnet restore`, `dotnet build`, and `dotnet test` on push/PR.
- Protect `main` branch and require the CI checks to pass before merging.

## Contributing
- Create feature branches and open PRs against `main`.
- Add unit tests for new functionality.
- Run `dotnet format` before committing to enforce `.editorconfig` rules.

## Publishing
- For deployment, configure a production database and set production configuration values via environment variables or a secrets store.
- Ensure `ApiSettings:BaseAddress` used by the client points to the deployed server.
