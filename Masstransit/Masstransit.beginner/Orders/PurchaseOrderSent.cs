using Masstransit.beginner.Models;

namespace Masstransit.beginner.Orders;

public class PurchaseOrderSent
{
    public PurchaseOrderSent(Guid orderId)
    {
        
    }

    public string Symbol { get; set; }
    public Guid OrderId { get; set; }
    public string Price { get; set; }
}
