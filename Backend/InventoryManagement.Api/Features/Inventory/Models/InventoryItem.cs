using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Models
{
    public class InventoryItem : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Sku { get; set; } = string.Empty;

        // Setting a default value for Status property to InventoryItemStatuses.New
        // This means that when a new InventoryItem object is created, its Status will be New by default
        public InventoryItemStatuses Status { get; set; } = InventoryItemStatuses.New;

        [Required]
        public string? Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int OnHandQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        // Precision attribute is used to define the precision and scale of the decimal property in the database
        // Here, UnitPrice can have up to 18 digits in total, with 2 digits after the decimal point
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        // Foreign key relationship to InventoryLocation
        public int LocationId { get; set; }

        // Navigation property to access the related InventoryLocation entity
        public InventoryLocation? Location { get; set; }

    }
}