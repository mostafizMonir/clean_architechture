using Contracts;
using MassTransit;

namespace Newletter.Reporting.API.Consumers;

public class ArticleCreatedConsumer:IConsumer<ArticleCreated>
{
    public Task Consume(ConsumeContext<ArticleCreated> context)
    {
        
        return Task.CompletedTask;
    }
}
