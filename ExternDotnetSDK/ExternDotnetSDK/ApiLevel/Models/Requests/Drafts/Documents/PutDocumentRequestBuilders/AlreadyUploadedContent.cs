using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
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