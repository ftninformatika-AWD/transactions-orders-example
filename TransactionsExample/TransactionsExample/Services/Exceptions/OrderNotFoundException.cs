﻿namespace TransactionsExample.Services.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(int id) : base("order", id)
    {
    }
}