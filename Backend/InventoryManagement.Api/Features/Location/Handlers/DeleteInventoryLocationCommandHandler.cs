using FluentValidation;
using InventoryManagement.Api.Features.Location.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Database;

namespace InventoryManagement.Api.Features.Location.Handlers;

public class DeleteInventoryLocationCommandHandler : ICommandHandler<DeleteInventoryLocationCommand, InventoryLocation>
{
    private readonly InventoryManagementDbContext dbContext;

    public DeleteInventoryLocationCommandHandler(InventoryManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<InventoryLocation> Handle(DeleteInventoryLocationCommand command, AbstractValidator<DeleteInventoryLocationCommand> validator, CancellationToken cancellationToken)
    {
        if (!validator.Validate(command).IsValid)
        {
            throw new ValidationException("Command validation failed");
        }

        var location = await dbContext.InventoryLocations.FindAsync(new object[] { command.LocationId }, cancellationToken);
        if (location == null)
        {
            throw new KeyNotFoundException($"Inventory location with ID {command.LocationId} not found");
        }

        dbContext.InventoryLocations.Remove(location);
        var result = await dbContext.SaveChangesAsync(cancellationToken);

        return location;
    }
}