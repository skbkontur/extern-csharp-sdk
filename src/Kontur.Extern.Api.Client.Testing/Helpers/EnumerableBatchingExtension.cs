using System.Collections.Generic;

namespace Kontur.Extern.Api.Client.Testing.Helpers
{
    public static class EnumerableBatchingExtension 
    { 
        public static IEnumerable<IReadOnlyList<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return MakeBatch(enumerator, batchSize);
            }
            
            static IReadOnlyList<T> MakeBatch(IEnumerator<T> source, int batchSize)
            {
                var batch = new List<T>(batchSize) {source.Current};
                batchSize--;
                for (var i = 0; i < batchSize && source.MoveNext(); i++)
                {
                    batch.Add(source.Current);
                }
                return batch;
            } 
        } 
    }
}