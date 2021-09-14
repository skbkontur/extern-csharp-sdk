using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.Common.Streams;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.Model.Configuration;
using Kontur.Extern.Client.Testing.Assertions;
using Kontur.Extern.Client.Testing.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class ContentPathExtensions_Tests : GeneratedAccountTests
    {
        private readonly Randomizer randomizer = new();
        
        public ContentPathExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
        }

        [Fact]
        public async Task Should_upload_and_download_content_as_stream()
        {
            var context = Context.OverrideExternOptions(x => x.OverrideContentsOptions(new ContentManagementOptions(downloadChunkSize: 80*1024)));
            var contentBytes = randomizer.Bytes(500*1024);

            var contentId = await context.Contents.UploadAsync(AccountId, new MemoryStream(contentBytes));
            contentId.Should().NotBeEmpty();
            
            var stream = await context.Contents.GetContentStream(AccountId, contentId);
            var bytes = await stream.ToArrayAsync();

            bytes.ShouldHaveExpectedBytes(contentBytes);
        }
    }
}