# FixIt QC

Unified aviation fueling operations + QC platform for Web + Windows + iOS + Android using a shared .NET architecture.

## Architecture

- `FixItQC.Domain`: shared domain model (roles, org hierarchy, dispatch, running balance ledger, inspections, damage zones, PM templates/schedules, bulletins).
- `FixItQC.Application`: scope-based authorization, fueling validation, compliance windows, approval-chain services, and app abstractions.
- `FixItQC.Infrastructure`: local file storage, deterministic PDF rendering abstraction, diagnostics middleware, and multi-tenant dev seed contracts.
- `FixItQC.Api`: ASP.NET Core API slices for dispatch, bulletins, fueling validation, and compliance scoring.
- `FixItQC.Web`: role-aware Blazor page scaffolding for dashboards, truck context, dispatch visual timeline, fueling workflow, and PM calendar.
- `FixItQC.Mobile`: MAUI Hybrid shell scaffold for Windows/iOS/Android.

## Product Rules implemented in scaffold

- Single unified app model with role-aware exposure.
- Exact role set: GlobalAdmin, OrganizationalAdmin, RegionalAdmin, StationAdmin, Dispatcher, Technician, Operator.
- Multi-tenant hierarchy: Organization -> Region -> Station.
- Organization types flags: FuelServiceProvider, Airline, FuelStorageFacility.
- Running balance built as event ledger model (not a single field).
- ATA/QC and PM/inspection records scaffolded as first-class entities.
- Damage zone IDs persisted for future `.glb` zone model support.
- Deterministic PDF renderer abstraction with explicit layout expectations.
- Real-time communication module contract (PTT + channel scopes + voice/text hooks).
- Safety bulletin scope + upward approval workflow scaffolding.
- Fueling workflow validation rules and on-time compliance scoring.
- Inspection 5-day window + grace period compliance evaluation.

## API Endpoints (current slices)

- `GET /health`
- `GET /api/dispatch/board`
- `GET /api/safetybulletins`
- `POST /api/fueling/validate?plannedGallons=5200`
- `GET /api/compliance/inspection-window?dueDate=2026-04-21&today=2026-04-20`
- `GET /api/compliance/on-time?departureUtc=...&completedUtc=...`

## Notes

This repository now includes functional business-rule services and endpoint scaffolding; remaining production integrations are intentionally deferred:

- Production auth provider.
- Production object/blob storage.
- Certified chart/table values.
- Final `.glb` damage model assets.
- App store signing.
