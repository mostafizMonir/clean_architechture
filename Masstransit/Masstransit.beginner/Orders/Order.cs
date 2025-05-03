namespace Masstransit.beginner.Orders;

public class Order
{
    public string Symbol { get; set; }
    public Guid Id { get; set; }
    public int LimitPrice { get; set; }
    public int Quantity { get; set; }
}
