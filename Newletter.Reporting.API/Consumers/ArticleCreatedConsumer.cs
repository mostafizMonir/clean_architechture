using Contracts;
using MassTransit;

namespace Newletter.Reporting.API.Consumers;

public class ArticleCreatedConsumer:IConsumer<ArticleViewed>
{
    public Task Consume(ConsumeContext<ArticleViewed> context)
    {
        
        return Task.CompletedTask;
    }
}
