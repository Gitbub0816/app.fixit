-- Initial schema bootstrap for FixIt QC (development scaffold)
create table if not exists "IntegrationMessages" (
  "Id" uuid primary key,
  "SourceSystem" text not null,
  "MessageType" text not null,
  "AirlineCode" text null,
  "SourceMessageId" text not null,
  "IdempotencyKey" text null,
  "RawPayload" text not null,
  "ProcessingState" text not null,
  "ErrorSummary" text null,
  "CreatedUtc" timestamptz not null,
  "UpdatedUtc" timestamptz not null
);
create unique index if not exists ix_integrationmessages_sourcesys_msgid on "IntegrationMessages" ("SourceSystem", "SourceMessageId");
