namespace Kontur.Extern.Client.Primitives
{
    public interface IEntityList<T>
    {
        
        /// <summary>
        /// Allows to change take items (and page size)
        /// </summary>
        /// <param name="sliceSize"></param>
        /// <returns></returns>
        IEntityListSlicing<T> SliceBy(uint sliceSize);
        
        /// <summary>
        /// return pagination interface
        /// </summary>
        /// <returns></returns>
        IPagination<T> Paging(uint pageSize);        
    }
}