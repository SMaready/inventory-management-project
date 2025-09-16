public class CreateInventoryItemLocation : ICommand
{
    public required string Warehouse { get; set; }
    public required string Aisle { get; set; }
    public required string Shelf { get; set; }
    public required string Bin { get; set; }

    public InventoryLocationTypes Type { get; set; }
}