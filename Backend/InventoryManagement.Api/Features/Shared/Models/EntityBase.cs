using System.ComponentModel.DataAnnotations;

// Base class for entities with common properties
// We use 'abstract' to prevent direct instantiation of this class, rather it should be inherited by other classes
public abstract class EntityBase
{
    // [Key] attribute indicates that this property is the primary key in the database
    // ints by default are not nullable so we don't need to add [Required] attribute for Id
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

// required modifier is used to indicate that a property must be initialized during object creation
// Nullable reference types (e.g., string?) are used to indicate that a property can hold a null value
// Data annotations like [Key], [Required], and [MaxLength] are used for validation and to define database schema constraints