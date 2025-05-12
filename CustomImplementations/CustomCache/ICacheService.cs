using System;

namespace CustomImplementations.CustomCache
{
    public interface ICacheService
    {
        void Set<T>(string key, T value,TimeSpan? expiryTime=null);
        CacheResult<T>? GetT<T>(string key);
        void Remove(string key);
    }
}
