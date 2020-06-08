using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;

namespace ProEvoCanary.DataAccess.Helpers
{
    public interface IXmlGenerator
    {
        string GenerateFixtures(List<TeamIds> teamIds, Guid eventId);
        string GenerateTournamentUsers(List<int> userIds, Guid eventId);
    }
}
