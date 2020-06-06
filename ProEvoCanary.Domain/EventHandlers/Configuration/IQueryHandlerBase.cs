namespace ProEvoCanary.Domain.EventHandlers.Configuration
{
	public interface IQueryHandlerBase<in TQuery, out TResult>
	{
		TResult Handle(TQuery command);
	}
}