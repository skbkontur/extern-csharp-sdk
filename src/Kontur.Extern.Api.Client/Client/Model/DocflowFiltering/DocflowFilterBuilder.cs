using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Common.Enums;

namespace Kontur.Extern.Api.Client.Model.DocflowFiltering
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocflowFilterBuilder
    {
        private DocflowFilter filter;
        private DateTime? createdTo;
        private DateTime? createdFrom;
        private DateTime? updatedTo;
        private DateTime? updatedFrom;

        public DocflowFilterBuilder() => filter = new DocflowFilter();

        /// <summary>
        /// True — получить завершенные документообороты. False — получить незавершенные документообороты
        /// </summary>
        public DocflowFilterBuilder WithFinishedDocflows(bool incoming = true)
        {
            filter.SetFinished(incoming);    
            return this;
        }

        /// <summary>
        /// True — получить входящие документообороты. False — получить исходящие документообороты
        /// </summary>
        public DocflowFilterBuilder WithIncomingDocflows(bool incoming = true)
        {
            filter.SetIncoming(incoming);
            return this;
        }

        /// <summary>
        /// ИНН-КПП, для которых нужно получить документообороты для юрлиц. Формат данных для юрлиц: 1234567890-123456789, для ИП: 123456789012
        /// </summary>
        public DocflowFilterBuilder WithInnKppOfALegalEntity(string inn, string kpp)
        {
            filter.SetInnKpp(InnKpp.Parse($"{inn}-{kpp}").Value);
            return this;
        }

        /// <summary>
        /// ИНН-КПП для юрлиц, для которых нужно получить документообороты. Формат данных: 1234567890-123456789
        /// </summary>
        public DocflowFilterBuilder WithInnKppOfALegalEntity(InnKpp innKpp)
        {
            filter.SetInnKpp(innKpp.Value);
            return this;
        }

        /// <summary>
        /// ИНН для ИП, для которых нужно получить документообороты. Формат данных: 123456789012
        /// </summary>
        public DocflowFilterBuilder WithIndividualEntrepreneurInn(Inn inn)
        {
            filter.SetInnKpp(inn.Value);
            return this;
        }

        /// <summary>
        /// Идентификатор организации, для которой нужно получить документообороты
        /// </summary>
        public DocflowFilterBuilder WithOrganizationId(Guid id)
        {
            filter.SetOrgId(id);
            return this;
        }

        /// <summary>
        /// Установить дату и время создания документооборотов, от которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithCreatedFrom(DateTime dateFrom)
        {
            if (createdTo < dateFrom)
                throw Errors.InvalidRange(nameof(createdFrom), nameof(createdTo), dateFrom, createdTo.Value);

            filter.SetCreatedFrom(dateFrom);
            createdFrom = dateFrom;
            return this;
        }

        /// <summary>
        /// Установить дату и время создания документооборотов, до которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithCreatedTo(DateTime dateTo)
        {
            if (createdFrom > dateTo)
                throw Errors.InvalidRange(nameof(createdFrom), nameof(createdTo), createdFrom.Value, dateTo);
            
            filter.SetCreatedTo(dateTo);
            createdTo = dateTo;
            return this;
        }

        /// <summary>
        /// Установить дату и время создания документооборотов, от которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithUpdatedFrom(DateTime dateFrom)
        {
            if (updatedTo < dateFrom)
                throw Errors.InvalidRange(nameof(updatedFrom), nameof(updatedTo), dateFrom, updatedTo.Value);

            filter.SetUpdatedFrom(dateFrom);
            updatedFrom = dateFrom;
            return this;
        }

        /// <summary>
        /// Установить дату и время создания документооборотов, до которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithUpdatedTo(DateTime dateTo)
        {
            if (updatedFrom > dateTo)
                throw Errors.InvalidRange(nameof(updatedFrom), nameof(updatedTo), updatedFrom.Value, dateTo);
            
            filter.SetUpdatedTo(dateTo);
            updatedTo = dateTo;
            return this;
        }

        /// <summary>
        /// Типы документооборотов
        /// </summary>
        public DocflowFilterBuilder WithTypes(params DocflowType[] types)
        {
            filter.SetTypes(types);
            return this;
        }

        /// <summary>
        /// КНД – код налоговой декларации.
        /// </summary>
        public DocflowFilterBuilder WithKnd(Knd knd)
        {
            filter.SetKnd(knd.Value);
            return this;
        }

        /// <summary>
        /// ОКУД – общероссийский классификатор управленческой документации.
        /// </summary>
        public DocflowFilterBuilder WithOkud(Okud okud)
        {
            filter.SetOkud(okud.Value);
            return this;
        }

        /// <summary>
        /// ОКПО – общероссийский классификатор предприятий и организаций.
        /// </summary>
        public DocflowFilterBuilder WithOkpo(Okpo okpo)
        {
            filter.SetOkpo(okpo.Value);
            return this;
        }
        
        /// <summary>
        /// Контролирующий орган.
        /// </summary>
        public DocflowFilterBuilder WithCu(AuthorityCode code)
        {
            filter.SetCu(code.Value);
            return this;
        }

        /// <summary>
        /// Получить документооборот ПФР по регистрационному номеру.
        /// </summary>
        public DocflowFilterBuilder WithRegNumberOfPfrDocflow(PfrRegNumber regNumber)
        {
            filter.SetRegNumber(regNumber.Value);
            return this;
        }

        /// <summary>
        /// Наименование формы
        /// </summary>
        public DocflowFilterBuilder WithFormName(string name)
        {
            filter.SetFormName(name);
            return this;
        }

        /// <summary>
        /// Типы писем ПФР. Только для документооборотов типа pfr-letter и pfr-cu-letter
        /// </summary>
        public DocflowFilterBuilder WithPfrLetterTypes(params PfrLetterType[] types)
        {
            filter.SetPfrLetterTypes(types);
            return this;
        }

        /// <summary>
        /// Получить документообороты требований, которые относятся к декларациям ФНС. Только для документооборота типа fns534-demand
        /// </summary>
        public DocflowFilterBuilder WithDemandsOnReports(bool enabled = true)
        {
            filter.SetDemandsOnReports(enabled);
            return this;
        }

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода.
        /// </summary>
        public DocflowFilterBuilder WithReportingPeriod(DateOnly from, DateOnly to)
        {
            if (from > to)
                throw Errors.InvalidRange(nameof(from), nameof(to), from, to);
            
            filter.SetPeriodFrom(@from);
            filter.SetPeriodTo(to);
            return this;
        }

        /// <summary>
        /// Получить документообороты всех пользователей (только для администратора)
        /// </summary>
        public DocflowFilterBuilder ForAllUsers(bool enabled = true)
        {
            filter.SetForAllUsers(enabled);
            return this;
        }

        public DocflowFilterBuilder WithSortingFilter(DocflowSortingFilter sortingFilter)
        {
            sortingFilter.ApplyTo(filter);
            return this;
        }

        public DocflowFilter CreateFilter() => filter;
    }
}