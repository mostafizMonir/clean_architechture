using MassTransit;

namespace Newsletter.API;


public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add MassTransit
        services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                // busConfigurator.AddConsumer<CurrentTimeConsumer>();
                // busConfigurator.AddConsumer<CurrentTimeConsumerV2>();

                // busConfigurator.AddConsumer<PurchaseOrderSentConsumer>()
                //     .Endpoint(e => e.InstanceId = "risk-management");

                // busConfigurator.AddConsumers(typeof(Program).Assembly);

                // busConfigurator.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
                busConfigurator.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(configuration["RabbitMQ:Host"]!), h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                    });
                    config.ConfigureEndpoints(context);
                });
               
            }

        );
      
        return services;
    }
}
