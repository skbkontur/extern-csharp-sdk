using System;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Models.Common;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Model.DocflowFiltering
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
        /// Установить дату и время обновления документооборотов, от которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public static DocflowSortingFilter UpdatedFrom(DateTime dateFrom) => new(updatedFrom: dateFrom);

        /// <summary>
        /// Установить дату и время обновления документооборотов, до которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public static DocflowSortingFilter UpdatedTo(DateTime dateTo) => new(updatedTo: dateTo);

        private readonly SortOrder? order;
        private readonly DateTime? updatedFrom;
        private readonly DateTime? updatedTo;
        
        private DocflowSortingFilter(SortOrder? order = null, DateTime? updatedFrom = null, DateTime? updatedTo = null)
        {
            this.order = order;
            this.updatedFrom = updatedFrom;
            this.updatedTo = updatedTo;
        }

        public void ApplyTo(DocflowFilter docflowFilter)
        {
            docflowFilter.SetOrderBy(order);
            docflowFilter.SetUpdatedFrom(updatedFrom);
            docflowFilter.SetUpdatedTo(updatedTo);
        }
    }
}