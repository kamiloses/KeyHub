using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace KeyHub.Market.Services.impl;

public class AuthService : IAuthService
{

    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Register(string username, string email, string password)
    {

        IdentityUser user = new IdentityUser() { UserName = username, Email = email };
        IdentityResult result =await  _userManager.CreateAsync(user, password);
        Console.WriteLine("DZIALA");
        if (result.Succeeded)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("AAAAAAAA");
        }
        else
        {
                Console.WriteLine("BBBBBBBBB");
            foreach (var error in result.Errors)
            {
            Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Błąd: {error.Description}");
            }
        }
        // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //
        
        
    }


}




