using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives
{
    public interface IEntityList<T>
    {
        /// <summary>
        /// Взаимодействие с независимыми участками списка сущности (получение части списка в независимом порядке, аналог skip/take)
        /// </summary>
        /// <returns></returns>
        IEntityListSlicing<T> SliceBy(uint sliceSize);

        /// <summary>
        /// Взаимодействие со списком сущности с помощью пагинации (получение части списка в очерёдном порядке)
        /// </summary>
        /// <returns></returns>
        IPagination<T> Paging(uint pageSize);

        /// <summary>
        /// Общее количество элементов списка
        /// </summary>
        /// <returns></returns>
        Task<long> CountAsync(TimeSpan? timeout = null);
    }
}