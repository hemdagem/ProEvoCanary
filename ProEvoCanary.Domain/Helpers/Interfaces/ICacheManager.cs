namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface ICacheManager
    {
        void Add(string key, object value, int cacheHours);
        T Get<T>(string key);
 
    }
}