using System.Runtime.Loader;
using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Diplom.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace Diplom.MVC.Controllers;

public class SourceController : Controller
{
    private ISourceService _sourceService;

    public SourceController(ISourceService sourceService)
    {
        _sourceService = sourceService;
    }
    // GET
    public async Task<IActionResult> Index() => View(await _sourceService.GetAllSourcesAsync());
    public RedirectToRouteResult GetGoods(Guid id) => RedirectToRoute(new { controller="Good", action="Index", id});
    
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateSourceViewModel model)
    {
        if (ModelState.IsValid)
        {
            var source = new SourceDto() { Name = model.Name };
            var result = await _sourceService.CreateSourceAsync(source);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var source = await _sourceService.GetSourceByIdAsync(id);
        if (source is null)
        {
            return NotFound();
        }

        return View(source);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SourceDto model)
    {
        if (ModelState.IsValid)
        {
            await _sourceService.UpdateSourceAsync(model);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _sourceService.DeleteSourceByIdAsync(id);
        return RedirectToAction("Index");
    }
}