namespace TransactionsExample.Services.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int id) : base("product", id)
    {
    }
}