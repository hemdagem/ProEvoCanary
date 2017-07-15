namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface ICacheManager
    {
        void Add(string key, object value, int cacheHours);
        object Get(string key);
 
    }
}