namespace ProEvoCanary.Application.EventHandlers.Configuration
{
	public interface IQueryHandler<in TQuery, out TResult>
	{
		TResult Handle(TQuery query);
	}
}