using CustomImplementations.Models;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace CustomImplementations.Services
{
    public class RateLimiterService : IRateLimiterService
    {
        private readonly RateLimitOptions _options;
        private readonly ConcurrentDictionary<string, ConcurrentQueue<DateTime>> _requestCounts = new();
        private readonly ConcurrentDictionary<string, DateTime> _spammers = new();
        private readonly LoggerService _logger; // Singleton instance of LoggerService

        public RateLimiterService(IOptions<RateLimitOptions> options, LoggerService loggerService)
        {
            _options = options.Value;
            _logger = loggerService; // Initialize the LoggerService
        }

        public bool IsRequestAllowed(string ip, out string message)
        {
            var now = DateTime.UtcNow;

            // Check if blocked
            if (_spammers.TryGetValue(ip, out var blockedTime))
            {
                if ((now - blockedTime) < _options.SpammerBlockTime)
                {
                    message = "You are temporarily blocked due to excessive requests.";
                    return false;
                }
                else
                {
                    _spammers.TryRemove(ip, out _);
                }
            }

            var queue = _requestCounts.GetOrAdd(ip, _ => new ConcurrentQueue<DateTime>());

            lock (queue)
            {
                // Remove outdated entries
                while (queue.TryPeek(out var timestamp) && (now - timestamp) > _options.TimeWindow)
                {
                    queue.TryDequeue(out _);
                }

                if (queue.Count >= _options.Limit)
                {
                    message = "Too many requests. Please try again later.";
                    _logger.Log($"Rate limit exceeded for {ip}");

                    // Detect spammer
                    if (queue.Count >= _options.SpammersLimit)
                    {
                        if (!_spammers.ContainsKey(ip))
                        {
                            _spammers.TryAdd(ip, now);
                            _logger.Log($"Spammer detected: {ip}");
                        }
                    }
                    queue.Enqueue(now);
                    return false;
                }

                queue.Enqueue(now);
            }

            message = string.Empty;
            return true;
        }
    }
}
