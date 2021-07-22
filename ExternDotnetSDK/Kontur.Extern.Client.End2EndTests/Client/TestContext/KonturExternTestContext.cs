using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.End2EndTests.Client.TestAuthProvider;
using Kontur.Extern.Client.End2EndTests.TestClusterClient;
using Kontur.Extern.Client.Model.Numbers;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class KonturExternTestContext
    {
        private readonly ILog log;
        private readonly IExtern konturExtern;

        public KonturExternTestContext(ILog log)
        {
            this.log = log;
            var clusterClient = ClusterClientFactory.CreateTestClient("https://extern-api.testkontur.ru/", log);
            konturExtern = ExternFactory
                .WithClusterClient(clusterClient, log)
                .WithTestOpenIdAuthProvider()
                .Create();
        }

        public IExtern Extern => konturExtern;

        public ValueTask<EntityScope<Account>> CreateAccount(LegalEntityInn inn, Kpp kpp, string organizationName) =>
            Scope<Account>(
                () => konturExtern.Accounts.CreateAsync(inn, kpp, organizationName),
                account => konturExtern.Accounts.WithId(account.Id).DeleteAsync()
            );

        public Task<Account> GetAccount(Guid id) => konturExtern.Accounts.WithId(id).GetAsync(); 
        
        public Task<Account?> GetAccountOrNull(Guid id) => konturExtern.Accounts.WithId(id).TryGetAsync(); 
        
        public Task<IReadOnlyList<Account>> LoadAllAccountsAsync() => 
            konturExtern.Accounts.List().SliceBy(100).LoadAllAsync();

        private ValueTask<EntityScope<TEntity>> Scope<TEntity>(
            Func<Task<TEntity>> entityCreate,
            Func<TEntity, Task> entityDelete)
        {
            return EntityScope<TEntity>.Create(entityCreate, entityDelete, log);
        }
    }
}