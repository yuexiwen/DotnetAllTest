
namespace CosmosWideTablePoc.CosmosDBWrapper.Instrumentation
{
    internal class SlidingBuffer<T>
    {
        private readonly Queue<T> queue;

        private readonly int maxCount;

        public SlidingBuffer(int maxCount)
        {
            this.maxCount = maxCount;
            this.queue = new Queue<T>(maxCount);
        }

        public void Add(T item)
        {
            lock (this.queue)
            {
                if (this.queue.Count == this.maxCount) 
                {
                    this.queue.Dequeue();
                }

                this.queue.Enqueue(item);
            }
        }

        public T[] ToArray()
        {
            lock (this.queue)
            {
                return this.queue.ToArray();
            }
        }

        public void Reset()
        {
            lock (this.queue)
            {
                this.queue.Clear();
            }
        }
    }
}
