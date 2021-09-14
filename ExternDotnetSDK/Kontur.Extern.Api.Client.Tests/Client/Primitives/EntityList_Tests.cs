using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.Primitives;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.Client.Primitives
{
    [TestFixture]
    internal class EntityList_Tests
    {
        [TestFixture]
        internal class Ctor
        {
            [Test]
            public void Should_fail_when_given_null_slice_loader()
            {
                Action action = () => _ = new EntityList<int>(null!);

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class Pagination
        {
            [Test]
            public void Paging_should_fail_when_given_zero_page_size()
            {
                var theCase = new EntityListCase();

                Action action = () => theCase.EntityList.Paging(0);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public async Task LoadPageAsync_should_return_empty_first_page_for_empty_data()
            {
                var theCase = new EntityListCase();

                var page = await theCase.EntityList.Paging(3).LoadPageAsync(0);

                page.Should().BeEmpty();
            }

            [Test]
            public async Task LoadPageAsync_should_load_first_page()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedPageData = theCase.GetExpectedPageData(0, 3);

                var page = await theCase.EntityList.Paging(3).LoadPageAsync(0);

                page.Should().BeEquivalentTo(expectedPageData);
                page.TotalPages.Should().Be(34);
            }

            [Test]
            public async Task LoadPageAsync_should_load_intermediate_page()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedPageData = theCase.GetExpectedPageData(10, 3);

                var page = await theCase.EntityList.Paging(3).LoadPageAsync(10);

                page.Should().BeEquivalentTo(expectedPageData);
                page.TotalPages.Should().Be(34);
            }

            [Test]
            public async Task LoadPageAsync_should_load_last_page()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                const int lastPageIndex = 33;
                var expectedPageData = theCase.GetExpectedPageData(lastPageIndex, 3);

                var page = await theCase.EntityList.Paging(3).LoadPageAsync(lastPageIndex);

                page.Should().BeEquivalentTo(expectedPageData);
                page.TotalPages.Should().Be(34);
            }

            [Test]
            public async Task LoadPageAsync_should_return_non_existent_page_if_page_index_is_too_big()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                const int lastPageIndex = 33;

                var page = await theCase.EntityList.Paging(3).LoadPageAsync(lastPageIndex + 1);

                page.Should().BeEmpty();
                page.CurrentPage.Should().Be(34);
                page.TotalPages.Should().Be(34);
            }

            [Test]
            public async Task LoadPageAsync_should_fail_when_given_negative_page_index()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                Func<Task> action = async () => await theCase.EntityList.Paging(3).LoadPageAsync(-1);

                await action.Should().ThrowAsync<ArgumentException>();
            }

            [Test]
            public async Task LoadAllAsync_should_load_all_items_by_pages()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);
                const int pagesCount = 34;

                var expectedItems = theCase.AllData;

                var allItems = await theCase.EntityList.Paging(3).LoadAllAsync();

                allItems.Should().BeEquivalentTo(expectedItems);
                theCase.DataLoadCount.Should().Be(pagesCount);
            }

            [Test]
            public async Task LoadAllAsync_should_return_empty_list_when_there_is_no_data()
            {
                var theCase = new EntityListCase();

                var allItems = await theCase.EntityList.Paging(3).LoadAllAsync();

                allItems.Should().BeEmpty();
                theCase.DataLoadCount.Should().Be(1);
            }
        }

        [TestFixture]
        internal class Slicing
        {
            [Test]
            public void SliceBy_should_fail_when_given_empty_slice_size()
            {
                var theCase = new EntityListCase();

                Action action = () => theCase.EntityList.SliceBy(0);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public async Task LoadSliceAsync_should_return_empty_slice_when_there_is_no_data()
            {
                var theCase = new EntityListCase();

                var (items, hasNextSlice) = await theCase.EntityList.SliceBy(10).LoadSliceAsync();

                items.Should().BeEmpty();
                hasNextSlice.Should().BeFalse();
            }

            [Test]
            public async Task LoadSliceAsync_should_return_first_slice()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedSliceData = theCase.GetExpectedSliceData(0, 10);

                var (items, hasNextSlice) = await theCase.EntityList.SliceBy(10).LoadSliceAsync();

                items.Should().BeEquivalentTo(expectedSliceData);
                hasNextSlice.Should().BeTrue();
            }

            [Test]
            public async Task LoadSliceAsync_should_return_first_slice_by_specifying_zero_skipped_items()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedSliceData = theCase.GetExpectedSliceData(0, 10);

                var (items, hasNextSlice) = await theCase.EntityList.SliceBy(10).Skip(0).LoadSliceAsync();

                items.Should().BeEquivalentTo(expectedSliceData);
                hasNextSlice.Should().BeTrue();
            }

            [Test]
            public async Task LoadSliceAsync_should_return_intermediate_slice()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedSliceData = theCase.GetExpectedSliceData(21, 11);

                var (items, hasNextSlice) = await theCase.EntityList.SliceBy(11).Skip(21).LoadSliceAsync();

                items.Should().BeEquivalentTo(expectedSliceData);
                hasNextSlice.Should().BeTrue();
            }

            [Test]
            public async Task LoadSliceAsync_should_return_last_slice()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                var expectedSliceData = theCase.GetExpectedSliceData(91, 9);

                var (items, hasNextSlice) = await theCase.EntityList.SliceBy(10).Skip(91).LoadSliceAsync();

                items.Should().BeEquivalentTo(expectedSliceData);
                hasNextSlice.Should().BeFalse();
            }

            [Test]
            public void Skip_should_fail_when_given_negative_skip_elements()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);

                Action action = () => theCase.EntityList.SliceBy(10).Skip(-1);

                action.Should().Throw<ArgumentException>();
            }
            
            [Test]
            public async Task LoadAllAsync_should_load_all_items_by_slices()
            {
                var theCase = new EntityListCase()
                    .MakeDataAvailable(100);
                const int sliceCount = 34;

                var expectedItems = theCase.AllData;

                var allItems = await theCase.EntityList.SliceBy(3).LoadAllAsync();

                allItems.Should().BeEquivalentTo(expectedItems);
                theCase.DataLoadCount.Should().Be(sliceCount);
            }

            [Test]
            public async Task LoadAllAsync_should_return_empty_list_when_there_is_no_data()
            {
                var theCase = new EntityListCase();

                var allItems = await theCase.EntityList.SliceBy(3).LoadAllAsync();

                allItems.Should().BeEmpty();
                theCase.DataLoadCount.Should().Be(1);
            }
        }

        private class EntityListCase
        {
            private int[] data;
            private int loadCount;

            public EntityListCase()
            {
                data = new int[0];
                EntityList = new EntityList<int>((skip, take, _) =>
                {
                    loadCount++;
                    var slice = data.Skip((int) skip).Take((int) take).ToArray();
                    return Task.FromResult<(IReadOnlyList<int> Items, long totalItems)>((slice, data.Length));
                });
            }

            public EntityList<int> EntityList { get; }
            public int[] AllData => data;
            public int DataLoadCount => loadCount;

            public EntityListCase MakeDataAvailable(int count)
            {
                data = Enumerable.Range(1, count).ToArray();
                return this;
            }

            public int[] GetExpectedPageData(int pageIndex, int pageSize) => 
                data.Skip(pageIndex*pageSize).Take(pageSize).ToArray();

            public int[] GetExpectedSliceData(int skip, int take) => 
                data.Skip(skip).Take(take).ToArray();
        }
    }
}