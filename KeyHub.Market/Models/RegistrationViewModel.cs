using System.ComponentModel.DataAnnotations;

namespace KeyHub.Market.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can contain only letters and digits")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", 
        ErrorMessage = "Password must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 digit.")]
    public string Password { get; set; }
}