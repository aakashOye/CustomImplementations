using CustomImplementations.CustomCache;
using System.Collections.Concurrent;

namespace CustomImplementations.Services
{
    public class InMemoryCacheService : ICacheService, IDisposable
    {
        private readonly LoggerService _loggerService;
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly Task _cleanupTask;

        public InMemoryCacheService(LoggerService loggerService)
        {
            _cleanupTask = Task.Run(CleanupTaskAsync);
            _loggerService = loggerService;
        }

        public CacheResult<T>? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                if (item.IsExpired)
                {
                    // If the item is expired, remove it and return a cache miss
                    _loggerService.Log($"[Cache EXPIRED] Key: {key}");
                    Remove(key);
                    return CacheResult<T>.Miss();
                }

                // Refresh the item to extend its expiration time
                item.Refresh(); // Extend the life of the item - Sliding Expiration

                if (item.Value is T typedValue)
                {
                    // If the item is found and not expired, return a cache hit
                    _loggerService.Log($"[Cache HIT] Key: {key}");
                    return CacheResult<T>.Hit(typedValue);
                }
                   
            }
            
            // If the item is not found or expired, return a cache miss
            _loggerService.Log($"[Cache MISS] Key: {key}");
            return CacheResult<T>.Miss();
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            var item = new CacheItem(value!, expiry);
            _cache[key] = item;
            _loggerService.Log($"[Cache SET] Key: {key}, TTL: {(expiry).Value.TotalSeconds}s");
        }

        public void Remove(string key)
        {
            _cache.TryRemove(key, out _);
            _loggerService.Log($"[Cache REMOVE] Key: {key}");
        }

        private async Task CleanupTaskAsync()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    foreach (var key in _cache.Keys)
                    {
                        if (_cache.TryGetValue(key, out var item) && item.IsExpired)
                        {
                            Remove(key);
                        }
                    }
                }
                catch(Exception ex)
                {
                    _loggerService.Log($"Error during cache cleanup: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token); // Cleanup interval
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            try { _cleanupTask.Wait(); } catch(Exception ex) { _loggerService.Log($"Error during Cache Service dispose: {ex.Message}"); }
            _cancellationTokenSource.Dispose();
        }
    }
}
