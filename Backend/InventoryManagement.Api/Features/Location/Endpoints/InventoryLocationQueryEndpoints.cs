using Microsoft.EntityFrameworkCore;
using InventoryManagement.Api.Database;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Api.Features.Location.Commands;
using InventoryManagement.Api.Features.Location.Validators;
using InventoryManagement.Api.Features.Location.Handlers;


namespace InventoryManagement.Api.Features.Location.Endpoints;

// This code defines an endpoint for handling commands related to inventory locations in an inventory management system.
// It uses ASP.NET Core's minimal APIs to create a POST endpoint for adding new inventory locations
public static class InventoryLocationQueryEndpoints
{

    public static void MapInventoryLocationQueryEndpoints(this IEndpointRouteBuilder app)
    {
        // Create a route group for inventory location-related endpoints
        var group = app.MapGroup("/api/locations")
            .WithTags("Inventory Locations");


        // Define a GET endpoint for retrieving all inventory locations
        group.MapGet("/", (InventoryManagementDbContext db) =>
        {
            var results = db.InventoryLocations.ToList();
            return Results.Ok(results);
        })
        .WithName("GetInventoryLocations")
        .WithTags("InventoryLocations");

        // Define a GET endpoint for retrieving a specific inventory location by its ID
        group.MapGet("/{id:int}", (int id, InventoryManagementDbContext db) =>
        {
            var result = db.InventoryLocations.FirstOrDefault(loc => loc.Id == id);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInventoryLocationById")
        .WithTags("InventoryLocations");
    }
}