public record DeleteInventoryItemCommand : ICommand
{
    public required string Sku { get; set; }
}