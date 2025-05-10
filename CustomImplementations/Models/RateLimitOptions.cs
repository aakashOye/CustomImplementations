namespace CustomImplementations.Models
{
    public class RateLimitOptions
    {
        public int Limit { get; set; } = 5;
        public int SpammersLimit { get; set; } = 10;
        public TimeSpan TimeWindow { get; set; } = TimeSpan.FromMinutes(1);
        public TimeSpan SpammerBlockTime { get; set; } = TimeSpan.FromMinutes(10);
    }
}
