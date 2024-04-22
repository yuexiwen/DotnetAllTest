namespace CosmosWideTablePoc.CosmosDBWrapper.Instrumentation
{
    using System.Collections.Concurrent;
    using System.Text;

    internal class StringBuilderPool
    {
        private ConcurrentBag<StringBuilder> objects;

        public StringBuilderPool()
        {
            this.objects = new ConcurrentBag<StringBuilder>();
        }

        public StringBuilder GetObject()
        {
            StringBuilder? item;
            
            if (this.objects.TryTake(out item))
            {
                return item;
            }

            return new StringBuilder(0x1000);
        }

        public void PutObject(StringBuilder item)
        {
            item.Clear();
            this.objects.Add(item);
        }
    }
}
