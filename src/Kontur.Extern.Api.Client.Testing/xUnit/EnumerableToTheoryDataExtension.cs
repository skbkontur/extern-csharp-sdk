using System.Collections.Generic;
using Xunit;

namespace Kontur.Extern.Api.Client.Testing.xUnit
{
    public static class EnumerableToTheoryDataExtension
    {
        public static TheoryData<T> ToTheoryData<T>(this IEnumerable<T> enumerable)
        {
            var theoryData = new TheoryData<T>();
            foreach (var item in enumerable)
            {
                theoryData.Add(item);
            }

            return theoryData;
        }
    }
}