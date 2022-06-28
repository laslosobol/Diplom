using System.Security.Claims;
using Diplom.BLL.Interfaces;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using Diplom.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using TatukGIS.RTL;

namespace Diplom.MVC.Controllers;

public class OrderController : Controller
{
    private UserManager<User> _userManager;
    private IUnitOfWork _unitOfWork;
    private IOrderService _orderService;

    public OrderController(IOrderService orderService, IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _orderService = orderService;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    // GET
    public async Task<IActionResult> Index(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        var goodOrders = await _unitOfWork.GoodOrderRepository.GetAllAsync();
        var resultGoodOrders = goodOrders.Where(_ => _.OrderId == order.Id);
        var model = new TakeOrderViewModel() 
        { 
            OrderId = order.Id,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            GoodDesc = new Tuple<List<string>, List<string>, List<int>>(new List<string>(), new List<string>(), new List<int>())
        };
        var allGoods = await _unitOfWork.GoodRepository.GetAllAsync();
        var allSources = await _unitOfWork.SourceRepository.GetAllAsync();
        foreach (var el in resultGoodOrders)
        {
            var good = allGoods.First(_ => _.Id == el.GoodId);
            model.GoodDesc.Item1.Add(good.Name);
            model.GoodDesc.Item2.Add(allSources.First(_ => _.Id == good.SourceId).Name);
            model.GoodDesc.Item3.Add(el.Amount);
            
        }
        return View(model);
    }

    public async Task<IActionResult> AvailableOrders()
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        var availableOrders = orders.Where(_ => _.Status == Status.New);

        return View(availableOrders);
    }

    public async Task<IActionResult> TakeOrder(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        var goodOrders = await _unitOfWork.GoodOrderRepository.GetAllAsync();
        var resultGoodOrders = goodOrders.Where(_ => _.OrderId == order.Id);
        var model = new TakeOrderViewModel() 
        { 
            CourierId = new Guid(_userManager.GetUserId(User)),
            OrderId = order.Id,
            TotalPrice = order.TotalPrice,
            GoodDesc = new Tuple<List<string>, List<string>, List<int>>(new List<string>(), new List<string>(), new List<int>())
        };
        var allGoods = await _unitOfWork.GoodRepository.GetAllAsync();
        var allSources = await _unitOfWork.SourceRepository.GetAllAsync();
        foreach (var el in resultGoodOrders)
        {
            var good = allGoods.First(_ => _.Id == el.GoodId);
            model.GoodDesc.Item1.Add(good.Name);
            model.GoodDesc.Item2.Add(allSources.First(_ => _.Id == good.SourceId).Name);
            model.GoodDesc.Item3.Add(el.Amount);
            
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> TakeOrder(TakeOrderViewModel model)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId);
        order.CourierId = model.CourierId;
        order.Status = Status.InProgress;
        
        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.CommitAsync();

        return RedirectToAction("AvailableOrders", "Order");
    }

    public async Task<IActionResult> OrderHistoryCustomer()
    {
        var entities = await _unitOfWork.OrderRepository.GetAllAsync();
        var orders = entities.Where(_ => _.CustomerId == new Guid(_userManager.GetUserId(User)) &&
                                                                  _.Status != Status.Cart);

        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CancelOrder(TakeOrderViewModel model)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId);
        order.Status = Status.Canceled;
        
        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.CommitAsync();

        return RedirectToAction();
    }

    public IActionResult SpecialOrder()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SpecialOrder(SpecialOrderViewModel model)
    { 
        await _unitOfWork.OrderRepository.InsertAsync(new Order()
        {
            CustomerId = new Guid(_userManager.GetUserId(User)),
            Status = Status.New,
            Description = model.Description,
            Location = new Point(50, 26),
            CreatedOn = DateTime.UtcNow,
            OrderType = OrderType.Special,
            PaymentType = PaymentType.Cash
        });
        await _unitOfWork.CommitAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<JsonResult> GetMarker(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        var location = new {x = order.Location.X, y = order.Location.Y};
        return Json(location);
    }

    public void Upload(SpecialOrderViewModel model, IFormFile? file)
    {
        if (file is not null)
        {
            model.ReceiptPicture = new byte[file.Length];
            file.OpenReadStream().Read(model.ReceiptPicture, 0, (int)file.Length);
        }
        return;
    }

    public async Task<IActionResult> CourierOrders()
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        var availableOrders = orders.Where(_ => _.CourierId == new Guid(_userManager.GetUserId(User)));

        return View(availableOrders);
    }
    [HttpPost]
    public async Task<IActionResult> FinishOrder(TakeOrderViewModel model)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.OrderId);
        order.Status = Status.Delivered;
        
        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.CommitAsync();

        return RedirectToAction("Index", "Home");
    }
}