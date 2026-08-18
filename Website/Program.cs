using Nocturne.Database;
using Serilog;
using Serilog.Sinks.SpectreConsole;
using Website.Components;

namespace Website;

public class Program
{
    public static readonly string DATA_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
    public static readonly string DATABASE_PATH = Path.Combine(DATA_PATH, "database.nocturne");

    public static readonly NocturneDatabase NOCTURNE_DATABASE = new NocturneDatabase
    {
        FilePath = DATABASE_PATH,
        AutomaticallyCompact = true,
        CompactOnLaunch = true
    };

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Directory.CreateDirectory(DATA_PATH);
        NOCTURNE_DATABASE.Open();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.SpectreConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u4}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        builder.Services.AddSerilog();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
