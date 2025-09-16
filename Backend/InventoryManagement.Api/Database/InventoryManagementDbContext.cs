using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

//Creating a class called InventoryManagementDbContext that inherits from DbContext to interact with the database
//DbContext is a part of Entity Framework Core, which is an Object-Relational Mapping framework for .NET applications
public class InventoryManagementDbContext : DbContext
{
    public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    //DbSet represents a collection of entities of a specific type that can be queried from the database
    //In this case, it represents a collection of InventoryItem entities and InventoryLocation entities
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<InventoryLocation> InventoryLocations { get; set; }

}