using TransactionsExample.DTOs;

namespace TransactionsExample.Services
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAll();
        Task<OrderDTO> GetOne(int id);
        Task<OrderDTO> Add(NewOrderDTO order);
    }
}
