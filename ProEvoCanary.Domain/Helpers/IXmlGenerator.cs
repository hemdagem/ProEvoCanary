using System.Collections.Generic;
using ProEvoCanary.Domain;

namespace ProEvoCanary.Helpers
{
    public interface IXmlGenerator
    {
        string GenerateXmlDocument(List<TeamIds> teamIds, int eventId);
    }
}
