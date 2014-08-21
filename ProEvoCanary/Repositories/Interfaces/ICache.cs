namespace ProEvoCanary.Repositories.Interfaces
{
    public interface ICache
    {
        void Add(string key, object value, int cacheHours);
        object Get(string key);
 
    }
}