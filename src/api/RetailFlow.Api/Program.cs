
using RetailFlow.Infrastructure.Persistence.Repositories;
using RetailFlow.Infrastructure.MultiTenancy;
using RetailFlow.Infrastructure.Persistence;
using RetailFlow.Infrastructure.Services;
using RetailFlow.Application.Interfaces;
using RetailFlow.Application.Services;
using Microsoft.EntityFrameworkCore;
using RetailFlow.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IProductRepository,  ProductRepository>();
builder.Services.AddScoped<IInventoryRepository,InventoryRepository>();
builder.Services.AddScoped<ISaleRepository,     SaleRepository>();
builder.Services.AddScoped<IProductService,     ProductService>();
builder.Services.AddScoped<IInventoryService,   InventoryService>();
builder.Services.AddScoped<ISaleService,        SaleService>();
builder.Services.AddScoped<IStoreService,       StoreService>();
builder.Services.AddScoped<ITenantService,      TenantService>();
builder.Services.AddScoped<IStoreService,       StoreService>();
builder.Services.AddScoped<ITenantContext,      TenantContext>();

//builder.Services.AddScoped<IInventoryService, InventoryService>();
//builder.Services.AddSingleton<IProductService, ProductService>();
//builder.Services.AddSingleton<IInventoryService, InventoryService>();
//builder.Services.AddSingleton<ISaleService, SaleService>();


builder.Services.AddDbContext<RetailFlowDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("RetailFlowDb")));


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<TenantContextMiddleware>();
app.MapControllers();
app.Run();
