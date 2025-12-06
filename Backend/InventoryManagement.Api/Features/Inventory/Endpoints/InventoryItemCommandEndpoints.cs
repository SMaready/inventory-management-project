using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Api.Features.Inventory.Commands;
using InventoryManagement.Api.Features.Inventory.Handlers;
using InventoryManagement.Api.Features.Inventory.Validators;
using InventoryManagement.Api.Models;
using System.Text.RegularExpressions;


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

		group.MapPut("/", async ([FromBody] UpdateInventoryItemCommand command,
			[FromServices] UpdateInventoryItemCommandHandler handler,
			[FromServices] UpdateInventoryItemCommandValidator validator,
			CancellationToken cancellationToken) =>
		{
			var result = await handler.Handle(command, validator, cancellationToken);
			return result is not null ? Results.Ok(result) : Results.NotFound();
		})
		.WithName("UpdateInventoryItem")
		.WithTags("InventoryItems");

		group.MapDelete("/{sku}", async ([FromRoute] string sku,
			[FromServices] DeleteInventoryItemCommandHandler handler,
			[FromServices] DeleteInventoryItemCommandValidator validator,
			CancellationToken cancellationToken) =>
		{
			var command = new DeleteInventoryItemCommand { Sku = sku };
			var result = await handler.Handle(command, validator, cancellationToken);
			return result is not null ? Results.Ok(result) : Results.NotFound();
		})
		.WithName("DeleteInventoryItem")
		.WithTags("InventoryItems");
	}
}



