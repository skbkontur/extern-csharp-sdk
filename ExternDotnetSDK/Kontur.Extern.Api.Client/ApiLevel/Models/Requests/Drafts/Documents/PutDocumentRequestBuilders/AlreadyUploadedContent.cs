using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal class AlreadyUploadedContent : IDocumentContentUploadStrategy
    {
        private readonly Guid contentId;
        private readonly Signature? signature;

        public AlreadyUploadedContent(Guid contentId, string? contentType, Signature? signature)
        {
            this.contentId = contentId;
            ContentType = contentType;
            this.signature = signature;
        }
        
        public string? ContentType { get; }

        public ValueTask<(Guid? contentId, Signature? signature)> UploadAndSignAsync(Guid accountId, IContentService uploader, ICrypt crypt, TimeSpan? uploadTimeout) =>
            new((contentId, signature));
    }
}