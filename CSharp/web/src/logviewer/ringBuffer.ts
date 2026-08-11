// Ring-buffer trim: appends new items and drops the oldest past `cap`.
// Implemented as a batched array trim (not a true circular index) because
// the buffer is rebuilt wholesale on every flush anyway — one slice per
// flush is cheap even at the 25k cap, and callers always get a fresh array
// reference (immutable-update convention), never a mutated one.

export function appendWithCap<T>(existing: readonly T[], incoming: readonly T[], cap: number): T[] {
  if (incoming.length === 0) {
    return existing as T[]
  }
  const combined = incoming.length > 0 ? (existing as T[]).concat(incoming) : (existing as T[])
  return combined.length > cap ? combined.slice(combined.length - cap) : combined
}
