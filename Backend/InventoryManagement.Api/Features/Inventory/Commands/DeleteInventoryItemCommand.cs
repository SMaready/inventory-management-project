using InventoryManagement.Api.Features.Shared.Interfaces;

namespace InventoryManagement.Api.Features.Inventory.Commands;

public record DeleteInventoryItemCommand : ICommand
{
    public required string Sku { get; set; }
}