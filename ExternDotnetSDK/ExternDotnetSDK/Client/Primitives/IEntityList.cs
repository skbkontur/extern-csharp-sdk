namespace Kontur.Extern.Client.Primitives
{
    interface IEntityList<T>
    {
        
        /// <summary>
        /// Allows to change take items (and page size)
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        IEntityListSlicing<T> SliceBy(uint take);
        
        /// <summary>
        /// return pagination interface
        /// </summary>
        /// <returns></returns>
        IPagination<T> Paging(uint pageSize);        
    }
}