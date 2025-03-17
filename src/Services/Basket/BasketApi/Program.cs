// CREATE WEB APPLICATION BUILDER
var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER
var assembly = typeof(Program).Assembly;
string postgresConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Redis")!;

// add 'MediatR' for CQRS pattern, validation and logging behaviors
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(assembly);
    options.AddOpenBehavior(typeof(ValidationBehavior<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

// add 'Carter' for minimal api
builder.Services.AddCarter();

// add 'Marten' for transactional document DB on postgres
builder.Services.AddMarten(options =>
{
    options.Connection(postgresConnectionString);
    options.Schema.For<ShoppingCard>().Identity(x => x.UserName);
}).UseLightweightSessions();

// add custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// add health check for the application, and for the database
builder.Services.AddHealthChecks()
    .AddNpgSql(postgresConnectionString)
    .AddRedis(redisConnectionString);

// add other services using dependency injection
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

// add redis cache
builder.Services.AddStackExchangeRedisCache(options => 
{
    options.Configuration = redisConnectionString;
});

// BUILD THE APPLICATION
var app = builder.Build();

// CONFIGURE THE HTTP REQUESTS PIPELINE
// map controllers
app.MapCarter();

// configure application to use global exceptions handler
app.UseExceptionHandler(options => { });

// configure health check
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// RUN THE APPLICATION
app.Run();
