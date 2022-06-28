using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class GoodOrderMapper : GenericMapper<GoodOrder, GoodOrderDto>
{
    public override GoodOrder Map(GoodOrderDto dtoEntity)
    {
        return new GoodOrder()
        {
            Id = dtoEntity.Id,
            OrderId = dtoEntity.OrderId,
            GoodId = dtoEntity.GoodId,
            Amount = dtoEntity.Amount,
            GoodPrice = dtoEntity.GoodPrice
        };
    }

    public override GoodOrderDto Map(GoodOrder dataEntity)
    {
        return new GoodOrderDto()
        {
            Id = dataEntity.Id,
            OrderId = dataEntity.OrderId,
            GoodId = dataEntity.GoodId,
            Amount = dataEntity.Amount,
            GoodPrice = dataEntity.GoodPrice
        };
    }
}