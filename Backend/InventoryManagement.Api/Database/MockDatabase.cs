using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Models;

public static class MockDataabase
{
    public static List<InventoryLocation> InventoryLocations { get; set; } = new List<InventoryLocation>
    {
        new InventoryLocation { Id = 1, Warehouse = "W1", Aisle = "A1", Shelf = "S1", Bin = "B1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 2, Warehouse = "W1", Aisle = "A1", Shelf = "S1", Bin = "B1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 1, Warehouse = "W1", Aisle = "A1", Shelf = "S1", Bin = "B1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 1, Warehouse = "W1", Aisle = "A1", Shelf = "S1", Bin = "B1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 1, Warehouse = "W1", Aisle = "A1", Shelf = "S1", Bin = "B1", CreatedBy = "system", CreatedOn = DateTime.UtcNow }
    };

    public static List<InventoryItem> inventoryItems { get; set; } = new List<InventoryItem>
    {
        new InventoryItem { Id = 1, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 2, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 3, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 1, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 1, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 1, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 1, Sku = "SKU1", Name = "Item1", Description = "Description1", Location = InventoryLocations[0], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },

    };
}