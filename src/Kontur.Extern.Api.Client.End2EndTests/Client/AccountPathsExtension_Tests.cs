using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Numbers;
using Vostok.Commons.Time;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client
{
    public class AccountExtensionsTests : GeneratedAccountTests
    {
        public AccountExtensionsTests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }

        [Fact]
        public async Task Get_should_fail_if_the_account_is_not_exist()
        {
            Func<Task> func = async () => await Context.Accounts.GetAccount(Guid.Parse("6A4F8D06-1CBC-4E63-BC1F-DB5AD91A720D"));

            await func.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_if_the_account_is_not_exist()
        {
            var account = await Context.Accounts.GetAccountOrNull(Guid.Parse("5E6E477F-DCE2-4B4F-9DC4-6B1992888E17"));

            account.Should().BeNull();
        }

        [Fact]
        public async Task Should_create_a_new_individual_entrepreneur_account_and_read_it()
        {
            await using var accountScope = await Context.Accounts.CreateAccount(Inn.Parse("678050110389"), "org");
            
            var account = accountScope.Entity;
            await Task.Delay(1.Seconds());
            var loadedAccount = await Context.Accounts.GetAccount(account.Id);
            
            loadedAccount.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Should_create_a_new_legal_entity_account_and_read_it()
        {
            await using var accountScope = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            
            var account = accountScope.Entity;
            await Task.Delay(1.Seconds());
            var loadedAccount = await Context.Accounts.GetAccount(account.Id);
            
            loadedAccount.Should().BeEquivalentTo(account);
        }

        [Fact]
        public async Task Should_load_accounts_list()
        {
            await using var accountScope1 = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            await using var accountScope2 = await Context.Accounts.CreateAccount(Inn.Parse("678050110389"), "org");
            await using var accountScope3 = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            
            var accountsAfterCreate = await Context.Accounts.LoadAllAccountsAsync();
            
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope1.Entity);
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope2.Entity);
            accountsAfterCreate.Should().ContainEquivalentOf(accountScope3.Entity);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
        public async Task Should_sign_off_user_after_deletion_an_account()
        {
            var context = Context.OverrideExternOptions(x => x.TryResolveUnauthorizedResponsesAutomatically(false));
            var konturExtern = context.Extern;

            var account = await konturExtern.Accounts.CreateLegalEntityAccountAsync(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            await konturExtern.Accounts.WithId(account.Id).DeleteAsync();

            await ShouldFailWhenLoadAccounts();

            await konturExtern.ReauthenticateAsync();

            await ShouldNotFailWhenLoadAccounts();

            async Task ShouldNotFailWhenLoadAccounts()
            {
                Func<Task> func = async () => await context.Accounts.LoadAllAccountsAsync();

                await func.Should().NotThrowAsync();
            }

            async Task ShouldFailWhenLoadAccounts()
            {
                Func<Task> func = async () => await context.Accounts.LoadAllAccountsAsync();

                (await func.Should().ThrowAsync<ApiException>()).Which.Message.Should().Contain("Unauthorized");
            }
        }

        [Fact]
        public async Task Should_not_list_deleted_account()
        {
            var accountScope = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            var account = accountScope.Entity;
            await using (accountScope)
            {
                await Task.Delay(1.Seconds());
                var accountsAfterCreate = await Context.Accounts.LoadAllAccountsAsync();
                accountsAfterCreate.Should().ContainEquivalentOf(account);
            }

            var accountsAfterDelete = await Context.Accounts.LoadAllAccountsAsync();
            accountsAfterDelete.Select(x => x.Id).Should().NotContain(account.Id);
        }

        [Fact]
        public async Task Should_unable_to_read_deleted_account()
        {
            var accountScope = await Context.Accounts.CreateAccount(LegalEntityInn.Parse("1754462785"), Kpp.Parse("515744582"), "org");
            var account = accountScope.Entity;
            await using (accountScope)
            {
                await Task.Delay(1.Seconds());
                var accountAfterCreate = await Context.Accounts.GetAccount(account.Id);
                accountAfterCreate.Should().BeEquivalentTo(account);
            }

            var loadedAccount = await Context.Accounts.GetAccountOrNull(account.Id);

            loadedAccount.Should().BeNull();
        }
    }
}