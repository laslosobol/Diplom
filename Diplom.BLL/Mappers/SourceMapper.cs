using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class SourceMapper : GenericMapper<Source, SourceDto>
{
    public override Source Map(SourceDto dtoEntity)
    {
        return new Source()
        {
            Id = dtoEntity.Id,
            Name = dtoEntity.Name
        };
    }

    public override SourceDto Map(Source dataEntity)
    {
        return new SourceDto()
        {
            Id = dataEntity.Id,
            Name = dataEntity.Name
        };
    }
}