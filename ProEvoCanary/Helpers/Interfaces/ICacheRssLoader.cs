namespace ProEvoCanary.Helpers.Interfaces
{
    public interface ICacheRssLoader :IRssLoader
    {
        void AddToCache(string key, object value, int cacheHours);
    }
}