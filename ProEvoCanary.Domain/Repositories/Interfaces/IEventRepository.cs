using System.Collections.Generic;
using EventModel = ProEvoCanary.Domain.EventModel;

namespace ProEvoCanary.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<EventModel> GetEvents();
        EventModel GetEvent(int id);
        EventModel GetEventForEdit(int id, int ownerId);
    }
}