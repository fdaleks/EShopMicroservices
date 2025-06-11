using Discount.Grpc;
using BuildingBlocks.Messaging.MassTransit;

// CREATE WEB APPLICATION BUILDER
var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER
var assembly = typeof(Program).Assembly;
string postgConStr = builder.Configuration.GetConnectionString("Database")!;
string redisConStr = builder.Configuration.GetConnectionString("Redis")!;

// add application services
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(assembly);
    options.AddOpenBehavior(typeof(ValidationBehavior<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();

// add data services
builder.Services.AddMarten(options =>
{
    options.Connection(postgConStr);
    options.Schema.For<ShoppingCard>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConStr;
});

// add grpc services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() => 
{
    var handler = new HttpClientHandler
    {
        // don't use it in prod!
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

// add async communication service
builder.Services.AddMessageBroker(builder.Configuration);

// add cross-cutting services
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(postgConStr)
    .AddRedis(redisConStr);

// BUILD THE APPLICATION
var app = builder.Build();

// CONFIGURE THE HTTP REQUESTS PIPELINE
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// RUN THE APPLICATION
app.Run();
