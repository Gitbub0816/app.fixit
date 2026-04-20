# FixIt QC

Unified aviation fueling operations + QC platform for Web + Windows + iOS + Android using a shared .NET architecture.

## Architecture

- `FixItQC.Domain`: shared domain model (roles, org hierarchy, dispatch, running balance ledger, inspections, damage zones).
- `FixItQC.Application`: scope-based authorization and app abstractions.
- `FixItQC.Infrastructure`: local file storage, deterministic PDF rendering abstraction, diagnostics middleware, seed scaffolding.
- `FixItQC.Api`: ASP.NET Core API slices for dispatch, bulletins, and health.
- `FixItQC.Web`: role-aware Blazor page scaffolding and premium dark design tokens.
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
- Safety bulletin workflow scope model.

## Feature Modules (scaffolded)

- Operations (truck context, walkaround, ATA/QC, damage, fuel entry, balance history).
- Dispatch/Receiving (Jet + GSE support).
- Audits (internal/safety templates).
- Equipment/Maintenance (work orders, lockout, PM schedules, recurring inspections).
- Resources (chart-driven calculator placeholders under `/data/charts`).
- Reports/Issues.
- Role-aware Admin.
- Global diagnostics/support views.

## Notes

This repository is scaffold-complete for architecture and core workflows. Integrations intentionally deferred by design:

- Production auth provider.
- Production object/blob storage.
- Certified chart/table values.
- Final `.glb` damage model assets.
- App store signing.
