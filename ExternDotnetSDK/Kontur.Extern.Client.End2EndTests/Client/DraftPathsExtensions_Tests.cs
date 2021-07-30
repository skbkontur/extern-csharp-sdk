using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Testing.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class DraftPathsExtensions_Tests : FromAccountEntityPathsTests
    {
        private readonly Randomizer randomizer = new();
        private readonly AuthoritiesCodesGenerator codesGenerator = new();
        
        public DraftPathsExtensions_Tests([NotNull] ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void Get_should_fail_when_try_to_read_non_existent_draft()
        {
            Func<Task> func = async () => await Context.Drafts.GetDraft(Account.Id, Guid.NewGuid());

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_draft()
        {
            var draft = await Context.Drafts.GetDraftOrNull(Account.Id, Guid.NewGuid());

            draft.Should().BeNull();
        }

        [Fact(Skip = "valid recipient codes needed")]
        public async Task Should_create_a_new_draft()
        {
            var newDraft = RandomDraft();

            await using var entityScope = await Context.Drafts.CreateNew(Account.Id, newDraft);

            var createdDraft = entityScope.Entity;
            var loadedDraft = await Context.Drafts.GetDraft(Account.Id, createdDraft.Id);
            
            loadedDraft.Should().BeEquivalentTo(createdDraft);
        }

        private NewDraft RandomDraft()
        {
            var payerInn = codesGenerator.PersonInn();
            var senderInn = codesGenerator.PersonInn();
            var senderCert = randomizer.Bytes(10);
            return new NewDraft(
                NewDraftPayer.IndividualEntrepreneur(payerInn),
                NewDraftSender.IndividualEntrepreneur(senderInn, senderCert),
                DraftRecipient.Ifns("ifns-code")
            );
        }
    }
}