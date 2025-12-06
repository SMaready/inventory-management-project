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
        group.MapPost("/", async ([FromBody] CreateInventoryLocationCommand command,
            [FromServices] CreateInventoryLocationCommandHandler handler,
            [FromServices] CreateInventoryLocationCommandValidator validator,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, validator, cancellationToken);
            return result is not null ? Results.Created($"/api/location/{result.Id}", result) : Results.Problem("Could not create inventory location");
        })
        .WithName("AddInventoryLocation")
        .WithTags("InventoryLocations");

        // Define a PUT endpoint for updating an existing inventory location
        group.MapPut("/", async (UpdateInventoryLocationCommand command,
            [FromServices] UpdateInventoryLocationCommandHandler handler,
            [FromServices] UpdateInventoryLocationCommandValidator validator,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, validator, cancellationToken);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("UpdateInventoryLocation")
        .WithTags("InventoryLocations");

        // Define a DELETE endpoint for deleting an existing inventory location
        group.MapDelete("/{id:int}", async (int id,
            [FromServices] DeleteInventoryLocationCommandHandler handler,
            [FromServices] DeleteInventoryLocationCommandValidator validator,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteInventoryLocationCommand { LocationId = id };
            var result = await handler.Handle(command, validator, cancellationToken);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("DeleteInventoryLocation")
        .WithTags("InventoryLocations");

    }
}