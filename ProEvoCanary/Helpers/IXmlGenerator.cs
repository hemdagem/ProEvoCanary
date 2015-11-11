using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public interface IXmlGenerator
    {
        string GenerateXmlDocument(List<TeamIds> teamIds, int eventId);
    }
}
