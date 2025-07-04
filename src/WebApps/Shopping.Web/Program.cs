// CREATE WEB APPLICATION BUILDER
var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO THE CONTAINER
builder.Services.AddRazorPages();

builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(x =>
    {
        x.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    });

builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(x =>
    {
        x.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    });

builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(x =>
    {
        x.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    });

// BUILD THE APPLICATION
var app = builder.Build();

// CONFIGURE THE HTTP REQUESTS PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// RUN THE APPLICATION
app.Run();
