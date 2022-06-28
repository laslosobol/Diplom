using System.Device.Location;
using System.Diagnostics;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using Diplom.Core.UnitOfWork;
using Diplom.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using PayPal;
using GeoCoordinate = GeoCoordinatePortable.GeoCoordinate;
using Order = Diplom.Core.Entities.Order;


namespace Diplom.MVC.Controllers;

public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserManager<User> _userManager;

    private readonly IConfiguration _configuration;

    public CartController(IUnitOfWork unitOfWork, UserManager<User> userManager, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _configuration = configuration;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var entities = await _unitOfWork.GoodOrderRepository.GetAllAsync();
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        var userId = _userManager.GetUserId(User);
        var goods = await _unitOfWork.GoodRepository.GetAllAsync();
        var cart = orders.FirstOrDefault(_ => _.CustomerId == new Guid(userId) && _.Status == Status.Cart);
        if (cart is null)
        {
            cart = new Order()
                { CustomerId = new Guid(userId), Status = Status.Cart, TotalPrice = 0, Location = new Point(50.19, 26.38), Description = ""};
            await _unitOfWork.OrderRepository.InsertAsync(cart);
        }

        var result = entities.Where(_ => _.OrderId == cart.Id);
        await _unitOfWork.CommitAsync();
        List<string> GoodNames = new List<string>();
        foreach (var goodOrder in result)
        {
            GoodNames.Add(goods.Where(_ => _.Id == goodOrder.GoodId).Select(_ => _.Name).First());
        }

        return View(new Tuple<Guid, List<string>, List<int>, List<double>, double>(cart.Id, GoodNames,
            result.Select(_ => _.Amount).ToList(), result.Select(_ => _.GoodPrice).ToList(), cart.TotalPrice));
    }

    public async Task<IActionResult> OrderCart(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        var model = new OrderCartViewModel(){Id = order.Id, IsCardPayment = false, TotalPrice = order.TotalPrice};
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> OrderCart(OrderCartViewModel model)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.Id);

        order.Location = new Point(model.Latitude, model.Longitude);
        order.Status = Status.New;
        order.CreatedOn = DateTime.UtcNow;
        order.Description = model.Description;
        if (model.Description is not null && model.TotalPrice == 0)
        {
            order.OrderType = OrderType.Special;
        }
        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.CommitAsync();

        return RedirectToAction("Index", "Home");
    }

    public void GoogleMap(){}
}