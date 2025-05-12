namespace CustomImplementations.CustomCache
{
    internal class CacheItem
    {
        public object Value { get; }
        public TimeSpan Expiry { get; }
        private DateTime _lastAccessed;

        public CacheItem(object value, TimeSpan? expiry)
        {
            Value = value;
            Expiry = expiry ?? TimeSpan.FromMinutes(5); // Default to 5 min
            _lastAccessed = DateTime.UtcNow;
        }

        public bool IsExpired => DateTime.UtcNow - _lastAccessed > Expiry;
        public void Refresh()
        {
            _lastAccessed = DateTime.UtcNow;
        }
    }
}
