using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Diplom.Api.Controllers;

[ApiController]
[Route("/user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    
    public UserController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }
//Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [HttpGet("get")]
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync() => await _userService.GetAllUserAsync();

    [HttpPut("update")]
    public async Task UpdateUserAsync(UserDto userDto) => await _userService.UpdateUserAsync(userDto);

    [HttpDelete("delete/{id:Guid}")]
    public async Task DeleteUserAsync(Guid id) => await _userService.DeleteUserByIdAsync(id);
    
    [HttpGet("get/{id:Guid}")]
    public async Task GetUserByIdAsync(Guid id) => await _userService.GetUserByIdAsync(id);
    
    // [HttpPost("login")]
    // public async Task<IResult> Login(UserLogin userLogin)
    // {
    //     if (!string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
    //     {
    //         var loggedInUser = await _userService.GetUser(userLogin);
    //         if(loggedInUser is null)
    //             return Results.NotFound();
    //         var claims = new[]
    //         {
    //             new Claim(ClaimTypes.Name, loggedInUser.FirstName),
    //             new Claim(ClaimTypes.Surname, loggedInUser.SecondName),
    //             new Claim(ClaimTypes.Email, loggedInUser.Email),
    //             new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber),
    //             new Claim(ClaimTypes.Role, loggedInUser.Role)
    //         };
    //         var token = new JwtSecurityToken
    //         (
    //             issuer : _config["Jwt:Issuer"],
    //             audience : _config["Jwt:Audience"],
    //             claims : claims,
    //             expires : DateTime.UtcNow.AddDays(60),
    //             notBefore: DateTime.UtcNow,
    //             signingCredentials: new SigningCredentials(
    //                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
    //                 SecurityAlgorithms.HmacSha256)
    //         );
    //
    //         var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    //         
    //         return Results.Ok(tokenString);
    //     }
    //     return Results.Problem();
    // }

    [HttpPost("register")]
    public async Task Register(UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
    }
}