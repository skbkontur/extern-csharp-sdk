using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Kontur.Extern.Client.Primitives;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Primitives
{
    [TestFixture]
    internal class Page_Tests
    {
        [Test]
        public void Should_initialize_page_for_no_data()
        {
            var expectedPage = new ExpectedPage<int>
            {
                Items = new int[0],
                PageSize = 10,
                Pages = new long[0],
                CurrentPage = 0,
                TotalPages = 0,
                TotalItems = 0
            };
            
            var page = new Page<int>(new int[0], 10, 0, 0);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_initialize_first_page()
        {
            var expectedPage = new ExpectedPage<int>
            {
                Items = new[] {1, 2, 3},
                PageSize = 3,
                Pages = new[] {0L, 1L, 2L},
                CurrentPage = 0,
                TotalPages = 3,
                TotalItems = 8
            };

            var page = new Page<int>(new[] {1, 2, 3}, 3, 0, 8);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_initialize_intermediate_page()
        {
            var expectedPage = new ExpectedPage<int>
            {
                Items = new[] {1, 2, 3},
                PageSize = 3,
                Pages = new[] {0L, 1L, 2L},
                CurrentPage = 1,
                TotalPages = 3,
                TotalItems = 8
            };

            var page = new Page<int>(new[] {1, 2, 3}, 3, 1, 8);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_initialize_last_page()
        {
            var expectedPage = new ExpectedPage<int>
            {
                IsPageNonExistent = true,
                Items = new[] {1, 2},
                PageSize = 4,
                Pages = new[] {0L, 1L, 2L},
                CurrentPage = 2,
                TotalPages = 3,
                TotalItems = 11
            };

            var page = new Page<int>(new[] {1, 2}, 4, 2, 11);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_initialize_empty_page_if_there_is_no_items_and_page_index_is_greater_than_zero()
        {
            var expectedPage = new ExpectedPage<int>
            {
                IsPageNonExistent = true,
                Items = new int[0],
                PageSize = 4,
                Pages = new long[0],
                CurrentPage = 4,
                TotalPages = 0,
                TotalItems = 0
            };

            var page = new Page<int>(new int[0], 4, 4, 0);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_initialize_empty_page_if_page_index_is_bigger_than_pages_available()
        {
            var expectedPage = new ExpectedPage<int>
            {
                Items = new int[0],
                PageSize = 4,
                Pages = new [] {0L, 1L, 2L},
                CurrentPage = 4,
                TotalPages = 3,
                TotalItems = 12
            };

            var page = new Page<int>(new int[0], 4, 4, 12);

            ShouldBeExpected(page, expectedPage);
        }

        [Test]
        public void Should_fail_if_page_index_is_bigger_than_pages_available_and_items_is_not_empty()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3, 4}, 4, 4, 12);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_zero_page_size()
        {
            Action action = () => _ = new Page<int>(new int[0], 0, 0, 0);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_when_given_negative_page_index()
        {
            Action action = () => _ = new Page<int>(new int[3], 3, -1, 100);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_when_given_negative_total_items()
        {
            Action action = () => _ = new Page<int>(new int[3], 3, 0, -1);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_non_empty_items_for_zero_total_items()
        {
            Action action = () => _ = new Page<int>(new int[3], 10, 0, 0);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_items_bigger_than_first_page_can_fit()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3, 4, 5}, 4, 0, 11);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_items_lesser_than_first_page_can_fit()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3}, 4, 0, 11);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_items_lesser_than_intermediate_page_can_fit()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3}, 4, 1, 11);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_items_bigger_than_single_page_can_fit()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3, 4}, 3, 0, 3);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_if_given_items_bigger_than_left_items_for_the_last_page()
        {
            Action action = () => _ = new Page<int>(new[] {1, 2, 3, 4}, 4, 3, 11);

            action.Should().Throw<ArgumentException>();
        }

        private static void ShouldBeExpected(IPage<int> page, ExpectedPage<int> expectedPage)
        {
            page.Should().BeEquivalentTo(expectedPage.Items);
            page.TotalItems.Should().Be(expectedPage.TotalItems);
            page.CurrentPage.Should().Be(expectedPage.CurrentPage);
            page.PageSize.Should().Be(expectedPage.PageSize);
            page.TotalPages.Should().Be(expectedPage.TotalPages);
            page.Pages.Should().BeEquivalentTo(expectedPage.Pages);
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class ExpectedPage<T> : IPage
        {
            public bool IsPageNonExistent { get; init; }
            public long TotalItems { get; init; }
            public long CurrentPage { get; init; }
            public uint PageSize { get; init; }
            public long TotalPages { get; init; }
            public IEnumerable<long> Pages { get; init; }
            public T[] Items { get; init; }
        }
    }
}