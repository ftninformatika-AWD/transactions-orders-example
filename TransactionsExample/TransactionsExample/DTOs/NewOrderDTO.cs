using System.ComponentModel.DataAnnotations;

namespace TransactionsExample.DTOs
{
    public class NewOrderDTO
    {
        public int ProductId { get; set; }

        [Required]
        public required string CustomerName { get; set; }

        [Range(1, Double.MaxValue)]
        public int Count { get; set; }
    }
}
