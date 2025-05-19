using Application.Abstractions.Behaviors;
using Application.Abstractions.Messaging;
using Application.Repositories;
using Application.Todos.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        // Register CreateTodoCommandHandler as a service
        //services.AddScoped<CreateTodoCommandHandler>();
       // services.AddScoped<ICommandHandler<CreateTodoCommand>, CreateTodoCommandHandler>();
        services.AddScoped<ICommandHandler<CreateTodoCommand>, CreateTodoCommandHandler>();

       services.Scan(
           scan=> scan
               .FromAssemblies(typeof(DependencyInjection).Assembly)
               .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime()
           );

        return services;
    }
}
