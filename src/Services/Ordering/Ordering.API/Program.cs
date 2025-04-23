using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

// CREATE WEB APPLICATION BUILDER
var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

// BUILD THE APPLICATION
var app = builder.Build();

// CONFIGURE THE HTTP REQUESTS PIPELINE
app.UseApiServices();

// RUN THE APPLICATION
app.Run();
