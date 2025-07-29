namespace TransactionsExample.Domain;

public interface IOrderRepository
{
    Task<Order?> GetOne(int id);
    Task<List<Order>> GetAll();
    Task Add(Order order);
    Task AddWithoutSave(Order order);
}