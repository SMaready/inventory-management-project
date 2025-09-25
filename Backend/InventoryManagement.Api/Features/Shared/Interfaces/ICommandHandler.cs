using FluentValidation;


namespace InventoryManagement.Api.Features.Shared.Interfaces;

// This interface defines a command handler that processes commands of type TCommand and returns results of type TResult.
// TCommand must implement the ICommand interface, and TResult must inherit from EntityBase.
// The Handle method takes a command, a validator for the command, and a cancellation token, and returns a Task that resolves to TResult.
public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand where TResult : EntityBase
{
    Task<TResult> Handle(TCommand command, AbstractValidator<TCommand> validator, CancellationToken cancellationToken);
}