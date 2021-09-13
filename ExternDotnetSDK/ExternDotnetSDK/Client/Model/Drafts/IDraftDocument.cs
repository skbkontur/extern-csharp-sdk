#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Uploading;

namespace Kontur.Extern.Client.Model.Drafts
{
    [PublicAPI]
    public interface IDraftDocument
    {
        Guid DocumentId { get; }

        ValueTask<(Signature? signature, DocumentRequest request)> CreateSignedRequestAsync(
            Guid accountId,
            IContentService uploader,
            ICrypt crypt,
            TimeSpan? uploadTimeout);
    }
}