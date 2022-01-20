using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Testing.xUnit;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    internal class DocflowFilterBuilderTimeFilter_Tests
    {
        private readonly string nameOfCreateTo = "createdTo";
        private readonly string nameOfCreateFrom = "createdFrom";
        private readonly DateTime someDate = new(2021, 07, 08, 01, 01, 01);

        [Test]
        public void WithCreatedFilter_should_create_when_has_correct_to_and_from_filters()
        {
            var from = someDate;
            var to = from.AddDays(1);

            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);
            docflowFilterBuilder.WithCreatedTo(to);

            var docflowFilter = docflowFilterBuilder.CreateFilter();
            ShouldHaveExpectedQueryParameters(
                docflowFilter,
                (nameOfCreateFrom, "2021-07-08T01:01:01.0000000"),
                (nameOfCreateTo, "2021-07-09T01:01:01.0000000"));

            docflowFilterBuilder.Should().NotBeNull();
        }

        [Test]
        public void WithFilter_should_create_when_has_correct_to_and_from_filters()
        {
            var from = someDate;
            var to = from.AddDays(1);

            var docflowFilter = new DocflowFilterBuilder().CreateFilter();
            docflowFilter.SetCreatedFrom(from);
            docflowFilter.SetCreatedTo(to);

            ShouldHaveExpectedQueryParameters(
                docflowFilter,
                (nameOfCreateFrom, "2021-07-08T01:01:01.0000000"),
                (nameOfCreateTo, "2021-07-09T01:01:01.0000000"));

            docflowFilter.Should().NotBeNull();
        }

        [Test]
        public void WithCreatedFilter_should_create_when_has_max_interval_in_to_and_from_filter()
        {
            var from = DateTime.MinValue;
            var to = DateTime.MaxValue;

            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);
            docflowFilterBuilder.WithCreatedTo(to);

            docflowFilterBuilder.CreateFilter();
            docflowFilterBuilder.Should().NotBeNull();
        }

        [Ignore("В ошибке перепутаны параметры from и to + d итоговом параметре используется dateTo вместо createdTo ")]
        [Test]
        public void WithCreatedTo_should_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = new DateTime(2021, 07, 08, 01, 01, 01);
            var to = from.AddDays(-1);
            var expectedMessageOfException = Errors.InvalidRange(nameOfCreateFrom, "dateTo", from, to).Message;

            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);
            var apiException = Assert.Throws<ArgumentException>(
                () => docflowFilterBuilder.WithCreatedTo(to));

            apiException.Should().NotBeNull();
            apiException?.Message.Should().Be(expectedMessageOfException);
        }

        [Test]
        public void WithFilter_should_not_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = new DateTime(2021, 07, 08, 01, 01, 01);
            var to = from.AddDays(-1);

            var docflowFilter = new DocflowFilterBuilder().CreateFilter();
            docflowFilter.SetCreatedFrom(from);
            docflowFilter.SetCreatedTo(to);

            docflowFilter.Should().NotBeNull();
        }

        [Test]
        public void WithFilter_should_be_empty_when_created_from_and_created_to_filter_was_null()
        {
            var docflowFilter = new DocflowFilterBuilder().CreateFilter();
            docflowFilter.SetCreatedFrom(null);
            docflowFilter.SetCreatedTo(null);
            docflowFilter.ToQueryParameters().Should().BeEmpty();
        }

        [Test]
        public void WithFilter_should_be_empty_when_build_with_created_to_and__set_created_to_is_null()
        {
            var docflowFilter = new DocflowFilterBuilder().WithCreatedTo(someDate).CreateFilter();

            docflowFilter.SetCreatedTo(null);

            docflowFilter.ToQueryParameters().Should().BeEmpty();
        }

        [Test]
        public void WithCreatedTo_should_not_fail_when_created_from_same_than_created_to_filter()
        {
            var from = someDate;
            var to = from;

            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            Assert.DoesNotThrow(() => docflowFilterBuilder.WithCreatedTo(to));
        }

        [Test]
        public void WithCreatedFrom_should_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = someDate;
            var to = from.AddDays(-1);
            var expectedMessageOfException = Errors.InvalidRange("dateFrom", nameOfCreateTo, from, to).Message;

            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedTo(to);

            var apiException = Assert.Throws<ArgumentException>(
                () => docflowFilterBuilder.WithCreatedFrom(from));

            apiException.Should().NotBeNull();
            apiException?.Message.Should().Be(expectedMessageOfException);
        }

        [Test]
        public void WithCreatedFrom_should_set_created_from_filter_without_created_to_bound()
        {
            var from = someDate;
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            var docflowFilter = docflowFilterBuilder.CreateFilter();

            ShouldHaveExpectedQueryParameters(docflowFilter, (nameOfCreateFrom, "2021-07-08T01:01:01.0000000"));
        }

        [Test]
        public void WithCreatedTo_should_set_created_to_filter_without_created_from_bound()
        {
            var to = someDate;

            var docflowFilter = new DocflowFilterBuilder().WithCreatedTo(to).CreateFilter();

            ShouldHaveExpectedQueryParameters(docflowFilter, (nameOfCreateTo, "2021-07-08T01:01:01.0000000"));
        }

        [Test]
        public void Should_fail_when_reporting_period_bounds_are_invalid()
        {
            var from = new DateOnly(2021, 07, 08);
            var to = from.AddDays(-1);
            var expectedMessageOfException = Errors.InvalidRange("from", "to", from, to).Message;

            var apiException = Assert.Throws<ArgumentException>(
                () => new DocflowFilterBuilder().WithReportingPeriod(from, to));

            apiException.Should().NotBeNull();
            apiException?.Message.Should().Be(expectedMessageOfException);
        }

        [Test]
        public void Should_be_correct_when_reporting_period_bounds_are_normal()
        {
            var from = new DateOnly(2021, 07, 08);
            var to = from.AddDays(1);

            var filter = new DocflowFilterBuilder().WithReportingPeriod(from, to).CreateFilter();

            ShouldHaveExpectedQueryParameters(
                filter,
                ("periodFrom", "2021-07-08T00:00:00.0000000"),
                ("periodTo", "2021-07-09T00:00:00.0000000"));
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}