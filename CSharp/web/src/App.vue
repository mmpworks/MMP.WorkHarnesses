<script setup lang="ts">
import { ref } from 'vue'
import { fetchHello, fetchStats } from './api/client'
import type { HelloResponse, StatsResponse } from './api/types'
import { formatClockTime } from './format'
import MachineStrip from './components/MachineStrip.vue'
import SystemCard from './components/SystemCard.vue'
import LoadingState from './components/LoadingState.vue'
import ErrorPanel from './components/ErrorPanel.vue'
import LogViewer from './components/LogViewer.vue'

type StatsPhase = 'idle' | 'loading' | 'loaded' | 'error'

const hello = ref<HelloResponse | null>(null)
const stats = ref<StatsResponse | null>(null)
const phase = ref<StatsPhase>('idle')
const errorMessage = ref('')

async function loadHello(): Promise<void> {
  try {
    hello.value = await fetchHello()
  } catch {
    // A failed hello probe just leaves the footer blank; nothing else
    // depends on it.
    hello.value = null
  }
}

async function loadStats(): Promise<void> {
  phase.value = 'loading'
  try {
    stats.value = await fetchStats()
    phase.value = 'loaded'
  } catch (err) {
    errorMessage.value = err instanceof Error ? err.message : 'Unknown error probing stats.'
    phase.value = 'error'
  }
}

void loadHello()
</script>

<template>
  <header class="hero">
    <div class="deco-label">WorkHarness</div>
    <h1 class="hero__title">System Dashboard</h1>
    <div class="deco-rule" />
  </header>

  <MachineStrip v-if="stats" :machine="stats.machine" />

  <div class="stat-launch">
    <button class="stat-button" type="button" :disabled="phase === 'loading'" @click="loadStats">
      {{ phase === 'loading' ? 'Probing…' : 'STAT' }}
    </button>
    <p v-if="stats" class="stat-launch__meta">
      Last probe {{ formatClockTime(stats.generatedAtUtc) }}
    </p>
  </div>

  <LoadingState v-if="phase === 'loading'" />

  <ErrorPanel v-else-if="phase === 'error'" :message="errorMessage" @retry="loadStats" />

  <section
    v-else-if="phase === 'loaded' && stats"
    class="grid"
    role="list"
    aria-label="AI system status cards"
  >
    <SystemCard
      v-for="(system, index) in stats.systems"
      :key="system.id"
      :system="system"
      :stagger-index="index"
      role="listitem"
    />
  </section>

  <p v-else class="hint">Press STAT to probe this machine for installed AI systems.</p>

  <LogViewer />

  <footer class="footer">
    <span v-if="hello">{{ hello.message }} &middot; {{ hello.harness }}</span>
  </footer>
</template>

<style scoped>
.hero {
  margin-bottom: var(--space-lg);
}

.hero__title {
  font-size: 2.1rem;
  letter-spacing: 0.04em;
  margin: var(--space-xs) 0 var(--space-md);
}

.stat-launch {
  display: flex;
  align-items: baseline;
  gap: var(--space-md);
  margin-bottom: var(--space-lg);
}

.stat-button {
  font-family: var(--font-display);
  font-size: 1rem;
  letter-spacing: 0.28em;
  text-transform: uppercase;
  background: transparent;
  color: var(--color-accent);
  border: var(--rule-brass);
  padding: 0.85rem 2.4rem;
  cursor: pointer;
  clip-path: polygon(
    8px 0%,
    calc(100% - 8px) 0%,
    100% 8px,
    100% calc(100% - 8px),
    calc(100% - 8px) 100%,
    8px 100%,
    0% calc(100% - 8px),
    0% 8px
  );
  transition:
    color var(--duration-micro) var(--ease-out),
    border-color var(--duration-micro) var(--ease-out),
    background var(--duration-micro) var(--ease-out);
}

.stat-button:hover:not(:disabled) {
  color: var(--color-accent-bright);
  border-color: var(--color-accent-bright);
  background: color-mix(in srgb, var(--color-accent) 8%, transparent);
}

.stat-button:disabled {
  color: var(--color-accent-dim);
  border-color: var(--color-accent-dim);
  cursor: progress;
}

.stat-launch__meta {
  font-family: var(--font-mono);
  font-size: 0.78rem;
  color: var(--color-text-secondary);
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: var(--space-md);
}

.hint {
  color: var(--color-text-secondary);
  font-size: 0.92rem;
}

.footer {
  margin-top: var(--space-xl);
  padding-top: var(--space-md);
  border-top: var(--rule-hairline);
  font-family: var(--font-mono);
  font-size: 0.72rem;
  color: var(--color-text-muted);
}
</style>
