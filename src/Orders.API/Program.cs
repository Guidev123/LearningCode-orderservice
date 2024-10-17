using Orders.API.Endpoint;
using Orders.API.Middlewares;
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
