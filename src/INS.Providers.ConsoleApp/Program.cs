// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

namespace INS.Providers.ConsoleApp;

using System;

using INS.Provider.AngleSharpScrapper.Parser;
using INS.Provider.Scrapper;
using INS.Provider.Scrapper.Parser;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

public class Program
{
    static async Task Main(string[] args)
    {

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .CreateLogger();

        Log.Logger.Information("INS Providers Starting ...");

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient<IParserStrategy, INSProvidersPageAngleSharpStrategy>();
                services.AddTransient<IINSProvidersPageScrapper, INSProvidersPageScrapper>();
            })
            .UseSerilog()
            .Build();
        

        var scrapper = ActivatorUtilities.CreateInstance<INSProvidersPageScrapper>(host.Services);
        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

        await scrapper.Parse(
            new Uri(config.GetValue<string>("INS:ProvidersPageURL")), "body a[href$=\".xlsx\"]");

        await host.RunAsync();



    }

    private static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", true)
            .AddEnvironmentVariables();
    }
}
