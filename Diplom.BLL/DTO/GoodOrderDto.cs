using Diplom.Core.Entities;

namespace Diplom.BLL.DTO;

public class GoodOrderDto
{
    public Guid Id { get; set; }
    public Guid GoodId { get; set; }

    public Guid OrderId { get; set; }

    public int Amount { get; set; }
    public double GoodPrice { get; set; }
}