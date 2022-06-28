using Diplom.Core.Context;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using Diplom.Core.Repositories;

namespace Diplom.Core.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    private IRepository<User> _userRepository;
    public IRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);
    
    private IRepository<Customer> _customerRepository;
    public IRepository<Customer> CustomerRepository => _customerRepository ??= new GenericRepository<Customer>(_context);
    
    private IRepository<Courier> _courierRepository;
    public IRepository<Courier> CourierRepository => _courierRepository ??= new GenericRepository<Courier>(_context);
    
    private IRepository<Good> _goodRepository;
    public IRepository<Good> GoodRepository => _goodRepository ??= new GenericRepository<Good>(_context);
    
    private IRepository<Order> _orderRepository;
    public IRepository<Order> OrderRepository => _orderRepository ??= new GenericRepository<Order>(_context);
    
    private IRepository<Source> _sourceRepository;
    public IRepository<Source> SourceRepository => _sourceRepository ??= new GenericRepository<Source>(_context);
    
    private IRepository<GoodOrder> _goodOrderRepository;
    public IRepository<GoodOrder> GoodOrderRepository => _goodOrderRepository ??= new GenericRepository<GoodOrder>(_context);
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }

    private bool disposed = false;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);

    }

    public void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }
}