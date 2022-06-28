using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Diplom.Api.Controllers;

[ApiController]
[Route("/source")]
public class SourceController : Controller
{
    private readonly ISourceService _sourceService;

    public SourceController(ISourceService sourceService)
    {
        _sourceService = sourceService;
    }
    
    [HttpPost("add")]
    public async Task Register(SourceDto sourceDto)
    {
        var user = await _sourceService.CreateSourceAsync(sourceDto);
    }
}