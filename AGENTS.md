# session-manager-grpc-plugin-server-csharp

An Extend Override app for the **session manager** written in C#. AGS calls this gRPC server with lifecycle hooks whenever game sessions or party sessions are created, updated, or deleted.

This is a template project — clone it, replace the sample logic in the service implementation, and deploy.

## Build & Test

```bash
dotnet build src/                    # Build the solution
dotnet test src/                     # Run tests
docker compose up --build            # Run locally with Docker
```

## Architecture

AGS invokes this app's gRPC methods instead of its default logic:

```
Game Client → AGS → [gRPC] → This App → Response → AGS
```

The sample implementation handles six lifecycle hooks (OnSessionCreated, OnSessionUpdated, OnSessionDeleted, OnPartyCreated, OnPartyUpdated, OnPartyDeleted) and demonstrates injecting custom attributes into session data on creation events.

### Key Files

| Path | Purpose |
|---|---|
| `src/AccelByte.PluginArch.SessionManager.Demo.Server/Program.cs` | Entry point — starts gRPC server, wires interceptors and observability |
| `src/AccelByte.PluginArch.SessionManager.Demo.Server/Classes/DefaultAccelByteServiceProvider.cs` | **Service implementation** — your custom logic goes here |
| `src/AccelByte.PluginArch.SessionManager.Demo.Server/Services/SessionManagerDemoService.cs` | **Service implementation** — your custom logic goes here |
| `src/AccelByte.PluginArch.SessionManager.Demo.Server/Protos/session-manager.proto` | gRPC service definition (AccelByte-provided, do not modify) |
| `src/AccelByte.PluginArch.SessionManager.Demo.Server/Protos/` | Generated code from proto (do not hand-edit) |
| `docker-compose.yaml` | Local development setup |
| `.env.template` | Environment variable template |

## Rules

See `.agents/rules/` for coding conventions, commit standards, and proto file policies.

## Environment

Copy `.env.template` to `.env` and fill in your credentials.

| Variable | Description |
|---|---|
| `AB_BASE_URL` | AccelByte base URL (e.g. `https://test.accelbyte.io`) |
| `AB_CLIENT_ID` | OAuth client ID |
| `AB_CLIENT_SECRET` | OAuth client secret |
| `AB_NAMESPACE` | Target namespace |
| `PLUGIN_GRPC_SERVER_AUTH_ENABLED` | Enable gRPC auth (`true` by default) |

## Dependencies

- [AccelByte .NET SDK](https://github.com/AccelByte/accelbyte-csharp-sdk) (`AccelByte.Sdk` on NuGet) — AGS platform SDK and gRPC plugin utilities
