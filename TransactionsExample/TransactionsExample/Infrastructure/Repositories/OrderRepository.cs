﻿using Microsoft.EntityFrameworkCore;
using TransactionsExample.Domain;

namespace TransactionsExample.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetAll()
    {
        return await _dbContext.Orders
            .Include(o => o.Product)
            .ToListAsync();
    }

    public async Task<Order?> GetOne(int id)
    {
        return await _dbContext.Orders
            .Include(o => o.Product)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddWithSave(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.Entry(order).Reference(o => o.Product).LoadAsync();
        await _dbContext.SaveChangesAsync();
    }

    public async Task Add(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.Entry(order).Reference(o => o.Product).LoadAsync();
    }
}