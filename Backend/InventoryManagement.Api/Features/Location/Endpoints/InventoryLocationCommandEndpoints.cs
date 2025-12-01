using Microsoft.EntityFrameworkCore;
using InventoryManagement.Api.Database;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Api.Features.Location.Commands;
using InventoryManagement.Api.Features.Location.Validators;
using InventoryManagement.Api.Features.Location.Handlers;
using InventoryManagement.Api.Features.Location.Models;


namespace InventoryManagement.Api.Features.Location.Endpoints;

// This code defines an endpoint for handling commands related to inventory locations in an inventory management system.
// It uses ASP.NET Core's minimal APIs to create a POST endpoint for adding new inventory locations
public static class InventoryLocationCommandEndpoints
{

    public static void MapInventoryLocationCommandEndpoints(this IEndpointRouteBuilder app)
    {
        // Create a route group for inventory location-related endpoints
        var group = app.MapGroup("/api/locations")
            .WithTags("Inventory Locations");

        // Define a POST endpoint for creating a new inventory location
        // The endpoint accepts a CreateInventoryItemLocation command in the request body, uses a handler to process the command, and a validator to validate the command
        // [FromBody] indicates that the command should be deserialized from the request body
        // [FromServices] indicates that the handler and validator should be resolved from the dependency injection container
        group.MapPost("/", ([FromBody] CreateInventoryLocationCommand command,
            [FromServices] CreateInventoryLocationCommandHandler handler,
            [FromServices] CreateInventoryLocationCommandValidator validator,
            CancellationToken cancellationToken) =>
        {
            // var result = await handler.Handle(command, validator, cancellationToken);
            // return result is not null ? Results.Created($"/api/location/{result.Id}", result) : Results.Problem("Could not create inventory location");

            var newLocation = new InventoryLocation
            {
                Warehouse = command.Warehouse,
                Aisle = command.Aisle,
                Shelf = command.Shelf,
                Bin = command.Bin,
                Type = command.Type,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System"
            };

            MockDatabase.InventoryLocations.Add(newLocation);
            return Results.Created($"/api/location/{newLocation.Id}", newLocation);
        })
        .WithName("AddInventoryLocation")
        .WithTags("InventoryLocations");

        // Define a PUT endpoint for updating an existing inventory location
        group.MapPut("/{id:int}", (int id, UpdateInventoryLocationCommand command) =>
        {
            var existingLocation = MockDatabase.InventoryLocations.FirstOrDefault(loc => loc.Id == id);
            if (existingLocation is null)
            {
                return Results.NotFound();
            }

            existingLocation.Warehouse = command.Warehouse;
            existingLocation.Aisle = command.Aisle;
            existingLocation.Shelf = command.Shelf;
            existingLocation.Bin = command.Bin;
            existingLocation.Type = command.Type;

            return Results.Ok(existingLocation);
        })
        .WithName("UpdateInventoryLocation")
        .WithTags("InventoryLocations");

        // Define a DELETE endpoint for deleting an existing inventory location
        group.MapDelete("/{id:int}", (int id) =>
        {
            var existingLocation = MockDatabase.InventoryLocations.FirstOrDefault(loc => loc.Id == id);
            if (existingLocation is null)
            {
                return Results.NotFound();
            }

            MockDatabase.InventoryLocations.Remove(existingLocation);
            return Results.NoContent();
        })
        .WithName("DeleteInventoryLocation")
        .WithTags("InventoryLocations");

    }
}