using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Diplom.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Api.Controllers;

[ApiController]
[Route("/good")]
public class GoodController : Controller
{
    private readonly IGoodService _goodService;

    public GoodController(IGoodService goodService)
    {
        _goodService = goodService;
    }
    [HttpPost("add")]
    public async Task Register(GoodDto goodDto)
    {
        var user = await _goodService.CreateGoodAsync(goodDto);
    }
}