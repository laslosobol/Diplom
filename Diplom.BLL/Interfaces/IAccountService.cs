using Diplom.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diplom.BLL.Interfaces;

public interface IAccountService
{
    public Task<IdentityResult> Register(User user, string password, bool isCourier);
    public Task<IdentityResult> Login(User user);
    public Task<IdentityResult> Logout();
}