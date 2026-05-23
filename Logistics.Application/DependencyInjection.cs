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

        // 2. تسجيل MediatR
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(assembly));

        // 3. تسجيل AutoMapper - دلوقتي الـ assembly مش هتدي ايرور
        services.AddAutoMapper(assembly);

        // 4. تسجيل FluentValidation
        services.AddValidatorsFromAssembly(assembly);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}