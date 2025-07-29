using Microsoft.EntityFrameworkCore;
using TransactionsExample.Controllers.Middleware;
using TransactionsExample.Domain;
using TransactionsExample.Infrastructure.Repositories;
using TransactionsExample.Services;
using TransactionsExample.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// builder.Services.AddScoped<IProductRepository, ProductRepositoryUnstable>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// builder.Services.AddScoped<IOrderService, OrderServiceUnsafe>();
// builder.Services.AddScoped<IOrderService, OrderServiceTransactional>();
builder.Services.AddScoped<IOrderService, OrderServiceUoW>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
