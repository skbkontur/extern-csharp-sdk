using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal interface IDocumentContentUploadStrategy
    {
        string? ContentType { get; }
        
        ValueTask<(Guid? contentId, Signature? signature)> UploadAndSignAsync(Guid accountId, IContentService uploader, ICrypt crypt, TimeSpan? uploadTimeout);
    }
}