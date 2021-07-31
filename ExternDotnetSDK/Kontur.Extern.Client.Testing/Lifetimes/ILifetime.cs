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

        /// <summary>
        /// Add new resource of <typeparamref name="T"/> which is implements <see cref="IDisposable"/> or <see cref="IAsyncDisposable"/> 
        /// </summary>
        /// <param name="resource">The resource</param>
        /// <typeparam name="T">The resource type which implements <see cref="IDisposable"/> or <see cref="IAsyncDisposable"/></typeparam>
        /// <returns>Returns <paramref name="resource"/></returns>
        /// <exception cref="ArgumentException">If <paramref name="resource"/> does not implement <see cref="IDisposable"/> or <see cref="IAsyncDisposable"/></exception>
        T Add<T>(T resource);
    }
}