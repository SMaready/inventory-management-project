using FluentValidation;
using InventoryManagement.Api.Features.Inventory.Commands;


namespace InventoryManagement.Api.Features.Inventory.Validators;

public class CreateInventoryItemCommandValidator : AbstractValidator<CreateInventoryItemCommand>
{
	public CreateInventoryItemCommandValidator()
	{
		RuleFor(x => x.Sku)
			.NotEmpty().WithMessage("Sku is required")
			.MaximumLength(100).WithMessage("Sku cannot exceed 100 characters");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Name is required")
			.MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

		RuleFor(x => x.Description)
			.MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

		RuleFor(x => x.OnHandQuantity)
			.GreaterThanOrEqualTo(0).WithMessage("OnHandQuantity cannot be negative");

		RuleFor(x => x.LocationId)
			.GreaterThan(0).WithMessage("LocationId must be a positive integer");
	}
}



