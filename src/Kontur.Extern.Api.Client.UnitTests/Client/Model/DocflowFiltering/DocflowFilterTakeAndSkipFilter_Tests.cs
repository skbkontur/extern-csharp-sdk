using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    internal class DocflowFilterTakeAndSkipFilter_Tests
    {
        private readonly int Skip = 2;
        private readonly int Take = 10;
        private readonly string nameOfSkip = "skip";
        private readonly string nameOfTake = "take";

        [Test]
        public void Should_correct_set_take_and_skip_filter()
        {
            var filter = new DocflowFilter();
            filter.SetSkip(Skip);
            filter.SetTake(Take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, Skip.ToString()),
                (nameOfTake, Take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_skip_take_is_negative()
        {
            var skip = -Skip;
            var take = -Take;
            var filter = new DocflowFilter();
            filter.SetSkip(skip);
            filter.SetTake(take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, skip.ToString()),
                (nameOfTake, take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_skip_take_the_same()
        {
            var take = Skip;
            var filter = new DocflowFilter();
            filter.SetSkip(Skip);
            filter.SetTake(take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, Skip.ToString()),
                (nameOfTake, take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_use_builder()
        {
            var filter = new DocflowFilterBuilder();
            filter.CreateFilter().SetSkip(Skip);
            filter.CreateFilter().SetTake(Take);

            filter.Should().NotBeNull();
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_take_is_zero()
        {
            var take = 0;
            var filter = new DocflowFilter();
            filter.SetSkip(Skip);
            filter.SetTake(take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, Skip.ToString()),
                (nameOfTake, take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_take_is_empty()
        {
            var filter = new DocflowFilter();
            filter.SetSkip(Skip);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, Skip.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_take_is_null()
        {
            var filter = new DocflowFilter();
            filter.SetSkip(Skip);
            filter.SetTake(null);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, Skip.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_skip_is_zero()
        {
            var skip = 0;
            var filter = new DocflowFilter();
            filter.SetSkip(skip);
            filter.SetTake(Take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfSkip, skip.ToString()),
                (nameOfTake, Take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_skip_is_empty()
        {
            var filter = new DocflowFilter();
            filter.SetTake(Take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfTake, Take.ToString()));
        }

        [Test]
        public void Should_correct_set_take_and_skip_filter_when_skip_is_null()
        {
            var filter = new DocflowFilter();
            filter.SetSkip(null);
            filter.SetTake(Take);
            ShouldHaveExpectedQueryParameters(
                filter,
                (nameOfTake, Take.ToString()));
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}