using TransactionsExample.Services.DTOs;

namespace TransactionsExample.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetAll();
    Task<OrderDto> GetOne(int id);
    Task<OrderDto> Add(NewOrderDto order);
}