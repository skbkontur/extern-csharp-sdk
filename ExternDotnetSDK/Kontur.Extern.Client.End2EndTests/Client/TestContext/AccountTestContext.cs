using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class AccountTestContext
    {
        private readonly IExtern konturExtern;
        private readonly EntityScopeFactory<Account> scopeFactory;

        public AccountTestContext(IExtern konturExtern, EntityScopeFactory<Account> scopeFactory)
        {
            this.konturExtern = konturExtern;
            this.scopeFactory = scopeFactory;
        }
        
        public ValueTask<EntityScope<Account>> CreateAccount(LegalEntityInn inn, Kpp kpp, string organizationName) =>
            scopeFactory(
                () => konturExtern.Accounts.CreateAsync(inn, kpp, organizationName),
                account => konturExtern.Accounts.WithId(account.Id).DeleteAsync()
            );

        public Task<Account> GetAccount(Guid id) => konturExtern.Accounts.WithId(id).GetAsync(); 
        
        public Task<Account?> GetAccountOrNull(Guid id) => konturExtern.Accounts.WithId(id).TryGetAsync(); 
        
        public Task<IReadOnlyList<Account>> LoadAllAccountsAsync() => 
            konturExtern.Accounts.List().SliceBy(100).LoadAllAsync();
    }
}