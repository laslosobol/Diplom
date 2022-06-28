using Diplom.BLL.DTO;
using Diplom.BLL.Mappers;

namespace Diplom.BLL.Interfaces;

public interface ICourierService
{
    Task<CourierDto> GetCourierByIdAsync(Guid id);
    Task<IEnumerable<CourierDto>> GetAllCourierAsync();
    Task UpdateCourierAsync(CourierDto courierDto);
    Task DeleteCourierByIdAsync(Guid id);
    Task<CourierDto> CreateCourierAsync(CourierDto courierDto);
    Task<CourierDto?> GetCourier(UserLogin userLogin);
}