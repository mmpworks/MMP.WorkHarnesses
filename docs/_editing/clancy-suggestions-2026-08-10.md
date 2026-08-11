# Clancy Pass — MMP.WorkHarnesses documentation (README, how-to, PRD)

**Date:** 2026-08-10
**Scope:** `README.md`, `docs/how-to-use-this-harness.md`, `docs/PRD.md`
**Mode:** suggestions only — nothing in these three files was edited.
**Register note:** this is developer documentation, so the fiction lenses are adapted.
Dialogue-tag and POV craft do not apply. What carries over: AI-ism density and
uniformity, adverb discipline, show-vs-tell (read here as "claim vs. demonstrated
behavior"), and scene logic (read here as "does the doc's account match what the
code actually does").

---

## Read-through impression

This docset is well above the usual harness-README bar. It is concrete, it names
real files and real line numbers of behavior, and it tells the reader what to
delete as clearly as what to keep — which is the hard part of a template repo's
docs and the part most projects skip. The voice is already close to house: plain,
forward, unhyped.

Three things pull it down. First, the README and the how-to overlap heavily in the
logging and testing sections — roughly 80% of the how-to's logging facts appear in
the README first, in different words, which means both documents have to be
maintained and a reader who reads both is reading the same thing twice. Second, the
logging section of the how-to carries the docset's densest cluster of AI-shaped
prose: a self-labeling "That's the DRY principle at work," a triad mic-drop
fragment, and an unexplained `IRegistrarStore` reference in the same twenty lines.
Third — and this is the one that matters most — the word **"invisibly"** on
how-to line 119 implies Herald's mechanism is hidden, and the same document
contradicts it sixty lines later. That is a house-rule violation, not a style
preference.

Total findings: **31** (6 high, 14 medium, 11 low).

---

## Cross-document structure notes

These sit above the line-level table because no single line fix resolves them.

- **Logging is documented twice at near-full depth.** README lines 56–71 carry the
  two-loggers-one-config explanation, both sink names, the file path, the daily
  roll, the 5-day retention, the 10 MB cap, `UseSerilogRequestLogging()`, and
  `Log.CloseAndFlush()`. How-to lines 106–172 carry all of the same facts again.
  **Suggested split:** README keeps three sentences — the engine is Herald.OSS in
  Serilog mode, the call sites are the Serilog ones you know, two sinks (console +
  rolling file) — then links. The how-to owns retention numbers, the code block,
  and the flush behavior. Every number then lives in exactly one file.
- **Testing is documented twice at near-full depth.** README lines 78–103 vs.
  how-to lines 260–299. Same fix: README says "seeded fuzz suites on both ends,
  here's how to run them"; the how-to owns what each suite asserts.
- **Non-goals appear in two files** (how-to 301–306, PRD 36–38) with different
  metaphors for the same idea. The PRD should own the non-goals list; the how-to
  should link to it or state it in one line.
- **"Run it" ordering differs between files.** README builds the SPA first, then
  runs the server. The how-to's dev loop starts the API first, then the SPA. Both
  are correct for their context, but the README never says it is showing the
  production-shaped path. One clause fixes it: "Production-shaped run:".
- **The repository is never linked.** How-to line 185 says "the source is there to
  read" without a URL. Add the GitHub link.

---

## Axis A+ density tally

Windowed at roughly 450 words. Counts are of the pattern instances flagged in the
table below, not every occurrence of a comma.

| Window | Approx words | List-rhythm | Metaphor | Simile | Rule of 3 | Not-X-but-Y | Clean-pivot | Emo/significance-label | Em-dash | Adverb | Total | Band |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| README W1 (l.1–52) | ~330 | 1 | 1 | 0 | 1 | 1 | 0 | 1 | 7 | 0 | 12 | watch |
| README W2 (l.53–115) | ~430 | 0 | 0 | 0 | 1 | 0 | 1 | 0 | 6 | 1 | 9 | watch |
| How-to W1 (l.9–104) | ~470 | 1 | 1 | 0 | 1 | 2 | 1 | 2 | 8 | 2 | 18 | over-dense |
| How-to W2 (l.106–186) | ~520 | 1 | 0 | 0 | 2 | 2 | 1 | 2 | 9 | 1 | 18 | over-dense |
| How-to W3 (l.187–259) | ~450 | 1 | 1 | 0 | 2 | 1 | 0 | 1 | 5 | 1 | 12 | watch |
| How-to W4 (l.260–307) | ~380 | 1 | 0 | 0 | 1 | 1 | 0 | 1 | 4 | 0 | 8 | watch |
| PRD (whole, l.1–39) | ~290 | 0 | 1 | 0 | 1 | 0 | 0 | 1 | 4 | 0 | 7 | watch |

Bands inherited from `editor-pass`: ≤6 healthy, 7–12 watch, ≥13 over-dense; any
single category ≥5 gets fixed regardless of total.

**Em-dash is the category that trips the single-category rule.** How-to W1 (8) and
W2 (9) both exceed 5. Across the three files there are roughly 43 em-dashes in
about 2,900 words — one every 67 words. They are all grammatically sound, which is
why they slipped in; the problem is that when a mark appears that often it stops
carrying rhetorical weight and becomes the default joint between any two clauses.
Roughly a third should become periods, colons, or parentheses. Specific
candidates are marked `Em-dash` in the table.

Two windows land **over-dense**, both in the how-to, and both in the same
stretch (the STAT explanation and the logging section). That is where the editing
effort belongs.

---

## Axis B check (uniformity / tics)

- **Burstiness — the weakest axis.** The how-to's logging and testing sections
  hold a narrow 25–40-word sentence band for long stretches. How-to lines 293–299
  is a single 62-word sentence with three embedded clauses; lines 150–157 run
  four consecutive sentences of 28, 31, 24, and 30 words. The README is
  noticeably better because its bullets force variation. Fix: break the long ones
  and let a genuinely short sentence stand alone. The doc already does this well
  in two places — "That's a real line from `Program.cs`" (l.116) and "There's no
  scaffolding to tear out" (l.244) — so the instinct is there; it just needs to
  fire more often.
- **Subject-verb distance:** healthy. Subjects and verbs stay close throughout;
  no buried-predicate problems found.
- **Predictable word choice:** mostly good, concrete, specific. Two soft spots:
  "for free" is used twice (how-to l.127, l.168) as the same rhetorical move, and
  "wired together" / "wires into" recur across all three files.
- **Synonym cycling:** minor and benign. "SPA" / "dashboard" / "frontend" rotate,
  but each has a genuinely different referent (build artifact, UI, tier), so this
  is precision rather than cycling. Leave it.
- **Circular endings / self-labeling significance:** the real Axis B problem here.
  Three sections end by naming their own importance rather than letting the
  content stand: "That's the point" (l.28), "That's the DRY principle at work"
  (l.155), "That's the *Unix philosophy* letter of CUPID in practice" (l.218). The
  third is the most defensible because the harness genuinely is teaching CUPID,
  but three instances of the same construction in one document is a tic.
- **Temporal vagueness:** none. Every claim is anchored to a file or a command.
- **Filter words / hedges:** low. Three superfluous adverbs total ("quietly,"
  "invisibly," "genuinely"), all flagged below. Clean by any normal standard.
- **Nuance-obsession:** none. The doc commits to positions.

---

## Issues — README.md

| # | Steve | Check | Location | Tic / Pattern | Passage (quoted) | Issue | Suggested Fix | Rec |
|---|---|---|---|---|---|---|---|---|
| 1 | | Redundancy | l.56–71 | Cross-doc duplication | (whole Logging section) | Restates ~80% of how-to l.106–172, including every retention number. Two files to maintain; a reader of both reads it twice. | Cut to three sentences: engine is Herald.OSS in Serilog mode, call sites are Serilog's, two sinks (console + rolling file). Move the numbers, the code block, and the flush behavior to the how-to and link. | UPDATE |
| 2 | | Redundancy | l.78–103 | Cross-doc duplication | (whole Testing section) | Same problem as #1 against how-to l.260–299. | README keeps the two run commands and one sentence on what a seeded fuzz suite is. Per-suite assertions live in the how-to. | UPDATE |
| 3 | | Fluff intensifier | l.81–82 | House-banned "exactly" | "so a failure reproduces exactly the same way every run" | "exactly" is an intensifier here, not a contract. House rule bans it in this sense. | "so a failure reproduces the same way every run" | UPDATE |
| 4 | | Repetition | l.4 / l.11 | Unusual-phrase echo | "growing past the C# core" … "grow past that screen without a rewrite" | The same uncommon verb phrase twice in eight lines, with two different meanings (past the language, past the feature). Reads as a tic and blurs both meanings. | Keep l.4. Change l.11 to "…so the *next* project you clone it for can replace that screen without tearing out the wiring underneath." | UPDATE |
| 5 | | Marketing claim | l.10–11 | Unbacked promise | "can grow past that screen without a rewrite" | Nothing in the code prevents a rewrite; what the harness actually provides is that the server, build, docker, and test wiring survive replacing the feature. Claim more precisely and it gets more credible, not less. | Covered by the fix in #4. | UPDATE |
| 6 | | Unclear referent | l.23 | Name that appears nowhere else | "PRD, this project's own docs, and the clone-to-start guide" | "the clone-to-start guide" is not the name of any file; the file is titled "How to use this harness." "this project's own docs" is vague — `docs/` holds the PRD and the guide. | "PRD and the how-to guide" — or name the file: "`PRD.md` and `how-to-use-this-harness.md`". | UPDATE |
| 7 | | Promise mismatch | l.74–76 | Forward reference the target doesn't fulfill | "and why the harness picked this shape over calling Herald directly" | The how-to's "Why the harness picked this shape" section argues migration cost and existing team habits. It never compares against calling Herald's native API directly. The README promises a comparison the guide doesn't make. | Either trim the README to "…and why the harness logs this way," or add one sentence to the how-to that names the direct-Herald alternative and why it lost. | UPDATE |
| 8 | | AI-ism | l.48 | Bolted-on metaphor label | "hello-world payload (the harness heartbeat)" | The parenthetical relabels the thing rather than adding information — `/api/hello` is already self-evident. | Cut the parenthetical, or make it functional: "(use it as a health check)". | UPDATE |
| 9 | | Em-dash | l.3–11 | Density | (7 em-dashes in the opening 330 words) | At this rate the mark stops signaling anything. | Convert two or three to periods or colons. Highest-value swaps: l.8 "AI coding tools — Claude Code, …" → colon; l.66 "one file per day, 5 days kept," → keep as is; l.96 "line endings —" → period. | UPDATE |
| 10 | | Structure | l.25–35 | Missing context label | "## Run it" | The README shows the production-shaped path (build SPA, then run server) without saying so; the how-to leads with the dev loop. A reader moving between them may think one is wrong. | Add a four-word lead-in: "The production-shaped path:" and one line pointing at the how-to's dev loop. | UPDATE |
| 11 | | Show-vs-tell | l.100–103 | Abstract where concrete is available | "a full page mount never throws no matter what the network handed back" | Fine on its own, but it duplicates how-to l.297–299 word-for-nearly-word. | Resolved by #2 — cut here, keep in the how-to. | UPDATE |

---

## Issues — docs/how-to-use-this-harness.md

| # | Steve | Check | Location | Tic / Pattern | Passage (quoted) | Issue | Suggested Fix | Rec |
|---|---|---|---|---|---|---|---|---|
| 12 | | **House rule — OSS mechanism** | l.119 | Implies hidden internals | "The Herald engine sits underneath it, invisibly, doing the actual work." | **Highest-priority finding.** "invisibly" says the mechanism is hidden. Herald.OSS is open source and its mechanism is public; implying otherwise is banned in consumer-facing writing. The same document contradicts this at l.182–185. It is also a superfluous adverb — "underneath" already carries the idea. | "The Herald engine does the work underneath, and the source for how it maps these calls is public." | UPDATE |
| 13 | | Unclear referent | l.123 | Undefined term, no antecedent | "`IRegistrarStore`-style pluggability aside, the practical reason is migration cost." | `IRegistrarStore` appears nowhere else in this doc, the README, or the PRD. A reader cloning this harness has no idea what it is or why it is being set aside. The clause costs the sentence its opening and gives nothing. | Delete the clause: "The practical reason is migration cost." | REMOVE |
| 14 | | Accuracy | l.96–97 | Overclaim the code can't support | "which keeps the harness's resource footprint at zero between clicks" | The server process is resident between clicks and holds memory; its footprint is not zero. What is true is that it performs no work and runs no timer. | "so the harness does no work between clicks — no background timer, no polling loop." | UPDATE |
| 15 | | Accuracy | l.262–264 | Overclaim | "so 'flaky' isn't a category of bug these tests can have" | A fixed seed removes flakiness from *input selection*. It does not remove environment flakiness, and this suite is unusually exposed to it — the STAT probe shells out to CLIs and scans live processes, which varies by machine. The claim will be falsified by the first developer whose Ollama is running. | "so a failing case replays the same input every run — you debug the failure instead of chasing it." | UPDATE |
| 16 | | AI-ism | l.152–157 | Triad mic-drop + self-labeling significance + banned intensifier | "One pipeline, two entry points, no duplicated sink configuration. That's the DRY principle at work: the sink list … is declared exactly once." | Three tells stacked in two sentences: a rule-of-three fragment staged as a punchline, a sentence that names its own importance instead of letting the code show it, and "exactly" as an intensifier. Densest AI-shaped moment in the docset. | "One pipeline, two entry points. The sink list — the console writer and the file writer with its rolling policy — is declared once, so changing a sink means changing one place." | UPDATE |
| 17 | | AI-ism | l.28 | Mic-drop + self-labeling | "You'll likely replace it. That's the point." | Manufactured punchline; the preceding sentence already made the point. | "You'll likely replace it with your own first screen." | UPDATE |
| 18 | | Negation tic | l.30–32 | "X, not Y" snap-contrast | "the dashboard is built to look good on camera from the first commit, not after a design pass" | House rule: positive phrasing unless the negation is load-bearing. It isn't here. | "the dashboard was designed for camera before the first commit shipped" | UPDATE |
| 19 | | Negation tic | l.19 | Negative where positive is available | "a dashboard that doesn't look like a placeholder" | Says what the dashboard avoids instead of what it is. | "a dashboard that already looks finished" | UPDATE |
| 20 | | Negation tic | l.205–206 | Multi-negation countdown (playbook Part B) | "a flat array of catalog rows — no factory, no registration step, no inheritance hierarchy to extend" | Three negations in a row, staged as a rhythmic countdown. The positive statement is shorter and clearer. | "a flat array of catalog rows. One row is one system; adding a system means adding a row." (The existing next line already says "One row is one system" — merge, don't repeat.) | UPDATE |
| 21 | | Negation tic | l.128 | Negative construction | "so nothing needs to be relearned" | | "so the call sites read the way you already write them" | UPDATE |
| 22 | | Negation tic | l.125 | Negative construction | "A cloned project usually isn't starting logging from zero" | | "A cloned project usually arrives with logging habits already in place" | UPDATE |
| 23 | | Adverb | l.85 | Superfluous | "quietly fetches `/api/hello` for a footer status line" | "quietly" adds nothing — a background fetch for a footer line is already quiet, and the sentence's own detail proves it. | "fetches `/api/hello` for a footer status line" | UPDATE |
| 24 | | Fluff intensifier | l.103 | Hollow intensifier (playbook Part B) | "can throw genuinely broken payloads at the app" | "genuinely" is a house-banned virtue intensifier. The payloads are broken or they aren't. | "can throw broken payloads at the app" | UPDATE |
| 25 | | Restatement | l.94–97 | Treadmill / echo | "…not a continuous monitor. The server doesn't poll in the background. It only looks when you ask…" | The same fact is stated three times in three consecutive sentences, once as a snap-contrast and twice as restatement. | "Think of STAT the way a hospital uses the word: a fast, on-demand check of vital signs. The server looks only when you ask — no background timer, no polling loop." (Also resolves #14.) | UPDATE |
| 26 | | Restatement | l.169–172 | Echo | "so any buffered file-sink output is written before the process exits — the harness doesn't lose the last few lines of a log to an unflushed buffer" | The clause after the em-dash restates the clause before it in different words. | Keep the first, cut the second. | REMOVE |
| 27 | | AI-ism | l.218–222 | Self-labeling significance (3rd instance) + 47-word sentence | "That's the *Unix philosophy* letter of CUPID in practice: the catalog does one job…" | The most defensible of the three "That's X at work" constructions, since teaching CUPID is a real goal of the harness. But it is the third instance of the same shape in one document, and the sentence runs long enough to lose the thread. | Keep the CUPID lesson; change the framing and split the sentence. "This is the *Unix philosophy* letter of CUPID. The catalog does one job — describe a system. Version probing, process scanning, JSON serialization, and card rendering all compose off that one flat structure, so no system needs its own code path." | UPDATE |
| 28 | | Accuracy | l.215–216 | Imprecise scope | "Add the row, and the probe, the API response, and the dashboard card all pick it up automatically — nothing else in the file changes." | "in the file" is wrong or at least confusing: the reader just changed that file. The point is that nothing else in the *codebase* changes. | "…all pick it up automatically. No other file changes." | UPDATE |
| 29 | | Burstiness | l.293–299 | 62-word sentence, three embedded clauses | "It checks three layers: the format helpers always return a string…, `sanitizeStats` always produces…, and a full `App.vue` mount fed a mutated payload never throws…, whether the response is malformed JSON, a non-2xx status, or a rejected network request." | Longest sentence in the docset and hard to hold. The backend section two paragraphs up handles the identical "three invariants" shape better by keeping each invariant short. | Convert to a three-item list, matching the backend section's rhythm: "It checks three layers: / — the format helpers always return a string, whatever they're given; / — `sanitizeStats` always produces the contract shape the components expect; / — a full `App.vue` mount never throws, whether the response is malformed JSON, a non-2xx status, or a rejected request." | UPDATE |
| 30 | | Missing link | l.182–185 | Actionable gap | "If you want to see how the compat layer maps `Log.Information` calls onto Herald's engine, the source is there to read." | Tells the reader to go read source and gives no URL. This is the sentence carrying the inspectability selling point, so the missing link costs the most here. | Add the GitHub URL inline. Also rewrite the preceding negation: "Herald.OSS is open source — the compat layer that maps `Log.Information` onto Herald's engine is on GitHub at <url>, ready to read." (drops "not a black box behind the Serilog-shaped surface," which is a negation-splice answering an objection that only exists because of #12) | UPDATE |
| 31 | | Repetition | l.127 / l.168 | Same rhetorical move twice | "Serilog mode gets both for free" … "you get that for free without writing a single `Log.Information` call yourself" | "for free" twice in the same section, doing the same job. The second is also internally redundant ("for free" and "without writing…yourself" say the same thing). | Keep one. Second becomes: "…adds one line per HTTP request with the method, path, status code, and duration, without a `Log.Information` call of your own." | UPDATE |
| 32 | | AI-ism | l.257–258 | Mic-drop closer (2nd in doc) | "A harness with no tests is just an empty folder with extra steps." | Good line, and it lands. But it is the second manufactured punchline in the document (with "That's the point," l.28). The playbook says keep at most one. | Author's call — if #17 is applied, this one can stay and earns its place as the doc's single closer. If both stay, cut this one. | FLAG |
| 33 | | Em-dash | l.106–186 | Density (9 in one section) | (logging section) | Exceeds the single-category threshold of 5. | Fixes #16, #25, #26, and #30 remove four on their own. Convert one or two more to periods. | UPDATE |
| 34 | | Fluff | l.254, l.306 | Filler adverb | "regardless of what your API actually returns" / "until a project actually needs it" | "actually" is doing nothing in either sentence. | Cut both instances. | UPDATE |

---

## Issues — docs/PRD.md

| # | Steve | Check | Location | Tic / Pattern | Passage (quoted) | Issue | Suggested Fix | Rec |
|---|---|---|---|---|---|---|---|---|
| 35 | | Unmeasurable acceptance | l.30 | Acceptance criterion that can't be checked | "the dashboard renders them beautifully" | A PRD acceptance item is a gate — someone has to be able to say pass or fail. "Beautifully" can't be gated. The rest of the list (items 1, 4, 5) is properly checkable, which makes this one stand out. | "the dashboard renders one card per system showing installed state, running state, process count, and memory." Move the aesthetic target to a separate design-intent line. | UPDATE |
| 36 | | Internal jargon in a public doc | l.28 | Undefined reference in an OSS repo | "(Nolan/Barrymore register: dark cinematic + art-deco elegance)" | This PRD ships in an open-source repository. "Nolan/Barrymore register" is an internal MMPWorks shorthand — an outside contributor reading the PRD has no way to decode it. | Keep the intent, drop the codename: "(dark cinematic palette with art-deco detailing)". If the internal shorthand matters, define it once in a footnote. | UPDATE |
| 37 | | Unclear referent | l.31 | Self-referential to a conversation | "returns a hello-world payload (the \"initially serves hello-world\" contract)" | The quoted phrase points back at a spoken requirement the reader never heard. It reads like a note-to-self left in a shipped document. | Cut the parenthetical. The line is complete without it. | REMOVE |
| 38 | | Placeholder text | l.22 | Stale scaffold | "/docs   this PRD + anything else" | "anything else" was accurate when the PRD was written and is now stale — `docs/` holds a real how-to guide. | "/docs   this PRD + the how-to guide" | UPDATE |
| 39 | | Unexplained metaphor | l.37–38 | House metaphor without its context | "Seams stay open (stable route prefix `/api`, compose file per-service), rooms stay unbuilt." | "rooms stay unbuilt" is the AIF door/room metaphor, which reads clearly inside MMPWorks and opaquely outside it. The how-to states the same idea in plain words at l.303–306 and is better for it. | "Seams stay open — a stable `/api` route prefix, one compose service per external dependency — and the machinery waits until a project needs it." | UPDATE |
| 40 | | Awkward construction | l.5–6 | Dangling preposition + em-dash inside parentheses | "used to start new MMPWorks projects (and to record YouTube videos from — first up: MMP.SlotGame)" | "record videos from" strands the preposition, and the em-dash inside the parenthetical stacks two interruptions on one clause. | "An open-source project harness: a ready-to-clone frontend/backend bootstrap for starting new MMPWorks projects. It also serves as the recording set for MMPWorks YouTube videos, starting with MMP.SlotGame." | UPDATE |
| 41 | | Consistency | l.29 | Ellipsis where the other docs are closed | "(Claude Code, GitHub Copilot, Codex, Cursor, Gemini, Ollama, …)" | The README and how-to both list the same six with no trailing ellipsis. In an acceptance criterion the ellipsis is a genuine problem: it makes the gate unbounded. | Drop the ellipsis. The extensibility point belongs in the how-to's "Add a new AI system" section, which already covers it. | UPDATE |
| 42 | | AI-ism | l.12–13 | Mic-drop closer | "Build it once, clone it forever." | Punchy, and a PRD tolerates punch better than a guide does. But note it's the third "Clone it, X it" imperative pair across the three files (README l.4, how-to l.22). | Author's call. If kept, vary one of the other two so the construction doesn't triple. | FLAG |
| 43 | | Missing metadata | l.1 | Document hygiene | "# MMP.WorkHarnesses — PRD (v1)" | No date, owner, or status line. The how-to has proper frontmatter (`last-reviewed: 2026-08-10`); the PRD has none, so a future reader can't tell whether v1 acceptance has been met. | Add a two-line header: status (Accepted / Delivered) and date. Given the harness ships and the tests pass, this PRD's acceptance is presumably met — say so. | UPDATE |

---

## Logic & plausibility notes

Adapted from scene logic to "does the doc match the code."

- **Verified accurate.** Package versions in the how-to table (l.176–180) match
  `WorkHarness.Server.csproj` exactly: Herald.OSS 0.12.11, MMP.Herald.Serilog.AspNetCore
  0.12.8, MMP.Herald.Sinks.File 0.2.1. The `AiSystemProbe.CaptureAsync` and
  `AiSystemProbe.Catalog` references resolve to real members in `AiSystems.cs`, and
  the catalog is a flat `CatalogEntry[]` exactly as described, with the four fields
  named in the correct order (Id, Name, VersionCommands, ProcessKeys).
- **Two overclaims** flagged in the table as accuracy issues: the "resource
  footprint at zero" line (#14) and the "flaky isn't a category of bug" line (#15).
  Both are the kind of claim a reader will test against reality and find wanting,
  and both have a true version that is nearly as strong.
- **One house-rule violation** (#12) — "invisibly" implying hidden Herald
  internals. This is the finding to fix first regardless of what else gets touched.
- **One structural note, not a line fix.** The how-to promises a comparison the
  README advertised (#7) and doesn't deliver it. Either the README's promise
  shrinks or the how-to gains a sentence. Handing this back rather than patching
  it, since the right answer depends on whether the direct-Herald path was
  actually considered and rejected.

---

## Net

Fix three things and the docset is in good shape: **"invisibly" on how-to line 119**
(house rule, one word), the **README/how-to duplication** in the logging and testing
sections (structural, biggest maintenance win), and the **DRY paragraph at how-to
lines 152–157** (the densest AI-shaped moment in the docset — triad mic-drop plus
self-labeling significance plus a banned intensifier in two sentences). Everything
else is a light touch: five negation-tic rewrites, three superfluous adverbs, an
em-dash thinning pass in the how-to's logging section, and the PRD's two
unmeasurable acceptance lines. The writing itself is already concrete and unhyped —
this is a tightening pass, not a rescue.

---

## Cussler disposition — 2026-08-10

Ghost-edit applied in place to all three files. **Applied: 30. Declined or resolved
differently: 2. Author's-call items ruled: 2.**

### Measured before / after

| File | tic-lint hits | Em-dashes |
|---|---|---|
| `README.md` | 21 → 8 | 15 → 8 |
| `docs/how-to-use-this-harness.md` | 52 → 11 | 25 → 9 |
| `docs/PRD.md` | 4 → 4 | 4 → 4 |
| **Total** | **77 → 23** | **44 → 21** |

Of the 21 remaining em-dashes, 8 sit inside code blocks (kept byte-identical) and
most of the rest are the bullet term-definition form (`**Console** — …`), which is a
list convention rather than a connector tic. No package version, path, command, or
number was changed anywhere.

### Applied

Findings **1–11** (README), **12–31, 33, 34** (how-to), and **35–41, 43** (PRD) are
all applied, in most cases with Clancy's suggested wording or a close variant.

Three worth calling out because the wording departs from the suggestion:

- **#12** (the house-rule fix). Landed as "running on the Herald engine underneath,"
  with the public-source point moved down to the Packages section where finding #30's
  GitHub link now lives. Clancy's suggested wording stated the source was public in
  both places; keeping it once avoids restating the same fact twice in one section.
- **#7** (the promise the how-to doesn't fulfill). Resolved by shrinking the README
  promise to "why the harness logs this way" rather than adding a direct-Herald
  comparison to the guide. Writing that comparison would have meant inventing a
  rationale for a path there's no evidence was weighed — outside a ghost-writer's
  remit. The how-to's section heading changed to match. **Flagging for Steve:** if the
  direct-Herald path *was* considered and rejected, that reasoning is worth a sentence
  in the how-to, and only you can supply it.
- **#30**. Repo URL verified from the Herald.OSS git remote rather than assumed:
  `https://github.com/mmpworks/Herald.OSS`.

### Author's-call items, ruled

- **#32** — "A harness with no tests is just an empty folder with extra steps."
  **Kept.** Since #17 removed "That's the point," this is now the document's only
  manufactured closer, which is the budget the playbook allows.
- **#42** — "Build it once, clone it forever." **Cut**, rather than kept-and-varied.
  It was the third instance of the same imperative-pair construction across the three
  files, and a PRD's "Why" section is the one place that can afford to state the
  reason without a slogan. The line now reads: "Build the skeleton once and every
  project after it starts from a working stack."

### Beyond Clancy's list

A handful of tics the line-level table didn't catch, fixed in passing: "the whole
stack" → "the stack" (how-to intro), "shape" as a vague category in the section
heading "Why the harness picked this shape" and in "a backend field changes shape"
(→ "changes form"), one stray "actually" in the how-to's opening paragraph, and the
README's dev-loop pointer, which duplicated itself once I added the
production-shaped label from #10.

---

## Cussler round-2 disposition — 2026-08-10

Scoped ghost-edit over the sections Heather rewrote for Herald native mode and the
14-level set: the README Logging section, and the how-to's Logging section (including
"The 14-level set" and "Two upstream findings, pinned as tests"), "Why the harness
logs this way", the Packages caption, and the "Add a new API endpoint" prose.
**9 edits applied.** tic-lint hits: `README.md` 11 → 8, `how-to-use-this-harness.md`
21 → 11; both files are now clean inside the edited sections, and every remaining hit
sits outside this round's scope or is the bullet term-definition form
(`**Console** — …`). No package version, level name, level order, path, or command
line changed. One rendering fix went in alongside the prose: the code span
`` `WorkHarnessLevels.AtOrAbove(...)` `` had been broken across a line wrap, which
rendered as `WorkHarnessLevels. AtOrAbove(...)` with a stray space; the sentence was
recast so the span stays intact. The upstream-findings bullets keep their plain
statement of what the engine does today.
