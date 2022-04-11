using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Exceptions;
using Vostok.Commons.Time;

namespace Kontur.Extern.Api.Client.Primitives
{
    internal class EntityList<T> : IEntityList<T>
    {
        private readonly GetSliceAsync<T> getSlice;
        
        public EntityList(GetSliceAsync<T> getSlice)
        {
            this.getSlice = getSlice ?? throw new ArgumentNullException(nameof(getSlice));
        }
        
        public IEntityListSlicing<T> SliceBy(uint sliceSize) => new Slicing(sliceSize, 0, getSlice);

        public IPagination<T> Paging(uint pageSize)
        {
            if (pageSize == 0)
                throw Errors.ValueShouldBeGreaterOrEqualTo(nameof(pageSize), pageSize, 1);
            return new Pagination(pageSize, getSlice);
        }

        public async Task<long> CountAsync(TimeSpan? timeout = null)
        {
            var (_, totalItems) = await getSlice(0, 1, timeout).ConfigureAwait(false);
            return totalItems;
        }

        private class Pagination : IPagination<T>
        {
            private readonly int pageSize;
            private readonly GetSliceAsync<T> getSliceAsync;

            public Pagination(uint pageSize, GetSliceAsync<T> getSliceAsync)
            {
                checked
                {
                    this.pageSize = (int) pageSize;
                }

                this.getSliceAsync = getSliceAsync;
            }

            public async Task<IReadOnlyList<T>> LoadAllAsync(TimeSpan? timeout = null)
            {
                var timeBudget = timeout == null ? null : TimeBudget.StartNew(timeout.Value);
                var data = new List<T>();
                var pageIndex = 0L;
                while (true)
                {
                    var page = await LoadPageAsync(pageIndex, timeBudget?.Remaining).ConfigureAwait(false);
                    if (!page.IsEmpty)
                    {
                        pageIndex++;
                        data.AddRange(page);
                    }

                    if (page.IsEmpty || page.IsLast)
                        break;
                }

                return data;
            }

            public async Task<Page<T>> LoadPageAsync(long pageIndex, TimeSpan? timeout = null)
            {
                var (items, totalItems) = await getSliceAsync(pageIndex*pageSize, pageSize, timeout).ConfigureAwait(false);
                return new Page<T>(items, (uint) pageSize, pageIndex, totalItems);
            }
        }

        private class Slicing : IEntityListSlicing<T>, IEntityListSliceLoading<T>
        {
            private readonly int sliceSize;
            private readonly long skipItems;
            private readonly GetSliceAsync<T> getSliceAsync;

            public Slicing(uint sliceSize, long skipItems, GetSliceAsync<T> getSliceAsync)
            {
                if (sliceSize == 0)
                    throw Errors.ValueShouldBeGreaterThanZero(nameof(sliceSize), sliceSize); 
                if (skipItems < 0)
                    throw Errors.ValueShouldBeGreaterThan(nameof(skipItems), skipItems, -1);
                
                checked
                {
                    this.sliceSize = (int) sliceSize;
                }

                this.skipItems = skipItems;
                this.getSliceAsync = getSliceAsync;
            }

            private Slicing(int sliceSize, long skipItems, GetSliceAsync<T> getSliceAsync)
            {
                if (skipItems < 0)
                    throw Errors.ValueShouldBeGreaterThan(nameof(skipItems), skipItems, -1);
                
                this.sliceSize = sliceSize;
                this.skipItems = skipItems;
                this.getSliceAsync = getSliceAsync;
            }

            public IEntityListSliceLoading<T> Skip(long skip) => 
                new Slicing(sliceSize, skip, getSliceAsync);

            public async Task<IReadOnlyList<T>> LoadAllAsync(TimeSpan? timeout = null)
            {
                var timeBudget = timeout == null ? null : TimeBudget.StartNew(timeout.Value);
                var data = new List<T>();
                var skip = 0L;
                while (true)
                {
                    var slice = await LoadSliceAsync(skip, sliceSize, timeBudget?.Remaining).ConfigureAwait(false);
                    if (slice.Items.Count > 0)
                    {
                        skip += sliceSize;
                        data.AddRange(slice.Items);
                    }

                    if (!slice.HasNextSlice)
                        break;
                }

                return data;
            }

            public Task<Slice<T>> LoadSliceAsync(TimeSpan? timeout = null) => LoadSliceAsync(skipItems, sliceSize, timeout);

            private async Task<Slice<T>> LoadSliceAsync(long skip, int take, TimeSpan? timeout = null)
            {
                var (items, totalItems) = await getSliceAsync(skip, take, timeout).ConfigureAwait(false);
                return new Slice<T>(items, items.Count > 0 && totalItems > take + skip, totalItems);
            }
        }
    }
}