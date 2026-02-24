using Microservices.Orders.Handlers;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;

namespace Microservices.Orders.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsumerRabbitMQ(this IServiceCollection services)
    {
        var rabbitConnection = Shared.Services.DependencyInjection.rabbitConnectionString;

        services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq(rabbitConnection, "event-listener"))
            .Routing(r => r.TypeBased())

            .Options(options => options.SetBusName("OrdersServiceBus"))
            .Logging(logging => logging.Console(Rebus.Logging.LogLevel.Info))
            .Serialization(serialize => serialize.UseSystemTextJson())
        );

        // registra automaticamente tutti gli handler in questo assembly
        services.AutoRegisterHandlersFromAssemblyOf<ProductCreatedHandler>();

        return services;
    }
}