using Diplom.BLL.DTO;
using Diplom.BLL.Exceptions;
using Diplom.BLL.Mappers;

namespace Diplom.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllUserAsync();
    Task UpdateUserAsync(UserDto userDto);
    Task DeleteUserByIdAsync(Guid id);
    Task<UserDto> CreateUserAsync(UserDto userDto);
}