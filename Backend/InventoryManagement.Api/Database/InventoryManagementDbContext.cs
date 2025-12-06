using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

//Creating a class called InventoryManagementDbContext that inherits from DbContext to interact with the database
//DbContext is a part of Entity Framework Core, which is an Object-Relational Mapping framework for .NET applications

namespace InventoryManagement.Api.Database;

public class InventoryManagementDbContext : DbContext
{
    public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InventoryLocation>().HasData(MockDatabase.InventoryLocations);
        modelBuilder.Entity<InventoryItem>().HasData(MockDatabase.InventoryItems);
    }

    //DbSet represents a collection of entities of a specific type that can be queried from the database
    //In this case, it represents a collection of InventoryItem entities and InventoryLocation entities
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<InventoryLocation> InventoryLocations { get; set; }

}