namespace JobLogger
{
    public interface ILogListener
    {
        void Log(string message, LogLevel logLevel);
    }
}
