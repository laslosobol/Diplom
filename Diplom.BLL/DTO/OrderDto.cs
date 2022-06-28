using Diplom.Core.Entities;

namespace Diplom.BLL.DTO;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedOn { get; set; }
    public Status Status { get; set; }
    public Guid CourierId { get; set; }
    public double TotalPrice { get; set; }
    public OrderType OrderType { get; set; }
    public string Description { get; set; }
    public PaymentType PaymentType { get; set; }
}