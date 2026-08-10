import type { HelloResponse, StatsResponse } from './types'

// Thin fetch wrapper — throws a message-bearing Error on any non-2xx or
// network failure so callers can render a styled error panel instead of
// juggling raw Response objects.
async function getJson<T>(url: string): Promise<T> {
  let response: Response
  try {
    response = await fetch(url)
  } catch {
    throw new Error(`Could not reach ${url}. Is the server running?`)
  }
  if (!response.ok) {
    throw new Error(`${url} responded with ${response.status} ${response.statusText}`)
  }
  return (await response.json()) as T
}

export function fetchHello(): Promise<HelloResponse> {
  return getJson<HelloResponse>('/api/hello')
}

export function fetchStats(): Promise<StatsResponse> {
  return getJson<StatsResponse>('/api/stats')
}
