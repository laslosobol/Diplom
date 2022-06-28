using Diplom.BLL.DTO;

namespace Diplom.BLL.Interfaces;

public interface IGoodService
{
    Task<GoodDto> GetGoodByIdAsync(Guid id);
    Task<IEnumerable<GoodDto>> GetAllGoodsAsync();
    Task UpdateGoodAsync(GoodDto goodDto);
    Task DeleteGoodByIdAsync(Guid id);
    Task<GoodDto> CreateGoodAsync(GoodDto goodDto);
    Task<IEnumerable<GoodDto>> GetAllGoodBySource(Guid sourceId);
    Task AddToCart(Guid goodId, Guid customerId, int amount);
}