#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Model.Documents.Contents;

namespace Kontur.Extern.Client.Model.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDraftDocument
    {
        Guid DocumentId { get; }

        bool TryGetDocumentContent(out IDocumentContent content);
        
        Task<(Signature? signature, DocumentRequest request)> CreateSignedRequestAsync(Guid contentId, ICrypt crypt);
        
        DocumentRequest CreateRequestWithoutContentAsync();
    }
}