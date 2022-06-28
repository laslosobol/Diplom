namespace Diplom.Core.Entities;

public class GoodOrder
{
    public Guid Id { get; set; }
    public Guid GoodId { get; set; }

    public Guid OrderId { get; set; }

    public int Amount { get; set; }
    public double GoodPrice { get; set; }
}