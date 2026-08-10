# MMP.WorkHarnesses

A ready-to-clone project harness: a .NET 10 backend + Vue 3 dashboard frontend, with a
docker root for growing past the C# core. Clone it, rename it, and a new project starts
with a working web stack instead of an empty folder.

## Layout

| Path | What it is |
|---|---|
| `CSharp/` | The C# bootstrap: server + web dashboard |
| `CSharp/src/WorkHarness.Server/` | .NET 10 minimal API — serves the SPA and `/api/*` |
| `CSharp/web/` | Vue 3 + Vite + TypeScript dashboard |
| `docker/` | Compose root — add external front/back services as new compose entries |
| `docs/` | PRD and project docs |

## Run it

```bash
# 1. Build the SPA
cd CSharp/web && npm install && npm run build && cd ../..

# 2. Start the server
dotnet run --project CSharp/src/WorkHarness.Server

# 3. Open http://localhost:5090 — click STAT
```

Dev loop for the SPA: `npm run dev` in `CSharp/web` (Vite on :5173, proxies `/api` to :5090).

Docker instead:

```bash
cd docker && docker compose up --build
```

## What v1 does

- `GET /api/hello` — hello-world payload (the harness heartbeat).
- `GET /api/stats` — probes the machine for AI systems (Claude Code, GitHub Copilot,
  Codex, Cursor, Gemini, Ollama): CLI version + live process scan (count, memory, start time).
- The dashboard renders it all behind one **STAT** button.

## Extending

- New API surface: add endpoints in `Program.cs` under `/api/*`.
- New AI system in the stats probe: one catalog row in `AiSystems.cs`.
- New external service (database, second frontend, worker): one block in
  `docker/docker-compose.yml`.

## License

Apache-2.0 — see [LICENSE](LICENSE).
