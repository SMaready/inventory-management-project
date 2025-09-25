using InventoryManagement.Api.Features.Location.Endpoints;
using InventoryManagement.Api.Features.Location.Handlers;
using InventoryManagement.Api.Features.Location.Validators;

public static class RegisterLocationFeature
{
    // Extension method to register location-related services
    public static IServiceCollection AddLocationFeature(this IServiceCollection services)
    {
        services.AddTransient<CreateInventoryLocationCommandValidator>();
        services.AddTransient<CreateInventoryLocationCommandHandler>();
        return services;
    }

    // Extension method to map location-related endpoints
    // Greatly improves the readability of the Program.cs file
    public static void UseLocationFeature(this IEndpointRouteBuilder app)
    {
        app.MapInventoryLocationQueryEndpoints();
        app.MapInventoryLocationCommandEndpoints();
    }
}