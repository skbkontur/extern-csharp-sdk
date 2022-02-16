using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.Common.Streams;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestContext;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.Configuration;
using Kontur.Extern.Api.Client.Testing.Assertions;
using Kontur.Extern.Api.Client.Testing.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client
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
        
        [Fact]
        public void Should_error_when_try_to_upload_null_content()
        {
            var apiException = Assert.ThrowsAsync<ArgumentNullException>(
                () => Context.Contents.UploadAsync(AccountId, null!));
           
            apiException.Result.Message.Should().Contain("Value cannot be null");
        } 
        
        [Fact]
        public void Should_error_when_try_to_upload_empty_memory_stream_content()
        {
            var apiException = Assert.ThrowsAsync<ArgumentException>(
                () => Context.Contents.UploadAsync(AccountId, new MemoryStream()));
           
            apiException.Result.Message.Should().Contain("wrong bounds");
        } 
        
        [Fact]
        public void Should_error_when_try_to_download_not_existing_content()
        {
            var apiException = Assert.ThrowsAsync<ApiException>(
                () => Context.Contents.GetContentStream(AccountId, Guid.NewGuid()));
           
            apiException.Result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }  
        
        [Fact]
        public void Should_error_when_try_to_download_by_empty_content_id()
        {
            var apiException = Assert.ThrowsAsync<ApiException>(
                () => Context.Contents.GetContentStream(AccountId, Guid.Empty));
           
            apiException.Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}