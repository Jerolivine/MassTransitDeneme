using MassTransit;
using MassTransitDeneme.Configuration;
using MassTransitDeneme.Configuration.RabbitMQ;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitWithRabbitMqTransport(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.ConfigureSwagger();

var app = builder.Build();

app.ConfigureSwagger();

app.MapControllers();

app.Run();
