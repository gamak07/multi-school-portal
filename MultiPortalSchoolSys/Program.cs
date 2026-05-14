using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;
using MultiPortalSchoolSys.Repositories;
using MultiPortalSchoolSys.Repositories.Interfaces;
using MultiPortalSchoolSys.Services;
using MultiPortalSchoolSys.Services.Interfaces;
using MultiPortalSchoolSys.UnitOfWork;
using MultiPortalSchoolSys.UnitOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// =========================================================================
// 4. SERVICES  ← next phase registrations go here
// Example:
//   builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//   builder.Services.AddScoped<IResultService,  ResultService>();
// =========================================================================

// =========================================================================
// 5. MVC
// =========================================================================
builder.Services.AddControllersWithViews();

// =========================================================================
// 6. MIDDLEWARE PIPELINE
// Order is mandatory in ASP.NET Core. Do not rearrange these lines.
// =========================================================================
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// CRITICAL FIX 3: UseAuthentication MUST come before UseAuthorization.
// Without this, [Authorize] and User.Identity are permanently broken —
// the app cannot identify who is making a request.
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Area route must be registered before the default route
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();