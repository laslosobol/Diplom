using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class GoodMapper : GenericMapper<Good, GoodDto>
{
    public override Good Map(GoodDto dtoEntity)
    {
        return new Good()
        {
            Id = dtoEntity.Id,
            Name = dtoEntity.Name,
            SourceId = dtoEntity.SourceId,
            Price = dtoEntity.Price
        };
    }

    public override GoodDto Map(Good dataEntity)
    {
        return new GoodDto()
        {
            Id = dataEntity.Id,
            Name = dataEntity.Name,
            SourceId = dataEntity.SourceId,
            Price = dataEntity.Price
        };
    }
}