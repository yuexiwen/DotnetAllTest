using System.Runtime.ExceptionServices;

namespace AsyncTest
{
    public class MyTask
    {
        private bool _completed;
        private Exception? _error;
        private Action<MyTask>? _continuation;
        private ExecutionContext? _ec;

        public void ContinueWith(Action<MyTask> action)
        {
            lock(this)
            {
                Console.WriteLine("ContinueWith");
                if (this._completed)
                {
                    ThreadPool.QueueUserWorkItem(_ => action(this));
                }
                else if (_continuation is not null)
                {
                    throw new InvalidOperationException("Unlike Task, this implementation only support a single continuation.");
                }
                else
                {
                    this._continuation = action;
                    this._ec = ExecutionContext.Capture();
                }
            }
        }

        public void SetResult() => this.Complete(null);

        public void SetException(Exception error) => this.Complete(error);

        public void wait()
        {
            ManualResetEventSlim? mres = null;
            lock (this)
            {
                if (!this._completed)
                {
                    mres = new ManualResetEventSlim();
                    this.ContinueWith(_ => mres.Set());
                }
            }

            mres?.Wait();
            if (_error is not null)
            {
                ExceptionDispatchInfo.Throw(this._error);
            }
        }

        public static MyTask WhenAll(MyTask t1, MyTask t2)
        {
            var t = new MyTask();
            int remaining = 2;
            Exception? e = null;
            Action<MyTask> continueation = completed =>
            {
                e ??= completed._error;
                if (Interlocked.Decrement(ref remaining) == 0)
                {
                    if (e is not null) t.SetException(e);
                    else t.SetResult();
                }
            };

            t1.ContinueWith(continueation);
            t2.ContinueWith(continueation);

            return t;
        }

        public static MyTask Run(Action action)
        {
            var t = new MyTask();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                    t.SetResult();
                }
                catch (Exception e)
                {
                    t.SetException(e);
                }
            });

            return t;
        }

        private void Complete(Exception? error)
        {
            lock (this)
            {
                Console.WriteLine("Complete");
                if (this._completed)
                {
                    throw new InvalidOperationException("Already completed");
                }
                this._error = error;
                this._completed = true;

                if (this._continuation is not null)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        if (this._ec is not null)
                        {
                            ExecutionContext.Run(_ec, _ => _continuation(this), null);
                        }
                        else
                        {
                            this._continuation(this);
                        }
                    });
                }
            }
        }
    }
}
