#nullable enable
using System;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
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
        /// Дата обновления документооборотов, от которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public static DocflowSortingFilter UpdatedFrom(DateTime dateFrom) => new(updatedFrom: dateFrom);

        /// <summary>
        /// Дата обновления документооборотов, до которой нужно получить список.
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
            docflowFilter.OrderBy = order;
            docflowFilter.UpdatedFrom = updatedFrom;
            docflowFilter.UpdatedTo = updatedTo;
        }
    }
}