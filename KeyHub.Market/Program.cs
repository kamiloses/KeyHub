using KeyHub.Market.data;
using KeyHub.Market.Services;
using KeyHub.Market.Services.impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IGameSearchService, GameSearchService>();
builder.Services.AddScoped<ISortingService, SortingService>();
builder.Services.AddScoped<IFilteringService,FilteringService>();
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers(); 


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Seed(dbContext);
}

app.Run();