namespace Diplom.BLL.Interfaces;

public interface IMapper<TData, TDto>
{
    TData Map(TDto dtoEntity);
    TDto Map(TData dataEntity);
        
    IEnumerable<TData> Map(IEnumerable<TDto> entities, Action<TData>? callback = null);
    IEnumerable<TDto> Map(IEnumerable<TData> entities, Action<TDto>? callback = null);
}