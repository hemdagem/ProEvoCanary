using System;

namespace ProEvoCanary.Application.EventHandlers.Results.AddResult
{
	public class AddResultCommand
	{
		public Guid Id { get; }
		public int HomeScore { get; }
		public int AwayScore { get; }

		public AddResultCommand(Guid id, int homeScore, int awayScore)
		{
			Id = id;
			HomeScore = homeScore;
			AwayScore = awayScore;
		}
	}
}