using NetTopologySuite.Geometries;

namespace Diplom.MVC.ViewModels;

public class OrderCartViewModel
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsCardPayment { get; set; }
    public double TotalPrice { get; set; }
    public string Description { get; set; }
    public bool IsSpecial { get; set; }
}