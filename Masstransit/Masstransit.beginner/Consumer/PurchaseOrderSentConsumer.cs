using MassTransit;
using Masstransit.beginner.Models;
using Masstransit.beginner.Orders;

namespace Masstransit.beginner.Consumer;

public class PurchaseOrderSentConsumer(StocksClient stocksClient):IConsumer<PurchaseOrderSent>
{
    public async Task Consume(ConsumeContext<PurchaseOrderSent> context)
    {
        var stocPriceResponse = await stocksClient.GetStockPrice(context.Message.Symbol);

        await context.Publish(new PurchaseOrderSent(context.Message.OrderId)
        {
            Symbol = context.Message.Symbol,
            Price = stocPriceResponse
        });

        Console.WriteLine($"Purchase order  consumed {context.Message.Symbol} with price  ");

    }
}
