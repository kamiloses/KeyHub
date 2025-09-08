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
  
            
        // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //
        
        
    }


}




