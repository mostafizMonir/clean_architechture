using Contracts;
using MassTransit;

namespace Newletter.Reporting.API.Consumers;

public class ArticleCreatedConsumer:IConsumer<AnotherExchange>
{
    public Task Consume(ConsumeContext<AnotherExchange> context)
    {
        var msg = context.Message.Msg;
        Console.Write(msg);
        return Task.CompletedTask;
    }
}
