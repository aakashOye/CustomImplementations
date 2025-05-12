namespace CustomImplementations.CustomCache
{
    public class CacheResult<T>
    {
        public bool Found { get; private set; }
        public T? Value { get; private set; }

        private CacheResult() { }

        public static CacheResult<T> Hit(T value) => new CacheResult<T> { Found = true, Value = value };

        public static CacheResult<T> Miss() => new CacheResult<T> { Found = false };
    }
}
