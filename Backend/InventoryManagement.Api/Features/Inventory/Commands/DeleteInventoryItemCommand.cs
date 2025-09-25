using InventoryManagement.Api.Features.Shared.Interfaces;

public record DeleteInventoryItemCommand : ICommand
{
    public required string Sku { get; set; }
}