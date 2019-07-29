using System;
using ExternDotnetSDK.Docflows;
using ExternDotnetSDK.JsonConverters;
using FluentAssertions;
using NUnit.Framework;

namespace ExternDotnetSDKTests.UnitTests
{
    [TestFixture]
    internal class DocflowFilterShould
    {
        [Test]
        public void StringifyCorrectly()
        {
            var filter = new DocflowFilter
            {
                CreatedFrom = null,
                CreatedTo = DateTime.Now,
                Cu = "cucu",
                Finished = false,
                FormName = "goodFormName",
                Incoming = null,
                InnKpp = "1234567890-123456789",
                Knd = "myknd",
                Okpo = "myokpo",
                OrderBy = SortOrder.Unspecified,
                RegNumber = "gregre",
                Type = "ntyntnt",
                UpdatedFrom = DateTime.Now,
                PeriodFrom = DateTime.Today
            };
            var result = filter.StringifyParams();
            result.Should().NotBeNullOrWhiteSpace();
            for (var i = 0; i < result.Length; i++)
            {
                if (result[i] == '&')
                    Assert.IsTrue(char.IsLower(result[i + 1]));
            }
        }

        [Test]
        public void StringifyEmptyFilter()
        {
            new DocflowFilter().StringifyParams().Should().NotBeNullOrWhiteSpace();
        }
    }
}