using Orders.API.Configurations;
using Orders.API.Endpoint;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCustomMiddlewares();

var app = builder.Build();

app.ConfigureDevEnvironment();

app.UseSecurity();
app.MapEndpoints();

app.Run();
