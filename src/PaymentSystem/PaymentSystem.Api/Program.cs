using System.Reflection;
using Common.Infrastructure.Extensions;
using FluentValidation;
using PaymentSystem.IoC;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddHttpClient("order-management", client => {
    client.BaseAddress = new Uri(configuration["Services:OrderManagement:BaseAddress"]);
});

builder.Services.Register(configuration);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.AddServer("http://localhost:5002");
    });
}

app.InitializeDB();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();