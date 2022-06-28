using Diplom.BLL.DTO;
using Diplom.BLL.Exceptions;
using Diplom.BLL.Interfaces;
using Diplom.BLL.Mappers;
using Diplom.Core.Interfaces;

namespace Diplom.BLL.Services;

public class  SourceService : ISourceService
{
    private IUnitOfWork _unitOfWork;

    private SourceMapper _sourceMapper;
    public SourceMapper SourceMapper => _sourceMapper ??= new SourceMapper();

    public SourceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<SourceDto> GetSourceByIdAsync(Guid id)
    {
        var source = await _unitOfWork.SourceRepository.GetByIdAsync(id);
        if (source is null) throw new NotExistException();

        return SourceMapper.Map(source);
    }

    public async Task<IEnumerable<SourceDto>> GetAllSourcesAsync()
    {
        var entity = await _unitOfWork.SourceRepository.GetAllAsync();
        return entity.Select(_ => SourceMapper.Map(_));
    }

    public async Task UpdateSourceAsync(SourceDto sourceDto)
    {
        var entity = SourceMapper.Map(sourceDto);
        _unitOfWork.SourceRepository.Update(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteSourceByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.SourceRepository.GetByIdAsync(id);
        if (entity is null)
            throw new NotExistException();
        _unitOfWork.SourceRepository.Delete(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<SourceDto> CreateSourceAsync(SourceDto sourceDto)
    {
        var entity = SourceMapper.Map(sourceDto);
        await _unitOfWork.SourceRepository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();

        return SourceMapper.Map(entity);
    }
}