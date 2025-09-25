using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Features.Location.Models;


namespace InventoryManagement.Api.Features.Location.Commands;

public class UpdateInventoryLocationCommand : ICommand
{
    public required string Warehouse { get; set; }
    public required string Aisle { get; set; }
    public required string Shelf { get; set; }
    public required string Bin { get; set; }

    public InventoryLocationTypes Type { get; set; }
}