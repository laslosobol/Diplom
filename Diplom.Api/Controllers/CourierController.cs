using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Diplom.BLL.DTO;
using Diplom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Diplom.Api.Controllers;

[ApiController]
[Route("/courier")]
public class CourierController : Controller
{
    private readonly ICourierService _courierService;
    private readonly IConfiguration _config;
    
    public CourierController(ICourierService courierService, IConfiguration config)
    {
        _courierService = courierService;
        _config = config;
    }
    
    [HttpGet("get")]
    public async Task<IEnumerable<CourierDto>> GetAllCouriersAsync() => await _courierService.GetAllCourierAsync();

    [HttpPut("update")]
    public async Task UpdateCourierAsync(CourierDto courierDto) => await _courierService.UpdateCourierAsync(courierDto);

    [HttpDelete("delete/{id:Guid}")]
    public async Task DeleteCourierAsync(Guid id) => await _courierService.DeleteCourierByIdAsync(id);
    
    [HttpGet("get/{id:Guid}")]
    public async Task GetCourierByIdAsync(Guid id) => await _courierService.GetCourierByIdAsync(id);
    
    // [HttpPost("login")]
    // public async Task<IResult> Login(UserLogin userLogin)
    // {
    //     if (!string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password))
    //     {
    //         var loggedInUser = await _courierService.GetCourier(userLogin);
    //         if(loggedInUser is null)
    //             return Results.NotFound();
    //         var claims = new[]
    //         {
    //             new Claim(ClaimTypes.Name, loggedInUser.Name),
    //             new Claim(ClaimTypes.Surname, loggedInUser.Surname),
    //             new Claim(ClaimTypes.Email, loggedInUser.Em),
    //             new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber),
    //             new Claim(ClaimTypes.Role, "Courier")
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
    public async Task Register(CourierDto courierDto)
    {
        var user = await _courierService.CreateCourierAsync(courierDto);
    }
}