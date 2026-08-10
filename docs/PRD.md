# MMP.WorkHarnesses — PRD (v1)

## What

An open-source project harness: a ready-to-clone frontend/backend bootstrap used to start
new MMPWorks projects (and to record YouTube videos from — first up: MMP.SlotGame).

## Why

Every new project re-derives the same skeleton: a .NET server that serves a Vite SPA plus an
API, a docker story for extra services, and a dashboard that looks good on camera. Build it
once, clone it forever.

## Layout

```
/CSharp            C# bootstrap — .NET 10 server + Vue 3/Vite/TS web dashboard
  /src/WorkHarness.Server   minimal API; serves web/dist statically + /api/*
  /web                      SPA (Vue 3 + Vite + TypeScript)
/docker            docker-compose root — add external front/back services beyond the C# core
/docs              this PRD + anything else
```

## v1 acceptance

1. `dotnet run` from `CSharp/src/WorkHarness.Server` serves the SPA at `http://localhost:5090`.
2. Page loads a dashboard (Nolan/Barrymore register: dark cinematic + art-deco elegance).
3. A **STAT** button calls `GET /api/stats`; the server probes the machine for running/installed
   AI systems (Claude Code, GitHub Copilot, Codex, Cursor, Gemini, Ollama, …) and returns
   process + CLI-version stats; the dashboard renders them beautifully.
4. `GET /api/hello` returns a hello-world payload (the "initially serves hello-world" contract).
5. `docker/docker-compose.yml` builds and runs the server container; extra services can be
   added as additional compose services without touching the C# core.

## Non-goals (v1)

- Auth, persistence, SignalR, multi-project templating machinery. Seams stay open
  (stable route prefix `/api`, compose file per-service), rooms stay unbuilt.
