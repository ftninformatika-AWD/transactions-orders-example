namespace TransactionsExample.Domain;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required string CustomerName { get; set; }

    public int Count { get; set; }

    public double TotalPrice { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }
}