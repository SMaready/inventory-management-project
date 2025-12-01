using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Models;

public static class MockDatabase
{
    public static List<InventoryLocation> InventoryLocations { get; set; } = new List<InventoryLocation>
    {
        new InventoryLocation { Id = 1, Warehouse = "Warehouse 1", Aisle = "Aisle 1", Shelf = "Shelf 1", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 2, Warehouse = "Warehouse 1", Aisle = "Aisle 1", Shelf = "Shelf 1", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 3, Warehouse = "Warehouse 1", Aisle = "Aisle 1", Shelf = "Shelf 2", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 4, Warehouse = "Warehouse 1", Aisle = "Aisle 1", Shelf = "Shelf 2", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 5, Warehouse = "Warehouse 1", Aisle = "Aisle 2", Shelf = "Shelf 1", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 6, Warehouse = "Warehouse 1", Aisle = "Aisle 2", Shelf = "Shelf 1", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 7, Warehouse = "Warehouse 1", Aisle = "Aisle 2", Shelf = "Shelf 2", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 8, Warehouse = "Warehouse 1", Aisle = "Aisle 2", Shelf = "Shelf 2", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 9, Warehouse = "Warehouse 2", Aisle = "Aisle 1", Shelf = "Shelf 1", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 10, Warehouse = "Warehouse 2", Aisle = "Aisle 1", Shelf = "Shelf 1", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 11, Warehouse = "Warehouse 2", Aisle = "Aisle 1", Shelf = "Shelf 2", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 12, Warehouse = "Warehouse 2", Aisle = "Aisle 1", Shelf = "Shelf 2", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 13, Warehouse = "Warehouse 2", Aisle = "Aisle 2", Shelf = "Shelf 1", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 14, Warehouse = "Warehouse 2", Aisle = "Aisle 2", Shelf = "Shelf 1", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 15, Warehouse = "Warehouse 2", Aisle = "Aisle 2", Shelf = "Shelf 2", Bin = "Bin 1", CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryLocation { Id = 16, Warehouse = "Warehouse 2", Aisle = "Aisle 2", Shelf = "Shelf 2", Bin = "Bin 2", CreatedBy = "system", CreatedOn = DateTime.UtcNow }
    };

    public static List<InventoryItem> inventoryItems { get; set; } = new List<InventoryItem>
    {
        new InventoryItem { Id = 1, Sku = "SKU16578", Name = "iPhone 16", Description = "Apple Phone", Location = InventoryLocations[0], OnHandQuantity = 5, ReservedQuantity = 0, LocationId = 1, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 2, Sku = "SKU13450", Name = "Macbook", Description = "Apple Laptop", Location = InventoryLocations[1], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 2, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 3, Sku = "SKU12356", Name = "PS5", Description = "PlayStation Gaming Console", Location = InventoryLocations[2], OnHandQuantity = 8, ReservedQuantity = 0, LocationId = 3, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 4, Sku = "SKU18734", Name = "Xbox Series X", Description = "Microsoft Xbox Gaming Console", Location = InventoryLocations[3], OnHandQuantity = 15, ReservedQuantity = 0, LocationId = 4, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 5, Sku = "SKU20934", Name = "Xbox Controller", Description = "Gaming Controller for the Xbox Gaming Console", Location = InventoryLocations[4], OnHandQuantity = 20, ReservedQuantity = 0, LocationId = 5, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 6, Sku = "SKU44590", Name = "PS5 Controller", Description = "Gaming Controller for the Playstation Gaming Console", Location = InventoryLocations[5], OnHandQuantity = 5, ReservedQuantity = 0, LocationId = 6, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 7, Sku = "SKU23458", Name = "Studio Beats", Description = "Headphones made by Beats", Location = InventoryLocations[6], OnHandQuantity = 10, ReservedQuantity = 0, LocationId = 7, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 8, Sku = "SKU67890", Name = "Airpods Pro", Description = "Wireless Earbuds made by Apple", Location = InventoryLocations[7], OnHandQuantity = 25, ReservedQuantity = 0, LocationId = 8, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 9, Sku = "SKU11223", Name = "iPad Pro", Description = "Apple Tablet", Location = InventoryLocations[8], OnHandQuantity = 30, ReservedQuantity = 0, LocationId = 9, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 10, Sku = "SKU44556", Name = "Samsung Galaxy S23", Description = "Samsung Phone", Location = InventoryLocations[9], OnHandQuantity = 12, ReservedQuantity = 0, LocationId = 10, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 11, Sku = "SKU77889", Name = "Google Pixel 8", Description = "Google Phone", Location = InventoryLocations[10], OnHandQuantity = 18, ReservedQuantity = 0, LocationId = 11, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 12, Sku = "SKU99001", Name = "OnePlus 11", Description = "OnePlus Phone", Location = InventoryLocations[11], OnHandQuantity = 22, ReservedQuantity = 0, LocationId = 12, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 13, Sku = "SKU22334", Name = "Dell XPS 13", Description = "Dell Laptop", Location = InventoryLocations[12], OnHandQuantity = 14, ReservedQuantity = 0, LocationId = 13, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 14, Sku = "SKU55667", Name = "HP Spectre x360", Description = "HP Laptop", Location = InventoryLocations[13], OnHandQuantity = 9, ReservedQuantity = 0, LocationId = 14, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 15, Sku = "SKU88990", Name = "Lenovo ThinkPad X1 Carbon", Description = "Lenovo Laptop", Location = InventoryLocations[14], OnHandQuantity = 11, ReservedQuantity = 0, LocationId = 15, CreatedBy = "system", CreatedOn = DateTime.UtcNow },
        new InventoryItem { Id = 16, Sku = "SKU10112", Name = "Asus ROG Zephyrus G14", Description = "Asus Gaming Laptop", Location = InventoryLocations[15], OnHandQuantity = 7, ReservedQuantity = 0, LocationId = 16, CreatedBy = "system", CreatedOn = DateTime.UtcNow }
    };
}