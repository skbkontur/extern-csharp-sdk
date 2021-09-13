#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders
{
    internal interface IDocumentContentUploadStrategy
    {
        string? ContentType { get; }
        
        ValueTask<(Guid? contentId, Signature? signature)> UploadAndSignAsync(Guid accountId, IContentService uploader, ICrypt crypt, TimeSpan? uploadTimeout);
    }
}