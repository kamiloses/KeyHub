using System.Text;
using KeyHub.Market.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace KeyHub.Market.Services.impl;

public class AuthService : IAuthService
{
// todo //todo ogarnij turtle
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task Register(string username, string email, string password)
    {

        User user = new User() { UserName = username, Email = email };
        IdentityResult result =await  _userManager.CreateAsync(user, password);
  
        
        
    }


}




