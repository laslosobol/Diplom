using Diplom.BLL.DTO;

namespace Diplom.BLL.Interfaces;

public interface ISourceService
{
    Task<SourceDto> GetSourceByIdAsync(Guid id);
    Task<IEnumerable<SourceDto>> GetAllSourcesAsync();
    Task UpdateSourceAsync(SourceDto sourceDto);
    Task DeleteSourceByIdAsync(Guid id);
    Task<SourceDto> CreateSourceAsync(SourceDto sourceDto);
}