

namespace CosmosWideTablePoc
{
    internal class UUIDGenerator: SleepThread
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
            get
            {
                return this.rwl;
            }
        }

        public UUIDGenerator(int size = 10)
        {
            Console.WriteLine("here");
            this.Size = size;
        }

        public void GenerateIds()
        {
            this.Rwl.AcquireWriterLock(Timeout.Infinite);
            this.UUIDLst.Clear();
            for (int i = 0; i < this.Size; ++i)
            {
                this.UUIDLst.Add(Guid.NewGuid().ToString());
            }
            this.Rwl.ReleaseWriterLock();
        }

        public List<string> FetchIds()
        {
            List<string> result = [];
            this.Rwl.AcquireReaderLock(Timeout.Infinite);
            foreach (var item in UUIDLst)
            {
                result.Add(item);
            }
            this.Rwl.ReleaseReaderLock();
            return result;
        }

        public override void Action()
        {
            this.GenerateIds();
        }
    }
}
