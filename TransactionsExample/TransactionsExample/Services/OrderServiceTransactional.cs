﻿using AutoMapper;
using TransactionsExample.Domain;
using TransactionsExample.Infrastructure.Repositories;
using TransactionsExample.Services.DTOs;
using TransactionsExample.Services.Exceptions;

namespace TransactionsExample.Services;

public class OrderServiceTransactional : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public OrderServiceTransactional(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper,
        AppDbContext dbContext
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _dbContext = dbContext;
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

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await _orderRepository.Add(newOrder);
            await _productRepository.RemoveFromStock(product, order.Count);

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

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