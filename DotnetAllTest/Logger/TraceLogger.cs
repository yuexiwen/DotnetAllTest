namespace Logger
{
    using Logger.Interfaces;
    using System.Text;

    public class TraceLogger
    {
        private static int maxCharCount = 6000;
        private static Lazy<StringBuilderPool> sbPool = new Lazy<StringBuilderPool>(() => new StringBuilderPool());
        private SlidingBuffer<Tuple<long, bool, string>> listSteps = new SlidingBuffer<Tuple<long, bool, string>>(1000);
        private long currentTime = StopWatchTimer.GetCurrent();
        private string activityId;

        public ILogger Logger { get; }

        public TraceLogger(ILogger logger)
        {
            this.Logger = logger;
            if (this.Logger != null)
            {
                this.activityId = this.Logger.GetActivityId();
            }
            else
            {
                this.activityId = Guid.NewGuid().ToString();
            }
        }

        public string GetActivityId()
        {
            return this.activityId;
        }

        public void AddTraceStep(string stepDetails, bool isError = false)
        {
            lock(this.listSteps)
            {
                this.listSteps.Add(new Tuple<long, bool, string>(StopWatchTimer.ElapsedMilliseconds(this.currentTime), isError, stepDetails));
            }
        }

        public void LogTraceInfo(string componentName, string eventUniqueName, params KeyValuePair<string, string>[] columns)
        {
            int index = 0;
            KeyValuePair<string, string>[] cols = new KeyValuePair<string, string>[2];
            if (columns != null && columns.Length > 0)
            {
                cols = new KeyValuePair<string, string>[columns.Length + 2];

                for (index = 0; index < columns.Length; index++)
                {
                    cols[index] = columns[index];
                }

                var logs = this.GetTraceInfo();
                int count = 1;

                foreach (var log in logs)
                {
                    cols[index] = new KeyValuePair<string, string>("Index", count.ToString());
                    cols[index + 1] = new KeyValuePair<string, string>("Trace", log);

                    this.Logger?.LogInformation(
                        componentName,
                        eventUniqueName,
                        cols);
                    count++;
                }
            }
        }

        private List<string> GetTraceInfo()
        {
            List<string> list = new List<string>();
            StringBuilder? sb = null;

            try
            {
                sb = sbPool.Value.GetObject();

                foreach (var step in this.listSteps.ToArray())
                {
                    string errorInfo = step.Item2 ? "Error" : "Info";
                    sb.AppendLine($@"{step.Item1.ToString().PadLeft(7, ' ')}: {errorInfo.PadRight(5,' ')}: {step.Item3}");

                    if (sb.Length >= maxCharCount)
                    {
                        list.Add (sb.ToString());
                        sb.Clear();
                    }
                }

                list.Add(sb.ToString());
            }
            finally
            {
                this.listSteps.Reset();
                this.currentTime = StopWatchTimer.GetCurrent();
                if (sb != null)
                {
                    sbPool.Value.PutObject(sb);
                }
            }

            return list;
        }
    }
}
