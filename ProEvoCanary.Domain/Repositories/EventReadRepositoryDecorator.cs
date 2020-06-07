using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class EventReadRepositoryDecorator : IEventReadRepository
    {
        private readonly ICacheManager _cacheManager;
        private readonly IEventReadRepository _eventRepository;
        private const string EventsListCacheKey = "EventsListCache";

        public EventReadRepositoryDecorator(ICacheManager cacheManager, IEventReadRepository eventRepository)
        {
            _cacheManager = cacheManager;
            _eventRepository = eventRepository;
        }

        public List<EventModel> GetEvents()
        {
	        return _cacheManager.AddOrGetExisting(EventsListCacheKey, () => _eventRepository.GetEvents());
        }

        public EventModel GetEvent(Guid id)
        {
	        return _cacheManager.AddOrGetExisting(id.ToString(), () => _eventRepository.GetEvent(id));
        }

    }
}