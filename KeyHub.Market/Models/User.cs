using Microsoft.AspNetCore.Identity;

namespace KeyHub.Market.Models;

public class User : IdentityUser
{
 
    
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    
}