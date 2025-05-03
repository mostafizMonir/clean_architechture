namespace Masstransit.beginner.Orders;

public class PurchaseOrderRequest
{
    public string Symbol { get; set; }
    public int LimitPrice { get; set; }
    public int Quantity { get; set; }
}
