// We define a command for creating a new inventory item
// This command implements the ICommand interface, which is a marker interface for commands in the CQRS pattern
// We use 'record' to define an immutable data structure that holds the properties needed to create an inventory item
using InventoryManagement.Api.Features.Shared.Interfaces;

public record CreateInventoryItemCommand : ICommand
{
    public required string Sku { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public int OnHandQuantity { get; set; }
    public InventoryItemStatuses Status { get; set; } = InventoryItemStatuses.New;
    public int LocationId { get; set; }
}