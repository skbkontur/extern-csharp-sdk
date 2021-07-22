using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Testing.Lifetimes
{
    [PublicAPI]
    public interface ILifetime
    {
        void Add(Func<ValueTask> resource);
        
        void Add(Func<Task> resource);
        
        void Add(Action resource);

        T Add<T>(T resource)
            where T : IDisposable;
    }
}