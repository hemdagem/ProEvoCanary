using System.Collections.Generic;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class EventRepositoryDecorator : IEventRepository
    {
        private readonly ICacheEventRepository _cacheEventRepository;
        private readonly IEventRepository _eventRepository;

        private const string EventsListCacheKey = "EventsListCache";

        public EventRepositoryDecorator(ICacheEventRepository cacheEventRepository, IEventRepository eventRepository)
        {
            _cacheEventRepository = cacheEventRepository;
            _eventRepository = eventRepository;
        }

        public EventRepositoryDecorator() : this(new CacheEventRepository(), new EventRepository()) { }

        public List<EventModel> GetEvents()
        {
            var events = _cacheEventRepository.GetEvents();

            if (events != null) return events;

            events = _eventRepository.GetEvents();

            _cacheEventRepository.AddToCache(EventsListCacheKey, events, 30);

            return events;
        }
    }
}