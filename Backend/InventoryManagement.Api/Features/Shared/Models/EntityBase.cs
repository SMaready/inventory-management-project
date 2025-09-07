using System.ComponentModel.DataAnnotations;

// Base class for entities with common properties
public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    [Required]
    [MaxLength(50)]
    public required string CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    [MaxLength(50)]
    public string? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    [MaxLength(50)]
    public string? DeletedBy { get; set; }
}