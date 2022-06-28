using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.MVC.ViewModels;

public class RegisterViewModel
{
    [Microsoft.Build.Framework.Required]
    [Display(Name = "Name")]
    public string Name { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [Display(Name = "Surname")]
    public string Surname { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords are not equal")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string PasswordConfirm { get; set; }
    public bool IsCourier { get; set; }
    public bool IsCustomer { get; set; }
}