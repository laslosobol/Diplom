using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class CourierMapper : GenericMapper<Courier, CourierDto>
{
    public override Courier Map(CourierDto dtoEntity)
    {
        return new Courier()
        {
            Name = dtoEntity.Name,
            Surname = dtoEntity.Surname,
        };
    }

    public override CourierDto Map(Courier dataEntity)
    {
        return new CourierDto()
        {
            Name = dataEntity.Name,
            Surname = dataEntity.Surname,
        };
    }
}