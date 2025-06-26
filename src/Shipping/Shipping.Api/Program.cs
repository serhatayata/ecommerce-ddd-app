using System.Reflection;
using Common.Infrastructure.Extensions;
using FluentValidation;
using Scalar.AspNetCore;
using Shipping.IoC;

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