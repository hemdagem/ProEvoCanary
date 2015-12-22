using System.Collections.Generic;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IXmlGenerator
    {
        string GenerateXmlDocument(List<TeamIds> teamIds, int eventId);
    }
}
