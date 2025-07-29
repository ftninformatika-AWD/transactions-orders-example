using AutoMapper;
using TransactionsExample.Domain;
using TransactionsExample.Services.DTOs;

namespace TransactionsExample.Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>();
    }
}