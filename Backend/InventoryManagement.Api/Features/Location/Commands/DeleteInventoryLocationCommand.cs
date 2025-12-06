using InventoryManagement.Api.Features.Shared.Interfaces;

namespace InventoryManagement.Api.Features.Location.Commands;

public class DeleteInventoryLocationCommand : ICommand
{
    public required int LocationId { get; set; }
}