// CREATE WEB APPLICATION BUILDER
var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER
string connectionString = builder.Configuration.GetConnectionString("Database")!;
// add grpc service
builder.Services.AddGrpc();
// add sqlite database
builder.Services.AddDbContext<DiscountDbContext>(options => options.UseSqlite(connectionString));

// BUILD THE APPLICATION
var app = builder.Build();

// CONFIGURE THE HTTP REQUESTS PIPELINE
// use custom extension method to apply migrations to the database
app.UseMigration();
// map grpc service
app.MapGrpcService<DiscountService>();

// RUN THE APPLICATION
app.Run();
