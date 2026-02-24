using Microservices.Orders.DataAccessLayer;
using Microservices.Orders.DependencyInjection;
using Microservices.Shared.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConsumerRabbitMQ();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(DependencyInjection.database);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();