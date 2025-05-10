namespace CustomImplementations.Services
{
    public interface IRateLimiterService
    {
        bool IsRequestAllowed(string ip,out string message);
    }
}
