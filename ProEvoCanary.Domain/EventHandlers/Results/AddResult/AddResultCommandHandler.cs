using System;
using System.Collections.Generic;
using System.Text;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.Helpers;

namespace ProEvoCanary.Domain.EventHandlers.Results
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
