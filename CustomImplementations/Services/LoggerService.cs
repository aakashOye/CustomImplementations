namespace CustomImplementations.Services
{
    public sealed class LoggerService
    {
        public Guid Guid { get; set; } = Guid.NewGuid(); // Unique identifier for the LoggerService instance
        public LoggerService()
        {
            // Private constructor to prevent instantiation from outside
        }
        public void Log(string message)
        {
            // Implement your logging logic here
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
