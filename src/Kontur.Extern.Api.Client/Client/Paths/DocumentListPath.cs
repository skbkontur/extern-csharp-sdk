using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DocumentListPath
    {
        public DocumentListPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DocumentPath WithId(Guid documentId) => new(AccountId, DocflowId, documentId, services);

        public Task<List<Document>> ListAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Docflows.GetDocumentsAsync(AccountId, DocflowId, timeout);
        }
    }
}