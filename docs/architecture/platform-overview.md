# FixIt QC Platform Architecture

## Layers
- Domain: canonical model for operations, compliance, dispatch, integrations, realtime, and KPI.
- Application: business workflows (fueling, PM/ATA/JIG, mapping, comms).
- Infrastructure: EF/Postgres persistence, storage, PDF layout, seed scripts.
- API: REST + SignalR + background workers + OpenAPI.
- Web/Mobile/SharedUI: role-aware client experiences.

## Real-time contracts
- DispatchHub: flight and assignment updates.
- CommsHub: PTT transmissions and transcript/TTS payload delivery.
- BulletinHub: safety bulletin notification fanout.

## Compliance engine
- PM cadence scheduling and next-due calculation.
- Inspection window validity (valid, grace, non-compliant).
- ATA execution with auto-work-order creation on critical failure.
- Optional station JIG mode toggles JIG templates and dashboards.

## Integration architecture
- Raw payload retention for every message.
- Source message IDs and idempotency keys for dedupe.
- Mapping profiles + mapping rules per source/airline.
- Transform and normalize into internal flight patch model.
- Partial updates supported without data loss.
