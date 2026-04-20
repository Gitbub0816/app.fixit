# Local Setup

1. Install .NET SDK 9 and PostgreSQL 15+.
2. Configure `ConnectionStrings__Default` in API environment.
3. Apply SQL bootstrap in `src/FixItQC.Infrastructure/Persistence/Migrations/0001_initial.sql` or run EF migrations.
4. Start API (`src/FixItQC.Api`) and Web (`src/FixItQC.Web`).
5. Open Swagger at `/swagger` for API exploration.
6. Connect Web clients to SignalR hubs for real-time channels.

## Core dev features to verify

- Dispatch board (`/api/dispatch/board`) with view + service filters.
- Fueling validation and on-time scoring.
- Safety bulletin create/submit/ack flow.
- Integration ingestion endpoints for AIDX and airline REST upsert.
- Mapping profile CRUD and suggestion engine.

## Environments
- Development
- Staging
- Production

## Production hardening checklist
- Remove seeded users.
- Rotate secrets and JWT keys.
- Replace local storage adapter with object storage provider.
- Configure production auth provider and policy provider.
- Add integration dead-letter queue and retry policy.
