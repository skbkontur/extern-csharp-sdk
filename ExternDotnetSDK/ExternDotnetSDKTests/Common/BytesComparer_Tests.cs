using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.Common;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.Common
{
    public class BytesComparer_Tests
    {
        [TestCaseSource(nameof(ZeroArrays))]
        [TestCaseSource(nameof(NonZeroArrays))]
        public void Should_indicate_that_array_contains_only_zeros((string title, byte[] bytes, bool expectedResult) theCase)
        {
            var (_, bytes, expectedResult) = theCase;
            
            BytesComparer.IsZero(bytes).Should().Be(expectedResult);
        }

        public static IEnumerable<(string title, byte[] bytes, bool expectedResult)> ZeroArrays
        {
            get
            {
                return ArraySizes.Select(s => ($"{s} zero bytes", new byte[s], true));
            }
        }
        
        public static IEnumerable<(string title, byte[] bytes, bool expectedResult)> NonZeroArrays
        {
            get
            {
                return ArraySizes.Select(s => ($"{s} non-zero bytes", AllocateNonZero(s), false));
            }
        }

        private static readonly int[] ArraySizes = {1, 7, 8, 15, 16, 31, 32, 1024, 4096};

        private static byte[] AllocateNonZero(int size)
        {
            var bytes = new byte[size];
            Array.Fill(bytes, byte.MaxValue);
            return bytes;
        }
    }
}