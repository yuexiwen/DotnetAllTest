namespace CosmosWideTablePoc
{
    internal abstract class UUIDConsumer(UUIDGenerator generator) : SleepThread
    {
        private List<string> uuidLst = [];

        private readonly UUIDGenerator generator = generator;

        public UUIDGenerator Generator { get { return generator; } }

        public List<string> UUIDLst
        {
            get { return uuidLst; }
            set { uuidLst = value; }
        }

        public override void Action()
        {
            UUIDLst = Generator.FetchIds();
            ComsumeAction();
            Console.WriteLine("current action over!");
        }

        public abstract void ComsumeAction();
    }
}
