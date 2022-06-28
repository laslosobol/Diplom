using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Api.Controllers;

[ApiController]
[Route("/order")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpPost("add")]
    public async Task Register(OrderDto orderDto)
    {
        var user = await _orderService.CreateOrderAsync(orderDto);
    }
}