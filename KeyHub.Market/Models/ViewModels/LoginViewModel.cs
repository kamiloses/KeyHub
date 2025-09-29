using System.ComponentModel.DataAnnotations;

namespace KeyHub.Market.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required!")]
    public string UserName { get; set; } 

    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}