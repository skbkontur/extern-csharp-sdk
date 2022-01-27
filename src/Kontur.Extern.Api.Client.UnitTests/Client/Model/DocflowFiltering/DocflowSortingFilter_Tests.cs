using System;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Common;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    [TestFixture]
    internal class DocflowSortingFilter_Tests
    {
        private readonly DateTime someDate = new(2021, 07, 08, 01, 01, 01);
        private readonly string nameOfUpdateTo = "updatedTo";

        [TestCase(SortOrder.Ascending)]
        [TestCase(SortOrder.Descending)]
        [TestCase(SortOrder.Unspecified)]
        public void OrderByCreationDate_should_apply_creation_date_sorting(SortOrder sortOrder)
        {
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.OrderByCreationDate(sortOrder).ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, ("orderBy", sortOrder.ToString().ToLower()));
        }

        [Test]
        public void Order_should_set_incorrect_sort_order()
        {
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.OrderByCreationDate((SortOrder) 10).ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, ("orderBy", "10"));
        }

        [Test]
        public void UpdatedTo_should_apply_update_to_storing_filter()
        {
            var updateTo = someDate;
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedTo(updateTo).ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, (nameOfUpdateTo, "2021-07-08T01:01:01.0000000"));
        }

        [Test]
        public void UpdatedTo_and_updatedFrom_should_correct_create_filter()
        {
            var updateTo = someDate;
            var updateFrom = someDate.AddDays(-1);

            var docflowFilter = new DocflowFilterBuilder().CreateFilter();
            docflowFilter.SetUpdatedTo(updateTo);
            docflowFilter.SetUpdatedFrom(updateFrom);

            ShouldHaveExpectedQueryParameters(
                docflowFilter,
                (nameOfUpdateTo, "2021-07-08T01:01:01.0000000"),
                ("updatedFrom", "2021-07-07T01:01:01.0000000"));
        }

        [Test]
        public void UpdatedTo_and_updatedFrom_should_erase_by_last_value()
        {
            var updateTo = someDate;
            var updateFrom = someDate.AddDays(1);

            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedTo(updateTo).ApplyTo(docflowFilter);
            DocflowSortingFilter.UpdatedFrom(updateFrom).ApplyTo(docflowFilter);

            docflowFilter.ToQueryParameters().Count().Should().Be(1);

            ShouldHaveExpectedQueryParameters(
                docflowFilter,
                ("updatedFrom", "2021-07-09T01:01:01.0000000"));
        }

        [Test]
        public void UpdatedFrom_should_apply_update_from_storing_filter()
        {
            var updateFrom = new DateTime(2021, 07, 08, 01, 01, 01);
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedFrom(updateFrom).ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, ("updatedFrom", "2021-07-08T01:01:01.0000000"));
        }

        [Test]
        public void Should_success_create_filter_when_updateTo_and_updateFrom_was_null()
        {
            var docflowFilter = new DocflowFilter();
            docflowFilter.SetUpdatedTo(null);
            docflowFilter.SetUpdatedFrom(null);
            docflowFilter.SetOrderBy(null);

            docflowFilter.ToQueryParameters().Should().BeEmpty();
        }

        [Test]
        public void NoSorting_should_reset_sorting_filter()
        {
            var docflowFilter = new DocflowFilter();
            docflowFilter.SetUpdatedTo(someDate);
            docflowFilter.SetUpdatedFrom(someDate.AddDays(-1));
            docflowFilter.SetOrderBy(SortOrder.Ascending);

            DocflowSortingFilter.NoSorting.ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, Array.Empty<(string name, string value)>());
        }

        [Test]
        public void Should_apply_sorting_filter()
        {
            var docflowFilter = new DocflowFilterBuilder()
                .WithSortingFilter(DocflowSortingFilter.OrderByCreationDate(SortOrder.Ascending))
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(docflowFilter, ("orderBy", "ascending"));
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}