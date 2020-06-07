using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IXmlGenerator
    {
        string GenerateFixtures(List<TeamIds> teamIds, Guid eventId);
        string GenerateTournamentUsers(List<int> userIds, Guid eventId);
    }
}
