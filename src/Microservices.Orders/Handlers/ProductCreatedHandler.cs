using Microservices.Orders.DataAccessLayer;
using Microservices.Orders.DataAccessLayer.Entities;
using Microservices.Shared.RabbitMQ;
using Rebus.Handlers;

namespace Microservices.Orders.Handlers;

public class ProductCreatedHandler(ApplicationDbContext dbContext) : IHandleMessages<ProductCreated>
{
    public async Task Handle(ProductCreated message)
    {
        var newProduct = new OrderProduct
        {
            Id = message.Id,
            Name = message.Name
        };

        dbContext.OrderProducts.Add(newProduct);
        await dbContext.SaveChangesAsync();
    }
}