using System.Reflection;
using FluentValidation;
using Logistics.Application.Behaviors;
using Logistics.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}