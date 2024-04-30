namespace Logger.Interfaces
{
    public interface IMeasureMetric
    {
        bool LogValue(long rawData, params string[] dimensionValues);
    }
}
