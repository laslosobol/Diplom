using System.Runtime.Intrinsics.X86;
using Diplom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.Storage;

namespace Diplom.Core.Context;
public sealed class ApplicationDbContext : IdentityDbContext<User>
{
    private readonly IEncryptionProvider _encryptionProvider;
    private readonly AesKeyInfo _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES128Bits);
    private readonly AesKeyInfo _encryptionIv = AesProvider.GenerateKey(AesKeySize.AES128Bits);
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<GoodOrder> GoodOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseEncryption(_encryptionProvider);
        base.OnModelCreating(modelBuilder);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}