using FluentValidation;
using InventoryManagement.Api.Features.Location.Commands;


namespace InventoryManagement.Api.Features.Location.Validators;

public class DeleteInventoryLocationCommandValidator : AbstractValidator<DeleteInventoryLocationCommand>
{
    public DeleteInventoryLocationCommandValidator()
    {
        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage("LocationId must be greater than zero");
    }
}