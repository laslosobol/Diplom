using Diplom.Core.Entities;
using Diplom.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CookieAuthenticationDefaults = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults;

namespace Diplom.MVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel() {ReturnUrl = returnUrl});
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user;
            if (model.IsCourier)
            {
                user = new Courier() {Email = model.Email, UserName = model.Email, Name = model.Name, Surname = model.Surname};
            }
            else if(model.IsCustomer)
            {
                user = new Customer() {Email = model.Email, UserName = $@"{model.Name} {model.Surname}", Name = model.Name, Surname = model.Surname};
            }
            else
            {
                user = new User() {Email = model.Email, UserName = model.Email};
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                if (model.IsCourier)
                    await _userManager.AddToRoleAsync(user, "Courier");
                else
                    await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Wrong Email or Password!");
        }
        return View(model);
    }
    public async Task Google()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        {
            RedirectUri = Url.Action("GoogleResponse")
        });
        
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(_ =>
        new {
            _.Issuer,
            _.OriginalIssuer,
            _.Type,
            _.Value,
        }).ToList();
        //return Json(claims);
        var temp = await _userManager.FindByEmailAsync(claims[4].Value);
        if (temp is null)
        {
            var newUser = new Courier()
            {
                UserName = claims[4].Value,
                Email = claims[4].Value,
                Name = claims[2].Value,
                Surname = claims[3].Value,
            };
            await _userManager.CreateAsync(newUser);
            await _userManager.AddToRoleAsync(newUser, "Customer");
            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            await _signInManager.SignInAsync(temp, false);
            return RedirectToAction("Index", "Home");
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Profile()
    {
        var profile = await _userManager.GetUserAsync(HttpContext.User);
        var model = new EditUserViewModel
        {
            Id = profile.Id,
            UserName = profile.UserName,
            Email = profile.Email,
            PhoneNumber = profile.PhoneNumber,
        };
        return View(model);
    }
}