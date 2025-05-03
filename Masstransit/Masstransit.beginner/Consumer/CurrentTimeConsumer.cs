using MassTransit;
using Masstransit.beginner.Services;

namespace Masstransit.beginner.Consumer;

public class CurrentTimeConsumer(ILogger<CurrentTimeConsumer> logger):IConsumer<CurrentTime>
{
    public Task Consume(ConsumeContext<CurrentTime> context)
    {
        logger.LogInformation("{Consumer} : {Message}", nameof(CurrentTimeConsumer),context.Message.Value);
        return Task.CompletedTask;
    }
}

public class CurrentTimeConsumerV2(ILogger<CurrentTimeConsumerV2> logger):IConsumer<CurrentTime>
{
    public Task Consume(ConsumeContext<CurrentTime> context)
    {
        logger.LogInformation("{Consumer} : {Message}", nameof(CurrentTimeConsumerV2),context.Message.Value);
        return Task.CompletedTask;
    }
}
