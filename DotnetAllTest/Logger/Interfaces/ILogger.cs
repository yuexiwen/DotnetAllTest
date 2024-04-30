namespace Logger.Interfaces
{
    public interface ILogger
    {
        string GetActivityId();

        public void LogInformation(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns);

        public void LogError(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns);

        public void LogWarning(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns);

        public void LogCritical(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns);

        public void LogTraceInfo(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)

        public void LogException(string componentName, string eventUniqueName, Exception exception, params KeyValuePair<string, string>[] columns);
    }
}
