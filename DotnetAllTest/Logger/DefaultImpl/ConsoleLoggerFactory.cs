namespace Logger.DefaultImpl
{
    using Logger.Interfaces;
    using System.Collections.Generic;

    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public IMeasureMetric CreateMetric(string metricNamespace, string metricName, List<string> dimensionNames)
        {
            return new ConsoleMetric();
        }

        public IMeasureMetric CreateMetric(string metricNamespace, string metricName, params string[] dimensionNames)
        {
            return new ConsoleMetric();
        }

        public ILogger GetLogger()
        {
            return new ConsoleLogger();
        }

        public ILogger GetLogger(string activityId)
        {
            return new ConsoleLogger(activityId);
        }
    }
}
