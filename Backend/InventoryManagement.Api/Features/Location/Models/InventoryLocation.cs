using System.ComponentModel.DataAnnotations;


namespace InventoryManagement.Api.Features.Location.Models;

// InventoryLocation class represents a location in the inventory system
public class InventoryLocation : EntityBase
{
    [Required]
    [MaxLength(100)]
    public required string Warehouse { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Aisle { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Shelf { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Bin { get; set; }

    public InventoryLocationTypes Type { get; set; }

}