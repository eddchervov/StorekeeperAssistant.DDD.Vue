using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StorekeeperAssistant.DataAccess;
using System;

#nullable disable

namespace StorekeeperAssistant.Web;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ConfigureLogging();

            var host = CreateHostBuilder(args)
                .Build();
            
            InitializationDb(host);

            Log.Logger.Information("StorekeeperAssistant.Web application is running");
            host.Run();
        }
        catch (Exception ex)
        {
            Log.Logger.Error("StorekeeperAssistant.Web stopped program because of exception");
            throw;
        }
    }

    private static void ConfigureLogging()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var loggerConfiguration = new LoggerConfiguration().Enrich.FromLogContext();

        if (configuration["Seq:Url"] is { } seqUrl)
        {
            loggerConfiguration = loggerConfiguration
                .WriteTo.Seq(seqUrl);
        }

        Log.Logger = loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static void InitializationDb(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
            });
}
