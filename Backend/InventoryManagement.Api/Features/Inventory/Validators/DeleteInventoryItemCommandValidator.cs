using FluentValidation;
using InventoryManagement.Api.Features.Inventory.Commands;

namespace InventoryManagement.Api.Features.Inventory.Validators;

public class DeleteInventoryItemCommandValidator : AbstractValidator<DeleteInventoryItemCommand>
{
    public DeleteInventoryItemCommandValidator()
    {
        RuleFor(command => command.Sku)
            .NotEmpty().WithMessage("SKU is required.")
            .MaximumLength(100).WithMessage("SKU must not exceed 100 characters.");
    }
}