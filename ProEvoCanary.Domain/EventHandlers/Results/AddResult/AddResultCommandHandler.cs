using System;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Results.GetResults;

namespace ProEvoCanary.Domain.EventHandlers.Results.AddResult
{
	public class AddResultCommandHandler :ICommandHandler<AddResultCommand,Guid>
	{
		private readonly IResultRepository _resultRepository;

		public AddResultCommandHandler(IResultRepository resultRepository)
		{
			_resultRepository = resultRepository;
		}
		public Guid Handle(AddResultCommand command)
		{
			_resultRepository.AddResult(command.Id, command.HomeScore, command.AwayScore);
			return command.Id;
		}
	}
}
