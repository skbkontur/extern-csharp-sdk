using System.Collections.Generic;

namespace Kontur.Extern.Api.Client.Primitives
{
    public class Slice<T>
    {
        public Slice(IReadOnlyList<T> items, bool hasNextSlice, long totalItems)
        {
            Items = items;
            HasNextSlice = hasNextSlice;
            TotalItems = totalItems;
        }

        public IReadOnlyList<T> Items { get; }
        public bool HasNextSlice { get; }
        public long TotalItems { get; }
    }
}