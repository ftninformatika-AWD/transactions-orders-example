using AutoMapper;
using TransactionsExample.DTOs;
using TransactionsExample.Exceptions;
using TransactionsExample.Models;
using TransactionsExample.Repositories;

namespace TransactionsExample.Services;

public class OrderServiceUoW : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderServiceUoW(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Add(NewOrderDto order)
    {
        Product? product = await  _unitOfWork.ProductRepository.GetOne(order.ProductId);
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
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.OrderRepository.Add(newOrder);
            await _unitOfWork.ProductRepository.RemoveFromStock(product, order.Count);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        return _mapper.Map<OrderDto>(newOrder);
    }

    public async Task<List<OrderDto>> GetAll()
    {
        var orders = await _unitOfWork.OrderRepository.GetAll();
        return orders
            .Select(_mapper.Map<OrderDto>)
            .ToList();
    }

    public async Task<OrderDto> GetOne(int id)
    {
        var order = await _unitOfWork.OrderRepository.GetOne(id);
        if (order == null)
        {
            throw new OrderNotFoundException(id);
        }
        return _mapper.Map<OrderDto>(order);
    }
}