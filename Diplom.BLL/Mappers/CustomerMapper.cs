using Diplom.BLL.DTO;
using Diplom.Core.Entities;

namespace Diplom.BLL.Mappers;

public class CustomerMapper : GenericMapper<Customer, CustomerDto>
{
    public override Customer Map(CustomerDto dtoEntity)
    {
        return new Customer()
        {
            Name = dtoEntity.Name,
            Surname = dtoEntity.Surname,
        };
    }

    public override CustomerDto Map(Customer dataEntity)
    {
        return new CustomerDto()
        {
            Name = dataEntity.Name,
            Surname = dataEntity.Surname,
        };
    }
}