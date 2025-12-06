using Microsoft.EntityFrameworkCore;
using InventoryManagement.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace InventoryManagement.Api.Features.Inventory.Endpoints;

public static class InventoryItemQueryEndpoints
{
    public static void MapInventoryItemQueryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventory")
            .WithTags("Inventory Items");

        // group.MapGet("/seed", (InventoryManagementDbContext db) =>
        // {
        //     db.InventoryLocations.AddRange(MockDatabase.InventoryLocations);
        //     db.InventoryItems.AddRange(MockDatabase.InventoryItems);
        //     db.SaveChanges();
        //     return Results.Ok();
        // });

        group.MapGet("/", (InventoryManagementDbContext db) =>
        {
            var results = db.InventoryItems.Include(x => x.Location).ToList();
            return Results.Ok(results);
        })
        .WithName("GetInventoryItems")
        .WithTags("InventoryItems");

        group.MapGet("/{id:int}", (int id, InventoryManagementDbContext db) =>
        {
            var result = db.InventoryItems.Where(item => item.Id == id).FirstOrDefault();
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInventoryItemById")
        .WithTags("InventoryItems");
    }
}