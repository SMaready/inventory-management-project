using FluentValidation;
using InventoryManagement.Api.Database;
using InventoryManagement.Api.Features.Inventory.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

public class DeleteInventoryItemCommandHandler : ICommandHandler<DeleteInventoryItemCommand, InventoryItem>
{
    private readonly InventoryManagementDbContext dbContext;
    private readonly HttpContext httpContext;

    public DeleteInventoryItemCommandHandler(InventoryManagementDbContext dbContext, IHttpContextAccessor IHttpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContext = IHttpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));
    }

    public async Task<InventoryItem> Handle(DeleteInventoryItemCommand command,
        AbstractValidator<DeleteInventoryItemCommand> validator,
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

        item.DeletedBy = httpContext.User?.Identity?.Name ?? "System";
        item.DeletedOn = DateTime.UtcNow;

        dbContext.InventoryItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);

        return item;
    }
}