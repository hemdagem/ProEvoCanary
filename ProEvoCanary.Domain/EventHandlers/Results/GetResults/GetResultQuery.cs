using System;

namespace ProEvoCanary.Application.EventHandlers.Results.GetResults
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