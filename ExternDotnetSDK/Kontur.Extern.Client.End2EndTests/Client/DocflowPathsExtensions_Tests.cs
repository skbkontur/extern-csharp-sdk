using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class DocflowPathsExtensions_Tests : FromAccountEntityPathsTests
    {
        public DocflowPathsExtensions_Tests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void Get_should_fail_when_try_to_read_non_existent_docflow()
        {
            Func<Task> func = async () => await Context.Docflows.GetDocflow(Account.Id, Guid.NewGuid());

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_when_try_to_read_non_existent_docflow()
        {
            var docflow = await Context.Docflows.GetDocflowOrNull(Account.Id, Guid.NewGuid());

            docflow.Should().BeNull();
        }
    }
}