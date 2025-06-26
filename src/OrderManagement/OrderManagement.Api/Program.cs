using System.Reflection;
using Common.Infrastructure.Extensions;
using FluentValidation;
using OrderManagement.IoC;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.Register(configuration);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddHttpClient("product-catalog", client => {
    client.BaseAddress = new Uri(configuration["Services:ProductCatalog:BaseAddress"]);
});

builder.Services.AddHttpClient("payment-system", client => {
    client.BaseAddress = new Uri(configuration["Services:PaymentSystem:BaseAddress"]);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.InitializeDB();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();