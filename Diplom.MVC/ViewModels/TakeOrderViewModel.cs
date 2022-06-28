using Diplom.Core.Entities;

namespace Diplom.MVC.ViewModels;

public class TakeOrderViewModel
{
    public Guid CourierId { get; set; }
    public Guid OrderId { get; set; }
    public double TotalPrice { get; set; }
    public Status Status { get; set; }
    public Tuple<List<string>, List<string>, List<int>> GoodDesc { get; set; }
}