using InventoryManagement.Api.Features.Shared.Interfaces;

namespace InventoryManagement.Api.Features.Inventory.Commands;

public record UpdateInventoryItemCommand : ICommand
{
    public required string Sku { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int OnHandQuantity { get; set; }
    public InventoryItemStatuses Status { get; set; }
    public int LocationId { get; set; }
}