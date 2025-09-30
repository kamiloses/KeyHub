using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Services;
using KeyHub.Market.Services.impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    options.User.RequireUniqueEmail = true;
    


});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IGameSearchService, GameSearchService>();
builder.Services.AddScoped<IGameManagerService, GameManagerService>();
builder.Services.AddScoped<ISortingService, SortingService>();
builder.Services.AddScoped<IFilteringService,FilteringService>();
builder.Services.AddScoped<IPurchaseHistoryService,PurchaseHistoryService>();
builder.Services.AddScoped<IPurchaseService,PurchaseService>();


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization();
app.MapControllers(); 






// seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    
    dbContext.Database.Migrate();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    
    DbInitializer.Seed(dbContext, userManager, roleManager);
}
app.Run();