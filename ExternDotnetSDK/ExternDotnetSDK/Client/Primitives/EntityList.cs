#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Exceptions;

namespace Kontur.Extern.Client.Primitives
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

        private class Pagination : IPagination<T>
        {
            private readonly uint pageSize;
            private readonly GetSliceAsync<T> getSliceAsync;

            public Pagination(uint pageSize, GetSliceAsync<T> getSliceAsync)
            {
                this.pageSize = pageSize;
                this.getSliceAsync = getSliceAsync;
            }

            public async Task<IReadOnlyList<T>> LoadAllAsync()
            {
                var data = new List<T>();
                var pageIndex = 0L;
                while (true)
                {
                    var page = await LoadPageAsync(pageIndex).ConfigureAwait(false);
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

            public async Task<Page<T>> LoadPageAsync(long pageIndex)
            {
                var (items, totalItems) = await getSliceAsync(pageIndex*pageSize, pageSize).ConfigureAwait(false);
                return new Page<T>(items, pageSize, pageIndex, totalItems);
            }
        }

        private class Slicing : IEntityListSlicing<T>, IEntityListSliceLoading<T>
        {
            private readonly uint sliceSize;
            private readonly long skipItems;
            private readonly GetSliceAsync<T> getSliceAsync;

            public Slicing(uint sliceSize, long skipItems, GetSliceAsync<T> getSliceAsync)
            {
                if (sliceSize == 0)
                    throw Errors.ValueShouldBeGreaterThanZero(nameof(sliceSize), sliceSize); 
                if (skipItems < 0)
                    throw Errors.ValueShouldBeGreaterThan(nameof(skipItems), skipItems, -1); 
                this.sliceSize = sliceSize;
                this.skipItems = skipItems;
                this.getSliceAsync = getSliceAsync;
            }

            public IEntityListSliceLoading<T> Skip(long skip) => 
                new Slicing(sliceSize, skip, getSliceAsync);

            public async Task<IReadOnlyList<T>> LoadAllAsync()
            {
                var data = new List<T>();
                var skip = 0L;
                while (true)
                {
                    var (items, hasNextSlice) = await LoadSliceAsync(skip, sliceSize).ConfigureAwait(false);
                    if (items.Count > 0)
                    {
                        skip += sliceSize;
                        data.AddRange(items);
                    }

                    if (!hasNextSlice)
                        break;
                }

                return data;
            }

            public Task<(IReadOnlyList<T> Items, bool HasNextSlice)> LoadSliceAsync() => LoadSliceAsync(skipItems, sliceSize);

            private async Task<(IReadOnlyList<T> Items, bool HasNextSlice)> LoadSliceAsync(long skip, long take)
            {
                var (items, totalItems) = await getSliceAsync(skip, take).ConfigureAwait(false);
                return (items, items.Count > 0 && totalItems > take + skip);
            }
        }
    }
}