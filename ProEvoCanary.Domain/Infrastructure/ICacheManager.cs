using System;

namespace ProEvoCanary.Domain.Helpers
{
    public interface ICacheManager
    {
        T AddOrGetExisting<T>(string key, Func<T> getItemFromSource);
    }
}