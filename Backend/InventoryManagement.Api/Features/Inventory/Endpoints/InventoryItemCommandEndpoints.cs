using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Api.Features.Inventory.Commands;
using InventoryManagement.Api.Features.Inventory.Handlers;
using InventoryManagement.Api.Features.Inventory.Validators;
using InventoryManagement.Api.Models;


namespace InventoryManagement.Api.Features.Inventory.Endpoints;

public static class InventoryItemCommandEndpoints
{
	public static void MapInventoryItemCommandEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/inventory")
			.WithTags("Inventory Items");

		group.MapPost("/", async ([FromBody] CreateInventoryItemCommand command,
			[FromServices] CreateInventoryItemCommandHandler handler,
			[FromServices] CreateInventoryItemCommandValidator validator,
			CancellationToken cancellationToken) =>
		{
			var result = await handler.Handle(command, validator, cancellationToken);
			return result is not null ? Results.Created($"/api/inventory/{result.Id}", result) : Results.Problem("Could not create inventory item");
		})
		.WithName("AddInventoryItem")
		.WithTags("InventoryItems");
	}
}



