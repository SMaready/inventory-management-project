using Microsoft.EntityFrameworkCore;
using InventoryManagement.Api.Database;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Features.Inventory.Endpoints;

public static class InventoryItemQueryEndpoints
{
    public static void MapInventoryItemQueryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventory")
            .WithTags("Inventory Items");

        group.MapGet("/", () =>
        {
            var results = MockDataabase.inventoryItems;
            return Results.Ok(results);
        })
        .WithName("GetInventoryItems")
        .WithTags("InventoryItems");

        group.MapGet("/{id:int}", (int id) =>
        {
            var result = MockDataabase.inventoryItems.FirstOrDefault(item => item.Id == id);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInventoryItemById")
        .WithTags("InventoryItems");
    }
}