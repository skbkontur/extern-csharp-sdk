using System;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DocflowListPath
    {
        public DocflowListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DocflowPath WithId(Guid docflowId) => new(AccountId, docflowId, services);

        public IEntityList<IDocflow> List(DocflowFilterBuilder? filterBuilder = null)
        {
            var apiClient = services.Api;
            var apiFilter = filterBuilder?.CreateFilter() ?? new DocflowFilter();

            var accountId = AccountId;
            return new EntityList<IDocflow>(
                async (skip, take, timeout) =>
                {
                    apiFilter.SetSkip(skip);
                    apiFilter.SetTake(take);

                    var docflowPage = await apiClient.Docflows.GetDocflowsAsync(accountId, apiFilter, timeout);

                    return (docflowPage.DocflowsPageItem, docflowPage.TotalCount);
                }
            );
        }
    }
}