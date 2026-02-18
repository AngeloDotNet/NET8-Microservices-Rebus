using Microservices.Orders.Handlers;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace Microservices.Orders.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsumerRabbitMQ(this IServiceCollection services)
    {
        var rabbitConnection = Shared.Services.DependencyInjection.rabbitConnectionString;

        services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq(rabbitConnection, "event-listener"))
            .Routing(r => r.TypeBased())
        );

        // registra automaticamente tutti gli handler in questo assembly
        services.AutoRegisterHandlersFromAssemblyOf<ProductCreatedHandler>();

        return services;
    }
}