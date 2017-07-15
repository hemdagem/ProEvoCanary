using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IXmlGenerator
    {
        string GenerateFixtures(List<TeamIds> teamIds, int eventId);
        string GenerateTournamentUsers(List<int> userIds, int eventId);
    }
}
