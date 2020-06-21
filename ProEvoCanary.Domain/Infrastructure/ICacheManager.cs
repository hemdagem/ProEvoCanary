using System;

namespace ProEvoCanary.Application.Infrastructure
{
    public interface ICacheManager
    {
        T AddOrGetExisting<T>(string key, Func<T> getItemFromSource);
    }
}