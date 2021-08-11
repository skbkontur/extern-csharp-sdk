using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Clients.Contents;
using Kontur.Extern.Client.Testing.Helpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Contents
{
    public class ChunkContentStream_Tests
    {
        [Test]
        public void Should_fail_when_given_null_part_download_function()
        {
            Func<Task> func = async () => await ChunkContentStream.CreateAsync(null!, 100);

            func.Should().Throw<ArgumentException>();
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_fail_when_given_invalid_part_size(int partSize)
        {
            Func<Task> func = async () => await ChunkContentStream.CreateAsync(_ => EmptyPartAsync(), partSize);

            func.Should().Throw<ArgumentException>();

            Task<(ArraySegment<byte> contentPart, long totalLength)> EmptyPartAsync() => 
                Task.FromResult<(ArraySegment<byte> contentPart, long totalLength)>((new ArraySegment<byte>(Array.Empty<byte>()), 100));
        }
        
        [TestCase(10, 3)]
        [TestCase(10, 1)]
        [TestCase(10, 5)]
        [TestCase(1000, 6)]
        public async Task Should_download_contents_by_parts_of_given_sizes(int contentSize, int partSize)
        {
            var wholeContent = Enumerable.Range(0, contentSize).Select(x => (byte)(x%byte.MaxValue)).ToArray();
            var expectedPartsLoads = wholeContent.Batch(partSize).ToList();
            var partsLoads = new List<byte[]>();

            await using var stream = await ChunkContentStream.CreateAsync(GetPart, partSize);

            var bytes = await stream.ReadAllBytesAsync();
            bytes.Should().BeEquivalentTo(wholeContent);
            partsLoads.Should().BeEquivalentTo(expectedPartsLoads);

            Task<(ArraySegment<byte> contentPart, long totalLength)> GetPart((long from, long to) range)
            {
                var rangeLength = (int)(range.to - range.from + 1);
                rangeLength.Should().BeLessOrEqualTo(partSize).And.BePositive();

                var part = wholeContent.Skip((int) range.from).Take(rangeLength).ToArray();
                partsLoads.Add(part);
                return Task.FromResult<(ArraySegment<byte> contentPart, long totalLength)>((part, wholeContent.Length));
            }
        }
    }
}