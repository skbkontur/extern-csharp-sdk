using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Common.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocflowFilter
    {
        private QueryParameters queryParameters = new();

        /// <summary>
        /// True — получить завершенные документообороты. False — получить незавершенные документообороты
        /// </summary>
        public void SetFinished(bool? value) => queryParameters.Set(QueryParameters.finished, value);

        /// <summary>
        /// True — получить входящие документообороты. False — получить исходящие документообороты
        /// </summary>
        public void SetIncoming(bool? value) => queryParameters.Set(QueryParameters.incoming, value);

        /// <summary>
        /// Количество записей, которые нужно пропустить при считывании
        /// </summary>
        public void SetSkip(long? value) => queryParameters.Set(QueryParameters.skip, value);

        /// <summary>
        /// Количество записей, которые нужно получить
        /// </summary>
        public void SetTake(int? value) => queryParameters.Set(QueryParameters.take, value);

        /// <summary>
        /// ИНН-КПП, для которых нужно получить документообороты.Формат данных для юрлиц: 1234567890-123456789, для ИП: 123456789012
        /// </summary>
        public void SetInnKpp(string? value) => queryParameters.Set(QueryParameters.innKpp, value);

        /// <summary>
        /// Идентификатор организации, для которой нужно получить документообороты
        /// </summary>
        public void SetOrgId(Guid? value) => queryParameters.Set(QueryParameters.orgId, value);

        /// <summary>
        /// Сортировка документооборотов по возрастанию/убыванию даты создания.
        /// Фильтр применяется, только если не указаны параметры updatedFrom, updatedTo
        /// </summary>
        public void SetOrderBy(SortOrder? value) => queryParameters.Set(QueryParameters.orderBy, value);

        /// <summary>
        /// Дата и время обновления документооборотов, от которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public void SetUpdatedFrom(DateTime? value) => queryParameters.Set(QueryParameters.updatedFrom, value);

        /// <summary>
        /// Дата и время обновления документооборотов, до которой нужно получить список.
        /// Документы автоматически будут отсортированы по дате изменения
        /// </summary>
        public void SetUpdatedTo(DateTime? value) => queryParameters.Set(QueryParameters.updatedTo, value);

        /// <summary>
        /// Дата и время создания документооборотов, от которой нужно получить список
        /// </summary>
        public void SetCreatedFrom(DateTime? value) => queryParameters.Set(QueryParameters.createdFrom, value);

        /// <summary>
        /// Дата и время создания документооборотов, до которой нужно получить список
        /// </summary>
        public void SetCreatedTo(DateTime? value) => queryParameters.Set(QueryParameters.createdTo, value);

        /// <summary>
        /// Типы документооборотов
        /// </summary>
        public void SetTypes(DocflowType[] value) => queryParameters.Set(value);

        /// <summary>
        /// КНД – код налоговой декларации. Задается по маске XXXXXXX, где Х - это цифра от 0 до 9
        /// </summary>
        public void SetKnd(string? value) => queryParameters.Set(QueryParameters.knd, value);

        /// <summary>
        /// ОКУД – общероссийский классификатор управленческой документации. Задается по маске ХХХХХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public void SetOkud(string? value) => queryParameters.Set(QueryParameters.okud, value);

        /// <summary>
        /// ОКПО – общероссийский классификатор предприятий и организаций. Восьмизначный цифровой код для ЮЛ или десятизначный для ИП
        /// </summary>
        public void SetOkpo(string? value) => queryParameters.Set(QueryParameters.okpo, value);

        /// <summary>
        /// Контролирующий орган. Формат данных: ФНС — ХХХХ, ПФР — ХХХ-ХХХ, ФСС — ХХХХХ, Росстат — ХХ-ХХ, где Х — это цифра от 0 до 9
        /// </summary>
        public void SetCu(string? value) => queryParameters.Set(QueryParameters.cu, value);

        /// <summary>
        /// Получить документооборот ПФР по регистрационному номеру. Маска для ввода ХХХ-ХХХ-ХХХХХХ, где Х - это цифра от 0 до 9
        /// </summary>
        public void SetRegNumber(string? value) => queryParameters.Set(QueryParameters.regNumber, value);

        /// <summary>
        /// Наименование формы
        /// </summary>
        public void SetFormName(string? value) => queryParameters.Set(QueryParameters.formName, value);

        /// <summary>
        /// Получить документообороты требований, которые относятся к декларациям ФНС. Только для документооборота типа fns534-demand
        /// </summary>
        public void SetDemandsOnReports(bool? value) => queryParameters.Set(QueryParameters.demandsOnReports, value);

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода. Обязательно указание обеих границ отчетного периода
        /// </summary>
        public void SetPeriodFrom(DateOnly? value) => queryParameters.Set(QueryParameters.periodFrom, value);

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода. Обязательно указание обеих границ отчетного периода
        /// </summary>
        public void SetPeriodTo(DateOnly? value) => queryParameters.Set(QueryParameters.periodTo, value);

        /// <summary>
        /// Получить документообороты всех пользователей (только для администратора)
        /// </summary>
        public void SetForAllUsers(bool? value) => queryParameters.Set(QueryParameters.forAllUsers, value);

        /// <summary>
        /// Категории писем ПФР. Только для документооборотов типа pfr-letter и pfr-cu-letter
        /// </summary>
        public void SetPfrLetterTypes(PfrLetterType[] value) => queryParameters.Set(value);

        public IEnumerable<(string name, string value)> ToQueryParameters() => 
            queryParameters.GetParameters();

        private string ToLowerCamelCase(string value) => char.ToLowerInvariant(value[0]) + value.Substring(1);

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private class QueryParameters
        {
            // NOTE: do not change const names -- they should be in camel case
            public const string finished = nameof(finished);
            public const string incoming = nameof(incoming);
            public const string skip = nameof(skip);
            public const string take = nameof(take);
            public const string innKpp = nameof(innKpp);
            public const string orgId = nameof(orgId);
            public const string orderBy = nameof(orderBy);
            public const string updatedFrom = nameof(updatedFrom);
            public const string updatedTo = nameof(updatedTo);
            public const string createdFrom = nameof(createdFrom);
            public const string createdTo = nameof(createdTo);
            public const string type = nameof(type);
            public const string knd = nameof(knd);
            public const string okud = nameof(okud);
            public const string okpo = nameof(okpo);
            public const string cu = nameof(cu);
            public const string regNumber = nameof(regNumber);
            public const string formName = nameof(formName);
            public const string demandsOnReports = nameof(demandsOnReports);
            public const string periodFrom = nameof(periodFrom);
            public const string periodTo = nameof(periodTo);
            public const string forAllUsers = nameof(forAllUsers);
            public const string pfrLetterCategory = nameof(pfrLetterCategory);
            
            private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffK";

            private readonly Dictionary<string, string> queryParameters = new();
            private readonly List<string> types = new(0);
            private readonly List<string> pfrLetterTypes = new(0);

            public IEnumerable<(string name, string value)> GetParameters()
            {
                foreach (var pair in queryParameters)
                {
                    yield return (pair.Key, pair.Value);
                }

                foreach (var typeValue in types)
                {
                    yield return (type, typeValue);
                }

                foreach (var category in pfrLetterTypes)
                {
                    yield return (pfrLetterCategory, category);
                }
            }
            
            public void Set(string parameterName, bool? value) => 
                SetValue(parameterName, value?.ToString().ToLowerInvariant());

            public void Set(string parameterName, long? value) => 
                SetValue(parameterName, value?.ToString());

            public void Set(string parameterName, int? value) => 
                SetValue(parameterName, value?.ToString());

            public void Set(string parameterName, Guid? value) => 
                SetValue(parameterName, value?.ToString());

            public void Set(string parameterName, SortOrder? sortOrder) => 
                SetValue(parameterName, sortOrder?.ToString().ToLowerInvariant());

            public void Set(string parameterName, DateOnly? value) => 
                SetValue(parameterName, value?.ToString(DateTimeFormat));

            public void Set(string parameterName, DateTime? value) =>
                SetValue(parameterName, value?.ToString(DateTimeFormat));

            public void Set(DocflowType[]? docflowTypes)
            {
                if (docflowTypes is null)
                {
                    types.Clear();
                }
                else
                {
                    foreach (var docflowType in docflowTypes)
                    {
                        var urn = docflowType.ToUrn();
                        if (!types.Contains(urn.Nss))
                            types.Add(urn.Nss);
                    }
                }
            }

            public void Set(PfrLetterType[]? letterTypes)
            {
                if (letterTypes is null)
                {
                    pfrLetterTypes.Clear();
                }
                else
                {
                    foreach (var letterType in letterTypes)
                    {
                        var urn = letterType.ToUrn();
                        if (!pfrLetterTypes.Contains(urn.Nss))
                            pfrLetterTypes.Add(urn.Nss);
                    }
                }
            }

            public void Set(string parameterName, string? value) => 
                SetValue(parameterName, value);

            private void SetValue(string parameterName, string? value)
            {
                if (value is not null)
                {
                    queryParameters[parameterName] = value;
                }
                else
                {
                    queryParameters.Remove(parameterName);
                }
            }
        }
    }
}