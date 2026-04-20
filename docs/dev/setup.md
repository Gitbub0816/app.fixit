# Local Setup

1. Install .NET SDK 9+ and PostgreSQL.
2. Configure `ConnectionStrings__Default` to local Postgres.
3. Run EF migrations from `FixItQC.Api`.
4. Start API and Web projects.
5. Use seeded dev logins (`docs/dev/seeded-logins.md`).

## Environments
- Development
- Staging
- Production

## Production hardening checklist
- Remove seeded users.
- Rotate secrets and JWT keys.
- Replace local storage adapter.
- Configure production auth provider.
