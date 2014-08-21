namespace ProEvoCanary.Repositories.Interfaces
{
    public interface ICachePlayerRepository : IPlayerRepository
    {
        void AddToCache(string key, object value, int cacheHours);
    }
}