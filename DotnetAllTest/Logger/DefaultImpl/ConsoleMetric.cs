
namespace Logger.DefaultImpl
{
    using Logger.Interfaces;

    public class ConsoleMetric : IMeasureMetric
    {
        public bool LogValue(long rawData, params string[] dimensionValues)
        {
            return true;
        }
    }
}
