namespace CosmosWideTablePoc
{
    internal class ComsumerTest(UUIDGenerator generator) : UUIDConsumer(generator)
    {
        public override void ComsumeAction()
        {
            Console.WriteLine("================================");
            for (int i = 0; i < UUIDLst.Count; i++)
            {
                Console.WriteLine(UUIDLst[i]);
            }
        }
    }
}
