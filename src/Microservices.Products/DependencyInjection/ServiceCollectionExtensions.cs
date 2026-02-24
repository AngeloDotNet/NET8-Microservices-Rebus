using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;

namespace Microservices.Products.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducerRabbitMQ(this IServiceCollection services)
    {
        var rabbitConnection = Shared.Services.DependencyInjection.rabbitConnectionString;

        services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq(rabbitConnection, "microservices-producer"))
            .Routing(r => r.TypeBased())

            .Options(options => options.SetBusName("ProductsServiceBus"))
            .Logging(logging => logging.Console(Rebus.Logging.LogLevel.Info))
            .Serialization(serialize => serialize.UseSystemTextJson())
        );
        return services;
    }
}