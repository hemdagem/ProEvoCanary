using System;

namespace ProEvoCanary.Domain.Infrastructure
{
    public interface ICacheManager
    {
        T AddOrGetExisting<T>(string key, Func<T> getItemFromSource);
    }
}