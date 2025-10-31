using Microsoft.EntityFrameworkCore;
using OrdersEntities;
using Repositories;
using RepositoryContracts;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using Services.OrderItems;
using Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<OrdersDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb"));
});

builder.Services.AddScoped<IOrderRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderAdderService, OrderAdderService>();
builder.Services.AddScoped<IOrderDeleterService, OrderDeleterService>();
builder.Services.AddScoped<IOrderGetterService, OrderGetterService>();
builder.Services.AddScoped<IOrderUpdaterService, OrderUpdaterService>();
builder.Services.AddScoped<IOrderItemAdderService, OrderItemAdderService>();
builder.Services.AddScoped<IOrderItemDeleterService, OrderItemDeleterService>();
builder.Services.AddScoped<IOrderItemGetterService, OrderItemGetterService>();
builder.Services.AddScoped<IOrderItemUpdaterService, OrderItemUpdaterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts(); 
}

app.UseRouting();


app.MapControllers();


app.Run();
