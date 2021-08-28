using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [PublicAPI]
    public class DocflowFilter
    {
        private static PropertyInfo[] properties = typeof (DocflowFilter).GetProperties();

        /// <summary>
        /// True — получить завершенные документообороты. False — получить незавершенные документообороты
        /// </summary>
        public bool? Finished { get; set; }

        /// <summary>
        /// True — получить входящие документообороты. False — получить исходящие документообороты
        /// </summary>
        public bool? Incoming { get; set; }

        /// <summary>
        /// Количество записей, которые нужно пропустить при считывании
        /// </summary>
        public long? Skip { get; set; }

        /// <summary>
        /// Количество записей, которые нужно получить
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// ИНН-КПП, для которых нужно получить документообороты.Формат данных для юрлиц: 1234567890-123456789, для ИП: 123456789012
        /// </summary>
        public string InnKpp { get; set; }

        /// <summary>
        /// Идентификатор организации, для которой нужно получить документообороты
        /// </summary>
        public Guid? OrgId { get; set; }

        /// <summary>
        /// Сортировка документооборотов по возрастанию/убыванию даты создания.
        /// Фильтр применяется, только если не указаны параметры updatedFrom, updatedTo
        /// </summary>
        public SortOrder? OrderBy { get; set; }

        /// <summary>
        /// Дата обновления документооборотов, от которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public DateTime? UpdatedFrom { get; set; }

        /// <summary>
        /// Дата обновления документооборотов, до которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public DateTime? UpdatedTo { get; set; }

        /// <summary>
        /// Дата создания документооборотов, от которой нужно получить список
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Дата создания документооборотов, до которой нужно получить список
        /// </summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary>
        /// Типы документооборотов
        /// </summary>
        public Urn[] Types { get; set; }

        /// <summary>
        /// КНД – код налоговой декларации. Задается по маске XXXXXXX, где Х - это цифра от 0 до 9
        /// </summary>
        public string Knd { get; set; }

        /// <summary>
        /// ОКУД – общероссийский классификатор управленческой документации. Задается по маске ХХХХХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public string Okud { get; set; }

        /// <summary>
        /// ОКПО – общероссийский классификатор предприятий и организаций. Восьмизначный цифровой код для ЮЛ или десятизначный для ИП
        /// </summary>
        public string Okpo { get; set; }

        /// <summary>
        /// Контролирующий орган. Формат данных: ФНС — ХХХХ, ПФР — ХХХ-ХХХ, ФСС — ХХХХХ, Росстат — ХХ-ХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public string Cu { get; set; }

        /// <summary>
        /// Получить документооборот ПФР по регистрационному номеру. Маска для ввода ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Наименование формы
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Получить документообороты требований, которые относятся к декларациям ФНС. Только для документооборота типа fns534-demand
        /// </summary>
        public bool? DemandsOnReports { get; set; }

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода. Обязательно указание обеих границ отчетного периода
        /// </summary>
        public DateTime? PeriodFrom { get; set; }

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода. Обязательно указание обеих границ отчетного периода
        /// </summary>
        public DateTime? PeriodTo { get; set; }

        /// <summary>
        /// Получить документообороты всех пользователей (только для администратора)
        /// </summary>
        public bool? ForAllUsers { get; set; }

        public Dictionary<string, string> ConvertToQueryParameters()
        {
            var result = new Dictionary<string, string>();
            foreach (var info in properties.Where(x => x.GetValue(this) != null))
                result[ToLowerCamelCase(info.Name)] = info.PropertyType == typeof (DateTime?)
                    ? (info.GetValue(this) as DateTime?)?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                    : info.GetValue(this)?.ToString();
            return result;
        }

        private string ToLowerCamelCase(string value) => char.ToLowerInvariant(value[0]) + value.Substring(1);
    }
}