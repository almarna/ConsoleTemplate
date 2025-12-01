using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ConsoleApp;

public static class ConfigurationLoader
{
    public static IConfigurationRoot Build(string fileName = "appsettings.json")
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: false, reloadOnChange: true)
                .Build();

            Log.Information("Configuration [{fileName}] loaded successfully.", fileName);
            return config;
        }
        catch (FileNotFoundException)
        {
            Log.Fatal("Missing configuration file: {FileName}. Please provide it before starting the application.", fileName);
            Environment.Exit(1);
        }
        catch (FormatException ex) when (ex.InnerException is JsonException jsonEx)
        {
            Log.Fatal("Configuration file {FileName} is malformed: {Message}", fileName, jsonEx.Message);
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unexpected error while loading configuration: {FileName}", fileName);
            Environment.Exit(1);
        }

        throw new InvalidOperationException("Will never throw!");
    }
}