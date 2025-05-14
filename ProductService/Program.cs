using ERP.Infrastructure.Data;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Features.Products.Commands;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add MediatR
builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

var app = builder.Build();

app.MapGet("/migrate", async (ProductDbContext db) =>
{
    await db.Database.MigrateAsync();
  
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
