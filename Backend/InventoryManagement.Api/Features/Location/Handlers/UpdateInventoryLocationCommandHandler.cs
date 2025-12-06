using FluentValidation;
using InventoryManagement.Api.Features.Location.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Database;


namespace InventoryManagement.Api.Features.Location.Handlers;

public class UpdateInventoryLocationCommandHandler : ICommandHandler<UpdateInventoryLocationCommand, InventoryLocation>
{
    private readonly InventoryManagementDbContext dbContext;
    private readonly HttpContext httpContext;

    public UpdateInventoryLocationCommandHandler(InventoryManagementDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<InventoryLocation> Handle(UpdateInventoryLocationCommand command, AbstractValidator<UpdateInventoryLocationCommand> validator, CancellationToken cancellationToken)
    {
        if (!validator.Validate(command).IsValid)
        {
            throw new ValidationException("Command validation failed");
        }

        var existingLocation = await dbContext.InventoryLocations.FindAsync(new object[] { command.Id }, cancellationToken);
        if (existingLocation == null)
        {
            throw new KeyNotFoundException($"Inventory location with ID {command.Id} not found");
        }

        existingLocation.Warehouse = command.Warehouse;
        existingLocation.Aisle = command.Aisle;
        existingLocation.Shelf = command.Shelf;
        existingLocation.Bin = command.Bin;
        existingLocation.Type = command.Type;
        existingLocation.UpdatedOn = DateTime.UtcNow;
        existingLocation.UpdatedBy = httpContext.User?.Identity?.Name ?? "System";

        dbContext.InventoryLocations.Update(existingLocation);
        await dbContext.SaveChangesAsync(cancellationToken);

        return existingLocation;
    }
}