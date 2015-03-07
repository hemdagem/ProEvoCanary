using System.Collections.Generic;
using EventModel = ProEvoCanary.Models.EventModel;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<EventModel> GetEvents();
        EventModel GetEvent(int id);
    }
}