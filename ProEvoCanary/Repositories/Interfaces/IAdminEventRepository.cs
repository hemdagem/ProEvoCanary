using System;
using ProEvoCanary.Areas.Admin.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IAdminEventRepository :IEventRepository
    {
        int CreateEvent(string tournamentname, DateTime utcNow, EventTypes? eventType, int ownerId);
    }
}