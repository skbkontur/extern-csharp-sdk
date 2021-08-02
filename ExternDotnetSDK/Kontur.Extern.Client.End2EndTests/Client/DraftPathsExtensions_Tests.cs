using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Model.Numbers;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class DraftPathsExtensions_Tests : GeneratedAccountTests
    {
        public DraftPathsExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }
        
        [Fact]
        public void Get_should_fail_when_try_to_read_non_existent_draft()
        {
            Func<Task> func = async () => await Context.Drafts.GetDraft(AccountId, Guid.NewGuid());

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_draft()
        {
            var draft = await Context.Drafts.GetDraftOrNull(AccountId, Guid.NewGuid());

            draft.Should().BeNull();
        }

        [Fact]
        public async Task Should_create_a_new_draft()
        {
            var newDraft = NewDraftOfDefaultAccount();

            await using var entityScope = await Context.Drafts.CreateNew(AccountId, newDraft);

            var createdDraft = entityScope.Entity;
            var loadedDraft = await Context.Drafts.GetDraft(AccountId, createdDraft.Id);
            
            loadedDraft.Should().BeEquivalentTo(createdDraft);
        }

        private NewDraft NewDraftOfDefaultAccount()
        {
            var certInn = GeneratedAccount.Inn;
            var certKpp = GeneratedAccount.Kpp;
            var senderCert = GeneratedAccount.CertificatePublicPart;

            return new NewDraft(
                NewDraftPayer.LegalEntityPayer(certInn, certKpp),
                NewDraftSender.LegalEntity(certInn, certKpp, senderCert).WithIpAddress(IPAddress.Parse("8.8.8.8")),
                DraftRecipient.Ifns(IfnsCode.Parse("0087"), MriCode.Parse("9660"))
            );
        }
    }
}