# Real-Time Updates and In-App Radio

SignalR hubs:
- `/hubs/dispatch`
- `/hubs/comms`
- `/hubs/bulletins`

Grouping strategy:
- `organization:{id}`
- `region:{id}`
- `station:{id}`
- `role:{name}`

## Push-to-talk flow
1. User presses floating PTT button.
2. Audio/text payload sent through `CommsHub`.
3. Message is transcribed / formatted for TTS.
4. Group receives event in real-time.
