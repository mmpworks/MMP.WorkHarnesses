using Microsoft.Extensions.FileProviders;
using MMP.Herald.Events;
using MMP.Herald.Quick;
using MMP.Herald.Templating;
using WorkHarness.Server;

// ---- Herald.OSS, native mode, with the harness's custom 14-level set. ------------
// Rendered console sink for the terminal, rolling NDJSON file sink (one JSON object
// per line — extension drives the format) for the structured record. The sys.* levels
// carry framework noise (routed there by SystemAwareHeraldProvider); the plain
// levels carry application signal. See WorkHarnessLevels for the ordering rationale.
var herald = QuickLogBuilder.Create("workharness")
    .WithConsoleSink()
    .WithFileSink("logs/workharness-.ndjson",
        interval: "daily",
        maxBytes: 10 * 1024 * 1024,
        maxRetainedFiles: 5)
    .WithCustomLevel(WorkHarnessLevels.SysVerbose, "SysVerbose")
    .WithCustomLevel(WorkHarnessLevels.SysDebug, "SysDebug")
    .WithCustomLevel(WorkHarnessLevels.SysInformation, "SysInformation")
    .WithCustomLevel(WorkHarnessLevels.SysWarning, "SysWarning")
    .WithCustomLevel(WorkHarnessLevels.Comms, "Comms")
    .WithCustomLevel(WorkHarnessLevels.Money, "Money")
    .WithCustomLevel(WorkHarnessLevels.Math, "Math")
    .WithCustomLevel(WorkHarnessLevels.Simulation, "Simulation")
    .WithLevelOrder(WorkHarnessLevels.Order)
    .WithMinimumLevel(WorkHarnessLevels.SysInformation)
    .WithCustomFilter(WorkHarnessLevels.AtOrAbove(WorkHarnessLevels.SysInformation))
    .BuildAndCommit();

var log = herald.Logger;
var appCategory = new LogCategory("WorkHarness");

var builder = WebApplication.CreateBuilder(args);
// ASPNETCORE_URLS wins (docker binds 0.0.0.0 there); localhost:5090 is the dev default.
builder.WebHost.UseUrls(builder.Configuration["urls"] ?? "http://localhost:5090");

builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .WithOrigins("http://localhost:5173")   // Vite dev server
    .AllowAnyHeader().AllowAnyMethod()));

// Framework categories -> sys.* levels, app categories -> plain levels.
builder.Logging.ClearProviders();
builder.Logging.AddProvider(new SystemAwareHeraldProvider(log));

var app = builder.Build();
app.UseCors();

app.MapGet("/api/hello", () =>
{
    log.Information(appCategory, "Hello endpoint served for harness {Harness}", new LogProperty("Harness", "CSharp"));
    return Results.Ok(new
    {
        message = "Hello from MMP.WorkHarnesses",
        harness = "CSharp",
        serverTimeUtc = DateTime.UtcNow,
    });
});

app.MapGet("/api/stats", async (CancellationToken ct) =>
{
    // Domain levels in action: comms for the probe conversation, math for the tally.
    log.Log(WorkHarnessLevels.CommsLevel, appCategory, "STAT probe requested",
        properties: null, context: null, eventId: null);
    var snapshot = await AiSystemProbe.CaptureAsync(ct);
    log.Log(WorkHarnessLevels.MathLevel, appCategory,
        "STAT tally: {SystemCount} systems, {RunningCount} running, {ProcessCount} processes",
        properties:
        [
            new LogProperty("SystemCount", snapshot.Systems.Count),
            new LogProperty("RunningCount", snapshot.Systems.Count(s => s.Running)),
            new LogProperty("ProcessCount", snapshot.Systems.Sum(s => s.ProcessCount)),
        ],
        context: null, eventId: null);
    return Results.Ok(snapshot);
});

// ---- SPA hosting: built web/dist in dev checkout, wwwroot inside the container. ----
string[] spaCandidates =
[
    Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "..", "web", "dist")),
    Path.Combine(app.Environment.ContentRootPath, "wwwroot"),
];
var spaRoot = spaCandidates.FirstOrDefault(Directory.Exists);
if (spaRoot is not null)
{
    var files = new PhysicalFileProvider(spaRoot);
    app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = files });
    app.UseStaticFiles(new StaticFileOptions { FileProvider = files });
    app.MapFallbackToFile("index.html", new StaticFileOptions { FileProvider = files });
}
else
{
    app.MapGet("/", () => Results.Text(
        "MMP.WorkHarnesses server is up. Build the SPA first: cd CSharp/web && npm install && npm run build",
        "text/plain"));
}

log.Information(appCategory, "WorkHarness server starting; SPA root: {SpaRoot}", new LogProperty("SpaRoot", spaRoot ?? "(not built)"));
try
{
    app.Run();
}
finally
{
    await herald.DisposeAsync();   // drain the pipeline — file lines land before exit
}

// Exposes the entry point to WebApplicationFactory<Program> in the test project.
public partial class Program;
