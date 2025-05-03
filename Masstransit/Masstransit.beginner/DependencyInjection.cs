using MassTransit;
using Masstransit.beginner.Consumer;
using Masstransit.beginner.Services;

namespace Masstransit.beginner;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add MassTransit
        services.AddMassTransit(busConfigurator =>
            {
               // busConfigurator.SetKebabCaseEndpointNameFormatter();

                // busConfigurator.AddConsumer<CurrentTimeConsumer>();
                // busConfigurator.AddConsumer<CurrentTimeConsumerV2>();

                busConfigurator.AddConsumer<PurchaseOrderSentConsumer>()
                    .Endpoint(e=>e.InstanceId = "risk-management");

               // busConfigurator.AddConsumers(typeof(Program).Assembly);
                
                // busConfigurator.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
                // busConfigurator.UsingRabbitMq((context, config) =>
                // {
                //     config.Host(new Uri(configuration["RabbitMQ:Host"]!), h =>
                //     {
                //         h.Username(configuration["RabbitMQ:Username"]!);
                //         h.Password(configuration["RabbitMQ:Password"]!);
                //     });
                //     config.ConfigureEndpoints(context);
                // });
                //
                busConfigurator.UsingAmazonSqs((context, config) =>
                {
                    config.Host("ap-south-1", h =>
                    {
                        h.AccessKey(configuration["Amazon:AccessKey"]!);
                        h.SecretKey(configuration["Amazon:SecretKey"]!);
                        
                    });
                    config.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("stocks-platform-",false));
                });
            }

        );
        services.AddHostedService<MessagePublisher>();
        return services;
    }
}
