// Pure display-formatting helpers, kept out of components so they stay
// unit-testable without mounting Vue.

export function formatMemoryMb(mb: number): string {
  if (!Number.isFinite(mb)) {
    return '—'
  }
  if (mb >= 1024) {
    return `${(mb / 1024).toFixed(1)} GB`
  }
  return `${mb.toFixed(0)} MB`
}

export function formatUptimeHours(hours: number): string {
  if (!Number.isFinite(hours)) {
    return '—'
  }
  if (hours >= 24) {
    const days = Math.floor(hours / 24)
    const rest = Math.round(hours % 24)
    return `${days}d ${rest}h`
  }
  return `${hours.toFixed(1)}h`
}

export function formatClockTime(isoUtc: string): string {
  if (typeof isoUtc !== 'string') {
    return '—'
  }
  const parsed = new Date(isoUtc)
  if (Number.isNaN(parsed.getTime())) {
    return isoUtc
  }
  return parsed.toLocaleTimeString(undefined, { hour: '2-digit', minute: '2-digit', second: '2-digit' })
}
