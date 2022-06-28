using System.Device.Location;
using System.Spatial;
using Diplom.BLL.DTO;
using Diplom.BLL.Exceptions;
using Diplom.BLL.Interfaces;
using Diplom.BLL.Mappers;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using NetTopologySuite.Geometries;

namespace Diplom.BLL.Services;

public class GoodService : IGoodService
{
    private IUnitOfWork _unitOfWork;

    private GoodMapper _goodMapper;
    public GoodMapper GoodMapper => _goodMapper ??= new GoodMapper();

    public GoodService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<GoodDto> GetGoodByIdAsync(Guid id)
    {
        var source = await _unitOfWork.GoodRepository.GetByIdAsync(id);
        if (source is null) throw new NotExistException();

        return GoodMapper.Map(source);
    }

    public async Task<IEnumerable<GoodDto>> GetAllGoodsAsync()
    {
        var entity = await _unitOfWork.GoodRepository.GetAllAsync();
        return entity.Select(_ => GoodMapper.Map(_));
    }

    public async Task UpdateGoodAsync(GoodDto goodDto)
    {
        var entity = GoodMapper.Map(goodDto);
        _unitOfWork.GoodRepository.Update(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteGoodByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.GoodRepository.GetByIdAsync(id);
        if (entity is null)
            throw new NotExistException();
        _unitOfWork.GoodRepository.Delete(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<GoodDto> CreateGoodAsync(GoodDto goodDto)
    {
        var entity = GoodMapper.Map(goodDto);
        await _unitOfWork.GoodRepository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();

        return GoodMapper.Map(entity);
    }

    public async Task<IEnumerable<GoodDto>> GetAllGoodBySource(Guid sourceId)
    {
        var entity = await _unitOfWork.GoodRepository.GetAllAsync();
        var result = entity.Where(_ => _.SourceId == sourceId).Select(_ => GoodMapper.Map(_));

        return result;
    }
    
    public async Task AddToCart(Guid goodId, Guid customerId, int amount)
    {
        var good = await _unitOfWork.GoodRepository.GetByIdAsync(goodId);
        if (good is null)
            throw new NotExistException();
        var temp1 = false;
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        var cartOrder = orders.FirstOrDefault(_ => _.Status == Status.Cart);
        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
        watcher.Start();
        GeoCoordinate coordinate = watcher.Position.Location;
        watcher.Stop();
        if (cartOrder is null)
        {
            cartOrder = new Order() {CustomerId = customerId, Status = Status.Cart, TotalPrice = 0, Location = new Point(coordinate.Latitude, coordinate.Longitude)};
            await _unitOfWork.OrderRepository.InsertAsync(cartOrder);
        }
        var goodOrders = await _unitOfWork.GoodOrderRepository.GetAllAsync();
        var goodOrder = goodOrders.FirstOrDefault(_ => _.GoodId == goodId && _.OrderId == cartOrder.Id);
        var temp = false;
        
        if (goodOrder is null)
        {
            goodOrder = new GoodOrder()
            {
                GoodId = good.Id,
                OrderId = cartOrder.Id,
                Amount = amount,
                GoodPrice = good.Price
            };
            await _unitOfWork.GoodOrderRepository.InsertAsync(goodOrder);
        }
        else
        {
            temp = true;
            goodOrder.Amount += amount;
        }
        cartOrder.TotalPrice += goodOrder.GoodPrice * amount;
        if(temp1)
            _unitOfWork.OrderRepository.Update(cartOrder);
        if(temp)
            _unitOfWork.GoodOrderRepository.Update(goodOrder);
        await _unitOfWork.CommitAsync();
    }
}