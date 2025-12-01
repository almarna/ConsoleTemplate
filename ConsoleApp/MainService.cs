using ConsoleApp.dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp;

public interface IMainService: IAsyncDisposable
{
    Task RunAsync();
}

public class MainService(IOptions<AppSettingsDto> options, ILogger<MainService> logger) : IMainService
{
    private readonly AppSettingsDto _settings = options.Value;

    public async Task RunAsync()
    {
        logger.LogInformation("Starting {appName} with interval {interval}s",
            _settings.AppName, _settings.RunIntervalSeconds);

        for (int i = 0; i < 5; i++)
        {
            logger.LogInformation("Tick {count}", i + 1);
            await Task.Delay(_settings.RunIntervalSeconds * 1000);
        }

        logger.LogInformation("Finished.");
    }

    public async ValueTask DisposeAsync()
    {
        // Placeholder
        await Task.Delay(1);
    }
}