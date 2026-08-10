using Microsoft.Extensions.FileProviders;
using WorkHarness.Server;

var builder = WebApplication.CreateBuilder(args);
// ASPNETCORE_URLS wins (docker binds 0.0.0.0 there); localhost:5090 is the dev default.
builder.WebHost.UseUrls(builder.Configuration["urls"] ?? "http://localhost:5090");

builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .WithOrigins("http://localhost:5173")   // Vite dev server
    .AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors();

app.MapGet("/api/hello", () => Results.Ok(new
{
    message = "Hello from MMP.WorkHarnesses",
    harness = "CSharp",
    serverTimeUtc = DateTime.UtcNow,
}));

app.MapGet("/api/stats", async (CancellationToken ct) =>
    Results.Ok(await AiSystemProbe.CaptureAsync(ct)));

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

app.Run();
