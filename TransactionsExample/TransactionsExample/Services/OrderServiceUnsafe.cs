using AutoMapper;
using TransactionsExample.Exceptions;
using TransactionsExample.Domain;
using TransactionsExample.Services.DTOs;

namespace TransactionsExample.Services;

public class OrderServiceUnsafe : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderServiceUnsafe(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Add(NewOrderDto order)
    {
        Product? product = await _productRepository.GetOne(order.ProductId);
        if (product == null)
        {
            throw new ProductNotFoundException(order.ProductId);
        }

        // provera da li ima dovoljno proizvoda na stanju
        if (product.Stock < order.Count)
        {
            throw new OutOfStockException(product.Name);
        }

        Order newOrder = new Order()
        {
            Count = order.Count,
            CustomerName = order.CustomerName,
            ProductId = product.Id,
            TotalPrice = order.Count * product.Price
        };

        await _orderRepository.Add(newOrder);
        await _productRepository.RemoveFromStock(product, order.Count);
        return _mapper.Map<OrderDto>(newOrder);
    }

    public async Task<List<OrderDto>> GetAll()
    {
        var orders = await _orderRepository.GetAll();
        return orders
            .Select(_mapper.Map<OrderDto>)
            .ToList();
    }

    public async Task<OrderDto> GetOne(int id)
    {
        var order = await _orderRepository.GetOne(id);
        if (order == null)
        {
            throw new OrderNotFoundException(id);
        }
        return _mapper.Map<OrderDto>(order);
    }
}