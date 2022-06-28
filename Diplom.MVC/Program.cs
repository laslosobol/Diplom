using Diplom.BLL.Interfaces;
using Diplom.BLL.Services;
using Diplom.Core.Context;
using Diplom.Core.Entities;
using Diplom.Core.Interfaces;
using Diplom.Core.UnitOfWork;
using Diplom.MVC;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CookieAuthenticationDefaults = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(_ => 
    _.UseNpgsql(builder.Configuration.GetConnectionString("DiplomDatabase"), _ => _.UseNetTopologySuite()));

builder.Services.AddIdentity<User, IdentityRole>(_ =>
{
    _.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISourceService, SourceService>();
builder.Services.AddScoped<IGoodService, GoodService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSession();
builder.Services.Configure<CookiePolicyOptions>(_ =>
{
    _.CheckConsentNeeded = x => true;
    _.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(_ =>
    {
        _.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        _.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(_ =>
    {
        _.ClientId = "453192460170-vnuhcef5hp32r4m24u1hq5nkds6a3id0.apps.googleusercontent.com"; 
        _.ClientSecret = "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";
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(rolesManager);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();