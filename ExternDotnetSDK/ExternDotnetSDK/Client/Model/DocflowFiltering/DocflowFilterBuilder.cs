#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Numbers;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Model.DocflowFiltering
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public class DocflowFilterBuilder
    {
        private DocflowFilter filter;

        public DocflowFilterBuilder() => filter = new DocflowFilter();

        /// <summary>
        /// True — получить завершенные документообороты. False — получить незавершенные документообороты
        /// </summary>
        public DocflowFilterBuilder WithFinishedDocflows(bool incoming = true)
        {
            filter.Finished = incoming;    
            return this;
        }

        /// <summary>
        /// True — получить входящие документообороты. False — получить исходящие документообороты
        /// </summary>
        public DocflowFilterBuilder WithIncomingDocflows(bool incoming = true)
        {
            filter.Incoming = incoming;
            return this;
        }

        /// <summary>
        /// ИНН-КПП, для которых нужно получить документообороты для юрлиц. Формат данных для юрлиц: 1234567890-123456789, для ИП: 123456789012
        /// </summary>
        public DocflowFilterBuilder WithInnKppOfALegalEntity(string inn, string kpp)
        {
            filter.InnKpp = InnKpp.Parse($"{inn}-{kpp}").Value;
            return this;
        }

        /// <summary>
        /// ИНН-КПП для юрлиц, для которых нужно получить документообороты. Формат данных: 1234567890-123456789
        /// </summary>
        public DocflowFilterBuilder WithInnKppOfALegalEntity(InnKpp innKpp)
        {
            filter.InnKpp = innKpp.Value;
            return this;
        }

        /// <summary>
        /// ИНН для ИП, для которых нужно получить документообороты. Формат данных: 123456789012
        /// </summary>
        public DocflowFilterBuilder WithIndividualEntrepreneurInn(Inn inn)
        {
            filter.InnKpp = inn.Value;
            return this;
        }

        /// <summary>
        /// Идентификатор организации, для которой нужно получить документообороты
        /// </summary>
        public DocflowFilterBuilder WithOrganizationId(Guid id)
        {
            filter.OrgId = id;
            return this;
        }

        /// <summary>
        /// Дата создания документооборотов, от которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithCreatedFrom(DateTime dateFrom)
        {
            if (filter.CreatedTo < dateFrom)
                throw Errors.InvalidRange(nameof(dateFrom), nameof(filter.CreatedTo), dateFrom, filter.CreatedTo.Value);

            filter.CreatedFrom = dateFrom;
            return this;
        }

        /// <summary>
        /// Дата создания документооборотов, до которой нужно получить список
        /// </summary>
        public DocflowFilterBuilder WithCreatedTo(DateTime dateTo)
        {
            if (filter.CreatedFrom > dateTo)
                throw Errors.InvalidRange(nameof(dateTo), nameof(filter.CreatedFrom), filter.CreatedFrom.Value, dateTo);
            
            filter.CreatedTo = dateTo;
            return this;
        }

        /// <summary>
        /// Типы документооборотов
        /// </summary>
        public DocflowFilterBuilder WithTypes(Urn[] docTypeUrns)
        {
            filter.Types = docTypeUrns;
            return this;
        }

        /// <summary>
        /// КНД – код налоговой декларации.
        /// </summary>
        public DocflowFilterBuilder WithKnd(Knd knd)
        {
            filter.Knd = knd.Value;
            return this;
        }

        /// <summary>
        /// ОКУД – общероссийский классификатор управленческой документации.
        /// </summary>
        public DocflowFilterBuilder WithOkud(Okud okud)
        {
            filter.Okud = okud.Value;
            return this;
        }

        /// <summary>
        /// ОКПО – общероссийский классификатор предприятий и организаций.
        /// </summary>
        public DocflowFilterBuilder WithOkpo(Okpo okpo)
        {
            filter.Okpo = okpo.Value;
            return this;
        }
        
        /// <summary>
        /// Контролирующий орган.
        /// </summary>
        public DocflowFilterBuilder WithCu(AuthorityCode code)
        {
            filter.Cu = code.Value;
            return this;
        }

        /// <summary>
        /// Получить документооборот ПФР по регистрационному номеру.
        /// </summary>
        public DocflowFilterBuilder WithRegNumberOfPfrDocflow(PfrRegNumber regNumber)
        {
            filter.RegNumber = regNumber.Value;
            return this;
        }

        /// <summary>
        /// Наименование формы
        /// </summary>
        public DocflowFilterBuilder WithFormName(string name)
        {
            filter.FormName = name;
            return this;
        }

        /// <summary>
        /// Получить документообороты требований, которые относятся к декларациям ФНС. Только для документооборота типа fns534-demand
        /// </summary>
        public DocflowFilterBuilder WithDemandsOnReports(bool enabled = true)
        {
            filter.DemandsOnReports = enabled;
            return this;
        }

        /// <summary>
        /// Поиск документооборотов по указанному началу отчетного периода.
        /// </summary>
        public DocflowFilterBuilder WithReportingPeriod(DateTime from, DateTime to)
        {
            if (from > to)
                throw Errors.InvalidRange(nameof(from), nameof(to), from, to);
            
            filter.PeriodFrom = from;
            filter.PeriodTo = to;
            return this;
        }

        /// <summary>
        /// Получить документообороты всех пользователей (только для администратора)
        /// </summary>
        public DocflowFilterBuilder ForAllUsers(bool enabled = true)
        {
            filter.ForAllUsers = enabled;
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