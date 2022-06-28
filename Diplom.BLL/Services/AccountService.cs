using Diplom.BLL.Interfaces;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Diplom.BLL.Services;

public class AccountService : IAccountService
{
    private IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IdentityResult> Register(User user, string password, bool isCourier)
    {
        await _userManager.CreateAsync(user, password);
        var result = await _userManager.CreateAsync(user, password);;

        if (!result.Succeeded) return result;
        if (isCourier)
            await _userManager.AddToRoleAsync(user, "Courier");
        else
            await _userManager.AddToRoleAsync(user, "Customer");
        await _signInManager.SignInAsync(user, false);
        return result;
    }

    public Task<IdentityResult> Login(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> Logout()
    {
        throw new NotImplementedException();
    }
}