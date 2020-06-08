namespace ProEvoCanary.Domain.EventHandlers.Configuration
{
	public interface IQuery<out TResult>
	{
		TResult Handle();
	}
}