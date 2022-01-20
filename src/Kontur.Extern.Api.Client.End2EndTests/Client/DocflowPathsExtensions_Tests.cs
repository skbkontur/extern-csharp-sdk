using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client
{
    public class DocflowPathsExtensions_Tests : GeneratedAccountTests
    {
        public DocflowPathsExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }

        [Fact]
        public void Get_should_fail_when_try_to_read_non_existent_docflow()
        {
            var apiException = Assert.ThrowsAsync<ApiException>(
                () => Context.Docflows.GetDocflow(AccountId, Guid.NewGuid()));

            apiException.Result.Message.Should().Contain("NotFound");
        }

        [Fact]
        public void Get_should_return_error_when_try_get_incorrect_accountId()
        {
            var apiException = Assert.ThrowsAsync<ApiException>(
                () => Context.Docflows.GetDocflow(Guid.Empty, Guid.Empty));

            apiException.Result.Message.Should().Contain("BadRequest");
        }

        [Fact]
        public void Get_should_return_error_when_try_get_other_accountId()
        {
            var apiException = Assert.ThrowsAsync<ApiException>(
                () => Context.Docflows.GetDocflow(Guid.NewGuid(), Guid.Empty));

            apiException.Result.Message.Should().Contain("Forbidden");
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_docflow()
        {
            var docflow = await Context.Docflows.GetDocflowOrNull(AccountId, Guid.NewGuid());

            docflow.Should().BeNull();
        }

        [Fact]
        public async Task TryGet_should_return_docflows_when_filtered_by_fss_type()
        {
            var type = DocflowType.Fss.Sedo.ProviderSubscription;
            var filter = new DocflowFilterBuilder().WithTypes(type);
            var docflow = await Context.Docflows.ListByFilter(AccountId, filter).ConfigureAwait(false);

            docflow.Should().NotBeNull();
        }

        [Fact]
        public async Task TryGet_should__not_return_docflows_when_filtered_all()
        {
            var docflow = await Context.Docflows.ListAll(AccountId);
            docflow.Should().BeEmpty();
        }

        [Fact]
        public async Task TryGet_should_not_return_docflows_when_filtered_by_not_existing_type()
        {
            var cu = AuthorityCode.Pfr.Parse("000-007");
            var filter = new DocflowFilterBuilder().WithCu(cu);
            var docflow = await Context.Docflows.ListByFilter(AccountId, filter);
            docflow.Should().BeEmpty();
        }
    }
}