using Diplom.BLL.DTO;
using Diplom.BLL.Exceptions;
using Diplom.BLL.Interfaces;
using Diplom.BLL.Mappers;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;

namespace Diplom.BLL.Services;

public class OrderService : IOrderService
{
    private IUnitOfWork _unitOfWork;

    private OrderMapper _orderMapper;
    public OrderMapper OrderMapper => _orderMapper ??= new OrderMapper();
    
    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var orderEntityToGet = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (orderEntityToGet is null) throw new NotExistException();

        return OrderMapper.Map(orderEntityToGet);
    } 
    public async Task<IEnumerable<OrderDto>> GetAllOrderAsync()
    {
        var entity = await _unitOfWork.OrderRepository.GetAllAsync();
        var result = entity.Select(el => OrderMapper.Map(el)).ToList();

        return result;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = OrderMapper.Map(orderDto);

        await _unitOfWork.OrderRepository.InsertAsync(order);
        await _unitOfWork.CommitAsync();
        return OrderMapper.Map(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrderByUser(Guid userId)
    {
        var entity = await _unitOfWork.OrderRepository.GetAllAsync();
        var result = entity.Where(_ => _.CustomerId == userId).Select(_ => OrderMapper.Map(_)).ToList();

        return result;
    }
    public async Task<IEnumerable<OrderDto>> GetOrderByCourier(Guid courierId)
    {
        var entity = await _unitOfWork.OrderRepository.GetAllAsync();
        var result = entity.Where(_ => _.CourierId == courierId).Select(_ => OrderMapper.Map(_)).ToList();

        return result;
    }

    public async Task ChangeStatus(Guid id, Status status)
    {
        var entity = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        entity.Status = status;
        
        _unitOfWork.OrderRepository.Update(entity);
        await _unitOfWork.CommitAsync();
    }
}