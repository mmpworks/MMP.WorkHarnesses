// Wire contract for WorkHarness.Server's /api/hello and /api/stats endpoints.
// Field names and shapes mirror the server's JSON exactly — keep in lockstep
// with the C# response models.

export interface HelloResponse {
  message: string
  harness: string
  serverTimeUtc: string
}

export interface MachineStats {
  host: string
  os: string
  processorCount: number
  totalMemoryMb: number
  uptimeHours: number
}

export interface ProcessStats {
  pid: number
  name: string
  memoryMb: number
  startedUtc: string | null
}

export interface AiSystemStats {
  id: string
  name: string
  installed: boolean
  cliVersion: string | null
  running: boolean
  processCount: number
  totalMemoryMb: number
  processes: ProcessStats[]
}

export interface StatsResponse {
  generatedAtUtc: string
  machine: MachineStats
  systems: AiSystemStats[]
}

// Wire contract for WorkHarness.Server's /api/logs/stream SSE endpoint.
// Each `data:` frame is one JSON object matching this shape.

export interface LogPropertyValue {
  value: unknown
  capture_mode?: string
}

export interface LogEvent {
  time: string
  level: string
  level_key: string
  level_rank: string
  category: string
  message_template: string
  message: string
  properties: Record<string, LogPropertyValue>
  context: Record<string, unknown>
}
