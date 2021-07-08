using System;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Kontur.Extern.Client.Model.DocflowFiltering;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.DocflowFiltering
{
    [TestFixture]
    internal class DocflowSortingFilter_Tests
    {
        [TestCase(SortOrder.Ascending)]
        [TestCase(SortOrder.Descending)]
        [TestCase(SortOrder.Unspecified)]
        public void OrderByCreationDate_should_apply_creation_date_sorting(SortOrder sortOrder)
        {
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.OrderByCreationDate(sortOrder).ApplyTo(docflowFilter);

            docflowFilter.OrderBy.Should().Be(sortOrder);
        }

        [Test]
        public void UpdatedTo_should_apply_update_to_storing_filter()
        {
            var updateTo = new DateTime(2021, 07, 08, 16, 38, 19);
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedTo(updateTo).ApplyTo(docflowFilter);

            docflowFilter.UpdatedTo.Should().Be(updateTo);
        }

        [Test]
        public void UpdatedFrom_should_apply_update_from_storing_filter()
        {
            var updateFrom = new DateTime(2021, 07, 08, 16, 38, 19);
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedFrom(updateFrom).ApplyTo(docflowFilter);

            docflowFilter.UpdatedFrom.Should().Be(updateFrom);
        }

        [Test]
        public void NoSorting_should_reset_sorting_filter()
        {
            var docflowFilter = new DocflowFilter
            {
                UpdatedTo = new DateTime(2021, 07, 08, 16, 38, 19),
                UpdatedFrom = new DateTime(2021, 07, 08, 16, 38, 19),
                OrderBy = SortOrder.Ascending
            };

            DocflowSortingFilter.NoSorting.ApplyTo(docflowFilter);

            docflowFilter.UpdatedFrom.Should().BeNull();
            docflowFilter.UpdatedTo.Should().BeNull();
            docflowFilter.OrderBy.Should().BeNull();
        }
    }
}