using Microsoft.AspNetCore.Mvc;
using TransactionsExample.DTOs;
using TransactionsExample.Services;

namespace TransactionsExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await _orderService.GetAll());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        return Ok(await _orderService.GetOne(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostApartment(NewOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }
        var savedOrder = await _orderService.Add(order);
        return Created(string.Empty, savedOrder);
    }
}