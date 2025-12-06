using FluentValidation;
using InventoryManagement.Api.Features.Location.Commands;


namespace InventoryManagement.Api.Features.Location.Validators;

public class UpdateInventoryLocationCommandValidator : AbstractValidator<UpdateInventoryLocationCommand>
{
    public UpdateInventoryLocationCommandValidator()
    {
        RuleFor(x => x.Warehouse)
            .NotEmpty().WithMessage("Warehouse is required")
            .MaximumLength(100).WithMessage("Warehouse cannot exceed 100 characters");
        RuleFor(x => x.Aisle)
            .NotEmpty().WithMessage("Aisle is required")
            .MaximumLength(100).WithMessage("Aisle cannot exceed 100 characters");
        RuleFor(x => x.Shelf)
            .NotEmpty().WithMessage("Shelf is required")
            .MaximumLength(100).WithMessage("Shelf cannot exceed 100 characters");
        RuleFor(x => x.Bin)
            .NotEmpty().WithMessage("Bin is required")
            .MaximumLength(100).WithMessage("Bin cannot exceed 100 characters");
    }
}