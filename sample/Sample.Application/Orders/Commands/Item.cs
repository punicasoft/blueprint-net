namespace Sample.Application.Orders.Commands;

public class Item
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Units { get; set; }
}