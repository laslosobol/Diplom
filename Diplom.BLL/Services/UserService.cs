using System.Security.Cryptography;
using System.Text;
using Diplom.BLL.DTO;
using Diplom.BLL.Exceptions;
using Diplom.BLL.Interfaces;
using Diplom.BLL.Mappers;
using Diplom.Core.Interfaces;
using Isopoh.Cryptography.Argon2;
using System.Linq;

namespace Diplom.BLL.Services;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;

    private UserMapper _userMapper;
    public UserMapper UserMapper => _userMapper ??= new UserMapper();
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    private string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var salt = new byte[16];
        Rng.GetBytes(salt);
        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 10,
            MemoryCost = 32768,
            Lanes = 12,
            Threads = Environment.ProcessorCount,
            Password = passwordBytes,
            Salt = salt,
            HashLength = 20
        };
        return Argon2.Hash(password);
    }
    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var userEntityToGet = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userEntityToGet is null) throw new NotExistException();

        return UserMapper.Map(userEntityToGet);
    }
    public async Task<IEnumerable<UserDto>> GetAllUserAsync()
    {
        var entity = await _unitOfWork.UserRepository.GetAllAsync();
        var result = entity.Select(el => UserMapper.Map(el)).ToList();

        return result;
    }
    public async Task UpdateUserAsync(UserDto userDto)
    {
        var entity = UserMapper.Map(userDto);
        _unitOfWork.UserRepository.Update(entity);

        await _unitOfWork.CommitAsync();
    }
    public async Task DeleteUserByIdAsync(Guid id)
    {
        var userEntityToDelete = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userEntityToDelete is null) 
            throw new NotExistException();
            
        _unitOfWork.UserRepository.Delete(userEntityToDelete);

        await _unitOfWork.CommitAsync();
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var userEntityToInsert = UserMapper.Map(userDto);
        await _unitOfWork.UserRepository.InsertAsync(userEntityToInsert);
        await _unitOfWork.CommitAsync();

        return UserMapper.Map(userEntityToInsert);
    }
}