using ExpenseTracker.Client.Pages;
using ExpenseTracker.Components;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor;
using Syncfusion.Telemetry;

var builder = WebApplication.CreateBuilder(args);
Telemetry.Disable();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<SfDialogService>();
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// SEO: Add X-Robots-Tag header for search engine indexing
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Robots-Tag", "index, follow");
    await next();
});

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(About).Assembly);

app.Run();
