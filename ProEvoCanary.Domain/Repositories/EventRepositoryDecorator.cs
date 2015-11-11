using System.Collections.Generic;
using ProEvoCanary.Domain;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class EventRepositoryDecorator : IEventRepository
    {
        private readonly ICacheEventRepository _cacheEventRepository;
        private readonly IEventRepository _eventRepository;
        private const int CacheHours = 30;

        private const string EventsListCacheKey = "EventsListCache";

        public EventRepositoryDecorator(ICacheEventRepository cacheEventRepository, IEventRepository eventRepository)
        {
            _cacheEventRepository = cacheEventRepository;
            _eventRepository = eventRepository;
        }

        public List<EventModel> GetEvents()
        {
            var events = _cacheEventRepository.GetEvents();

            if (events == null)
            {
                events = _eventRepository.GetEvents();
                _cacheEventRepository.AddToCache(EventsListCacheKey, events, CacheHours);
            }

            return events;
        }

        public EventModel GetEvent(int id)
        {
            throw new System.NotImplementedException();
        }

        public EventModel GetEventForEdit(int id, int ownerId)
        {
            throw new System.NotImplementedException();
        }

    }
}