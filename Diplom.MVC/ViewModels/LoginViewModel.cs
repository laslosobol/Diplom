using System.ComponentModel.DataAnnotations;

namespace Diplom.MVC.ViewModels;

public class LoginViewModel
{
    [Microsoft.Build.Framework.Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
         
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
         
    public string ReturnUrl { get; set; }
}