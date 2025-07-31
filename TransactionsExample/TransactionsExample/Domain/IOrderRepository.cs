namespace TransactionsExample.Domain;

public interface IOrderRepository
{
    Task<Order?> GetOne(int id);
    Task<List<Order>> GetAll();
    Task AddWithSave(Order order);
    Task Add(Order order);
}