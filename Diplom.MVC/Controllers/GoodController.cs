using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Diplom.Core.Entities;
using Diplom.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.MVC.Controllers;

public class GoodController : Controller
{
    private IGoodService _goodService;
    private UserManager<User> _userManager;

    public GoodController(IGoodService goodService, UserManager<User> userManager)
    {
        _goodService = goodService;
        _userManager = userManager;
    }

    // GET
    public async Task<IActionResult> Index(Guid id) => View(await _goodService.GetAllGoodBySource(id));

    public async Task<IActionResult> All() => View(await _goodService.GetAllGoodsAsync());

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid id)
    {
        var userId = _userManager.GetUserId(User);
        await _goodService.AddToCart(id, new Guid(userId), 1);

        return RedirectToAction("Index", "Source");
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateGoodViewModel model)
    {
        if (ModelState.IsValid)
        {
            var good = new GoodDto() { Name = model.Name, Price = model.Price, SourceId = model.SourceId };
            var result = await _goodService.CreateGoodAsync(good);
            return RedirectToAction("All");
        }

        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var good = await _goodService.GetGoodByIdAsync(id);
        if (good is null)
        {
            return NotFound();
        }

        return View(good);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GoodDto model)
    {
        if (ModelState.IsValid)
        {
            await _goodService.UpdateGoodAsync(model);
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _goodService.DeleteGoodByIdAsync(id);
        return RedirectToAction("All");
    }
}