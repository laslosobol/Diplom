using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Spatial;
using NetTopologySuite.Geometries;

namespace Diplom.Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Status Status { get; set; }
    public Guid CustomerId { get; set; }
    public Guid CourierId { get; set; }
    public double TotalPrice { get; set; }
    
    public OrderType OrderType { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Description { get; set; }

    [Column(TypeName = "geography")]
    public Point Location { get; set; }
    
}

public enum Status
{
    Cart,
    New,
    InProgress,
    Delivered,
    Canceled
}

public enum OrderType
{
    FromAvailableSources,
    Special
}

public enum PaymentType
{
    CreditCard,
    Cash
}