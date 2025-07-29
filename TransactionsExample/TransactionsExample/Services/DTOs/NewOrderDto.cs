using System.ComponentModel.DataAnnotations;

namespace TransactionsExample.Services.DTOs;

public class NewOrderDto
{
    public int ProductId { get; set; }

    [Required]
    public required string CustomerName { get; set; }

    [Range(1, double.MaxValue)]
    public int Count { get; set; }
}