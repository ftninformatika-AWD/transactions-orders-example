using AutoMapper;
using TransactionsExample.DTOs;
using TransactionsExample.Models;

namespace TransactionsExample.Settings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>();
    }
}