using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
	public class EventWriteRepository : IEventWriteRepository
	{
		private readonly IDbHelper _helper;
		private readonly IXmlGenerator _xmlGenerator;

		public EventWriteRepository(IDbHelper helper, IXmlGenerator xmlGenerator)
		{
			_helper = helper;
			_xmlGenerator = xmlGenerator;
		}
		public int CreateEvent(string tournamentName, DateTime date, int eventType)
		{
			if (string.IsNullOrEmpty(tournamentName))
			{
				throw new NullReferenceException("Tournament Name is null or empty");
			}

			return _helper.ExecuteScalar("up_AddTournament", new { TournamentName = tournamentName, TournamentType = eventType, Date = date });
		}

		public void GenerateFixtures(Guid eventId, List<int> userIds)
		{
			var generator = new FixtureGenerator();
			var teamIds = generator.Generate(userIds);

			var documentString = _xmlGenerator.GenerateFixtures(teamIds, eventId);

			var parameters = new
			{
				XmlString = documentString
			};

			_helper.ExecuteNonQuery("up_AddFixtures", parameters);
		}

		public int AddTournamentUsers(Guid eventId, List<int> userIds)
		{
			var documentString = _xmlGenerator.GenerateTournamentUsers(userIds, eventId);

			var parameters = new
			{
				XmlString = documentString
			};

			return _helper.ExecuteNonQuery("up_AddTournamentUsers", parameters);
		}
	}
}