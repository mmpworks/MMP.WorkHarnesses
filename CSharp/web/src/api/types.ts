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
