using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Testing.Lifetimes;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext
{
    internal class EntityScope<TEntity> : IAsyncDisposable
    {
        public static async ValueTask<EntityScope<TEntity>> Create(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete,
            ILog log)
        {
            var lifetime = new Lifetime(log);
            try
            {
                var entity = await entityCreate();
                lifetime.Add(() => entityDelete(entity));
                var result = new EntityScope<TEntity>(entity, lifetime);
                lifetime = null;
                return result;
            }
            finally
            {
                if (lifetime != null)
                {
                    await lifetime.DisposeAsync();
                }
            }
        }
        
        private readonly Lifetime lifetime;

        private EntityScope(TEntity entity, Lifetime lifetime)
        {
            this.lifetime = lifetime;
            Entity = entity;
        }
        
        public TEntity Entity { get; }

        public ValueTask DisposeAsync() => lifetime.DisposeAsync();
    }
}