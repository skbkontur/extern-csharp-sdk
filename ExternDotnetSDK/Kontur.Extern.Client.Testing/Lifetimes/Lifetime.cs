using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Vostok.Commons.Threading;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Testing.Lifetimes
{
    public class Lifetime : ILifetime, IDisposable, IAsyncDisposable
    {
        private readonly AtomicBoolean disposed = new(false);
        private readonly ILog log;
        private readonly ConcurrentStack<object> resources;

        public Lifetime(ILog log)
        {
            this.log = log;
            resources = new ConcurrentStack<object>();
        }

        public void Add(Action resource) => resources.Push(resource);

        public void Add(Func<ValueTask> resource) => resources.Push(resource);

        public void Add(Func<Task> resource) => resources.Push(resource);

        public T Add<T>(T resource)
            where T : IDisposable
        {
            resources.Push(resource);
            return resource;
        }

        public void Dispose() => DisposeAsync().GetAwaiter().GetResult();

        public async ValueTask DisposeAsync()
        {
            if (!disposed.TrySetTrue())
                return;

            while (resources.TryPop(out var resource))
            {
                try
                {
                    switch (resource)
                    {
                        case Func<ValueTask> asyncValueTaskResource:
                            await asyncValueTaskResource().ConfigureAwait(false);
                            break;
                        case Func<Task> asyncTaskResource:
                            await asyncTaskResource().ConfigureAwait(false);
                            break;
                        case Action action:
                            action();
                            break;
                        case IDisposable disposable:
                            disposable.Dispose();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }
    }
}