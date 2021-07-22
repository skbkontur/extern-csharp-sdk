using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal delegate ValueTask<EntityScope<TEntity>> EntityScopeFactory<TEntity>(Func<Task<TEntity>> entityCreate, 
                                                                                  Func<TEntity, Task> entityDelete);
}