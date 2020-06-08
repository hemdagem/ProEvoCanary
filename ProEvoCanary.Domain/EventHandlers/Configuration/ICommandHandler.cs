namespace ProEvoCanary.Domain.EventHandlers.Configuration
{
	public interface ICommandHandler<in TCommand, out TResult>
	{
		TResult Handle(TCommand command);
	}
}