using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class OrderMapper : GenericMapper<Order, OrderDto>
{
    public override Order Map(OrderDto dtoEntity)
    {
        return new Order()
        {
            Id = dtoEntity.Id,
            CustomerId = dtoEntity.CustomerId,
            CourierId = dtoEntity.CourierId,
            CreatedOn = dtoEntity.CreatedOn,
            TotalPrice = dtoEntity.TotalPrice,
            Status = dtoEntity.Status,
            PaymentType = dtoEntity.PaymentType,
            OrderType = dtoEntity.OrderType,
            Description = dtoEntity.Description
        };
    }

    public override OrderDto Map(Order dataEntity)
    {
        return new OrderDto()
        {
            Id = dataEntity.Id,
            CustomerId = dataEntity.CustomerId,
            CourierId = dataEntity.CourierId,
            CreatedOn = dataEntity.CreatedOn,
            TotalPrice = dataEntity.TotalPrice,
            Status = dataEntity.Status,
            PaymentType = dataEntity.PaymentType,
            OrderType = dataEntity.OrderType,
            Description = dataEntity.Description
        };
    }
}