using System.Collections.Generic;
using ProEvoCanary.Models;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<EventModel> GetEvents();
    }
}