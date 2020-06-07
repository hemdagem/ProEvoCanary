using System;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface ICacheManager
    {
        T AddOrGetExisting<T>(string key, Func<T> getItemFromSource);
    }
}