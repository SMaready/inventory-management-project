using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Models
{
    public class InventoryItem
    {
        // Primary key
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public int OnHandQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        //ints by default are not nullable so we don't need to add [Required] attribute for Id and Quantity
    }
}