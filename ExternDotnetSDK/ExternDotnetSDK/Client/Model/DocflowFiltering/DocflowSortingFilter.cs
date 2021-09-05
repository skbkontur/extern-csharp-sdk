#nullable enable
using Kontur.Extern.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Models.Common;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Model.DocflowFiltering
{
    public class DocflowSortingFilter
    {
        public static readonly DocflowSortingFilter NoSorting = new();
        
        /// <summary>
        /// Сортировка документооборотов по возрастанию/убыванию даты создания.
        /// Фильтр применяется, только если не указаны параметры updatedFrom, updatedTo
        /// </summary>
        public static DocflowSortingFilter OrderByCreationDate(SortOrder order) => new(order);

        /// <summary>
        /// Установить дату обновления документооборотов, от которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public static DocflowSortingFilter UpdatedFrom(DateOnly dateFrom) => new(updatedFrom: dateFrom);

        /// <summary>
        /// Установить дату обновления документооборотов, до которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public static DocflowSortingFilter UpdatedTo(DateOnly dateTo) => new(updatedTo: dateTo);

        private readonly SortOrder? order;
        private readonly DateOnly? updatedFrom;
        private readonly DateOnly? updatedTo;
        
        private DocflowSortingFilter(SortOrder? order = null, DateOnly? updatedFrom = null, DateOnly? updatedTo = null)
        {
            this.order = order;
            this.updatedFrom = updatedFrom;
            this.updatedTo = updatedTo;
        }

        public void ApplyTo(DocflowFilter docflowFilter)
        {
            docflowFilter.OrderBy = order;
            docflowFilter.UpdatedFrom = updatedFrom;
            docflowFilter.UpdatedTo = updatedTo;
        }
    }
}