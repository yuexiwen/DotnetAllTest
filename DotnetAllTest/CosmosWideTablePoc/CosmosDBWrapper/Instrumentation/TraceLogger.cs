namespace CosmosWideTablePoc.CosmosDBWrapper.Instrumentation
{
    public class TraceLogger
    {
        private static int maxCharCount = 6000;
        private static Lazy<StringBuilderPool> sbPool = new Lazy<StringBuilderPool>(() => new StringBuilderPool());
        private SlidingBuffer<Tuple<long, bool, string>> listSteps = new SlidingBuffer<Tuple<long, bool, string>>(1000);

    }
}
