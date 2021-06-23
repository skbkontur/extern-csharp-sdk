using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;

namespace Kontur.Extern.Client.Engine.Services.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class DocflowsService : IDocflowsService
    {
        public IDocflowsClient Client { get; }
        private readonly IAccountIdProvider accountIdProvider;
        private readonly TimeSpan? timeout;

        public DocflowsService(IAccountIdProvider accountIdProvider, IDocflowsClient docflowsClient, TimeSpan? timeout)
        {
            this.accountIdProvider = accountIdProvider;
            Client = docflowsClient;
            this.timeout = timeout;
        }

        public Task<DocflowPage> GetDocflowsAsync(DocflowFilter filter = null) => Client.GetDocflowsAsync(accountIdProvider.AccountId, filter, timeout);

        public Task<DocflowPage> GetRelatedDocflows(
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null)
            => Client.GetRelatedDocflows(accountIdProvider.AccountId, relatedDocflowId, relatedDocumentId, filter, timeout);

        public Task<Docflow> GetDocflowAsync(Guid docflowId)
            => Client.GetDocflowAsync(accountIdProvider.AccountId, docflowId, timeout);

        public Task<List<Document>> GetDocumentsAsync(Guid docflowId)
            => Client.GetDocumentsAsync(accountIdProvider.AccountId, docflowId, timeout);

        public Task<Document> GetDocumentAsync(Guid docflowId, Guid documentId)
            => Client.GetDocumentAsync(accountIdProvider.AccountId, docflowId, documentId, timeout);

        public Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(Guid docflowId, Guid documentId)
            => Client.GetDocumentDescriptionAsync(accountIdProvider.AccountId, docflowId, documentId, timeout);

        public Task<List<Signature>> GetDocumentSignaturesAsync(Guid docflowId, Guid documentId)
            => Client.GetDocumentSignaturesAsync(accountIdProvider.AccountId, docflowId, documentId, timeout);

        public Task<Signature> GetSignatureAsync(Guid docflowId, Guid documentId, Guid signatureId)
            => Client.GetSignatureAsync(
                accountIdProvider.AccountId,
                docflowId,
                documentId,
                signatureId,
                timeout);

        public Task<byte[]> GetSignatureContentAsync(
            Guid docflowId,
            Guid documentId,
            Guid signatureId)
            => Client.GetSignatureContentAsync(accountIdProvider.AccountId, docflowId, documentId, signatureId, timeout);

        public Task<PrintDocumentResult> PrintDocumentAsync(
            Guid docflowId,
            Guid documentId,
            Guid contentId)
            => Client.PrintDocumentAsync(accountIdProvider.AccountId, docflowId, documentId, contentId, timeout);

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(
            Guid docflowId,
            Guid documentId,
            Guid contentId)
            => Client.StartPrintDocumentAsync(accountIdProvider.AccountId, docflowId, documentId, contentId, timeout);

        public Task<ApiTaskResult<PrintDocumentResult>> GetPrintTaskAsync(
            Guid docflowId,
            Guid documentId,
            Guid taskId)
            => Client.StartPrintDocumentAsync(accountIdProvider.AccountId, docflowId, documentId, taskId, timeout);

        public Task<CloudDecryptionInitResult> StartCloudDecryptDocumentAsync(
            Guid docflowId,
            Guid documentId,
            byte[] certificate)
            => Client.StartCloudDecryptDocumentAsync(accountIdProvider.AccountId, docflowId, documentId, certificate, timeout);

        public Task<DecryptDocumentResult> ConfirmDocumentDecryptionAsync(
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool? unzip = null)
            => Client.ConfirmDocumentDecryptionAsync(
                accountIdProvider.AccountId,
                docflowId,
                documentId,
                requestId,
                code,
                unzip,
                timeout);

        public Task<DssDecryptionInitResult> StartDssDecryptDocumentAsync(
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            bool? unzip = null)
            => Client.StartDssDecryptDocumentAsync(
            accountIdProvider.AccountId,
            docflowId,
            documentId,
            certificate,
            unzip,
            timeout);

        public Task<ApiTaskResult<DecryptDocumentResult>> GetDssDecryptDocumentTaskAsync(
            Guid docflowId,
            Guid documentId,
            Guid taskId)
                => Client.GetDssDecryptDocumentTaskAsync(
            accountIdProvider.AccountId,
            docflowId,
            documentId,
            taskId,
            timeout);

        public Task<RecognizeResult> RecognizeDocumentAsync(
            Guid docflowId,
            Guid documentId,
            Guid contentId  )
            => Client.RecognizeDocumentAsync(
            accountIdProvider.AccountId,
            docflowId,
            documentId,
            contentId,
            timeout);
    }
}