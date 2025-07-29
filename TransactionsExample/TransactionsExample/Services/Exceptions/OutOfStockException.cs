namespace TransactionsExample.Services.Exceptions;

public class OutOfStockException : BadRequestException
{
    public OutOfStockException(string productName) : base($"Out of stock of product {productName}.")
    {
    }
}