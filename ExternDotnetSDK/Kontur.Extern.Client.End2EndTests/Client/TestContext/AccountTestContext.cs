using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Numbers;

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
        
        public ValueTask<EntityScope<Account>> CreateAccount(Inn inn, string organizationName) =>
            scopeFactory(
                () => konturExtern.Accounts.CreateIndividualEntrepreneurAccountAsync(inn, organizationName),
                account => konturExtern.Accounts.WithId(account.Id).DeleteAsync()
            );
        
        public ValueTask<EntityScope<Account>> CreateAccount(LegalEntityInn inn, Kpp kpp, string organizationName) =>
            scopeFactory(
                () => konturExtern.Accounts.CreateLegalEntityAccountAsync(inn, kpp, organizationName),
                account => konturExtern.Accounts.WithId(account.Id).DeleteAsync()
            );

        public Task<Account> GetAccount(Guid id) => konturExtern.Accounts.WithId(id).GetAsync(); 
        
        public Task<Account?> GetAccountOrNull(Guid id) => konturExtern.Accounts.WithId(id).TryGetAsync(); 
        
        public Task<IReadOnlyList<Account>> LoadAllAccountsAsync() => 
            konturExtern.Accounts.List().SliceBy(100).LoadAllAsync();

        public Task<IReadOnlyList<Certificate>> GetAccountCertificatesAsync(Guid accountId) => 
            konturExtern.Accounts.WithId(accountId).Certificates().SliceBy(100).LoadAllAsync();
    }
}