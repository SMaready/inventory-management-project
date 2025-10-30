using InventoryManagement.Api.Features.Inventory.Endpoints;
using InventoryManagement.Api.Features.Inventory.Handlers;
using InventoryManagement.Api.Features.Inventory.Validators;

public static class RegisterInventoryFeature
{
    // Extension method to register inventory-related services
    public static IServiceCollection AddInventoryFeature(this IServiceCollection services)
    {
        services.AddTransient<CreateInventoryItemCommandValidator>();
        services.AddTransient<CreateInventoryItemCommandHandler>();
        return services;
    }

    // Extension method to map inventory-related endpoints
    // Greatly improves the readability of the Program.cs file
    public static void UseInventoryFeature(this IEndpointRouteBuilder app)
    {
        app.MapInventoryItemQueryEndpoints();
        app.MapInventoryItemCommandEndpoints();
    }
}