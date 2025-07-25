using Identity.Models;
using Identity.Repositories;
using Identity.Security.Default;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 7;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL:ZXCVBNM<>?1234567890-=qwertyuiopasdfghjkl;xcvbnm,.";

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployeeListPolicy", policy => policy
        .RequireClaim(ClaimTypesStore.EmployeeList, true.ToString())
        .RequireClaim(ClaimTypesStore.EmployeeDetails , true.ToString()));

    options.AddPolicy("ClaimOrRole", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(ClaimTypesStore.EmployeeList, true.ToString()) ||
            context.User.IsInRole("Admin")
            ));

    options.AddPolicy("ClaimRequirement", policy =>
        policy.Requirements.Add(new ClaimRequirement(ClaimTypesStore.EmployeeList, true.ToString())
        ));
});

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IAuthorizationHandler, ClaimHandler>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
