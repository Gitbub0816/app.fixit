-- Development seed data (FOR DEVELOPMENT / TESTING ONLY)
insert into "IntegrationMessages" ("Id","SourceSystem","MessageType","AirlineCode","SourceMessageId","IdempotencyKey","RawPayload","ProcessingState","ErrorSummary","CreatedUtc","UpdatedUtc")
values
(gen_random_uuid(),'AIDX','FlightUpdate','AC','seed-msg-001','idem-001','<AIDX><Airline>AC</Airline></AIDX>','Received',null,now(),now())
on conflict do nothing;

-- Additional seed tables are expected through EF migrations/seed runner when full DB schema is present.
