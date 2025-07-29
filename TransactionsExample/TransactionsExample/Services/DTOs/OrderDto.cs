namespace TransactionsExample.Services.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public required string CustomerName { get; set; }

    public int Count { get; set; }

    public double TotalPrice { get; set; }

    public int ProductId { get; set; }

    public required string ProductName { get; set; }
}