using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiPortalSchoolSys.Application.Interfaces;
using MultiPortalSchoolSys.Infrastructure.Data;
using MultiPortalSchoolSys.Infrastructure.Identity;

namespace MultiPortalSchoolSys.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Database ──────────────────────────────────────────────────────────
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                // OPTION B: tells EF Core that migrations live in Infrastructure,
                // not the startup project (Web or Api).
                b => b.MigrationsAssembly("MultiPortalSchoolSys.Infrastructure")
            );
        });

        
        // ── Unit of Work ──────────────────────────────────────────────────────
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}