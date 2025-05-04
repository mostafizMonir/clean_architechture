using Contracts;
using MassTransit;

namespace Newletter.Reporting.API.Consumers;

public class AnotherConsumer:IConsumer<AnotherExchange>
{
    public Task Consume(ConsumeContext<AnotherExchange> context)
    {
        
        return Task.CompletedTask;
    }
}
