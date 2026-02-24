using Microservices.Products.DataAccessLayer;
using Microservices.Products.DataAccessLayer.Entities;
using Microservices.Shared.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rebus.Bus;

namespace Microservices.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ApplicationDbContext dbContext, IBus bus) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var products = await dbContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Product newProduct)
    {
        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync();

        await bus.Publish(new ProductCreated
        {
            Id = newProduct.Id,
            Name = newProduct.Name
        });

        return CreatedAtAction("GET", new { id = newProduct.Id }, newProduct);
    }
}