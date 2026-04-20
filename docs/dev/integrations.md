# Airline Integration + Mapping Engine

## Endpoints

- `POST /api/v1/integrations/aidx/messages`
- `POST /api/v1/integrations/airlines/{airlineCode}/flights:upsert`
- `POST /api/v1/integrations/airlines/{airlineCode}/flights:bulk-upsert`
- `POST /api/v1/integrations/airlines/{airlineCode}/assignments:upsert`

## Behavior

- Stores raw payloads with source message IDs.
- Supports idempotent handling for AIDX via source message uniqueness checks.
- Runs mapping suggestions and transformation layer into normalized flight patches.
- Allows partial updates, cancellations, gate changes, and aircraft swaps.

## Mapping Profiles

- Profiles are source-specific and versioned.
- Mapping rules define external-to-internal fields and transform behaviors.
- Admins validate mapping in test mode before publishing.
