namespace ProEvoCanary.Domain.Repositories.Interfaces
{
    public interface ICacheEventRepository : IEventRepository
    {
        void AddToCache(string key, object value, int cacheHours);
    }
}