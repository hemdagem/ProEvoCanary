namespace ProEvoCanary.Application.EventHandlers.Configuration
{
	public interface IQuery<out TResult>
	{
		TResult Handle();
	}
}