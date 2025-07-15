namespace TransactionsExample.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Stock { get; set; }

        public double Price { get; set; }
    }
}
