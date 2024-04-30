namespace Logger.Interfaces
{
    public interface ILoggerFactory
    {
        IMeasureMetric CreateMetric(string metricNamespace, string metricName, List<string> dimensionNames);

        IMeasureMetric CreateMetric(string metricNamespace, string metricName, params string[] dimensionNames);

        ILogger GetLogger();

        ILogger GetLogger(string activityId);
    }
}
