using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Interfaces;

public interface IOrderService
{
    Task<OrderDto> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<OrderDto>> GetAllOrderAsync();
    Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
    Task<IEnumerable<OrderDto>> GetOrderByUser(Guid userId);
    Task<IEnumerable<OrderDto>> GetOrderByCourier(Guid courierId);
    Task ChangeStatus(Guid id, Status status);
}