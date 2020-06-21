using System;
using ProEvoCanary.Application.EventHandlers.Configuration;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.Application.EventHandlers.Results.AddResult
{
	public interface IAddResultCommandHandler : ICommandHandler<AddResultCommand, Guid>
	{

	}
	public class AddResultCommandHandler : IAddResultCommandHandler
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
