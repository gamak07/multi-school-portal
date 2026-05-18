using Microsoft.AspNetCore.Identity;
using MultiPortalSchoolSys.Application.Interfaces.Services;
using MultiPortalSchoolSys.Infrastructure;
using MultiPortalSchoolSys.Infrastructure.Data;
using MultiPortalSchoolSys.Infrastructure.Identity;
// using MultiPortalSchoolSys.Services;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. INFRASTRUCTURE (DbContext, EF Core, UnitOfWork)
// All database and repository wiring is handled inside AddInfrastructure().
// =========================================================================
builder.Services.AddInfrastructure(builder.Configuration);

// =========================================================================
// 2. IDENTITY
// AddIdentity lives here — not in Infrastructure — because it requires
// the ASP.NET Core web hosting stack unavailable in class libraries.
// =========================================================================
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit           = true;
    options.Password.RequiredLength         = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase       = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


// =========================================================================
// 3. COOKIE CONFIGURATION
// =========================================================================
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath        = "/Account/Login";
    options.LogoutPath       = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan   = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

// =========================================================================
// 4. APPLICATION SERVICES
// Add each service here as it is built.
// =========================================================================
// builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IStudentService, StudentService>();
// builder.Services.AddScoped<ITeacherService, TeacherService>();
// builder.Services.AddScoped<IResultService,  ResultService>();

// =========================================================================
// 5. MVC
// =========================================================================
builder.Services.AddControllersWithViews();

// =========================================================================
// 6. BUILD & SEED
// =========================================================================
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider);
}

// =========================================================================
// 7. MIDDLEWARE PIPELINE
// Order is mandatory — do not rearrange.
// =========================================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();