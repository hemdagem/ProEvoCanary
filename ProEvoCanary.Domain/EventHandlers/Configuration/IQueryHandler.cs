namespace ProEvoCanary.Domain.EventHandlers.Configuration
{
	public interface IQueryHandler<in TQuery, out TResult>
	{
		TResult Handle(TQuery query);
	}
}