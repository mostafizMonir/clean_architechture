using MassTransit;

namespace Masstransit.beginner.Services;


public class MessagePublisher(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Publish(
                new CurrentTime
                {
                    Value = $"current time is {DateTime.UtcNow}"
                },
                stoppingToken
                );
            await Task.Delay(1000, stoppingToken);
        }
    }
}
