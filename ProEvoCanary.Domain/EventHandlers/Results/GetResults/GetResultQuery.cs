using System;

namespace ProEvoCanary.Domain.EventHandlers.Results.GetResults
{
	public class GetResultQuery
	{
		public Guid Id { get; }

		public GetResultQuery(Guid id)
		{
			Id = id;
		}
	}
}