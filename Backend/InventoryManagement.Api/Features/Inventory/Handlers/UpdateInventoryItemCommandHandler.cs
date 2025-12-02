using FluentValidation;
using InventoryManagement.Api.Database;
using InventoryManagement.Api.Features.Inventory.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

public class UpdateInventoryItemCommandHandler : ICommandHandler<UpdateInventoryItemCommand, InventoryItem>
{
    private readonly InventoryManagementDbContext dbContext;
    private readonly HttpContext httpContext;

    public UpdateInventoryItemCommandHandler(InventoryManagementDbContext dbContext, IHttpContextAccessor IHttpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContext = IHttpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));
    }

    public async Task<InventoryItem> Handle(UpdateInventoryItemCommand command,
        AbstractValidator<UpdateInventoryItemCommand> validator,
        CancellationToken cancellationToken)
    {
        if (!validator.Validate(command).IsValid)
        {
            throw new ValidationException("Invalid inventory item data.");
        }

        var item = await dbContext.InventoryItems
            .Where(item => item.Sku == command.Sku)
            .FirstOrDefaultAsync(cancellationToken);

        if (item == null)
        {
            throw new KeyNotFoundException($"Inventory item with SKU {command.Sku} not found.");
        }

        item.Sku = command.Sku;
        item.Name = command.Name;
        item.Description = command.Description;
        item.OnHandQuantity = command.OnHandQuantity;
        item.Status = command.Status;
        item.LocationId = command.LocationId;
        item.UpdatedOn = DateTime.UtcNow;
        item.UpdatedBy = httpContext.User?.Identity?.Name ?? "System";

        dbContext.InventoryItems.Update(item);
        await dbContext.SaveChangesAsync(cancellationToken);

        return item;
    }
}