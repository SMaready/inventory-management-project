using FluentValidation;
using InventoryManagement.Api.Features.Location.Commands;
using InventoryManagement.Api.Features.Shared.Interfaces;
using InventoryManagement.Api.Features.Location.Models;
using InventoryManagement.Api.Database;


namespace InventoryManagement.Api.Features.Location.Handlers;

public class CreateInventoryLocationCommandHandler : ICommandHandler<CreateInventoryLocationCommand, InventoryLocation>
{
    private readonly InventoryManagementDbContext dbContext;
    private readonly HttpContext httpContext;

    public CreateInventoryLocationCommandHandler(InventoryManagementDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<InventoryLocation> Handle(CreateInventoryLocationCommand command, AbstractValidator<CreateInventoryLocationCommand> validator, CancellationToken cancellationToken)
    {
        if (!validator.Validate(command).IsValid)
        {
            throw new ValidationException("Command validation failed");
        }

        var newLocation = new InventoryLocation
        {
            Warehouse = command.Warehouse,
            Aisle = command.Aisle,
            Shelf = command.Shelf,
            Bin = command.Bin,
            Type = command.Type,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = httpContext.User?.Identity?.Name ?? "System"
        };

        dbContext.InventoryLocations.Add(newLocation);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLocation;
    }
}