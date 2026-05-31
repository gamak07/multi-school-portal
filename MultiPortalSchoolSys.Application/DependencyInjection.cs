using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using MultiPortalSchoolSys.Application.Common.Behaviours;
using FluentValidation;

namespace MultiPortalSchoolSys.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Automatically finds and links all FluentValidation validators in this project assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // Explicit, professional pipeline execution sequence
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        });

        return services;
    }
}