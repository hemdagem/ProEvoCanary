namespace ProEvoCanary.Domain.EventHandlers.Configuration
{
	public interface ICommandHandlerBase<in TCommand, out TResult>
	{
		TResult Handle(TCommand generateFixturesForEventCommand);
	}
}