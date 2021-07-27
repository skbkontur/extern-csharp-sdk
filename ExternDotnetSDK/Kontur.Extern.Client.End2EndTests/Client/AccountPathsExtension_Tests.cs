using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Numbers;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class AccountPathsExtensions_Tests
    {
        private readonly ILog log;

        public AccountPathsExtensions_Tests(ITestOutputHelper output) => log = new TestLog(output);

        [Fact]
        public void Get_should_fail_if_the_account_is_not_exist()
        {
            var context = new KonturExternTestContext(log);
            
            Func<Task> func = async () => await context.Accounts.GetAccount(Guid.Parse("6A4F8D06-1CBC-4E63-BC1F-DB5AD91A720D"));

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_if_the_account_is_not_exist()
        {
            var context = new KonturExternTestContext(log);

            var account = await context.Accounts.GetAccountOrNull(Guid.Parse("5E6E477F-DCE2-4B4F-9DC4-6B1992888E17"));

            account.Should().BeNull();
        }

        [Fact]
        public async Task Should_create_a_new_individual_entrepreneur_account_and_read_it()
        {
            var context = new KonturExternTestContext(log);
            await using var accountScope = await context.Accounts.CreateAccount(Inn.Parse("678050110389"), "org");
            
            var account = accountScope.Entity;
            var loadedAccount = await context.Accounts.GetAccount(account.Id);
            
            loadedAccount.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Should_create_a_new_legal_entity_account_and_read_it()
        {
            var context = new KonturExternTestContext(log);
            await using var accountScope = await context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            
            var account = accountScope.Entity;
            var loadedAccount = await context.Accounts.GetAccount(account.Id);
            
            loadedAccount.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Should_load_accounts_list()
        {
            var context = new KonturExternTestContext(log);
            await using var accountScope1 = await context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            await using var accountScope2 = await context.Accounts.CreateAccount(Inn.Parse("678050110389"), "org");
            await using var accountScope3 = await context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            
            var accountsAfterCreate = await context.Accounts.LoadAllAccountsAsync();
            
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope1.Entity);
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope2.Entity);
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope3.Entity);
        }

        [Fact]
        public async Task Should_not_list_deleted_account()
        {
            var context = new KonturExternTestContext(log);
            var accountScope = await context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            var account = accountScope.Entity;
            await using (accountScope)
            {
                var accountsAfterCreate = await context.Accounts.LoadAllAccountsAsync();
                accountsAfterCreate.Should().ContainEquivalentOf(account);
            }

            var accountsAfterDelete = await context.Accounts.LoadAllAccountsAsync();
            accountsAfterDelete.Select(x => x.Id).Should().NotContain(account.Id);
        }

        [Fact]
        public async Task Should_unable_to_read_deleted_account()
        {
            var context = new KonturExternTestContext(log);
            var accountScope = await context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            var account = accountScope.Entity;
            await using (accountScope)
            {
                var accountAfterCreate = await context.Accounts.GetAccount(account.Id);
                accountAfterCreate.Should().BeEquivalentTo(account);
            }

            var loadedAccount = await context.Accounts.GetAccountOrNull(account.Id);

            loadedAccount.Should().BeNull();
        }
    }
}