using FluentValidation;
using InventoryManagement.Api.Database;
using InventoryManagement.Api.Features.Inventory.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Models;


namespace InventoryManagement.Api.Features.Inventory.Handlers;

public class CreateInventoryItemCommandHandler : ICommandHandler<CreateInventoryItemCommand, InventoryItem>
{
	private readonly InventoryManagementDbContext dbContext;
	private readonly HttpContext httpContext;

	public CreateInventoryItemCommandHandler(InventoryManagementDbContext dbContext, IHttpContextAccessor httpContextAccessor)
	{
		this.dbContext = dbContext;
		this.httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	public async Task<InventoryItem> Handle(CreateInventoryItemCommand command, AbstractValidator<CreateInventoryItemCommand> validator, CancellationToken cancellationToken)
	{
		var validationResult = validator.Validate(command);
		if (!validationResult.IsValid)
		{
			throw new ValidationException("Command validation failed", validationResult.Errors);
		}

		var newItem = new InventoryItem
		{
			Sku = command.Sku,
			Name = command.Name,
			Description = command.Description,
			OnHandQuantity = command.OnHandQuantity,
			Status = command.Status,
			LocationId = command.LocationId,
			CreatedOn = DateTime.UtcNow,
			CreatedBy = httpContext.User?.Identity?.Name ?? "System"
		};

		dbContext.InventoryItems.Add(newItem);
		await dbContext.SaveChangesAsync(cancellationToken);

		return newItem;
	}
}



