using InventoryManagement.Api.Features.Inventory.Endpoints;

public static class RegisterInventoryFeature
{
    // Extension method to register inventory-related services
    public static IServiceCollection AddInventoryFeature(this IServiceCollection services)
    {
        //Once we have commands and handlers, we can register them here just like we do for Locations
        return services;
    }

    // Extension method to map inventory-related endpoints
    // Greatly improves the readability of the Program.cs file
    public static void UseInventoryFeature(this IEndpointRouteBuilder app)
    {
        app.MapInventoryItemQueryEndpoints();
        //app.MapInventoryItemCommandEndpoints(); // Uncomment when command endpoints are implemented
    }
}