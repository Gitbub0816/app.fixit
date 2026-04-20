# FixIt QC

Production-minded unified aviation fueling operations platform for Web + Windows + iOS + Android.

## Platform Scope

FixIt QC unifies:
- operations / walkaround / ATA 103-aligned QC
- dispatch (visual + table) and receiving
- fueling workflow validation and on-time compliance
- PM and recurring inspections
- optional station-level JIG compliance scaffolding
- in-app radio (PTT) and real-time bulletins
- airline integration (AIDX + proprietary REST upserts)
- KPI dashboards and leaderboards

## Solution Layout

- `src/FixItQC.Domain` - entities, enums, hierarchy, dispatch, compliance, integrations, realtime.
- `src/FixItQC.Application` - authorization, fueling/compliance rules, comms, and mapping engine logic.
- `src/FixItQC.Infrastructure` - EF Core `FixItQcDbContext`, storage, PDF renderer, diagnostics, seed data.
- `src/FixItQC.Api` - REST APIs, v1 integration endpoints, SignalR hubs, hosted worker, Swagger.
- `src/FixItQC.Web` - Blazor pages and shared UI styles/components including floating PTT button.
- `src/FixItQC.Mobile` - MAUI-ready project placeholder.
- `src/FixItQC.SharedUI` - shared Razor UI project.
- `tests/FixItQC.UnitTests` - unit tests for rules/authorization.
- `tests/FixItQC.IntegrationTests` - integration-oriented behavior tests.

## Key APIs

### Core
- `GET /health`
- `GET /api/dispatch/board`
- `POST /api/fueling/validate`
- `GET /api/compliance/inspection-window`
- `GET /api/compliance/on-time`
- `GET/POST /api/safetybulletins`

### Integrations
- `POST /api/v1/integrations/aidx/messages`
- `POST /api/v1/integrations/airlines/{airlineCode}/flights:upsert`
- `POST /api/v1/integrations/airlines/{airlineCode}/flights:bulk-upsert`
- `POST /api/v1/integrations/airlines/{airlineCode}/assignments:upsert`
- `POST /api/v1/integrations/mappings/suggestions`
- `POST /api/v1/integrations/mappings/profiles`

- `GET /api/v1/compliance/pm/schedules`
- `GET /api/v1/compliance/pm/due-eval`
- `POST /api/v1/compliance/inspections/execute`
- `POST /api/v1/comms/transmit`
- `POST /api/v1/comms/users/{userId}/background-audio`
- `GET /api/v1/reports/work-orders/{id}.pdf`
- `GET /api/v1/reports/audits/{id}.pdf`
- `GET /api/v1/reports/damage/{id}.pdf`

### Realtime (SignalR)
- `/hubs/dispatch`
- `/hubs/comms`
- `/hubs/bulletins`

## Production-minded characteristics already implemented

- Raw integration payload retention and source message tracking.
- Mapping profile abstraction and field suggestion heuristics.
- Partial flight upsert behavior and idempotent AIDX handling by source message ID.
- Dispatch status model includes `AtRisk`, `Delayed`, `Cancelled`, and `Exception`.
- Fueling rules enforce required fields, tank balancing thresholds, and variance checks.
- On-time scoring model (`Green` >= 5 min, `Yellow` 0-5 min, `Red` late).
- PM / inspection entities and compliance window evaluation.
- Bulletin creation, submit-upward workflow, and acknowledgements.
- KPI snapshot and station leaderboard endpoints.

- ATA/JIG/PM compliance APIs with cadence logic and auto-work-order generation from failed inspections.
- Radio comms background-audio user setting endpoint.
- Deterministic PDF pagination engine with explicit block/page model.

## Deferred production integrations

- enterprise auth provider and IAM.
- finalized deterministic PDF engine implementation (QuestPDF/iText integration layer).
- production blob/object provider.
- full MAUI shell wiring and native audio stack implementation.
