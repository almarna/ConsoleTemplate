using ConsoleApp;
using ConsoleApp.dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = ConfigurationLoader.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// 'using' fixes cleanup like serilog flush
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettingsDto>(appSettingsSection);
        services.AddTransient<IMainService, MainService>();
    })
    .UseSerilog()
    .Build();

var mainService = host.Services.GetRequiredService<IMainService>();
await mainService.RunAsync();

