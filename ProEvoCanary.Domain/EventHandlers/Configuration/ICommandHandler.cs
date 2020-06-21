namespace ProEvoCanary.Application.EventHandlers.Configuration
{
	public interface ICommandHandler<in TCommand, out TResult>
	{
		TResult Handle(TCommand command);
	}
}