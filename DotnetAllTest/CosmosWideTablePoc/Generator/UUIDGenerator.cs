namespace CosmosWideTablePoc
{
    internal class UUIDGenerator : SleepThread
    {
        private readonly List<string> uuidlst = [];

        private int size;

        private readonly ReaderWriterLock rwl = new();

        public List<string> UUIDLst
        {
            get { return uuidlst; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private ReaderWriterLock Rwl
        {
            get { return rwl; }
        }

        public UUIDGenerator(int size = 10)
        {
            Console.WriteLine("here");
            Size = size;
        }

        public void GenerateIds()
        {
            Rwl.AcquireWriterLock(Timeout.Infinite);
            UUIDLst.Clear();
            for (int i = 0; i < Size; ++i)
            {
                UUIDLst.Add(Guid.NewGuid().ToString());
            }
            Rwl.ReleaseWriterLock();
        }

        public List<string> FetchIds()
        {
            List<string> result = [];
            Rwl.AcquireReaderLock(Timeout.Infinite);
            foreach (var item in UUIDLst)
            {
                result.Add(item);
            }
            Rwl.ReleaseReaderLock();
            return result;
        }

        public override void Action()
        {
            GenerateIds();
        }
    }
}
