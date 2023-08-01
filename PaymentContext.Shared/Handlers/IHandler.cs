using PaymentContext.Shared.Commands;
using ICommand = System.Windows.Input.ICommand;

namespace PaymentContext.Shared.Handlers;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}