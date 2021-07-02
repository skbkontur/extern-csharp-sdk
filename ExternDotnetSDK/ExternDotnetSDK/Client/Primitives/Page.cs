using System.Collections;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Clients.Exceptions;

namespace Kontur.Extern.Client.Primitives
{
    public class Page<T> : IPage<T>
    {
        private readonly IReadOnlyList<T> items;

        public Page(IReadOnlyList<T> items, uint pageSize, long pageIndex, long totalItems)
        {
            if (pageSize == 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(pageSize), pageSize);
            if (pageIndex < 0)
                throw Errors.ValueShouldBeGreaterThan(nameof(pageIndex), pageIndex, -1);
            if (totalItems < 0)
                throw Errors.ValueShouldBeGreaterThan(nameof(totalItems), totalItems, -1);
            if (items.Count > pageSize)
                throw Errors.ListCannotBeGreaterThanParamSpecify(nameof(items), nameof(pageSize), items, pageSize);

            PageSize = pageSize;
            CurrentPage = pageIndex;
            TotalItems = totalItems;
            this.items = items;
            if (totalItems == 0)
            {
                if (items.Count > 0)
                    throw Errors.ItemsOfLastPageIsGreaterThanLeftItems(nameof(items), items.Count, 0);
                return;
            }
            
            var totalPages = totalItems > 0 ? (totalItems - 1) / pageSize + 1 : 0u;
            if (pageIndex == totalPages - 1)
            {
                var leftItems = totalItems - pageIndex*pageSize;
                if (items.Count > leftItems)
                    throw Errors.ItemsOfLastPageIsGreaterThanLeftItems(nameof(items), items.Count, leftItems);
            }
            else if (pageIndex >= totalPages)
            {
                if (items.Count > 0)
                    throw Errors.ItemsOfLastPageIsGreaterThanLeftItems(nameof(items), items.Count, 0);
            }
            else if (items.Count < pageSize)
            {
                throw Errors.IntermediatePageSizeCannotBeLessThanPageSize(nameof(items), items.Count, pageIndex, pageSize);
            }

            TotalPages = totalPages;
        }
        
        public bool IsPageNonExistent => CurrentPage >= TotalPages;
        
        public long TotalItems { get; }
        public long CurrentPage { get; }
        public uint PageSize { get; }
        public long TotalPages { get; }
        
        public IEnumerable<long> Pages
        {
            get
            {
                for (long i = 0; i < TotalPages; i++)
                {
                    yield return i;
                }
            }
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => items.Count;
        public bool IsEmpty => items.Count == 0;
        public bool IsLast => CurrentPage == TotalPages - 1;
        public T this[int index] => items[index];
    }
}