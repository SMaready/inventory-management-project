using InventoryManagement.Api.Database;
using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("InventoryManagementDatabase"));
    options.UseInMemoryDatabase("InventoryManagementInMemoryDatabase");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddLocationFeature();
builder.Services.AddInventoryFeature();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<InventoryManagementDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseLocationFeature();
app.UseInventoryFeature();

app.Run();