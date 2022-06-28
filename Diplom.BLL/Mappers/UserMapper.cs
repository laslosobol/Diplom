using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class UserMapper : GenericMapper<User, UserDto>
{
    public override User Map(UserDto dtoEntity)
    {
        throw new NotImplementedException();
    }

    public override UserDto Map(User dataEntity)
    {
        return new UserDto(){};
    }
}