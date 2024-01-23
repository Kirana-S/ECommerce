using ECommerce.Api.Search.Interface;
using ECommerce.Api.Search.Services;
using Microsoft.Extensions.Configuration;
using Polly; // Add this namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddHttpClient("OrdersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
    //var configuration = builder.Configuration; // Get the configuration from the builder
    //config.BaseAddress = new Uri(configuration["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductsService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
})/*.AddTransientHttpErrorPolicy(p=> p.WaitAndRetryAsync(5, _=> TimeSpan.FromMilliseconds(500)))*/;

builder.Services.AddHttpClient("CustomersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
