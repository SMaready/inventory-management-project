using System.ComponentModel.DataAnnotations;


namespace InventoryManagement.Api.Features.Location.Models;

public enum InventoryLocationTypes
{
    Stockroom = 1,
    PickingArea = 2,
    PackingArea = 3,
    ShippingArea = 4
}