using Diplom.Core.Entities;

namespace Diplom.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Customer> CustomerRepository { get; }
    IRepository<Courier> CourierRepository { get; }
    IRepository<Good> GoodRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<Source> SourceRepository { get; }
    IRepository<GoodOrder> GoodOrderRepository { get; }
    
    Task CommitAsync();
    Task RollbackAsync();
}