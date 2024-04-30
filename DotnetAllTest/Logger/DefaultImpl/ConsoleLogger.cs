using Logger.Interfaces;

namespace Logger.DefaultImpl
{
    public class ConsoleLogger : ILogger
    {
        private string activityId;

        public ConsoleLogger()
        {
            this.activityId = Guid.NewGuid().ToString();
        }

        public ConsoleLogger(string activityId)
        {
            this.activityId = activityId;
        }

        public string GetActivityId()
        {
            return this.activityId;
        }

        public void LogCritical(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            this.LogToConsole(componentName, eventUniqueName, "Critical", null, columns);
        }

        public void LogError(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            this.LogToConsole(componentName, eventUniqueName, "Error", null, columns);
        }

        public void LogException(string componentName, string eventUniqueName, Exception exception, params KeyValuePair<string, string>[] columns)
        {
            this.LogToConsole(componentName, eventUniqueName, "Exception", exception, columns);
        }

        public void LogInformation(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            this.LogToConsole(componentName, eventUniqueName, "Information", null, columns);
        }

        public void LogWarning(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            throw new NotImplementedException();
        }

        public void LogTraceInfo(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            this.LogToConsole(componentName, eventUniqueName, "Trace", null, columns);
        }

        private void LogToConsole(string componentName, string eventUniqueName, string severityLevel, Exception? exception, params KeyValuePair<string, string>[] columns)
        {
            string ex = string.Empty;

            if (exception != null)
            {
                ex = $"\r\n\t\texception:{exception}";
            }

            if (columns == null)
            {
                columns = new KeyValuePair<string, string>[0];
            }

            Console.WriteLine($"************************** ComponentName: {componentName}, EventUniqueName: {eventUniqueName}, SeverityLevel: {severityLevel}, activityId: {this.activityId}, Values: {string.Join("\r\n\t\t", columns.Select(x => x.Key + ':' + x.Value))}{ex}");
            Console.WriteLine();
        }
    }
}
